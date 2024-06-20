import { Mutations } from 'vuex-smart-module';
import State from './base.state';
import { ValidationErrorInterface } from '../../interfaces/responses/errors/validationError.interface';
import { ResponseErrorInterface } from '../../interfaces/responses/errors/responseError.interface';

export default class BaseMutations<TState extends State> extends Mutations<TState> {
  public startLoading () {
    this.state.exception = undefined;
    this.state.validationError = undefined;
    this.state.isLoading = true;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }

  public setException (payload: ResponseErrorInterface) {
    this.state.exception = payload;
    this.state.isLoading = false;
  }

  public setValidationError (payload: ValidationErrorInterface) {
    this.state.validationError = payload;
    this.state.isLoading = false;
  }
}
