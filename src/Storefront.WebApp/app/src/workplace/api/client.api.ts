import {
  SearchCountCounterpartRequestInterface,
  SearchCounterpartRequestInterface
} from '../interfaces/requests/client/search';
import baseApi from './base.api';
import {
  CounterpartResponseModel,
  CounterpartCollectionResponseModel,
  CounterpartCountResponseModel
} from '../models/responses/counterparts';
import {
  CounterpartCountResponseInterface,
  CounterpartCollectionResponseInterface,
  CounterpartResponseInterface
} from '../interfaces/responses/client';
import { SearchClientDetailRequestInterface } from '../interfaces/requests/client/detail';
import constants from '../common/constants';
import CoreApi from './core.api';

class ClientApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getClientListAsync (params: SearchCounterpartRequestInterface): Promise<CounterpartCollectionResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/search`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CounterpartCollectionResponseInterface>(url.toString());
    return new CounterpartCollectionResponseModel(response.data.isNextPageAvailable, response.data.rows);
  }

  public async getTotalAsync (params: SearchCountCounterpartRequestInterface) : Promise<CounterpartCountResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/count`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CounterpartCountResponseInterface>(url.toString());
    return new CounterpartCountResponseModel(response.data.totalRows);
  }

  public async getDetailAsync (params: SearchClientDetailRequestInterface) : Promise<CounterpartResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CounterpartResponseInterface>(url.toString());
    // @ts-ignore
    return new CounterpartResponseModel(response.data);
  }
}

export const clientApi = new ClientApi(constants.api.counterpart_host);
