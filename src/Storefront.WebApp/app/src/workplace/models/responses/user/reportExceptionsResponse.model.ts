import { ReportExceptionsResponseInterface } from '../../../interfaces/responses/user/reportExceptionsResponse.interface';

export default class ReportExceptionsResponseModel implements ReportExceptionsResponseInterface {
  constructor (data :ReportExceptionsResponseInterface) {
    this.success = data.success;
    this.message = data.message;
  }

  public success: boolean;
  public message?: string;
}