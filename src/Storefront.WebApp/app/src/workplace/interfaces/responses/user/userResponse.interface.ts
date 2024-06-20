import { UserProfileModel } from '../../../models/user';

export interface UserResponseInterface {
  token: string;
  tokenExpAt: string;
  userProfile: UserProfileModel;
}
