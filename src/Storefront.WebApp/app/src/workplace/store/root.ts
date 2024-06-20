import { Module, Actions } from 'vuex-smart-module';
import { AxiosError } from 'axios';
import { Dialog, Notify } from 'quasar';
import searchCountStore from './modules/searchCount.module';
import searchClientStore from './modules/searchClient.module';
import clientDetailStore from './modules/clientDetail.module';
import cardDetailStore from './modules/cardDetail.module';
import cardListStore from './modules/cardList.module';
import cardTariffDetailStore from './modules/cardTariffDetail.module';
import cardTariffListStore from './modules/cardTariffList.module';
import clientAccountListStore from './modules/accountList.module';
import agreementListStore from './modules/agreementList.module';
import agreementDetailStore from './modules/agreementDetail.module';
import agreementTariffDetailStore from './modules/agreementTariffDetail.module';
import agreementTariffListStore from './modules/agreementTariffList.module';
import serviceListStore from './modules/serviceList.module';
import menuLeftStore from './modules/menuLeftDetail.module';
import cardFullBalanceStore from './modules/cardFullBalance.module';
import cardImageStore from './modules/cardImage.module'
// import userStore from './modules/user.module';
import travelListStore from './modules/travelList.module';
import travelDetailStore from './modules/travelDetail.module';
import countryListStore from './modules/countryList.module';
import moneyBoxListStore from './modules/moneyBoxList.module';
import ReportExceptionsResponse from '../models/responses/user/reportExceptionsResponse.model';
import { ResponseErrorInterface } from '../interfaces/responses/errors/responseError.interface';
import { ValidationErrorInterface } from '../interfaces/responses/errors/validationError.interface';
import ExceptionParser from '../utils/exception.parser';
import ErrorPopupComponent from '../components/shared/errorPopup.component.vue';
import ValidationError from '../models/error/validationError.model';
import BaseGetters from './base/base.getters';
import BaseState from './base/base.state';
import BaseMutations from './base/base.mutations';
import { userApi } from '../api/user.api';

class RootState extends BaseState {
  public Exceptions: Array<ResponseErrorInterface> = [];
  public isShowExceptionForm = false;

  public exception?: ResponseErrorInterface = undefined;
  public validationError?: ValidationErrorInterface = undefined;

  public get isSuccess () : boolean {
    return !this.exception && !this.validationError;
  }

  public get hasError () : boolean {
    return !!this.exception;
  }
}

class RootGetters extends BaseGetters<RootState> {
  public get Count (): number {
    return this.state.Exceptions.length;
  }

  public get Exceptions (): Array<ResponseErrorInterface> {
    return this.state.Exceptions;
  }

  public get isSuccess () : boolean {
    return !this.state.exception && !this.state.validationError;
  }
}

class RootMutations extends BaseMutations<RootState> {
  public addException (payload: ResponseErrorInterface): void {
    this.state.exception = payload;
    this.state.Exceptions.push(payload);
  }

  public clearExceptions (): void {
    this.state.exception = undefined;
    this.state.Exceptions.splice(0);
  }

  public showExceptionForm (payload: boolean): void {
    this.state.isShowExceptionForm = payload;
  }

  public setValidationError (payload: ValidationErrorInterface) {
    this.state.validationError = payload;
  }
}

class RootActions extends Actions<RootState, RootGetters, RootMutations, RootActions> {
  public reportExceptions () {
    return userApi.reportExeptionsAsync({ exceptions: this.state.Exceptions, requestedUrl: window.location.href });
  }

  public addExceptions (error: AxiosError): void {
    const result = ExceptionParser.errorParse(error);
    if (result.status !== undefined) {
      switch (result.status) {
        case 400 : {
          const variableErr = result as ValidationError;
          this.mutations.setValidationError(variableErr);

          if (variableErr.errors === undefined) {
            // General validation
            Notify.create({
              type: 'warning',
              position: 'top',
              timeout: 2500,
              textColor: 'white',
              actions: [{ icon: 'close', color: 'white' }],
              caption: variableErr.message
            });
          }
          break;
        }
        case 404:
          /* this.mutations.addException(result);
          if (!this.state.isShowExceptionForm) {
            this.mutations.showExceptionForm(true);
            Dialog.create({
              title: 'Error',
              component: ErrorPopupComponent,
              errorText: result.message,
              isEnableDetail: false
            }).onOk(() => {
              this.mutations.clearExceptions();
              this.mutations.showExceptionForm(false);
            }).onCancel(() => {
              this.mutations.showExceptionForm(false);
            });
          } */
          break;
        case 500 : {
          this.mutations.addException(result);
          if (!this.state.isShowExceptionForm) {
            this.mutations.showExceptionForm(true);
            Dialog.create({
              title: 'Error',
              component: ErrorPopupComponent,
              errorText: result.message,
              sessionId: result.sessionId,
              errorDetail: result.exception,
              errorCount: this.state.Exceptions.length,
              isEnableDetail: true
            }).onOk(() => {
              this.dispatch('reportExceptions').then((res: ReportExceptionsResponse) => {
                this.showReportResult(res);
              });
            }).onCancel(() => {
              this.mutations.showExceptionForm(false);
            });
          }
          break;
        }
      }
    } else {
      // General error (ex. Network Error etc.)
      this.mutations.addException(result);
      if (!this.state.isShowExceptionForm) {
        this.mutations.showExceptionForm(true);
        Dialog.create({
          title: 'Error',
          component: ErrorPopupComponent,
          errorText: result.message,
          isEnableDetail: false
        }).onOk(() => {
          this.dispatch('reportExceptions').then((res: ReportExceptionsResponse) => {
            this.showReportResult(res);
          });
        }).onCancel(() => {
          this.mutations.showExceptionForm(false);
        });
      }
    }
  }

  public clearExceptions (): void {
    this.mutations.clearExceptions();
  }

  public showExceptionForm (show: boolean): void {
    this.mutations.showExceptionForm(show);
  }

  public showReportResult (val: ReportExceptionsResponse): void {
    this.clearExceptions();
    this.showExceptionForm(false);
    if (val.success) {
      Notify.create({
        type: 'positive',
        position: 'top',
        message: val.message
      });
    } else {
      Notify.create({
        type: 'negative',
        position: 'top',
        message: val.message
      });
    }
  }
}

export default new Module({
  namespaced: true,
  state: RootState,
  mutations: RootMutations,
  getters: RootGetters,
  actions: RootActions,
  modules: {
    searchCountStore,
    searchClientStore,
    clientDetailStore,
    cardDetailStore,
    cardListStore,
    cardTariffDetailStore,
    cardTariffListStore,
    clientAccountListStore,
    agreementListStore,
    agreementDetailStore,
    agreementTariffDetailStore,
    agreementTariffListStore,
    serviceListStore,
    menuLeftStore,
    cardFullBalanceStore,
    cardImageStore,
    travelListStore,
    travelDetailStore,
    countryListStore,
    moneyBoxListStore,
    // userStore
  }
});
