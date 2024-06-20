import baseApi from './base.api';
import constants from '../common/constants';
import CoreApi from './core.api';

import { TravelListRequestInterface, TravelDetailRequestInterface, TravelDeleteRequestInterface, TravelNewRequestInterface, TravelCountRequestInterface } from '../interfaces/requests/travel';
import { TravelListResponseInterface, TravelDetailResponseInterface, TravelDeleteStatusResponseInterface, TravelNewStatusResponseInterface, TravelCountResponseInterface } from '../interfaces/responses/travel';
import { TravelListResponseModel, TravelDetailResponseModel, TravelDeleteStatusResponseModel, TravelNewStatusResponseModel, TravelCountResponseModel } from '../models/responses/travel';

class TravelApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getTravelListAsync (params: TravelListRequestInterface): Promise<TravelListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TravelListResponseInterface>(url.toString());
    return new TravelListResponseModel(response.data.rows, response.data.isNextPageAvailable);
  }

  public async getDetailAsync (params: TravelDetailRequestInterface): Promise<TravelDetailResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TravelDetailResponseInterface>(url.toString());
    // @ts-ignore
    return new TravelDetailResponseModel(response.data);
  }

  public async deleteAsync (params: TravelDeleteRequestInterface): Promise<TravelDeleteStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/delete`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.delete<TravelDeleteStatusResponseInterface>(url.toString());
    // @ts-ignore
    return new TravelDeleteStatusResponseModel(response.data);
  }

  public async createAsync (params: TravelNewRequestInterface): Promise<TravelNewStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}`);
    const response = await baseApi.put<TravelNewStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new TravelNewStatusResponseModel(response.data);
  }

  public async getCountAsync (params: TravelCountRequestInterface): Promise<TravelCountResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/count`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TravelCountResponseInterface>(url.toString());
    // @ts-ignore
    return new TravelCountResponseModel(response.data.totalRows);
  }
}

export const travelApi = new TravelApi(constants.api.travel_host);
