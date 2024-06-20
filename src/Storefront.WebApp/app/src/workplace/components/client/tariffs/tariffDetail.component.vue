<template>
  <div>
      <q-header elevated class="text-black bg-white" style="height: 55px">
          <q-toolbar class="toolbar row items-center">
            <div class="col"><img @click="goHomePage()" :class="{'cursor-pointer logo': this.$route.path !== '/'}" svg-inline src="../../../../assets/images/platform_logo_white.svg" style="max-width: 81.8px" alt="" /></div>
            <div class="col-auto row items-center">
              <block-spinner-component v-if="this.tariffListIsLoading"
                             v-bind:loaded="this.tariffListIsLoading"
                             v-bind:size="'1em'"
                             v-bind:label="$t('components.shared.blockSpinner.label')"/>
              <error-data-component v-else-if="this.tariffListHasError" />
              <template v-else>
                <div class="col-auto q-pr-sm">{{ $t('components.client.tariffs.tariffDetail.edition') }}</div>
                <!--<div class="col">
                  <q-select dense
                            class="visual-select"
                            outlined
                            :option-label ="getTariffLabel"
                            v-model="selectedOption"
                            :options="data.options"
                            @input="onValueChange"
                            :disable="data.options.length < 2"
                            /></div>-->
                <div class="col-auto q-pl-sm">
                  <q-btn color="primary" size="12px" no-caps
                          :label="$t('components.client.tariffs.tariffDetail.sendTariff')"
                          @click="sendTariff"
                          :disable="!this.ClientDetail"
                          :loading="this.Loading"
                          /></div>
              </template>
            </div>
            <div class="col"></div>
          </q-toolbar>
      </q-header>
      <q-page-container>
        <div class="row full-width items-center justify-center pdf-header">
          <div class="col">
            <div class="full-width text-white text-center">{{this.fileName}}</div>
          </div>
          <div class="col-auto row text-white items-center">
            <span v-if="pageCount == 1">{{currentPage}}</span>
            <q-input v-else class="pageInput" dark mask="##" @blur="scrollToPage" dense standout="text-white" color="white" v-model="currentPage" />
            / {{pageCount}}
          </div>
          <div class="col items-end">
            <div class="row float-right" style="width: 162px;">
              <q-icon name="refresh" class="cursor-pointer col text-white" style="font-size: 24px;" @click="rotatePDF"/>
              <q-icon name="cloud_download" class="cursor-pointer col text-white" style="font-size: 24px;" @click="downloadPDF"/>
              <q-icon name="print" class="cursor-pointer col text-white" style="font-size: 24px;" @click="printPDF"/>
            </div>
          </div>
        </div>
        <div class="q-pt-xl" v-if="src">
          <pdf
              v-for="i in pageCount"
              :key="i"
              :src="src"
              :page="i"
              :ref="i"
              :id="i"
              :rotate="rotate"
              :style="'width: ' + formattedZoom +'%;padding: 20px; margin: 0 auto;'"
              :scale.sync="scale"
          ></pdf>
          <pdf v-if="src"
              :src="src"
              ref="pdfForPrint"
              class = "hidden"
          ></pdf>
        </div>
        <div class="column buttons">
          <q-btn class="col q-mb-lg" round color="white" text-color="black" label="+" @click="scale += scale < 2 ? 0.1 : 0"/>
          <q-btn class="col" round color="white" text-color="black" label="-" @click="scale -= scale > 0.2 ? 0.1 : 0"/>
        </div>
        <q-scroll-observer @scroll="changePage" />
      </q-page-container>

      <q-dialog v-model="isShowDialog">
        <q-card style="width: 364px" class="q-pa-sm">
          <q-card-section>
            <q-btn
              icon="close"
              flat
              round
              dense
              v-close-popup
              style="color: #a8a9aa;"
              class="float-right"
            />
            <div class="text-grey-7 text-caption" >Email</div>
            <div class="q-mb-sm">{{ this.getEmail || this.$t('components.client.tariffs.tariffDetail.notSet') }}</div>
            <div class="text-grey-7 text-caption" >{{ $t('components.client.tariffs.tariffDetail.finPhone') }}</div>
            <div class="q-mb-md">{{ this.getFinancialPhone || this.$t('components.client.tariffs.tariffDetail.notSet') }}</div>

            <div class="text-weight-medium">{{ $t('components.client.tariffs.tariffDetail.sendBy') }}</div>
            <div class="row">
              <q-btn
                class="q-mt-sm"
                color="primary"
                no-caps
                label="Email"
                :disable="!this.getEmail"
                @click="sendEmail"
                :loading="isSendingEmail" />
              <q-btn
                class="q-mt-sm q-ml-md"
                color="deep-purple-4"
                no-caps
                :disable="!this.getFinancialPhone"
                @click="sendViber"
                :loading="isSendingViber">
                <div class="row items-center no-wrap">
                  <img class="q-mr-xs" svg-inline src="../../../../assets/images/icons/viber.svg" alt=""/>
                  <div class="text-center">Viber</div>
                </div>
              </q-btn>
            </div>
          </q-card-section>
        </q-card>
      </q-dialog>
  </div>
</template>

<script lang="ts">
// @ts-ignore
import pdf from 'vue-pdf';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { TariffInterface } from '../../../interfaces/tariff';
import AgreementTariffListMixin from '../../../mixins/agreement/list/agreementTariffList.mixin';
import CardTariffListMixin from '../../../mixins/card/list/cardTariffList.mixin';
import ClientDetailMixin from '../../../mixins/client/clientDetail.mixin';
import { NavigateQueryHelper } from '../../../helpers/navigateQuery.helper';
import { mixins } from 'vue-class-component';
import { cardApi } from '../../../api/card.api';
import { agreementApi } from '../../../api/agreement.api';

import BlockSpinnerComponent from '../../../components/shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../shared/errorData.component.vue';
import { TariffSendingResponseModel } from '../../../models/responses/tariff/detail';

@Component({
  components: { pdf, BlockSpinnerComponent, ErrorDataComponent }
})
export default class ClientTariffDetailComponent extends mixins(AgreementTariffListMixin, CardTariffListMixin, ClientDetailMixin) {
  @Prop({ required: true }) detailData!: TariffInterface;
  @Prop({ required: true }) tariffType!: string;

  private data?: any;
  private selectedOption: any;
  private isShowDialog: boolean;
  private src: any;
  private fileName: string;
  private currentPage: number;
  private pageCount: number;
  private loadingTask: any;
  private scale: number;
  private rotate: number;
  private path: any;
  private clientId: number;
  private isSendingViber: boolean;
  private isSendingEmail: boolean;

  private tariffListIsLoading: boolean;
  private tariffListHasError: boolean;

  @Watch('tariffListIsLoading')
  onPropertyChanged (value: any) {
    if (!value) {
      if (this.detailData) {
        this.selectedOption = this.detailData;
      }
    }
  }

  constructor () {
    super();
    this.data = {};
    this.currentPage = 0;
    this.pageCount = 0;
    this.scale = 1;
    this.rotate = 0;
    this.fileName = '';
    this.src = '';
    this.isSendingViber = false;
    this.isSendingEmail = false;

    this.data.options = [];

    this.selectedOption = {};
    this.isShowDialog = false;
    this.tariffListIsLoading = false;
    this.tariffListHasError = false;
    this.clientId = NavigateQueryHelper.tariffs.getClientId(this.$route) as number;
  }

  async beforeMount () {
    this.tariffListIsLoading = true;
    if (this.tariffType === 'agreement') {
      await this.getAgreementTariffList(this.clientId, NavigateQueryHelper.tariffs.getAgreementId(this.$route) as number);
      this.tariffListIsLoading = this.AgreementTariffsLoading;
      this.tariffListHasError = this.AgreementTariffsHasError;
      this.data.options = this.AgreementTariffs;
    }
    if (this.tariffType === 'card') {
      await this.getCardTariffList(this.clientId, NavigateQueryHelper.tariffs.getCardId(this.$route) as number);
      this.tariffListIsLoading = this.CardTariffsLoading;
      this.tariffListHasError = this.CardTariffsHasError;
      this.data.options = this.CardTariffs;
    }
    await this.loadDetail(this.clientId);
  }

  get getEmail () {
    return this.ClientDetail?.email;
  }

  get getFinancialPhone () {
    return this.ClientDetail?.financialPhone;
  }

  mounted () {
    this.src.promise.then((pdf: any) => {
      this.pageCount = pdf.numPages;
      this.currentPage = 1;
    });
  }

  created () {
    this.updatePdfView(this.detailData);
  }

  get formattedZoom () {
    return this.scale * 100;
  }

  updatePdfView (tariff: any) {
    this.fileName = tariff.tariffUrl as string;
    this.path = this.getFile(this.fileName);
    if (this.path !== undefined) {
      this.loadingTask = pdf.createLoadingTask(this.path.default);
      this.src = this.loadingTask;
    }
  }

  getFile (fn: string) {
    return require('../../../../statics/files/' + fn);
  }

  changePage () {
    if (this.$refs) {
      if (this.pageCount === 1) {
        this.currentPage = 1;
        return;
      }
      let pageHeight = 0;
      let currentPageOffset = 0;
      let nextPageOffset = 0;
      Object.keys(this.$refs).forEach((el:string) => {
        currentPageOffset = this.findPos(this.$refs[el]);
        if (Number(el) < this.pageCount) {
          nextPageOffset = this.findPos(this.$refs[(Number(el) + 1).toString()]);
          pageHeight = nextPageOffset - currentPageOffset;
        }
        if ((currentPageOffset + pageHeight) <= 0) {
          this.currentPage = Number(el) + 1;
        } else if (Number(el) === 1) {
          this.currentPage = Number(el);
        }
      });
    }
  }

  findPos (obj:any) {
    try {
      if (obj) {
        return obj[0].$el.getBoundingClientRect().y - 100;
      } else {
        return 0;
      }
    } catch {
      return 0;
    }
  }

  sendTariff () {
    this.isShowDialog = true;
  }

  goHomePage () {
    if (this.$route.path !== '/') {
      void this.$router.push('/');
    }
  }

  scrollToPage () {
    if (this.currentPage > this.pageCount) {
      this.currentPage = this.pageCount;
    }
    const container = this.$el.querySelector('[id=\'' + this.currentPage.toString() + '\']');
    if (container) {
      container.scrollIntoView(true);
      container.scrollTop += 200;
    }
  }

  printPDF () {
    // @ts-ignore
    this.$refs.pdfForPrint.print();
  }

  rotatePDF () {
    this.rotate += 90;
  }

  downloadPDF () {
    const link = document.createElement('a');
    link.href = this.path.default;
    link.setAttribute('download', this.fileName);
    document.body.appendChild(link);
    link.click();
  }

  getTariffLabel (tariff: any) {
    if (tariff.tariffId) {
      if (tariff.tariffEnd) {
        return `з ${tariff.tariffStart} по ${tariff.tariffEnd}`;
      } else {
        return `з ${tariff.tariffStart}`;
      }
    }
  }

  onValueChange (tariff: any) {
    this.updatePdfView(tariff);
  }

  async sendEmail () {
    let result : TariffSendingResponseModel = new TariffSendingResponseModel({ success: false, message: this.$t('components.shared.errorData.label').toString() });
    this.isSendingEmail = true;
    try {
      switch (this.tariffType) {
        case 'card':
          result = await cardApi.sendTariffEmailAsync({ clientId: this.clientId, cardId: NavigateQueryHelper.tariffs.getCardId(this.$route) as number });
          break;
        case 'agreement':
          result = await agreementApi.sendTariffEmailAsync({ clientId: this.clientId, agreementId: NavigateQueryHelper.tariffs.getAgreementId(this.$route) as number });
          break;
        default:
          break;
      }
    } finally {
      this.isSendingEmail = false;
      this.showSendingResult(result);
    }
  }

  async sendViber () {
    let result : TariffSendingResponseModel = new TariffSendingResponseModel({ success: false, message: this.$t('components.shared.errorData.label').toString() });
    this.isSendingViber = true;
    try {
      switch (this.tariffType) {
        case 'card':
          result = await cardApi.sendTariffViberAsync({ clientId: this.clientId, cardId: NavigateQueryHelper.tariffs.getCardId(this.$route) as number });
          break;
        case 'agreement':
          result = await agreementApi.sendTariffViberAsync({ clientId: this.clientId, agreementId: NavigateQueryHelper.tariffs.getAgreementId(this.$route) as number });
          break;
        default:
          break;
      }
    } finally {
      this.isSendingViber = false;
      this.showSendingResult(result);
    }
  }

  showSendingResult (result: TariffSendingResponseModel) {
    if (result.success) {
      this.$q.notify({
        type: 'positive',
        message: result.message,
        position: 'top'
      });
    } else {
      this.$q.notify({
        type: 'negative',
        message: result.message,
        position: 'top'
      });
    }
  }
}
</script>

<style scoped>
.visual-select >>> .q-field__control,
.visual-select >>> .q-field__native,
.visual-select >>> .q-field__marginal,
.visual-select >>> .q-item {
    min-height: 32px;
    height: 32px;
}
.logo{
  filter: invert(50%) sepia(14%) saturate(3200%) hue-rotate(-30deg) brightness(135%) contrast(80%);
}
.pdf-header {
  position: fixed;
  z-index:1;
  height: 50px;
  background-color: #323639;
}
.buttons {
  position: fixed;
  bottom: 40px;
  right: 20px;
}
.pageInput {
    width: 24px;
}
.pageInput >>> .q-field__control {
  padding-left: 4px;
  padding-right: 4px;
  height: 24px;
  margin-top: 1px;
}
</style>
