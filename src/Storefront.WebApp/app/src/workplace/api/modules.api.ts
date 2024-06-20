import baseApi from './base.api';
import { PersonalDataListResponseModel } from '../models/responses/modules';
import { PersonalDataListResponseInterface } from '../interfaces/responses/modules';
import { PersonalDataListRequestInterface } from '../interfaces/requests/modules';
import constants from '../common/constants';
import CoreApi from './core.api';

class ModulesApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getPersonalDataServiceAsync (params: PersonalDataListRequestInterface): Promise<PersonalDataListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/personaldata/configuration`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<PersonalDataListResponseInterface>(url.toString());
    return new PersonalDataListResponseModel(response.data.rows);
  }
}

export const modulesApi = new ModulesApi(constants.api.modules_host);
