<template>
  <div>
      <q-header elevated class="text-black bg-white" style="height: 55px">
          <q-toolbar class="toolbar row items-center">
            <div class="col"><img @click="goHomePage()" :class="{'cursor-pointer logo': this.$route.path !== '/'}" svg-inline src="../../../../../../assets/images/platform_logo_white.svg" style="max-width: 81.8px" alt="" /></div>
            <div class="col-auto row items-center">
              <block-spinner-component v-if="this.statementIsLoading"
                             v-bind:loaded="this.statementIsLoading"
                             v-bind:size="'1em'"
                             v-bind:label="$t('components.shared.blockSpinner.label')"/>
              <error-data-component v-else-if="this.statementHasError" />
              <template v-else>
                <div class="col-auto q-pr-sm">{{ $t('components.client.statement.statementDetail.edition') }}</div>
                <div class="col q-pl-md">
                  <q-input filled v-model="getDateRange" :label="$t('components.client.statement.statementDetail.period')" stack-label dense readonly @click="dateRangeClick">
                    <template v-slot:prepend>
                      <q-icon name="event" class="cursor-pointer">
                        <q-popup-proxy transition-show="scale" transition-hide="scale" ref="dateRangePopup">
                          <q-date v-model="dateRange"
                            range
                            :mask="formatting.date"
                            dense
                            today-btn
                            :options="dateOptions"
                            @input="selectPeriod"
                            >
                            <div class="row items-center justify-end">
                              <q-btn v-close-popup :label="$t('components.client.detail.clientActivity.filter.calendar.close')" color="primary" outline no-caps/>
                            </div>
                          </q-date>
                        </q-popup-proxy>
                      </q-icon>
                    </template>
                  </q-input>
                </div>
                <div class="col-auto q-pl-sm">
                  <q-btn color="primary" size="12px" no-caps
                          :label="$t('components.client.statement.statementDetail.sendStatement')"
                          @click="sendStatement"
                          :disable="!this.ClientDetail"
                          /></div>
              </template>
            </div>
            <div class="col"></div>
          </q-toolbar>
      </q-header>
      <q-page-container>
        <div v-if="this.htmlData" v-html="this.htmlData" class="html-wrapper bg-white q-pa-sm q-mt-lg"></div>
         <block-spinner-component class="bg-white q-pa-xl" v-else-if="this.statementIsLoading"
                             v-bind:loaded="this.statementIsLoading"
                             v-bind:size="'4em'"
                             v-bind:label="$t('components.shared.blockSpinner.label')"/>
      </q-page-container>
      <q-dialog ref="sendEmailPopUp" v-model="isShowDialog">
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
            <div class="q-mb-sm">{{ this.getEmail || this.$t('components.client.statement.statementDetail.notSet') }}</div>
            <div class="row">
              <q-btn
                class="q-mt-sm"
                color="primary"
                no-caps
                label="Email"
                :disable="!this.getEmail"
                :loading="isSendingEmail"
                @click="sendEmail"/>
            </div>
          </q-card-section>
        </q-card>
      </q-dialog>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';
import { NavigateQueryHelper } from '../../../../../helpers/navigateQuery.helper';
import { mixins } from 'vue-class-component';
import { agreementApi } from '../../../../../api/agreement.api';
import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { QPopupProxy, date as QuasarDate, QInput, QDialog } from 'quasar';
import constants from '../../../../../common/constants';
import { StatementRequestInterface } from '../../../../../interfaces/requests/agreement/statement';
import { StatementSendingResponseModel } from '../../../../../models/responses/agreement/statement';

interface DateRange {
  from: string;
  to: string;
}

@Component({
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class StatementDetailComponent extends mixins(ClientDetailMixin) {
  private htmlData: string;
  private isShowDialog: boolean;
  private fileName: string;
  private currentPage: number;
  private pageCount: number;
  private loadingTask: any;
  private path: any;
  private clientId: number;
  private agreementId: number;
  private isSendingEmail: boolean;
  private src: any;
  private formatting: any;
  private currentDate: Date;
  private dateRange: DateRange | string;
  private defaultRange: DateRange | string;
  private statementIsLoading: boolean;
  private statementHasError: boolean;

  constructor () {
    super();

    this.formatting = constants.formatting;
    this.currentPage = 0;
    this.pageCount = 0;
    this.fileName = '';
    this.htmlData = '';
    this.isSendingEmail = false;
    this.isShowDialog = false;
    this.statementIsLoading = false;
    this.statementHasError = false;
    this.src = {};
    this.currentDate = new Date();
    this.dateRange = '';
    this.defaultRange = '';
    this.clientId = NavigateQueryHelper.statements.getClientId(this.$route) as number;
    this.agreementId = NavigateQueryHelper.statements.getAgreementId(this.$route) as number;
  }

  $refs!: {
    dateRangePopup: QPopupProxy;
    amountFrom: QInput;
    amountTo: QInput;
    sendEmailPopUp: QDialog;
  }

  private dateRangeClick () {
    this.$refs.dateRangePopup.show();
  }

  get getFormatCurrentDate () {
    return QuasarDate.formatDate(this.currentDate, this.formatting.dateSlashed);
  }

  private dateOptions (date: any) {
    return date <= this.getFormatCurrentDate;
  }

  get getDateRange () {
    if (this.dateRange === undefined || this.dateRange === null) return '';

    if (typeof this.dateRange === 'string') {
      return QuasarDate.formatDate(this.dateRange, this.formatting.dateVue);
    }

    // @ts-ignore
    const from = QuasarDate.formatDate(this.dateRange?.from, this.formatting.dateVue);
    // @ts-ignore
    const to = QuasarDate.formatDate(this.dateRange?.to, this.formatting.dateVue);
    return `${from} – ${to}`;
  }

  get getEmail () {
    return this.ClientDetail?.email;
  }

  async created () {
    if (!this.ClientDetail) {
      await this.loadDetail(this.clientId);
    }
    // @ts-ignore
    const from = QuasarDate.formatDate(new Date().setMonth(new Date().getMonth() - 1), this.formatting.date);
    // @ts-ignore
    const to = QuasarDate.formatDate(new Date(), this.formatting.date);
    this.dateRange = { from: from, to: to };
    await this.selectPeriod(this.dateRange);
  }

  async updateHtmlView (dateStart: string, dateEnd: string) {
    const params: StatementRequestInterface = {
      solarId: this.clientId,
      agreementId: this.agreementId,
      dateStart,
      dateEnd
    };
    this.statementIsLoading = true;
    this.htmlData = await agreementApi.getStatementAsync(params);
    this.statementIsLoading = false;
  }

  sendStatement () {
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

  async selectPeriod (date: any) {
    if (typeof date === 'string') {
      const formatteddate = QuasarDate.formatDate(date, this.formatting.date);
      await this.updateHtmlView(formatteddate, formatteddate);
      return;
    }

    if (!date.from || !date.to) {
      return;
    }
    // @ts-ignore
    const from = QuasarDate.formatDate(date.from, this.formatting.date);
    // @ts-ignore
    const to = QuasarDate.formatDate(date.to, this.formatting.date);
    await this.updateHtmlView(from, to);
  }

  get getFinalRange () : DateRange {
    if (typeof this.dateRange === 'string') {
      return {
        from: QuasarDate.formatDate(this.dateRange, this.formatting.date),
        to: QuasarDate.formatDate(this.dateRange, this.formatting.date)
      };
    } else {
      return {
        from: QuasarDate.formatDate(this.dateRange.from, this.formatting.date),
        to: QuasarDate.formatDate(this.dateRange.to, this.formatting.date)
      };
    }
  }

  async sendEmail () {
    let result : StatementSendingResponseModel = new StatementSendingResponseModel({ success: false, message: this.$t('components.shared.errorData.label').toString() });
    this.isSendingEmail = true;
    const period = this.getFinalRange;
    try {
      result = await agreementApi.sendStatementEmailAsync({ solarId: this.clientId, agreementId: NavigateQueryHelper.statements.getAgreementId(this.$route) as number, dateStart: period.from, dateEnd: period.to });
    } finally {
      this.isSendingEmail = false;
      this.showSendingResult(result);
    }
  }

  showSendingResult (result: StatementSendingResponseModel) {
    if (result.success) {
      this.$refs.sendEmailPopUp.hide();
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
.html-wrapper{
  margin: 20px auto;
  width: 1170px;
}
.logo{
  filter: invert(50%) sepia(14%) saturate(3200%) hue-rotate(-30deg) brightness(135%) contrast(80%);
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
