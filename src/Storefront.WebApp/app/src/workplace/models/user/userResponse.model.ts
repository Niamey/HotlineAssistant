import { UserProfileModel } from './userProfile.model';
import { UserResponseInterface } from '../../interfaces/responses/user/userResponse.interface';

export class UserResponse implements UserResponseInterface {
  constructor (data: UserResponseInterface) {
    this.userProfile = data.userProfile;
    this.token = data.token;
    this.tokenExpAt = data.tokenExpAt;
  }

  public token: string;
  public tokenExpAt: string;
  public userProfile : UserProfileModel;
}
