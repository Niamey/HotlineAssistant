import { FailedRequestInterface } from '../../interfaces/responses/errors/failedRequest.interface';

export class FailedRequestModel implements FailedRequestInterface {
  public url?: string;
  public httpMethod?: string;
  public requestParams?: Array<any>;
}