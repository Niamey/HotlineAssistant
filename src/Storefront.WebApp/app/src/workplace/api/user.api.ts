import CoreApi from './core.api';
import constants from '../common/constants';
import { AuthRequestInterface } from '../interfaces/requests/authRequest.interface';
import { FeedbackRequestInterface } from '../interfaces/requests/feedbackRequest.interface';
import { ReportExceptionsRequestInterface } from '../interfaces/requests/reportExceptionsRequest.interface';
import baseApi from './base.api';
import UserResponse from '../models/responses/user/userResponse.model';
import FeedbackResponse from '../models/responses/user/feedbackResponse.model';
import ReportExceptionsResponse from '../models/responses/user/reportExceptionsResponse.model'
import { UserResponseInterface, FeedbackResponseInterface, ReportExceptionsResponseInterface } from '../interfaces/responses/user/index';

class UserApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async loginAsync (params: AuthRequestInterface): Promise<UserResponse> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/Authorization/Login`);
    // @ts-ignore
    const response = await baseApi.post<UserResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new UserResponse(response.data);
  }

  public async refreshTokenAsync (): Promise<UserResponse> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/Authorization/refreshToken`);
    const response = await baseApi.post<UserResponseInterface>(url.toString());
    // @ts-ignore
    return new UserResponse(response.data);
  }

  public async sendFeedbackAsync (params: FeedbackRequestInterface) : Promise<FeedbackResponse> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/FeedBack/send`);
    const response = await baseApi.post<FeedbackResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new FeedbackResponse(response.data);
  }

  public async reportExeptionsAsync (params: ReportExceptionsRequestInterface) : Promise<ReportExceptionsResponse> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/FeedBack/report/exceptions`);
    const response = await baseApi.post<ReportExceptionsResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new ReportExceptionsResponse(response.data);
  }

  public async logoutAsync () : Promise<void> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/Authorization/logout`);
    await baseApi.post<UserResponseInterface>(url.toString());
  }
}

export const userApi = new UserApi(constants.api.user_host);
