import { Module, Mutation, Action, getModule } from 'vuex-module-decorators';
import { userApi } from '../../api/user.api';
import { setJWT, clearJWT } from '../../api/base.api';
import { AuthRequestInterface } from '../../interfaces/requests/authRequest.interface';
import { FeedbackRequestInterface } from '../../interfaces/requests/feedbackRequest.interface';
import { UserResponseInterface, FeedbackResponseInterface } from '../../interfaces/responses/user/index';
import FeedbackResponseModel from '../../models/responses/user/feedbackResponse.model';
import newStore from '../index';
import RootModule, { IRootModuleState } from './root.module';
import { UserProfileModel } from '../../models/user';
import { removeToken, setToken, getToken } from '../../utils/cookies';
import ExceptionParser from '../../utils/exception.parser';

export interface IUserState extends IRootModuleState {
  identity: {
    isAuthenticated: boolean
  },
  token: string | null,
  tokenExpAt?: string,
  profile?: UserProfileModel | null
}

const authToken = getToken();

@Module({ namespaced: true, dynamic: true, store: newStore, name: 'user', stateFactory: true })
class User extends RootModule implements IUserState {
  public identity = authToken ? { isAuthenticated: true } : { isAuthenticated: false };
  public token = authToken;
  public tokenExpAt?: string;
  public profile?: UserProfileModel | null = null;

  get isAuthenticated () {
    return this?.identity?.isAuthenticated ?? false;
  }

  get profileLoaded () {
    return this.isAuthenticated && this.profile !== null;
  }

  @Mutation
  protected LOGIN_SUCCESS (response: UserResponseInterface): void {
    this.identity.isAuthenticated = true;
    this.profile = response.userProfile;
    this.token = response.token;
    this.tokenExpAt = response.tokenExpAt;
  }

  @Mutation
  protected LOGIN_FAILURE (): void {
    this.identity.isAuthenticated = false;
    this.profile = null;
  }

  @Mutation
  protected LOGOUT (): void {
    this.identity.isAuthenticated = false;
    this.profile = null;
    this.token = null;
    this.tokenExpAt = undefined;
  }

  @Action({ rawError: true })
  public async Login (params: AuthRequestInterface) : Promise<boolean> {
    this.START_LOADING();

    await userApi.loginAsync(params).then((response: UserResponseInterface) => {
      this.LOGIN_SUCCESS(response);

      setJWT(response.token);
      setToken(response.token);
    }).catch((error) => {
      this.SET_EXCEPTION(ExceptionParser.errorParse(error));
    }).finally(() => {
      this.STOP_LOADING();
    });

    return !this.hasError;
  }

  @Action({ rawError: true })
  public async SignOut (): Promise<void> {
    this.START_LOADING();

    if (this.token != null) {
      setJWT(this.token);
    }

    await userApi.logoutAsync();
    this.LOGOUT();

    clearJWT();
    removeToken();

    this.STOP_LOADING();
  }

  @Action({ rawError: true })
  public async RefreshToken (): Promise<boolean> {
    let success = true;
    this.START_LOADING();

    if (this.token != null) {
      setJWT(this.token);
    }

    await userApi.refreshTokenAsync().then((response: UserResponseInterface) => {
      this.LOGIN_SUCCESS(response);

      setJWT(response.token);
      setToken(response.token);
    }).catch((error) => {
      void super.ShowErrorForm(error);
      success = false;
    }).finally(() => {
      this.STOP_LOADING();
      // this.context.commit('STOP_LOADING');
    });
    return success;
  }

  @Action({ rawError: true })
  public async SendFeedback (params: FeedbackRequestInterface) : Promise<FeedbackResponseModel> {
    let result : FeedbackResponseModel = new FeedbackResponseModel({ success: false, message: this.getErrorMessage });

    this.START_LOADING();

    await userApi.sendFeedbackAsync(params).then((response: FeedbackResponseInterface) => {
      result = new FeedbackResponseModel(response);
    }).catch((error) => {
      this.SET_EXCEPTION(ExceptionParser.errorParse(error));
      result.message = this.getErrorMessage;
    }).finally(() => {
      this.STOP_LOADING();
    });

    return result;
  }
}

export const UserModule = getModule(User);
