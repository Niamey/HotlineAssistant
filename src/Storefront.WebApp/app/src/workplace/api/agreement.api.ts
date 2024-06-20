import baseApi from './base.api';
import { AgreementListResponseModel } from '../models/responses/agreement/list';
import { AgreementListResponseInterface } from '../interfaces/responses/agreement/list';
import { AgreementListRequestInterface } from '../interfaces/requests/agreement/agreementListRequest.interface';

import { AgreementDetailResponseModel } from '../models/responses/agreement/detail';
import { AgreementDetailResponseInterface } from '../interfaces/responses/agreement/detail';
import { AgreementDetailRequestInterface } from '../interfaces/requests/agreement/agreementDetailRequest.interface';

import { TariffListResponseModel } from '../models/responses/tariff/list';
import { TariffListResponseInterface } from '../interfaces/responses/tariff/list';

import { TariffDetailResponseModel, TariffSendingResponseModel } from '../models/responses/tariff/detail';
import { TariffDetailResponseInterface, TariffSendingResponseInterface } from '../interfaces/responses/tariff/detail';

import { AgreementTariffDetailRequestInterface, AgreementTariffListRequestInterface, AgreementTariffSendingRequestInterface } from '../interfaces/requests/agreement/tariff';

import constants from '../common/constants';
import CoreApi from './core.api';

import { AgreementStatusHistoryRequestInterface } from '../interfaces/requests/agreement/status';
import { AgreementStatusHistoryResponseInterface } from '../interfaces/responses/agreement/status';
import { AgreementStatusHistoryResponseModel } from '../models/responses/agreement/status';

import { AgreementBalanceRequestInterface } from '../interfaces/requests/agreement/balance/agreementBalanceRequest.interface';
import { AgreementBalanceResponseInterface } from '../interfaces/responses/agreement/balance/agreementBalanceResponse.interface';
import { AgreementBalanceResponseModel } from '../models/responses/agreement/balance/agreementBalanceResponse.model';

import { ClassifierListRequestInterface } from '../interfaces/requests/classifier/classifierListRequest.interface';
import { ClassifierListResponseInterface } from '../interfaces/responses/classifier/list/classifierListResponse.interface';
import { ClassifierListResponseModel } from '../models/responses/classifier/list/classifierListResponse.model';

import { StatementRequestInterface, StatementSendingRequestInterface } from '../interfaces/requests/agreement/statement';
import { StatementResponseInterface, StatementSendingResponseInterface } from '../interfaces/responses/agreement/statement';
import { StatementResponseModel, StatementSendingResponseModel } from '../models/responses/agreement/statement';

import { MoneyBoxListRequestInterface } from '../interfaces/requests/agreement/moneybox/moneyBoxListRequest.interface';
import { MoneyBoxListResponseInterface } from '../interfaces/responses/agreement/moneybox/moneyBoxListResponse.interface';
import { MoneyBoxListResponseModel } from '../models/responses/agreement/moneybox/moneyBoxListResponse.model';
import { SessionStorage } from 'quasar';

class AgreementApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  protected apiStatementHostAddress: string | null;
  constructor (host: string) {
    super(host);
    this.apiStatementHostAddress = null;
  }
  
    protected async loadStatementConfig() {
      await super.loadConfig();
      this.apiStatementHostAddress = SessionStorage.getItem(constants.api.statement_host) || '';
    }

  public async getAgreementListAsync (params: AgreementListRequestInterface): Promise<AgreementListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<AgreementListResponseInterface>(url.toString());
    return new AgreementListResponseModel(response.data.rows);
  }

  public async getDetailAsync (params: AgreementDetailRequestInterface): Promise<AgreementDetailResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<AgreementDetailResponseInterface>(url.toString());
    // @ts-ignore
    return new AgreementDetailResponseModel(response.data);
  }

  public async getStatusHistoryAsync (params: AgreementStatusHistoryRequestInterface): Promise<AgreementStatusHistoryResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/status/history`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<AgreementStatusHistoryResponseInterface>(url.toString());
    // @ts-ignore
    return new AgreementStatusHistoryResponseModel(response.data);
  }

  public async getAllTariffAsync (params: AgreementTariffListRequestInterface): Promise<TariffListResponseInterface> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/all`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TariffListResponseInterface>(url.toString());
    return new TariffListResponseModel(response.data.rows);
  }

  public async getCurrentTariffAsync (params: AgreementTariffDetailRequestInterface): Promise<TariffDetailResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/current`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TariffDetailResponseInterface>(url.toString());
    // @ts-ignore
    return new TariffDetailResponseModel(response.data);
  }

  public async sendTariffEmailAsync (params: AgreementTariffSendingRequestInterface): Promise<TariffSendingResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/send/email`);
    const response = await baseApi.post<TariffSendingResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new TariffSendingResponseModel(response.data);
  }
  
  public async sendTariffViberAsync (params: AgreementTariffSendingRequestInterface): Promise<TariffSendingResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/send/viber`);
    const response = await baseApi.post<TariffSendingResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new TariffSendingResponseModel(response.data);
  }
  
  public async getBalanceAsync (params: AgreementBalanceRequestInterface): Promise<AgreementBalanceResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/balance`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<AgreementBalanceResponseInterface>(url.toString());
    // @ts-ignore
    return new AgreementBalanceResponseModel(response.data);
  }
  
  public async getClassifiersAsync (params: ClassifierListRequestInterface): Promise<ClassifierListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/classifiers`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<ClassifierListResponseInterface>(url.toString());
    // @ts-ignore
    return new ClassifierListResponseModel(response.data.rows);
  }
  
  public async getStatementAsync (params: StatementRequestInterface): Promise<string> {
    await this.loadStatementConfig();
    const url = new URL(`${this.apiStatementHostAddress}`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<StatementResponseInterface>(url.toString(), { responseType: 'blob' });
    // @ts-ignore
    const blob = new Blob([response.data]);
    return await blob.text();
  }

  public async sendStatementEmailAsync (params: StatementSendingRequestInterface): Promise<StatementSendingResponseModel> {
    await this.loadStatementConfig();
    const url = new URL(`${this.apiStatementHostAddress}/send/email`);
    const response = await baseApi.post<StatementSendingResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new StatementSendingResponseModel(response.data);
  }
  
  public async getMoneyBoxesAsync (params: MoneyBoxListRequestInterface): Promise<MoneyBoxListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/moneybox/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<MoneyBoxListResponseInterface>(url.toString());
    // @ts-ignore
    return new MoneyBoxListResponseModel(response.data.rows);
  }
}

export const agreementApi = new AgreementApi(constants.api.agreement_host);
