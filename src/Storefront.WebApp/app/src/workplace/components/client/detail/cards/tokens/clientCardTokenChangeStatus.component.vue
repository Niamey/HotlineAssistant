<template>
  <div>
    <q-dialog v-model="isShowDialog" @hide="hideDialog()">
      <q-card style="width: 328px">
        <q-card-section class="row items-center q-pb-none">
          <div class="text-h6 q-mt-sm">{{ $t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.label') }}</div>
          <q-space />
          <q-btn
            icon="close"
            flat
            round
            dense
            v-close-popup
            style="color: #a8a9aa"
          />
        </q-card-section>
        <q-card-section>
            <q-select filled v-model="selectedOption" :options="getOptions" class="visual-select" @input="optionChanged"/>
            <q-input v-if="selectedOption.id == reasonEnum.Other"
                v-model="reasonComment"
                filled
                type="textarea"
                class="q-my-md"
                :placeholder="$t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.commentHint')"
                @input="reasonChanged"
            />
            <div v-else class="q-mt-md">
              <div>{{ $t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.blockMobileApp.label') }}</div>
              <q-radio v-model="isBlockMobApp" :val="true" :label="$t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.blockMobileApp.yes')" />
              <q-radio v-model="isBlockMobApp" :val="false" :label="$t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.blockMobileApp.no')" class="q-ml-lg"/>
            </div>
            <div class="text-grey-7 text-caption q-mt-md" >{{ $t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.attention') }}</div>
            <q-btn
              :loading="isLoading"
              :disable="!isValue"
              class="q-mt-md"
              color="primary"
              size="14px"
              no-caps
              :label="$t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.deleteBtnLabel')"
              @click="deleteClick"
            />
        </q-card-section>
      </q-card>
    </q-dialog>
  </div>
</template>

<script lang="ts">
import { CardTokenReasonCodeEnumHelper } from '../../../../../helpers/enumHelpers/cardTokenReasonCodeEnum.helper';
import { Vue, Component, Prop, Watch } from 'vue-property-decorator';

import { CardTokenReasonCodeEnum } from '../../../../../enums/card/token';

import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { CardTokenActionResponseInterface } from '../../../../../interfaces/responses/card/token';
import { CardTokenDeleteRequestInterface } from '../../../../../interfaces/requests/card/token';
import { CardTokenActionModel } from '../../../../../models/card/token';

@Component
export default class ClientCardTokenChangeStatusComponent extends Vue {
  @Prop({ default: false }) isShow?: boolean;
  @Prop({ required: true }) tokenId!: string;
  @Prop({ required: true }) cardId!: number;

  private data?: any;
  private reasons: Array<CardTokenReasonCodeEnum>;
  private reasonComment: string;
  private selectedOption: any;
  private isShowDialog: boolean;
  private isValue: boolean;

  private isLoading: boolean;
  private hasError: boolean;
  private deleteResult?: CardTokenActionModel;

  private reasonEnum = CardTokenReasonCodeEnum;

  private isBlockMobApp: boolean;

  constructor () {
    super();
    this.isShowDialog = false;
    this.isValue = true;
    this.reasonComment = '';
    this.isLoading = false;
    this.hasError = false;

    this.reasons = [
      CardTokenReasonCodeEnum.Lost,
      CardTokenReasonCodeEnum.Stolen,
      CardTokenReasonCodeEnum.Closed,
      CardTokenReasonCodeEnum.Transaction,
      CardTokenReasonCodeEnum.Other
    ];

    this.selectedOption = this.getOptions[0];
    this.isBlockMobApp = false;
  }

  get getOptions () {
    return this.reasons.map(i => {
      return {
        id: i,
        label: CardTokenReasonCodeEnumHelper.getDeleteReasonCodeName(i)
      };
    });
  }

  @Watch('isShow')
  private isShowChanged (newVal: boolean) {
    this.isShowDialog = newVal;
  }

  private reasonChanged (value:string) {
    this.isValue = value.length > 10;
  }

  private optionChanged (value:any) {
    this.isValue = !(value.id === CardTokenReasonCodeEnum.Other);
    this.reasonComment = '';
  }

  private hideDialog () {
    this.$emit('hide');
  }

  private async deleteClick () {
    await this.doRequest();

    if (!this.hasError) {
      // @ts-ignore
      if (this.deleteResult?.success) {
        this.$q.notify({
          type: 'positive',
          message: this.$t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.actionResult.positive').toString(),
          position: 'top',
          timeout: 2500
        });
        this.$emit('tokenDeleted', this.tokenId);
        this.$emit('hide', true);
      } else {
        // @ts-ignore
        var msg = this.deleteResult?.message || '';
        this.$q.notify({
          type: 'negative',
          message: `${this.$t('components.client.detail.cards.tokens.clientCardTokenChangeStatus.actionResult.negative').toString()} ${msg}`,
          position: 'top',
          timeout: 2500
        });
      }
    }
  }

  protected async doRequest () {
    this.isLoading = true;

    const result = await this.deleteToken(this.$store, {
      solarId: parseInt(this.$route.params.id),
      cardId: this.cardId,
      tokenUniqueReference: this.tokenId,
      commentText: this.reasonComment,
      reasonCode: this.selectedOption.id,
      blockMobApp: this.isBlockMobApp
    });

    // await utils.delay(2000);

    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      if (!this.hasError) {
        this.deleteResult = result?.result?.item;
      }
    }
    this.isLoading = false;
  }

  private async deleteToken (store: Store<any>, params: CardTokenDeleteRequestInterface) : Promise<void | ApiResultModel<CardTokenActionResponseInterface>> {
    const promise = cardApi.deleteTokenAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardTokenActionResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
.text-caption {
  line-height: 1em;
}
.visual-select >>> .q-field__control,
.visual-select >>> .q-field__native,
.visual-select >>> .q-field__marginal,
.visual-select >>> .q-item {
    min-height: 32px;
    height: 32px;
}
</style>
