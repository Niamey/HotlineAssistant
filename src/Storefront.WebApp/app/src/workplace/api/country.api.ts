import baseApi from './base.api';
import constants from '../common/constants';
import CoreApi from './core.api';

import { CountryListRequestInterface } from '../interfaces/requests/country';
import { CountryListResponseInterface } from '../interfaces/responses/country';
import { CountryListResponseModel } from '../models/responses/country';

class CountryApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getCountryListAsync (params: CountryListRequestInterface): Promise<CountryListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CountryListResponseInterface>(url.toString());
    return new CountryListResponseModel(response.data.rows);
  }
}

export const countryApi = new CountryApi(constants.api.country_host);