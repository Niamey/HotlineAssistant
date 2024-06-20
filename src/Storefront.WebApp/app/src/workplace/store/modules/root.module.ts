import { Action, Mutation, VuexModule } from 'vuex-module-decorators';
import { ResponseErrorInterface } from '../../interfaces/responses/errors/responseError.interface';
import { ValidationErrorInterface } from '../../interfaces/responses/errors/validationError.interface';
import { AxiosError } from 'axios';
import ReportExceptionsResponse from '../../models/responses/user/reportExceptionsResponse.model';
import ExceptionParser from '../../utils/exception.parser';
import ValidationError from '../../models/error/validationError.model';
import { Dialog, Notify } from 'quasar';
import { userApi } from '../../api/user.api';
import ErrorPopupComponent from '../../components/shared/errorPopup.component.vue';

export interface IRootModuleState {
  exception?: ResponseErrorInterface;
  exceptionList?: Array<ResponseErrorInterface>;
  validationError?: ValidationErrorInterface;
  isLoading: boolean;
}

export default class RootModule extends VuexModule implements IRootModuleState {
  public exceptionList: Array<ResponseErrorInterface> = [];
  public showForm = false;
  public exception?: ResponseErrorInterface = undefined;
  public validationError?: ValidationErrorInterface = undefined;
  public isLoading = false;
  // public isSuccess = false;// boolean = !this.exception && !this.validationError;
  public exceptionCount = this.exceptionList.length;
  public isShowExceptionForm = this.showForm;

  public get isSuccess () : boolean {
    return !this.exception && !this.validationError;
  }

  public get hasError () : boolean {
    return !!this.exception;
  }

  public get getErrorMessage (): string|undefined {
    return this.exception?.message;
  }

  @Mutation
  protected START_LOADING (): void {
    this.exception = undefined;
    this.validationError = undefined;
    this.isLoading = true;
  }

  @Mutation
  protected STOP_LOADING (): void {
    this.isLoading = false;
  }

  @Mutation
  protected SET_EXCEPTION (payload: ResponseErrorInterface): void {
    // this.hasError = true;
    this.exception = payload;
    this.exceptionList.push(payload);
    this.isLoading = false;
  }

  @Mutation
  protected SET_VALIDATION_ERROR (payload: ValidationErrorInterface): void {
    // this.hasError = true;
    this.validationError = payload;
    this.isLoading = false;
  }

  @Mutation
  protected SET_SHOW_EXCEPTION_FORM (payload: boolean): void {
    this.showForm = payload;
  }

  @Mutation
  protected CLEAR_EXCEPTIONS (): void {
    // this.hasError = false;
    this.exception = undefined;
    this.validationError = undefined;
    this.exceptionList.splice(0);
  }

  @Action
  public StartLoading () {
    this.context.commit('START_LOADING');
  }

  @Action
  public StopLoading () {
    this.context.commit('STOP_LOADING');
  }

  @Action
  public async ReportExceptions () {
    return userApi.reportExeptionsAsync({exceptions: this.exceptionList, requestedUrl: window.location.href});
  }

  @Action({ rawError: true })
  public ShowErrorForm (error: AxiosError): void {
    const result = ExceptionParser.errorParse(error);
    if (result.status !== undefined) {
      switch (result.status) {
        case 400 : {
          const variableErr = result as ValidationError;
          this.context.commit('SET_VALIDATION_ERROR', variableErr);
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
        case 404 : {
          /* this.context.commit('SET_EXCEPTION', result);
          if (!this.showForm) {
            this.context.commit('SET_SHOW_EXCEPTION_FORM', true);
            Dialog.create({
              title: 'Error',
              component: ErrorPopupComponent,
              errorText: result.message,
              isEnableDetail: false
            }).onOk(() => {
              this.context.commit('CLEAR_EXCEPTIONS');
              this.context.commit('SET_SHOW_EXCEPTION_FORM', false);
            }).onCancel(() => {
              this.context.commit('SET_SHOW_EXCEPTION_FORM', false);
            });
          } */
          break;
        }
        case 500 : {
          this.context.commit('SET_EXCEPTION', result);
          if (!this.showForm) {
            this.context.commit('SET_SHOW_EXCEPTION_FORM', true);
            Dialog.create({
              title: 'Error',
              component: ErrorPopupComponent,
              errorText: result.message,
              sessionId: result.sessionId,
              errorDetail: result.exception,
              errorCount: this.exceptionCount,
              isEnableDetail: true
            }).onOk(() => {
              this.context.dispatch('ReportExceptions').then((res: ReportExceptionsResponse) => {
                this.showReportResult(res);
              });
            }).onCancel(() => {
              this.context.commit('SET_SHOW_EXCEPTION_FORM', false);
            });
          }
          break;
        }
      }
    } else {
      this.context.commit('SET_EXCEPTION', result);
      // General error (ex. Network Error etc.)
      if (!this.isShowExceptionForm) {
        this.context.commit('SET_SHOW_EXCEPTION_FORM', true);
        Dialog.create({
          title: 'Error',
          component: ErrorPopupComponent,
          errorText: result.message,
          isEnableDetail: false
        }).onOk(() => {
          this.context.dispatch('ReportExceptions').then((res: ReportExceptionsResponse) => {
            this.showReportResult(res);
          });
        }).onCancel(() => {
          this.context.commit('SET_SHOW_EXCEPTION_FORM', false);
        });
      }
    }
  }

  public showReportResult (val: ReportExceptionsResponse): void {
    this.context.commit('CLEAR_EXCEPTIONS');
    this.context.commit('SET_SHOW_EXCEPTION_FORM', false);
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
