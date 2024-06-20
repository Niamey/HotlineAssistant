import baseApi from './base.api';
import { AccountCollectionResponseInterface } from '../interfaces/responses/account';
import { AccountCollectionResponseModel } from '../models/responses/account';
import SearchAccountRequestModel from '../models/requests/account/searchAccountRequest.model';
import constants from '../common/constants';
import CoreApi from './core.api';

class AccountApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getAccountListAsync (params: SearchAccountRequestModel): Promise<AccountCollectionResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<AccountCollectionResponseInterface>(url.toString());
    return new AccountCollectionResponseModel(response.data.rows);
  }
}

export const accountApi = new AccountApi(constants.api.account_host);
