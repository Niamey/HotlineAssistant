<template>
  <q-dialog ref="dialog" v-model="isShowDialog" @hide="hideDialog()" persistent>
    <q-card :style="getCurrentWizardStepComponentName === 'ChoiceClientTransactionsComponent' ? 'width: 1000px; max-width: none;' : 'width: 612px;'">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">{{ $t('Разблокировка') }}</div>
        <q-space />
        <!-- <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" /> -->
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
          <q-step
            title=""
            :prefix="item.step"
            :name="item.step"
            :done="step > item.step"
            v-for="item in wizardSteps" :key="item.step"
            :error="item.error"
          >
            <component v-if="item.component" :is="item.component" v-bind="dynamicComponentProps(item.component.extendOptions.name)" :unlocking-info.sync="cardUnlockingData.unlockingInformation" @updateStep="updateCurrentStep" ref="dynamicComponent" />
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
                <div class="col">
                    <q-btn v-if="getShowBackVisible" outline color="primary" :label="(step === 1) ? $t('components.client.wizard.wizard.cancel') : $t('components.client.wizard.wizard.back')" @click="clickPrevious" no-caps />
                </div>
                <div class="col">
                  <q-btn class="float-right"
                    :loading="requestIsLoading"
                    @click="clickNext"
                    color="primary"
                    :label="getNextButtonLabel"
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
import ChoiceClientTransactionsComponent from './cardUnlockingWizard/choiceClientTransactions.component.vue';
import CardConfirmFromClientComponent from './cardUnlockingWizard/cardConfirmFromClient.component.vue';
import CardDialogForUnlockingComponent from './cardUnlockingWizard/cardDialogForUnlocking.component.vue';
import CardDialogInsufficientDataComponent from './cardUnlockingWizard/cardDialogInsufficientData.component.vue';
import CardFindNumberCheckComponent from './cardUnlockingWizard/cardFindNumberCheck.component.vue';
import ClientNearATMComponent from './cardUnlockingWizard/clientNearATM.component.vue';
import CardUnlockingComponent from './cardUnlockingWizard/cardUnlocking.component.vue';
import { CardCurrentStatusModel } from '../../../../models/card/status';

import { QDialog, QStepper } from 'quasar';

import { mixins } from 'vue-class-component';
import CardDetailMixin from '../../../../mixins/card/detail/cardDetail.mixin';

import constants from '../../../../common/constants';

@Component({
  components: {
    ChoiceClientTransactionsComponent,
    CardConfirmFromClientComponent,
    CardDialogForUnlockingComponent,
    CardDialogInsufficientDataComponent,
    CardFindNumberCheckComponent,
    ClientNearATMComponent,
    CardUnlockingComponent
  }
})
export default class CardUnlockingProcessComponent extends mixins(CardDetailMixin) {
  @Prop({ default: false }) isShow?: boolean;
  @Prop({ required: true }) cardCurrentStatus?: CardCurrentStatusModel;
  private reasons: any;
  private isShowDialog: boolean;
  private step!: number;
  private wizardSteps!: Array<any>;
  private errorText!: string | string[];

  private requestIsLoading: boolean;
  private requestHasError: boolean;
  private requestResult: any;
  private isShowDone: boolean;

  private cardUnlockingData: any;

  private dynamicComponentProps (componentName: string) {
    const result = {
      dynamicData: {
        componentName: componentName
      }
    };

    if (componentName === 'CardFindNumberCheckComponent') {
      result.dynamicData = Object.assign(result.dynamicData, {
        financePhone: '',
        contactPhone: '+380'
      });
    }

    if (componentName === 'CardConfirmFromClientComponent') {
      result.dynamicData = Object.assign(result.dynamicData, {
        cardCurrentStatus: this.cardCurrentStatus
      });
    }

    if (componentName === 'CardDialogForUnlockingComponent') {
      let currentView = 'unlocked';
      if (this.step === 3) {
        currentView = 'locked';
      }
      result.dynamicData = Object.assign(result.dynamicData, {
        currentView,
        cardCurrentStatus: this.cardCurrentStatus
      });
    }

    if (componentName === 'CardDialogInsufficientDataComponent') {
      result.dynamicData = Object.assign(result.dynamicData, {
        cardUnlockingData: this.cardUnlockingData
      });
    }

    return result;
  }

  private get getShowBackVisible () {
    const wizardStep = this.getCurrentWizardItem;
    if ('showBack' in wizardStep) {
      return wizardStep.showBack;
    } else {
      return true;
    }
  }

  private initCardUnlockingData () {
    this.cardUnlockingData = {
      unlockingInformation: {
        isFinancePhoneConfirmed: false
      }
    };
  }

  get getCurrentWizardItem () {
    if (this.wizardSteps.length > 0) {
      const wizardStep = this.wizardSteps.find(i => i.step === this.step);
      return wizardStep;
    }
    return null;
  }

  get getCurrentWizardStepComponentName () {
    return this.getCurrentWizardItem?.component.extendOptions.name;
  }

  updateCurrentStep (data: any) {
    Vue.set(this.wizardSteps, this.step - 1, Object.assign(this.wizardSteps[this.step - 1], data));
  }

  $refs!: {
    dialog: QDialog;
    stepper: QStepper;
    dynamicComponent: Vue;
  }

  constructor () {
    super();
    this.cardUnlockingData = {};

    this.isShowDialog = this.isShow || false;
    this.isShowDone = false;

    this.step = 1;
    this.wizardSteps = [];

    this.errorText = '';

    this.requestIsLoading = false;
    this.requestHasError = false;
    this.requestResult = null;
  }

  private setInitData () {
    this.cardUnlockingData = {};
    this.step = 1;
    this.isShowDone = false;
    this.requestResult = {};
    this.wizardSteps = [];
    this.errorText = '';
  }

  private setInitialStep () {
    const comment = this.cardCurrentStatus?.reason?.toLowerCase();
    if (comment?.includes(constants.blockingReasonText.checkWithClient)) {
      this.wizardSteps = [
        { step: 1, component: CardFindNumberCheckComponent },
        { step: 2, component: CardConfirmFromClientComponent },
        { step: 3, component: null }
      ];
    }
    if (comment?.includes(constants.blockingReasonText.cardCompromise)) {
      this.wizardSteps = [
        { step: 1, component: ClientNearATMComponent },
        { step: 2, component: CardDialogForUnlockingComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.lockCard') },
        { step: 3, component: CardDialogForUnlockingComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.finish'), isFinish: true }
      ];
    }
  }

  @Watch('isShow')
  private isShowChanged (newVal: boolean) {
    this.isShowDialog = newVal;
    if (newVal) {
      this.setInitialStep();
    }
  }

  private hideDialog () {
    this.$emit('hide');
    this.setInitData();
  }

  get getErrorText () {
    return (Array.isArray(this.errorText) ? (this.errorText.length === 1 ? this.errorText[0] : this.errorText) : this.errorText);
  }

  private get getNextButtonLabel () {
    const wizardStep = this.getCurrentWizardItem;
    if ('nextButtonLabel' in wizardStep) {
      return wizardStep.nextButtonLabel;
    } else {
      return this.$t('components.client.wizard.wizard.continue');
    }
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
    let canGoFurther = true;
    if (this.step === 1) {
      this.initCardUnlockingData();
    }
    const step = this.getCurrentWizardItem;
    const component = this.getCurrentComponent();

    if (component !== undefined && component !== null) {
      const errComponent = component.validateStep();
      this.errorText = (errComponent === undefined ? '' : errComponent);
      if (errComponent !== undefined) return;

      this.requestIsLoading = true;

      var result = await component.collectData();

      this.requestIsLoading = false;

      if (result) {
        if (result.hasError) {
          canGoFurther = false;
          this.$q.notify({
            type: 'negative',
            message: result.errorText,
            position: 'top',
            timeout: 2500
          });
        } else {
          this.$q.notify({
            type: 'positive',
            message: result.result.message,
            position: 'top',
            timeout: 2500
          });
        }
      }

      this.nextStepComponent();
    }
    if (!step.isFinish && canGoFurther) {
      this.$refs.stepper.next();
    } else if (canGoFurther) {
      await this.refreshCardDetail();
      this.closePopup();
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

  closePopup () {
    this.$refs.dialog.hide();
  }

  nextStepComponent () {
    switch (this.getCurrentWizardStepComponentName) {
      case 'CardConfirmFromClientComponent':
        if (this.cardUnlockingData.unlockingInformation.isFinancePhoneConfirmed) {
          if (this.cardUnlockingData.unlockingInformation.dataConfirmed) {
            this.wizardSteps[2] = { step: 3, component: CardUnlockingComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.unlockCard'), isFinish: true };
          } else {
            this.wizardSteps[2] = { step: 3, component: CardDialogInsufficientDataComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.finish'), isFinish: true };
          }
        } else {
          if (this.cardUnlockingData.unlockingInformation.dataConfirmed) {
            this.wizardSteps[2] = { step: 3, component: ChoiceClientTransactionsComponent };
            if (this.wizardSteps.length < 4) {
              this.wizardSteps.push({ step: 4, component: null });
            } else {
              this.wizardSteps[3]({ step: 4, component: null });
            }
          } else {
            this.wizardSteps[2] = { step: 3, component: CardDialogInsufficientDataComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.finish'), isFinish: true };
          }
        }
        break;
      case 'ChoiceClientTransactionsComponent':
        if (this.cardUnlockingData.unlockingInformation.success) {
          this.wizardSteps[3] = { step: 4, component: CardUnlockingComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.unlockCard'), isFinish: true };
        } else {
          this.wizardSteps[3] = { step: 4, component: CardDialogInsufficientDataComponent, showBack: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.finish'), isFinish: true };
        }
        break;
      default:
        break;
    }
  }
}
</script>

<style scoped>
  .q-stepper .q-banner ul {
    margin: 0;
  }
</style>
