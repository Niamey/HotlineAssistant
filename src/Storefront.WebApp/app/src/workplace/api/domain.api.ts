import baseApi from './base.api';
import { ConfigurationResponseInterface } from '../interfaces/responses/configuration/configurationResponse.interface';
import { ConfigurationResponseModel } from '../models/responses/configuration';
import ConfigurationRequestInterface from '../interfaces/requests/configurationRequest.interface';
import { SessionStorage } from 'quasar';
import constants from '../common/constants';

class DomainApi {
  public async getDomainConfigurationAsync (params: ConfigurationRequestInterface): Promise<void> {
    const url = new URL(`${process.env.API_STORE_FRONT_URL}/domain/configuration`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<ConfigurationResponseInterface>(url.toString());
    DomainApi.saveSettings(new ConfigurationResponseModel(response.data));
  }

  private static saveSettings (payload: ConfigurationResponseInterface) {
    SessionStorage.clear();
    SessionStorage.set(constants.api.ui_host, payload.settings.userInterfaceApiSetting?.host || '');
    SessionStorage.set(constants.api.account_host, payload.settings.accountApiSetting?.host || '');
    SessionStorage.set(constants.api.agreement_host, payload.settings.agreementApiSetting?.host || '');
    SessionStorage.set(constants.api.counterpart_host, payload.settings.counterpartApiSetting?.host || '');
    SessionStorage.set(constants.api.card_host, payload.settings.cardApiSetting?.host || '');
    SessionStorage.set(constants.api.transaction_host, payload.settings.transactionApiSetting?.host || '');
    SessionStorage.set(constants.api.modules_host, payload.settings.modulesApiSetting?.host || '');
    SessionStorage.set(constants.api.travel_host, payload.settings.travelApiSetting?.host || '');
    SessionStorage.set(constants.api.country_host, payload.settings.countryApiSetting?.host || '');
    SessionStorage.set(constants.api.statement_host, payload.settings.statementApiSetting?.host || '');

    SessionStorage.set(constants.versionId, payload.versionId || '');
    SessionStorage.set(constants.isNeedUpdate, false);
  }
}

export const domainApi = new DomainApi();
