import { ValidationErrorInterface } from '../../interfaces/responses/errors/validationError.interface';
import { ResponseErrorInterface } from '../../interfaces/responses/errors/responseError.interface';

export default class BaseState {
  constructor () {
    this.isLoading = true;
  }

  public exception?: ResponseErrorInterface = undefined;
  public validationError?: ValidationErrorInterface = undefined;

  public isLoading: boolean;

  public get isSuccess () : boolean {
    return !this.exception && !this.validationError;
  }

  public get hasError () : boolean {
    return !!this.exception;
  }
}
