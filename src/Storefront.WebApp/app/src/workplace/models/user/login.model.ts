import { AuthRequestInterface } from '../../interfaces/requests/authRequest.interface';

export class UserLogin implements AuthRequestInterface {
  constructor (login: string, password: string) {
    this.login = login;
    this.password = password;
  }

    public login: string;
    public password: string;
}
