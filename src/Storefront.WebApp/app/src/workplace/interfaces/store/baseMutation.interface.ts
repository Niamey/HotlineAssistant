import { ValidationErrorInterface } from '../responses/errors/validationError.interface';
import { ResponseErrorInterface } from '../responses/errors/responseError.interface';

export interface BaseMutationInterface {
  startLoading(): void;
  stopLoading(): void;
  setException(payload: ResponseErrorInterface): void;
  setValidationError(payload: ValidationErrorInterface): void;
}
