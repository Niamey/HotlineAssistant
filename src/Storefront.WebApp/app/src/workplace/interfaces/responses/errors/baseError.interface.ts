import { FailedRequestModel } from '../../../models/error/failedRequest.model';

export interface BaseErrorInterface {
  sessionId?: string;
  statusCode?: string;
  message?: string;
  request?: FailedRequestModel;
}
