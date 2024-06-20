<template>
  <q-dialog ref="dialog" v-model="isShowDialog" @hide="hideDialog()" persistent>
    <q-card :style="(getCurrentWizardStepComponentName === 'ChoiceClientTransactionsComponent' || getCurrentWizardStepComponentName === 'CardBlockManageTokenComponent') ? 'width: 1000px; max-width: none;' : 'width: 612px;'">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">{{ $t('components.client.wizard.wizard.limitLabel') }}</div>
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
      </q-card-section>

      <q-card-section class="q-pa-none">
        <q-stepper
          v-model="step"
          ref="stepper"
          color="primary"
          alternative-labels
          contracted
          animated
          class="no-shadow"

        >
          <!-- First step always need to choose components for next steps  -->
          <q-step
            title=""
            :prefix="1"
            :name="1"
            :done="step > 1"
            active-icon=''
          >
            <div class="row">
              <div class="col-6">
                <div class="text-grey-7 text-caption q-mb-sm">{{ $t('components.client.wizard.wizard.chooseBlockReason') }}</div>
                <q-select dense filled v-model="selectedReason" :options="reasons"/>
                <q-input v-if="showReasonInput"
                  v-model="reasonComment"
                  filled
                  type="textarea"
                  class="q-my-md"
                  :placeholder="$t('components.client.wizard.wizard.reasonBlocking')"
                  maxlength="200"
                />
                <div class="q-mt-md" v-if="showIsClientCall">
                  {{ $t('components.client.wizard.wizard.isClientCall') }}
                  <q-option-group
                    v-model="isClientCall"
                    :options="clientCallOptions"
                    color="primary"
                    inline
                    class="is-client-call"
                  />
                </div>
                <div class="q-mt-md" v-if="showIsClientLostPhone">
                  {{ $t('components.client.wizard.wizard.isClientLostPhone') }}
                  <q-option-group
                    v-model="isClientLostPhone"
                    :options="clientLostPhoneOptions"
                    color="primary"
                    inline
                    class="is-client-call"
                  />
               </div>
               <div class="q-mt-md" v-if="showIsClientToldSMCodeFromSMS">
                  {{ $t('components.client.wizard.wizard.isClientToldSMCodeFromSMS') }}
                  <q-option-group
                    v-model="isClientToldSMCodeFromSMS"
                    :options="isClientToldSMCodeFromSMSOptions"
                    color="primary"
                    inline
                    class="is-client-call"
                  />
               </div>
                </div>
                 <div class="q-mt-sm row text-grey-7 flex-center" v-if="isClientLostPhone && showIsClientLostPhone">
                  <q-icon class="col-auto q-mr-xs" name="o_info" style="font-size:16px; margin-top:-1px;" />
                  <div class="col text-caption">{{ $t('components.client.wizard.wizard.clientLostPhoneInfo') }}</div>
                </div>
            </div>
            <div v-if="showCardWillbeBlocked" class="q-mt-lg row text-grey-7 flex-center">
              <q-icon class="col-auto q-mr-xs q-ml-md" name="o_info" />
              <div class="col text-caption">{{ $t('components.client.wizard.wizard.cardWillbeBlocked') }}</div>
            </div>
          </q-step>
          <!-- Dynamic component and step section -->
          <q-step
            title=""
            :prefix="item.step"
            :name="item.step"
            :done="step > item.step"
            v-for="item in wizardSteps" :key="item.step"
            :error="item.error"
          >
            <component v-if="item.component" :is="item.component" v-bind="dynamicComponentProps(item.component.extendOptions.name)" :reason-info.sync="cardBlockingData.reasonInformation" ref="dynamicComponent" />
          </q-step>

          <template v-slot:message>
            <q-banner v-if="getErrorText" class="bg-primary text-white q-px-md">
                <template v-slot:avatar>
                  <q-icon name="error_outline"  />
                </template>
                <template v-if="typeof getErrorText == 'string'">
                  {{ getErrorText }}
                </template>
                <template v-else>
                  <ul>
                    <li v-for="(e,index) in getErrorText" :key="index">{{ e }}</li>
                  </ul>
                </template>
            </q-banner>
          </template>

          <template v-slot:navigation>
            <q-stepper-navigation>
              <div class="row">
                <div :class="isShowDone ? ['col hidden'] : ['col']">
                    <q-btn outline color="primary" :label="(step === 1) ? $t('components.client.wizard.wizard.cancel') : $t('components.client.wizard.wizard.back')" @click="clickPrevious" no-caps />
                </div>
                <div :class="isShowDone && !hideDetailsButton ? ['col'] : ['col hidden']">
                    <q-btn outline color="primary" :label="$t('components.client.wizard.wizard.details')" @click="clickDetails" no-caps />
                </div>
                <div :class="isShowDone ? ['col hidden'] : ['col']">
                  <q-btn class="float-right"
                    :disable="selectedReason === null"
                    @click="clickNext"
                    :loading="requestIsLoading"
                    color="primary"
                    :label="getNextButtonLabel"
                    no-caps
                  />
                </div>
                <div :class="isShowDone ? ['col'] : ['col hidden']">
                  <q-btn class="float-right"
                    @click="closePopup()"
                    color="primary"
                    :label="$t('components.client.wizard.wizard.done')"
                    no-caps
                  />
                </div>
              </div>
            </q-stepper-navigation>
          </template>
        </q-stepper>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Vue, Component, Watch, Prop } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';
import ChoiceClientTransactionsComponent from './cardBlockingWizard/choiceClientTransactions.component.vue';
import CardFinderDetailsComponent from './cardBlockingWizard/cardFinderDetails.component.vue';
import CardNumberComponent from './cardBlockingWizard/cardNumber.component.vue';
import CardBlockSuccessComponent from './cardBlockingWizard/cardBlockSuccess.component.vue';
import CheckPersonalDataComponent from './cardBlockingWizard/checkPersonalData.component.vue';
// import FraudFinderDetailsComponent from './cardBlockingWizard/fraudFinderDetails.component.vue';
import PersonWhoCallsDetailsComponent from './cardBlockingWizard/personWhoCallsDetails.component.vue';
import CardTransactionsTypeComponent from './cardBlockingWizard/cardTransactionsType.component.vue';
import CardBlockManageTokenComponent from './cardBlockingWizard/cardBlockManageToken.component.vue';
import CheaterDetailsComponent from './cardBlockingWizard/cheaterDetails.component.vue';
import CardBlockSMSDetailUPCComponent from './cardBlockingWizard/cardBlockSMSDetailUPC.component.vue';
import CardBlockSMSDetailGateComponent from './cardBlockingWizard/cardBlockSMSDetailGate.component.vue';
import CardBlockSMSSenderNameComponent from './cardBlockingWizard/cardBlockSMSSenderName.component.vue';
import CardSecurityCodeOperationTypeComponent from './cardBlockingWizard/cardSecurityCodeOperationType.component.vue';

import { SenderNameTypeEnum } from '../../../../enums/card/limits/senderNameType.enum';
import { SecurityCodeOperationTypeEnum } from '../../../../enums/card/limits/securityCodeOperationType.enum';
import { ReasonTypeEnum } from '../../../../enums/card/limits/reasonType.enum';
import { QDialog, QStepper } from 'quasar';
import {
  CardBlockingModel,
  ClientIsCheaterReasonModel,
  ClientTransactionsReasonModel,
  FoundbByAnotherPersonReasonModel,
  ReportedDataReasonModel,
  WithdrawnFromATMReasonModel,
  ReceivedSMSCodeReasonModel
} from '../../../../models/card/limits';

// import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';
import { CardBlockingInterface } from '../../../../interfaces/card/limits';

import { cardApi } from '../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { CardBlockingResponseInterface, CardUnlockingStatusResponseInterface } from '../../../../interfaces/responses/card/limits';
import { CardBlockingRequestInterface, CardUnlockingRequestInterface } from '../../../../interfaces/requests/card/limits';
import CardDetailMixin from '../../../../mixins/card/detail/cardDetail.mixin';
// import { utils } from '../../../../utils/utils';

@Component({
  components: {
    CardFinderDetailsComponent,
    CardNumberComponent,
    CardBlockSuccessComponent,
    ChoiceClientTransactionsComponent,
    CheckPersonalDataComponent,
    PersonWhoCallsDetailsComponent,
    CardTransactionsTypeComponent,
    // FraudFinderDetailsComponent,
    CardBlockManageTokenComponent,
    CheaterDetailsComponent,
    CardBlockSMSDetailUPCComponent,
    CardBlockSMSDetailGateComponent,
    CardBlockSMSSenderNameComponent,
    CardSecurityCodeOperationTypeComponent
  }
})
export default class CardBlockingProcessComponent extends mixins(CardDetailMixin) {
  @Prop({ default: false }) isShow?: boolean;
  private reasons: any;
  private isShowDialog: boolean;
  private selectedReason: any;
  private step!: number;
  private wizardSteps!: Array<any>;
  private errorText!: string | string[];
  private showReasonInput: boolean;
  private reasonComment: string;

  private cardBlockingData: any;
  private reasonInformationType: any;

  private requestIsLoading: boolean;
  private requestHasError: boolean;
  private requestResult: any;
  private isShowDone: boolean;
  private isShowDetails: boolean;
  private hideDetailsButton: boolean;
  private blockInThisStep: boolean;

  private showIsClientCall: boolean;
  private showIsClientLostPhone: boolean;
  private showIsClientToldSMCodeFromSMS: boolean;
  private isClientCall: boolean;
  private isClientLostPhone: boolean;
  private isClientToldSMCodeFromSMS: boolean;
  private clientCallOptions: any;
  private clientLostPhoneOptions: any;
  private isClientToldSMCodeFromSMSOptions: any;

  private dynamicComponentProps (componentName: string) {
    const result = {
      dynamicData: {
        componentName: componentName,
        reasonId: this.selectedReason?.id,
        cardDetail: this.CardDetail,
        contactPhone: this.cardBlockingData?.reasonInformation?.contactPhone || ''
      }
    };

    if (componentName === 'CardBlockSuccessComponent') {
      result.dynamicData = Object.assign(result.dynamicData, {
        isLoading: this.requestIsLoading,
        hasError: this.requestHasError,
        result: this.requestResult,
        showDetails: this.isShowDetails
      });

      if (this.selectedReason?.id === ReasonTypeEnum.WithdrawnFromATM ||
          this.selectedReason?.id === ReasonTypeEnum.ReportedData) {
        result.dynamicData = Object.assign(result.dynamicData, { isClientCall: this.isClientCall });
      }
    }

    return result;
  }

  get getCurrentWizardItem () {
    if (this.wizardSteps.length > 0 && this.step > 1) {
      const wizardStep = this.wizardSteps.find(i => i.step === this.step);
      return wizardStep;
    }
    return null;
  }

  get getCurrentWizardStepComponentName () {
    return this.getCurrentWizardItem?.component.extendOptions.name;
  }

  $refs!: {
    dialog: QDialog;
    stepper: QStepper;
    dynamicComponent: Vue;
  }

  constructor () {
    super();

    this.isShowDialog = this.isShow || false;
    this.isShowDone = false;
    this.isShowDetails = false;
    this.hideDetailsButton = false;
    this.showReasonInput = false;
    this.reasonComment = '';
    this.blockInThisStep = false;

    this.showIsClientCall = false;
    this.showIsClientLostPhone = false;
    this.showIsClientToldSMCodeFromSMS = false;
    this.isClientCall = false;
    this.isClientLostPhone = false;
    this.isClientToldSMCodeFromSMS = false;

    this.clientCallOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];

    this.clientLostPhoneOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];

    this.isClientToldSMCodeFromSMSOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];

    this.reasons = [];
    for (const reason in ReasonTypeEnum) {
      this.reasons.push({ id: reason, label: this.getReasonType(reason) });
    }

    this.step = 1;
    this.wizardSteps = [
      { step: 2, component: null },
      { step: 3, component: null },
      { step: 4, component: null }
    ];
    this.selectedReason = null;
    this.errorText = '';
    this.cardBlockingData = {};

    this.requestIsLoading = false;
    this.requestHasError = false;
    this.requestResult = null;
  }

  private setInitData () {
    this.step = 1;
    this.isShowDone = false;
    this.isShowDetails = false;
    this.hideDetailsButton = false;
    this.requestResult = {};
    this.wizardSteps = [
      { step: 2, component: null },
      { step: 3, component: null },
      { step: 4, component: null }
    ];
    this.selectedReason = null;
    this.errorText = '';
    this.cardBlockingData = {};
    this.showReasonInput = false;
    this.reasonComment = '';
    this.blockInThisStep = false;

    this.showIsClientCall = false;
    this.isClientCall = false;
    this.showIsClientLostPhone = false;
    this.isClientLostPhone = false;
    this.showIsClientToldSMCodeFromSMS = false;
    this.isClientToldSMCodeFromSMS = false;
  }

  private getReasonType (type: string) {
    switch (type) {
      case ReasonTypeEnum.Other: return this.$t('components.client.wizard.wizard.reasons.other').toString();
      case ReasonTypeEnum.FoundbByAnotherPerson: return this.$t('components.client.wizard.wizard.reasons.foundbByAnotherPerson').toString();
      case ReasonTypeEnum.Lost: return this.$t('components.client.wizard.wizard.reasons.lost').toString();
      case ReasonTypeEnum.Stolen: return this.$t('components.client.wizard.wizard.reasons.stolen').toString();
      case ReasonTypeEnum.ForgottenInATM: return this.$t('components.client.wizard.wizard.reasons.forgottenInATM').toString();
      case ReasonTypeEnum.UnknownTransactions: return this.$t('components.client.wizard.wizard.reasons.unknownTransactions').toString();
      case ReasonTypeEnum.ReportedData: return this.$t('components.client.wizard.wizard.reasons.reportedData').toString();
      case ReasonTypeEnum.ClientIsCheater: return this.$t('components.client.wizard.wizard.reasons.clientIsCheater').toString();
      case ReasonTypeEnum.WithdrawnFromATM: return this.$t('components.client.wizard.wizard.reasons.withdrawnFromATM').toString();
      case ReasonTypeEnum.BlockingOperations: return this.$t('components.client.wizard.wizard.reasons.blockingOperations').toString();
      case ReasonTypeEnum.RequestedByClient: return this.$t('components.client.wizard.wizard.reasons.requestedByClient').toString();
      case ReasonTypeEnum.ReceivedSMSCode: return this.$t('components.client.wizard.wizard.reasons.receivedSMSCode').toString();
    }
  }

  @Watch('isShow')
  private isShowChanged (newVal: boolean) {
    this.isShowDialog = newVal;
  }

  private async hideDialog () {
    if (this.isShowDone) {
      await this.refreshCardDetail();
    }
    this.$emit('hide');
    this.setInitData();
  }

  @Watch('isClientCall')
  private onUpdateIsClientCall () {
    this.onUpdateSelectedReason();
  }

  @Watch('isClientLostPhone')
  private onUpdateIsClientLostPhone () {
    this.onUpdateSelectedReason();
  }

  @Watch('isClientToldSMCodeFromSMS')
  private onUpdateIsClientToldSMCodeFromSMS () {
    this.onUpdateSelectedReason();
  }

  @Watch('cardBlockingData.reasonInformation')
  private onUpdateReasonInformation () {
    if (this.selectedReason?.id === ReasonTypeEnum.ForgottenInATM ||
    this.selectedReason?.id === ReasonTypeEnum.Lost ||
    this.selectedReason?.id === ReasonTypeEnum.Stolen ||
    this.selectedReason?.id === ReasonTypeEnum.ReportedData ||
    this.selectedReason?.id === ReasonTypeEnum.ReceivedSMSCode) {
      this.onUpdateSelectedReason();
    }
  }

  @Watch('selectedReason')
  private onUpdateSelectedReason () {
    this.errorText = '';
    this.blockInThisStep = false;
    this.showReasonInput = false;
    this.showIsClientCall = false;
    this.showIsClientLostPhone = false;

    switch (this.selectedReason?.id) {
      case ReasonTypeEnum.Other:
        this.blockInThisStep = true;
        this.showReasonInput = true;
        this.wizardSteps = [
          {
            step: 2,
            component: CardBlockSuccessComponent
          }
        ];
        break;
      case ReasonTypeEnum.FoundbByAnotherPerson:
        this.wizardSteps = [
          {
            step: 2,
            component: CardFinderDetailsComponent,
            nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
            blockCard: true
          },
          {
            step: 3,
            component: CardBlockSuccessComponent
          }
        ];
        break;
      case ReasonTypeEnum.ForgottenInATM:
        this.showIsClientCall = true;
        if (this.isClientCall) {
          if (this.cardBlockingData.reasonInformation?.allClientOperations) {
            this.wizardSteps = [
              {
                step: 2,
                component: ChoiceClientTransactionsComponent,
                nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                blockCard: true
              },
              {
                step: 3,
                component: CardBlockSuccessComponent
              }
            ];
          } else {
            this.wizardSteps = [
              {
                step: 2,
                component: ChoiceClientTransactionsComponent
              },
              {
                step: 3,
                component: CardTransactionsTypeComponent,
                nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                blockCard: true
              },
              {
                step: 4,
                component: CardBlockSuccessComponent
              }
            ];
          }
        } else {
          this.wizardSteps = [
            {
              step: 2,
              component: PersonWhoCallsDetailsComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
              blockCard: true
            },
            {
              step: 3,
              component: CardBlockSuccessComponent
            }
          ];
        }
        break;
      case ReasonTypeEnum.Lost:
      case ReasonTypeEnum.Stolen:
        this.showIsClientCall = true;
        this.showIsClientLostPhone = true;
        if ((!this.isClientCall && !this.isClientLostPhone) || (!this.isClientCall && this.isClientLostPhone)) {
          this.wizardSteps = [
            {
              step: 2,
              component: PersonWhoCallsDetailsComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
              blockCard: true
            },
            {
              step: 3,
              component: CardBlockSuccessComponent
            }
          ];
        } else {
          if (this.cardBlockingData.reasonInformation?.allClientOperations) {
            this.wizardSteps = [
              {
                step: 2,
                component: ChoiceClientTransactionsComponent,
                nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                blockCard: true
              },
              {
                step: 3,
                component: CardBlockSuccessComponent
              }
            ];
          } else {
            this.wizardSteps = [
              {
                step: 2,
                component: ChoiceClientTransactionsComponent
              },
              {
                step: 3,
                component: CardTransactionsTypeComponent,
                nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                blockCard: true
              },
              {
                step: 4,
                component: CardBlockSuccessComponent
              }
            ];
          }
        }
        break;
      case ReasonTypeEnum.UnknownTransactions:
        this.blockInThisStep = true;
        this.wizardSteps = [
          {
            step: 2,
            component: ChoiceClientTransactionsComponent,
            nextButtonLabel: this.$t('components.client.wizard.wizard.continue')
          },
          {
            step: 3,
            component: CardBlockSuccessComponent
          }
        ];
        break;
      case ReasonTypeEnum.ReportedData:
        this.showIsClientCall = true;
        if (this.isClientCall) {
          let stepNum = 2;
          if (this.step === 1) {
            this.blockInThisStep = true;
          }
          this.wizardSteps = [
            {
              step: stepNum,
              component: CheckPersonalDataComponent
            }
          ];
          if (this.cardBlockingData?.reasonInformation?.isNeedDeleteToken) {
            this.wizardSteps.push({
              step: stepNum + 1,
              component: CardBlockManageTokenComponent
            });
            stepNum++;
          }
          this.wizardSteps.push(
            {
              step: stepNum + 1,
              component: CheaterDetailsComponent
            },
            {
              step: stepNum + 2,
              component: ChoiceClientTransactionsComponent
            },
            {
              step: stepNum + 3,
              component: CardTransactionsTypeComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
              blockCard: true
            },
            {
              step: stepNum + 4,
              component: CardBlockSuccessComponent
            }
          );
        } else {
          this.wizardSteps = [
            {
              step: 2,
              component: PersonWhoCallsDetailsComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
              blockCard: true
            },
            {
              step: 3,
              component: CardBlockSuccessComponent
            }
          ];
        }
        break;
      case ReasonTypeEnum.ClientIsCheater:
        this.wizardSteps = [
          {
            step: 2,
            component: ChoiceClientTransactionsComponent
          },
          {
            step: 3,
            component: PersonWhoCallsDetailsComponent,
            nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
            blockCard: true
          },
          {
            step: 4,
            component: CardBlockSuccessComponent
          }
        ];
        break;
      case ReasonTypeEnum.WithdrawnFromATM:
        this.showIsClientCall = true;

        if (this.isClientCall) {
          this.blockInThisStep = true;
          this.wizardSteps = [
            {
              step: 2,
              component: CardBlockSuccessComponent
            }
          ];
        } else {
          this.wizardSteps = [
            {
              step: 2,
              component: PersonWhoCallsDetailsComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
              blockCard: true
            },
            {
              step: 3,
              component: CardBlockSuccessComponent
            }
          ];
        }
        break;
      case ReasonTypeEnum.BlockingOperations:
        this.blockInThisStep = true;
        this.wizardSteps = [
          {
            step: 2,
            component: CardBlockSuccessComponent
          }
        ];
        break;
      case ReasonTypeEnum.RequestedByClient:
        this.wizardSteps = [];
        break;
      case ReasonTypeEnum.ReceivedSMSCode:
        this.showIsClientToldSMCodeFromSMS = true;

        if (this.isClientToldSMCodeFromSMS) {
          if (this.step === 1) this.blockInThisStep = true;
          let stepNum = 2;
          this.wizardSteps = [
            {
              step: stepNum,
              component: CheckPersonalDataComponent
            }
          ];
          if (this.cardBlockingData?.reasonInformation?.isNeedDeleteToken) {
            this.wizardSteps.push({
              step: stepNum + 1,
              component: CardBlockManageTokenComponent
            });
            stepNum++;
          }
          this.wizardSteps.push(
            {
              step: stepNum + 1,
              component: CheaterDetailsComponent
            },
            {
              step: stepNum + 2,
              component: ChoiceClientTransactionsComponent
            },
            {
              step: stepNum + 3,
              component: CardTransactionsTypeComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
              blockCard: true
            },
            {
              step: stepNum + 4,
              component: CardBlockSuccessComponent
            }
          );
        } else {
          let stepNum = 2;
          this.wizardSteps = [
            {
              step: stepNum,
              component: CardBlockSMSDetailUPCComponent
            }
          ];
          stepNum++;
          if (this.cardBlockingData?.reasonInformation?.isSMSMissingUPC) {
            this.wizardSteps.push({
              step: stepNum,
              component: CardBlockSMSDetailGateComponent,
              nextButtonLabel: this.$t('components.client.wizard.wizard.continue')
            });
          } else {
            if (this.cardBlockingData?.reasonInformation?.isClientAgreeBlockMobileApp) {
              this.wizardSteps.push({
                step: stepNum,
                component: CardTransactionsTypeComponent,
                nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                blockCard: true
              });
              this.wizardSteps.push({
                step: stepNum + 1,
                component: CardBlockSuccessComponent
              });
            } else {
              this.wizardSteps.push({
                step: stepNum,
                component: CardBlockSuccessComponent
              });
            }
            break;
          }

          stepNum++;
          if (this.cardBlockingData?.reasonInformation?.isSMSMissingGate) {
            this.wizardSteps.push({
              step: stepNum,
              component: CardBlockSMSSenderNameComponent
            });
            if (this.cardBlockingData?.reasonInformation?.senderName as SenderNameTypeEnum === SenderNameTypeEnum.BankVostok) {
              if (this.cardBlockingData?.reasonInformation?.isClientAgreeBlockMobileApp) {
                this.wizardSteps.push({
                  step: stepNum + 1,
                  component: CardTransactionsTypeComponent,
                  nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                  blockCard: true
                });
                stepNum++;
              }
              this.wizardSteps.push({
                step: stepNum + 1,
                component: CardBlockSuccessComponent
              });
            } else {
              this.isShowDone = this.hideDetailsButton = this.step === stepNum;
              this.wizardSteps.push({
                step: stepNum + 1,
                component: CardBlockSuccessComponent
              });
            }
          } else {
            this.wizardSteps.push({
              step: stepNum,
              component: CardSecurityCodeOperationTypeComponent
            });
            if (this.cardBlockingData?.reasonInformation?.securityCodeOperationType as SecurityCodeOperationTypeEnum === SecurityCodeOperationTypeEnum.CardVerification) {
              this.wizardSteps.push({
                step: stepNum + 1,
                component: CardBlockManageTokenComponent
              });
              stepNum++;
            }
            if (this.cardBlockingData?.reasonInformation?.isClientAgreeBlockMobileApp) {
              this.wizardSteps.push({
                step: stepNum + 1,
                component: CardTransactionsTypeComponent,
                nextButtonLabel: this.$t('components.client.wizard.wizard.blockCard'),
                blockCard: true
              });
              stepNum++;
            }
            this.wizardSteps.push({
              step: stepNum + 1,
              component: CardBlockSuccessComponent
            });
          }
        }
        break;
    }
  }

  private initCardBlockingData () {
    switch (this.selectedReason?.id) {
      case ReasonTypeEnum.FoundbByAnotherPerson:
        this.initCardBlockingDataTyped<FoundbByAnotherPersonReasonModel>(new FoundbByAnotherPersonReasonModel()); break;
      case ReasonTypeEnum.Stolen:
      case ReasonTypeEnum.Lost:
        this.initCardBlockingDataTyped<ClientTransactionsReasonModel>(new ClientTransactionsReasonModel());
        this.cardBlockingData.reasonInformation.isClientCall = this.isClientCall;
        this.cardBlockingData.reasonInformation.isClientLostPhone = this.isClientLostPhone;
        break;
      case ReasonTypeEnum.UnknownTransactions:
        this.initCardBlockingDataTyped<ClientTransactionsReasonModel>(new ClientTransactionsReasonModel()); break;
      case ReasonTypeEnum.ReportedData:
        this.initCardBlockingDataTyped<ReportedDataReasonModel>(new ReportedDataReasonModel());
        this.cardBlockingData.reasonInformation.isClientCall = this.isClientCall;
        break;
      case ReasonTypeEnum.ClientIsCheater:
        this.initCardBlockingDataTyped<ClientIsCheaterReasonModel>(new ClientIsCheaterReasonModel()); break;
      case ReasonTypeEnum.WithdrawnFromATM:
        this.initCardBlockingDataTyped<WithdrawnFromATMReasonModel>(new WithdrawnFromATMReasonModel());
        this.cardBlockingData.reasonInformation.isClientCall = this.isClientCall;
        break;
      case ReasonTypeEnum.ForgottenInATM:
        this.initCardBlockingDataTyped<ClientTransactionsReasonModel>(new ClientTransactionsReasonModel());
        this.cardBlockingData.reasonInformation.isClientCall = this.isClientCall;
        break;
      case ReasonTypeEnum.BlockingOperations:
      case ReasonTypeEnum.RequestedByClient:
      case ReasonTypeEnum.Other:
        this.initCardBlockingDataTyped<null>(null); break;
      case ReasonTypeEnum.ReceivedSMSCode:
        this.initCardBlockingDataTyped<ReceivedSMSCodeReasonModel>(new ReceivedSMSCodeReasonModel());
        this.cardBlockingData.reasonInformation.isClientToldSMCodeFromSMS = this.isClientToldSMCodeFromSMS;
        break;
      default: this.cardBlockingData = undefined; break;
    }
    // return FoundbByAnotherPersonReasonModel;
  }

  private initCardBlockingDataTyped<T> (reasonInfo: T) {
    const params:CardBlockingInterface<T> = {
      solarId: parseInt(this.$route.params.id),
      cardId: this.CardDetail?.cardId,
      reasonType: this.selectedReason?.id,
      reasonComment: this.getReasonType(this.selectedReason?.id),
      reasonInformation: reasonInfo
    };

    if (this.showReasonInput) {
      params.reasonComment = this.reasonComment;
    }

    this.cardBlockingData = new CardBlockingModel<T>(params);
  }

  get getErrorText () {
    return (Array.isArray(this.errorText) ? (this.errorText.length === 1 ? this.errorText[0] : this.errorText) : this.errorText);
  }

  private getCurrentComponent () {
    const components:any = this.$refs.dynamicComponent;
    if (components !== undefined) {
      // eslint-disable-next-line
      return components.filter((i:any) => i.dynamicData.componentName === this.getCurrentWizardStepComponentName)[0];
    }
    return null;
  }

  private async clickNext () {
    if (this.step === 1) {
      if (this.getIsFirstStepValid) {
        this.initCardBlockingData();
      } else {
        return;
      }
    }

    const component = this.getCurrentComponent();
    if (this.getCurrentWizardItem !== null && component !== undefined && component !== null) {
      const errComponent = component.validateStep();
      this.errorText = (errComponent === undefined ? '' : errComponent);

      if (errComponent !== undefined) return;
      component.collectData();

      await this.nextStepComponent();
    }

    const wizardItem = this.getCurrentWizardItem;

    if ((this.step === this.wizardSteps.length + 1 || this.blockInThisStep || wizardItem?.blockCard) && !this.getPassBlockAction) {
      // console.log('collected data object', JSON.stringify(this.cardBlockingData));
      await this.doRequest();
      if (!this.requestHasError) {
        this.isShowDone = true;
        if (this.checkNeedBlock) {
          this.showNotify(this.requestResult);
        }
        this.isShowDone = this.getShowDone;

        if (this.step === 1 && !this.blockInThisStep) {
          this.hideDetailsButton = true;
        }
      }

      this.$refs.stepper.next();
    } else {
      if (!this.blockInThisStep || this.step !== this.wizardSteps.length + 1) {
        this.$refs.stepper.next();
      }
    }
  }

  private get checkNeedBlock () {
    if (this.selectedReason?.id === ReasonTypeEnum.ClientIsCheater && this.cardBlockingData.reasonInformation.person.isRefusedToProvideInfo) {
      return false;
    }
    return true;
  }

  private showNotify (result: any) {
    this.$q.notify({
      type: result?.status?.success ? 'positive' : 'negative',
      message: result?.status?.message,
      position: 'top',
      timeout: 2500
    });
  }

  async nextStepComponent () {
    switch (this.getCurrentWizardStepComponentName) {
      case 'ChoiceClientTransactionsComponent':
        if (this.selectedReason?.id === ReasonTypeEnum.UnknownTransactions) {
          if (this.cardBlockingData.reasonInformation.allClientOperations) {
            const comment = this.$t('components.client.wizard.wizard.clientConfirmOperations').toString();
            await this.doUnlockedRequest(comment);
          } else {
            this.wizardSteps[1] = { step: 3, component: CardTransactionsTypeComponent };
            if (this.wizardSteps.length < 3) {
              this.wizardSteps.push({ step: 4, component: CardBlockSuccessComponent });
            } else {
              this.wizardSteps[2] = { step: 4, component: CardBlockSuccessComponent };
            }
          }
        }
        break;
      case 'CardBlockSMSSenderNameComponent':
        if (this.selectedReason?.id === ReasonTypeEnum.ReceivedSMSCode) {
          if (this.cardBlockingData.reasonInformation.isSMSMissingUPC &&
          this.cardBlockingData.reasonInformation.isSMSMissingGate &&
          this.cardBlockingData.reasonInformation.senderName as SenderNameTypeEnum === SenderNameTypeEnum.BankVostok &&
          !this.cardBlockingData.reasonInformation.isClientAgreeBlockMobileApp) {
            const comment = `�� ��������� ��� ���������� �������� ${this.cardBlockingData.reasonInformation.merchantDate} ${this.cardBlockingData.reasonInformation.merchantName} � HlA/ SMSGate �� ������������, ��������� ����, ��������� ������ ���������`;
            await this.doUnlockedRequest(comment);
          }
        }
        break;
      case 'CardBlockSMSDetailUPCComponent':
        if (this.selectedReason?.id === ReasonTypeEnum.ReceivedSMSCode) {
          if (!this.cardBlockingData.reasonInformation.isSMSMissingUPC &&
           !this.cardBlockingData.reasonInformation.isClientAgreeBlockMobileApp) {
            const comment = `�� ��������� SecureCode ${this.cardBlockingData.reasonInformation.smsDateUPC}, ��������� ������ ���������`;
            await this.doUnlockedRequest(comment);
          }
        }
        break;
      case 'CardSecurityCodeOperationTypeComponent':
      case 'CardBlockManageTokenComponent':
        if (this.selectedReason?.id === ReasonTypeEnum.ReceivedSMSCode) {
          if (((this.getCurrentWizardStepComponentName === 'CardSecurityCodeOperationTypeComponent' &&
            this.cardBlockingData?.reasonInformation?.securityCodeOperationType as SecurityCodeOperationTypeEnum !== SecurityCodeOperationTypeEnum.CardVerification) ||
            this.getCurrentWizardStepComponentName === 'CardBlockManageTokenComponent') &&
            !this.cardBlockingData.reasonInformation.isClientAgreeBlockMobileApp) {
            let comment = '';

            if (this.cardBlockingData?.reasonInformation?.securityCodeOperationType as SecurityCodeOperationTypeEnum === SecurityCodeOperationTypeEnum.MobileAppPassword) {
              comment = `�� ��������� ������ ��������� ������� ${this.cardBlockingData?.reasonInformation?.smsDateGate}, ��������� ������ ���������`;
            }

            if (this.cardBlockingData?.reasonInformation?.securityCodeOperationType as SecurityCodeOperationTypeEnum === SecurityCodeOperationTypeEnum.CardVerification) {
              comment = '������ ���������� ����� ��� ����� �볺���, ����� ���������, ��������� ������ ���������';
            }

            if (this.cardBlockingData?.reasonInformation?.securityCodeOperationType as SecurityCodeOperationTypeEnum === SecurityCodeOperationTypeEnum.Other) {
              comment = `�� ��������� ��� ${this.cardBlockingData?.reasonInformation?.smsDateGate}, ��������� ������ ���������`;
            }

            await this.doUnlockedRequest(comment);
          }
        }
        break;
      default:
        break;
    }
  }

  get getShowDone () {
    if (this.selectedReason?.id === ReasonTypeEnum.UnknownTransactions) {
      return this.getCurrentWizardStepComponentName !== undefined;
    }
    if (this.selectedReason?.id === ReasonTypeEnum.ReportedData ||
        this.selectedReason?.id === ReasonTypeEnum.ReceivedSMSCode) {
      this.blockInThisStep = false;
      return this.getCurrentWizardStepComponentName !== undefined;
    }
    return true;
  }

  get getPassBlockAction () {
    switch (this.getCurrentWizardStepComponentName) {
      case 'ChoiceClientTransactionsComponent':
        if (this.selectedReason?.id === ReasonTypeEnum.UnknownTransactions) {
          return true;
        }
        return false;
      default:
        return false;
    }
  }

  private clickPrevious () {
    this.isShowDone = false;
    this.errorText = '';
    if (this.step === 1) {
      this.closePopup();
    } else {
      this.$refs.stepper.previous();
    }
  }

  private clickDetails () {
    this.isShowDetails = true;
    this.hideDetailsButton = true;
  }

  closePopup () {
    this.$refs.dialog.hide();
  }

  protected async doRequest () {
    this.requestIsLoading = true;
    const result = await this.blockCard(this.$store, this.cardBlockingData);

    // await utils.delay(3000);

    // @ts-ignore
    this.requestHasError = result.hasError;
    if (!this.requestHasError) {
      // @ts-ignore
      this.requestResult = result?.result;
    }
    this.requestIsLoading = false;
  }

  protected async doUnlockedRequest (comment: string) {
    const request:CardUnlockingRequestInterface = {
      cardId: this.CardDetail?.cardId,
      comment: comment
    };
    this.requestResult = {};
    this.requestIsLoading = true;
    const result = await this.unlockCard(this.$store, request);
    this.requestIsLoading = false;
    // @ts-ignore
    if (!result.hasError) {
      this.isShowDone = true;
      this.requestResult.operationStatuses = [];
      this.requestResult.resultForOperator = '';
      // @ts-ignore
      this.requestResult.operationStatuses.push(result.result);
      // @ts-ignore
      this.requestResult.status = result.result;
      this.showNotify(this.requestResult);
    } else {
      this.showNotify(
        {
          status: {
            success: false,
            message: '������� �������'
          }
        }
      );
    }
  }

  private async blockCard (store: Store<any>, params: CardBlockingRequestInterface) : Promise<void | ApiResultModel<CardBlockingResponseInterface>> {
    const promise = cardApi.blockCardAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardBlockingResponseInterface>(store, promise);
  }

  private async unlockCard (store: Store<any>, params: CardUnlockingRequestInterface) : Promise<void | ApiResultModel<CardUnlockingStatusResponseInterface>> {
    const promise = cardApi.unlockingAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardUnlockingStatusResponseInterface>(store, promise);
  }

  private get getIsFirstStepValid () {
    if (!this.reasonComment && this.showReasonInput) {
      this.errorText = this.$t('components.client.wizard.wizard.reasonBlocking').toString();
      return false;
    } else {
      return true;
    }
  }

  private get getNextButtonLabel () {
    const wizardItem = this.getCurrentWizardItem;
    if (wizardItem) {
      if (wizardItem.nextButtonLabel) {
        return wizardItem.nextButtonLabel;
      }
    }
    if (this.blockInThisStep) {
      return this.$t('components.client.wizard.wizard.blockCard');
    }
    return (this.step === this.wizardSteps.length + 1) ? this.$t('components.client.wizard.wizard.blockCard') : this.$t('components.client.wizard.wizard.continue');
  }

  private get showCardWillbeBlocked () {
    let result = false;
    switch (this.selectedReason?.id) {
      case ReasonTypeEnum.UnknownTransactions:
      case ReasonTypeEnum.BlockingOperations:
        result = true;
        break;
      case ReasonTypeEnum.ReportedData:
        result = this.isClientCall;
        break;
      default:
        break;
    }
    return result;
  }
}
</script>

<style scoped>
  .q-stepper .q-banner ul {
    margin: 0;
  }

  .is-client-call {
    margin-top: -5px;
  }
</style>
