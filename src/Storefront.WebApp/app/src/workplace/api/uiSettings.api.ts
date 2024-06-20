import constants from '../common/constants';
import { MenuLeftRequestInterface } from '../interfaces/requests/menu/menuLeftRequest.interface';
import { MenuLeftResponseModel } from '../models/responses/menu';
import { MenuLeftResponseInterface } from '../interfaces/responses/menu';
import ServiceListRequestModel from '../models/requests/service/serviceListRequest.model';
import ServiceListResponseModel from '../models/responses/service/serviceListResponse.model';
import { ServiceListResponseInterface } from '../interfaces/responses/service';
import baseApi from './base.api';
import CoreApi from './core.api';

class UiSettingsApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getMenuLeftAsync (params: MenuLeftRequestInterface): Promise<MenuLeftResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/menu/left`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<MenuLeftResponseInterface>(url.toString());
    return new MenuLeftResponseModel(response.data.rows);
  }

  public async getServiceListAsync (params: ServiceListRequestModel): Promise<ServiceListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<ServiceListResponseInterface>(url.toString());
    return new ServiceListResponseModel(response.data.rows);
  }
}

export const uiSettingsApi = new UiSettingsApi(constants.api.ui_host);
