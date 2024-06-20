import { BaseErrorInterface } from '../../interfaces/responses/errors/baseError.interface';
import { FailedRequestModel } from './failedRequest.model';

export class BaseErrorModel implements BaseErrorInterface {
  public sessionId?: string;
  public statusCode?: string;
  public status?: number;
  public message?: string;
  public request?: FailedRequestModel;
}
