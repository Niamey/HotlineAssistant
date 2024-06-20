import { ValidationDataType } from '../../types';
import { ValidationErrorInterface } from '../../interfaces/responses/errors/validationError.interface';
import { BaseErrorModel } from './baseError.model';

export default class ValidationError extends BaseErrorModel implements ValidationErrorInterface {
  constructor (data: BaseErrorModel) {
    super();
    this.sessionId = data.sessionId;
    this.statusCode = data.statusCode;
    this.status = data.status;
    this.message = data.message;
  }

  public errors?: Array<ValidationDataType> = [];
}
