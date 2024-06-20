<template>
  <block-spinner-component class="full-width row flex-center" v-if="tokenIsLoading" />
  <error-data-component  class="full-width row flex-center" style="height: 150px;" v-else-if="tokenHasError" />
  <div v-else>
    <q-table class="visual-table no-shadow"
            row-key="tokenId"
            :hide-bottom="isHideBottom"
            :data="tokenData"
            :columns="columns"
            :rows-per-page-options="[rowsPerPage]"
    >
      <template v-slot:body="props">
        <q-tr>
          <q-td>{{ props.row.deviceName }}</q-td>
          <q-td>{{ props.row.requestorName }} ({{ props.row.wallet }})</q-td>
          <q-td>
            <span class="rounded-borders cursor-pointer chip" :class="getStatusClass(props.row.tokenStatus)">
              {{ getStatus(props.row.tokenStatus) }}
              <q-menu v-if="(props.row.tokenStatus === tokenStatusEnum.Active || props.row.tokenStatus === tokenStatusEnum.Inactive) && props.row.tokenId">
                <q-list style="min-width: 200px">
                  <q-item dense>
                    <q-item-section class="text-body2 text-grey">{{ $t('components.client.detail.cards.tokens.clientCardTokenList.changeStatus') }}:</q-item-section>
                  </q-item>
                  <q-item clickable v-close-popup>
                    <q-item-section @click="changeTokenStatus(props.row.tokenId)" class="text-body1">
                        {{ getStatus(tokenStatusEnum.Deleted) }}
                    </q-item-section>
                  </q-item>
                  <q-item clickable v-close-popup v-if="props.row.tokenStatus === tokenStatusEnum.Inactive">
                    <q-item-section @click="prepareActivate(props.row)" class="text-body1">
                        {{ getStatus(tokenStatusEnum.Active) }}
                    </q-item-section>
                  </q-item>
                </q-list>
              </q-menu>
            </span>
          </q-td>
          <q-td>
            <span v-if="props.row.tokenId" @click="showStatusDetail(props.row.tokenId)" class="rounded-borders cursor-pointer float-right chip detail">
              {{ $t('components.client.detail.cards.tokens.clientCardTokenList.statusDetail.label') }}
            </span>
          </q-td>
        </q-tr>
      </template>
      <template v-slot:no-data>
          <div style="height: 150px;" class="full-width row flex-center">
            <no-data-component />
          </div>
        </template>
    </q-table>

    <q-dialog v-model="isShowStatusDetail">
      <q-card style="width: 364px">
        <q-card-section class="row items-center q-pb-none">
          <div class="text-weight-medium dialog-title">{{ $t('components.client.detail.cards.tokens.clientCardTokenList.statusDetail.title') }}</div>
          <q-space />
          <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
        </q-card-section>

        <q-card-section>
          <block-spinner-component v-if="statusIsLoading" />
          <error-data-component v-else-if="statusHasError" />
          <no-data-component v-else-if="!statusHasError & !statusData" />
          <div v-else-if="statusData" class="column q-gutter-y-sm">
            <div>
              <div class="text-subtitle2 text-grey">{{ $t('components.client.detail.cards.tokens.clientCardTokenList.statusDetail.lastStatusDate') }}</div>
              <div class="text-subtitle1">{{ statusData.statusDateTime | formatDate(formatDate)}}</div>
            </div>
            <div>
              <div class="text-subtitle2 text-grey">{{ $t('components.client.detail.cards.tokens.clientCardTokenList.statusDetail.changedBy') }}</div>
              <div class="text-subtitle1">{{ getInitiator(statusData.initiator) }}</div>
            </div>
            <div>
              <div class="text-subtitle2 text-grey">{{ $t('components.client.detail.cards.tokens.clientCardTokenList.statusDetail.reason') }}</div>
              <div class="text-subtitle1">{{ getReason(statusData.reasonCode) }}</div>
            </div>
          </div>
        </q-card-section>
      </q-card>
    </q-dialog>

    <client-card-token-change-status-component :isShow="showChangeDialog" @hide="hideChangeDialog" @tokenDeleted="tokenDeleted" :tokenId="selectedToken" :cardId="cardId" />

    <q-dialog v-model="confirmActivate" persistent>
      <q-card>
        <q-card-section class="row items-center">
          <q-avatar icon="check_circle" color="primary" text-color="white" />
          <span class="q-ml-sm">{{ $t('components.client.detail.cards.tokens.clientCardTokenList.confirmActivate') }}</span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn flat :label="$t('components.client.detail.cards.tokens.clientCardTokenList.cancelActivate')" color="primary" v-close-popup :disable="actionIsLoading" />
          <q-btn flat :label="$t('components.client.detail.cards.tokens.clientCardTokenList.activateToken')" color="primary" @click="doActivate" :loading="actionIsLoading" />
        </q-card-actions>
      </q-card>
    </q-dialog>

  </div>
</template>

<script lang="ts">
import { mixins } from 'vue-class-component';
import { Component } from 'vue-property-decorator';
import ClientCardTokenChangeStatusComponent from '../tokens/clientCardTokenChangeStatus.component.vue';
import { CardTokenListMixin, CardTokenActivateMixin } from '../../../../../mixins/card/token';
import { CardTokenStatusEnum, CardTokenInitiatorEnum, CardTokenReasonCodeEnum } from '../../../../../enums/card/token';

import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';

import { CardTokenStatusEnumHelper } from '../../../../../helpers/enumHelpers/cardTokenStatusEnum.helper';
import { CardTokenInitiatorEnumHelper } from '../../../../../helpers/enumHelpers/cardTokenInitiatorEnum.helper';
import { CardTokenReasonCodeEnumHelper } from '../../../../../helpers/enumHelpers/cardTokenReasonCodeEnum.helper';

import constants from '../../../../../common/constants';
import { FormatDateFilter } from '../../../../../filters';
import { CardTokenInterface } from '../../../../../interfaces/card/token';

@Component({
  components: { ClientCardTokenChangeStatusComponent, NoDataComponent, BlockSpinnerComponent, ErrorDataComponent },
  filters: { FormatDateFilter }
})
export default class ClientCardTokenListComponent extends mixins(CardTokenListMixin, CardTokenActivateMixin) {
  private columns: Array<any>;
  private isShowStatusDetail: boolean;
  private showChangeDialog: boolean;
  private selectedToken: string;

  private formatDate = constants.formatting.dateTimeVue;
  private rowsPerPage = constants.token.pageSize;

  private confirmActivate: boolean;

  constructor () {
    super();
    this.isShowStatusDetail = false;
    this.showChangeDialog = false;
    this.selectedToken = '';

    this.columns = [
      {
        name: 'deviceName',
        align: 'left',
        label: this.$t('components.client.detail.cards.tokens.clientCardTokenList.columns.deviceName'),
        headerStyle: 'width:25%'

      },
      {
        name: 'wallet',
        align: 'left',
        label: this.$t('components.client.detail.cards.tokens.clientCardTokenList.columns.wallet'),
        headerStyle: 'width:20%'
      },
      {
        name: 'tokenStatus',
        align: 'left',
        label: this.$t('components.client.detail.cards.tokens.clientCardTokenList.columns.status'),
        format: (val: any) => this.getStatus(val),
        headerStyle: 'width:160px'
      },
      { name: 'spacer', label: '' }
    ];

    this.confirmActivate = false;
  }

  private get isHideBottom () {
    return !!(this.tokenData.length <= this.rowsPerPage);
  }

  private showStatusDetail (tokenId: string) {
    this.isShowStatusDetail = true;
    void this.doTokenLastStatusRequest(tokenId);
  }

  get tokenStatusEnum () {
    return CardTokenStatusEnum;
  }

  private getStatus (type: CardTokenStatusEnum) {
    return CardTokenStatusEnumHelper.getStatusName(type);
  }

  private getStatusClass (type: CardTokenStatusEnum) {
    switch (type) {
      case CardTokenStatusEnum.Active: return 'active';
      case CardTokenStatusEnum.Deleted: return 'deleted';
      case CardTokenStatusEnum.Inactive: return 'inactive';
      case CardTokenStatusEnum.Suspended: return 'suspended';
      default: return '';
    }
  }

  private getInitiator (initiator: CardTokenInitiatorEnum) {
    return CardTokenInitiatorEnumHelper.getInitiatorName(initiator);
  }

  private getReason (reason: CardTokenReasonCodeEnum) {
    return CardTokenReasonCodeEnumHelper.getReasonCodeName(reason);
  }

  private changeTokenStatus (tokenId: string) {
    this.selectedToken = tokenId;
    this.showChangeDialog = true;
  }

  private async hideChangeDialog (refresh = false) {
    this.showChangeDialog = false;
    if (refresh) {
      await this.doTokenListRequest();
    }
  }

  private prepareActivate (row: CardTokenInterface) {
    this.tokenToActivate = row;
    this.confirmActivate = true;
  }

  protected async doActivate () {
    await this.doActivateRequest();
    this.confirmActivate = false;
  }

  tokenDeleted (tokenId: string) {
    const item = this.tokenData.find(x => x.tokenId === tokenId);
    this.$emit('tokenDeleted', item);
  }
}
</script>

<style scoped>
.ellipsis {
  max-width: 160px;
}

.ellipsis:hover {
    white-space: normal;
    word-wrap: break-word;
  }

.chip {
  padding: 2px 8px;
  color: #7D7D80;
  margin-left: 1px;
}
.chip:hover {
  border: 1px solid #7D7D80;
  margin-left: 0;
}

.chip.active {
  color: #107C10;
  background-color: #DFF6DD;
}
.chip.active:hover {
  border: 1px solid #107C10;
}

.chip.deleted {
  color: #D83B01;
  background-color: #FDE7E9;
}
.chip.deleted:hover {
  border: 1px solid #D83B01;
}

.chip.inactive {
  color: #0078D4;
  background-color: #E6F2FB;
}
.chip.inactive:hover {
  border: 1px solid #0078D4;
}

.chip.suspended {
  color: #515255;
  background-color: #FFF4CE;
}
.chip.suspended:hover {
  border: 1px solid #515255;
}

.text-grey {
  color:#7D7D80;
}

.chip.detail:hover {
  margin-right: 0;
}
.detail {
  padding: 0 8px;
  visibility: hidden;
  margin-right: 1px;
}
.visual-table .q-tr:hover .detail {
  visibility: visible;
}

.dialog-title {
  font-size:18px;
}
</style>
