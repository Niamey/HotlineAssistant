import { ResponseErrorInterface } from '../../interfaces/responses/errors/responseError.interface';

export interface ReportExceptionsRequestInterface {
  exceptions: Array<ResponseErrorInterface>,
  requestedUrl: string
}