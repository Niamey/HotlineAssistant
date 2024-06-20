import { UserProfileModel } from '../../user';
import { UserResponseInterface } from '../../../interfaces/responses/user/userResponse.interface';

export default class UserResponseModel implements UserResponseInterface {
  constructor (data :UserResponseInterface) {
    this.userProfile = data.userProfile;
    this.token = data.token;
    this.tokenExpAt = data.tokenExpAt;
  }

  public token: string;
  public tokenExpAt: string;
  public userProfile : UserProfileModel
}
