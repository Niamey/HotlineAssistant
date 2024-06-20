import { ExceptionType } from '../../types';
import { ResponseErrorInterface } from '../../interfaces/responses/errors/responseError.interface';
import { BaseErrorModel } from './baseError.model';

export default class ResponseError extends BaseErrorModel implements ResponseErrorInterface {
  constructor (data: BaseErrorModel) {
    super();
    this.sessionId = data.sessionId;
    this.statusCode = data.statusCode;
    this.status = data.status;
    this.request = data.request;
  }

  public exception?: ExceptionType;
}
