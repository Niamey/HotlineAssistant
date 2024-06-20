<template>
  <q-dialog ref="dialog" v-model="isShowDialog" @hide="hideDialog()" persistent>
    <q-card style="width: 612px;">
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
            <component v-if="item.component" :is="item.component" v-bind="dynamicComponentProps(item.component.extendOptions.name)" :travel-info.sync="clientTravelData" @updateStep="updateCurrentStep" ref="dynamicComponent" />
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
import ClientTravelNewComponent from './newTravelWizard/clientTravelNew.component.vue';
import ClientTravelChooseCardsComponent from './newTravelWizard/clientTravelChooseCards.component.vue';
import ClientTravelChangeLimitComponent from './newTravelWizard/clientTravelChangeLimit.component.vue';
import ClientTravelSuccessfullyCreatedComponent from './newTravelWizard/clientTravelSuccessfullyCreated.component.vue';
import { QDialog, QStepper } from 'quasar';
import { TravelInterface } from '../../../../interfaces/travel';
import { CountryInterface } from '../../../../interfaces/country';
import { TravelStatusEnum } from '../../../../enums/travel/travelStatus.enum';

@Component({
  components: {
    ClientTravelNewComponent,
    ClientTravelChangeLimitComponent,
    ClientTravelChooseCardsComponent,
    ClientTravelSuccessfullyCreatedComponent
  }
})
export default class ClientTravelProcessComponent extends Vue {
  @Prop({ default: false }) isShow?: boolean;
  private isShowDialog: boolean;
  private step!: number;
  private wizardSteps: Array<any>;
  private errorText!: string | string[];

  private riskyCountries: Array<CountryInterface>;

  private requestIsLoading: boolean;
  private requestHasError: boolean;
  private requestResult: any;
  private isShowDone: boolean;

  private clientTravelData: TravelInterface | null;

  private dynamicComponentProps (componentName: string) {
    if (this.clientTravelData === null) {
      this.clientTravelData = {
        travelId: 0,
        solarId: parseInt(this.$route.params.id),
        status: TravelStatusEnum.Active,
        countries: [],
        startTravel: undefined,
        endTravel: undefined,
        contactPhone: '',
        cards: [],
        cashWithdrawalLimit: 0,
        limitCardTransfers: 0,
        isClientAbroad: false
      };
    }

    const result = {
      dynamicData: {
        componentName: componentName,
        travel: this.clientTravelData,
        isExistRiskyCountries: this.isExistRiskyCountries,
        riskyCountries: this.riskyCountries
      }
    };

    return result;
  }

  private get getShowBackVisible () {
    const wizardStep = this.getCurrentWizardItem;
    if (wizardStep) {
      if ('showBack' in wizardStep) {
        return wizardStep.showBack;
      }
    }
    return true;
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
    this.clientTravelData = null;

    this.isShowDialog = this.isShow || false;
    this.isShowDone = false;

    this.step = 1;
    this.wizardSteps = [];
    this.riskyCountries = [];

    this.errorText = '';

    this.requestIsLoading = false;
    this.requestHasError = false;
    this.requestResult = null;
  }

  private setInitData () {
    this.clientTravelData = null;
    this.step = 1;
    this.isShowDone = false;
    this.requestResult = {};
    this.wizardSteps = [];
    this.errorText = '';
  }

  private setInitialStep () {
    this.wizardSteps = [
      { step: 1, component: ClientTravelNewComponent },
      { step: 2, component: ClientTravelChooseCardsComponent },
      { step: 3, component: null }
    ];
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
    if (wizardStep) {
      if ('nextButtonLabel' in wizardStep) {
        return wizardStep.nextButtonLabel;
      }
    }
    return this.$t('components.client.wizard.wizard.continue');
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
    const currentStep = this.getCurrentWizardItem;
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
          canGoFurther = result.success;
          this.$q.notify({
            type: result.success ? 'positive' : 'negative',
            message: result.message,
            position: 'top',
            timeout: 2500
          });
        }
      }

      this.nextStepComponent();
    }
    if (!currentStep.isFinish && canGoFurther) {
      this.$refs.stepper.next();
    } else if (canGoFurther) {
      this.$bus.emit('travel-aggregator-list-update');
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

  private get isExistRiskyCountries () {
    if (this.clientTravelData?.countries) {
      this.riskyCountries = this.clientTravelData?.countries.filter(e => e.isRisky);
      return this.riskyCountries.length !== 0;
    }
    return false;
  }

  nextStepComponent () {
    const wizardStep = this.getCurrentWizardItem;
    switch (wizardStep.component) {
      case ClientTravelChooseCardsComponent:
        if (this.isExistRiskyCountries) {
          this.wizardSteps[2] = { step: 3, component: ClientTravelChangeLimitComponent };
          if (this.wizardSteps.length < 4) {
            this.wizardSteps.push({ step: 4, component: ClientTravelSuccessfullyCreatedComponent, nextButtonLabel: this.$t('components.client.detail.travels.finish'), isFinish: true });
          }
        } else {
          this.wizardSteps[2] = { step: 3, component: ClientTravelSuccessfullyCreatedComponent, showBack: false, nextButtonLabel: this.$t('components.client.detail.travels.finish'), isFinish: true };
          if (this.wizardSteps.length > 3) {
            this.wizardSteps.splice(2);
          }
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
