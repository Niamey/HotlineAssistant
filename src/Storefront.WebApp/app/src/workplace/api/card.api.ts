import baseApi from './base.api';
import { CardListResponseModel } from '../models/responses/card/list';
import { CardListResponseInterface } from '../interfaces/responses/card/list';
import { CardDetailRequestInterface } from '../interfaces/requests/card/detail';
import { CardDetailResponseModel } from '../models/responses/card/detail';
import { CardDetailResponseInterface } from '../interfaces/responses/card/detail';
import CardListRequestModel from '../models/requests/card/cardListRequest.model';
import constants from '../common/constants';
import { CardBalanceRequestInterface } from '../interfaces/requests/card/balance';
import { CardBalanceExtendedRequestInterface } from '../interfaces/requests/card/balance/extended';
import { CardBalanceResponseInterface } from '../interfaces/responses/card/balance';
import { CardBalanceResponseModel } from '../models/responses/card/balance';
import { CardBalanceExtendedResponseModel } from '../models/responses/card/balance/extended';
import { CardBalanceExtendedResponseInterface } from '../interfaces/responses/card/balance/extended';
import { CardFullBalanceRequestInterface } from '../interfaces/requests/card/balance/full';
import { CardFullBalanceResponseModel } from '../models/responses/card/balance/full';
import { CardFullBalanceResponseInterface } from '../interfaces/responses/card/balance/full';

import CoreApi from './core.api';
import { CardStatusHistoryRequestInterface, CardSecure3DStatusHistoryRequestInterface, CardCurrentStatusRequestInterface } from '../interfaces/requests/card/status';
import { CardStatusHistoryResponseInterface, CardSecure3DStatusHistoryResponseInterface, CardCurrentStatusResponseInterface } from '../interfaces/responses/card/status';
import { CardStatusHistoryResponseModel, CardSecure3DStatusHistoryResponseModel, CardCurrentStatusResponseModel } from '../models/responses/card/status';

import {
  CardSmsInfoRequestInterface,
  Card3DSecureRequestInterface,
  CardSmsInfoHistoryStatusRequestInterface,
  ChangeSmsInfoStatusRequestInterface
} from '../interfaces/requests/card/services';
import {
  CardSmsInfoResponseInterface,
  Card3DSecureResponseInterface,
  CardSmsInfoHistoryStatusResponseInterface,
  ChangeSmsInfoStatusResponseInterface
} from '../interfaces/responses/card/services';
import {
  CardSmsInfoResponseModel,
  Card3DSecureResponseModel,
  CardSmsInfoHistoryStatusResponseModel,
  ChangeSmsInfoStatusResponseModel
} from '../models/responses/card/services';

import { TariffListResponseModel } from '../models/responses/tariff/list';
import { TariffListResponseInterface } from '../interfaces/responses/tariff/list';

import { TariffDetailResponseModel, TariffSendingResponseModel } from '../models/responses/tariff/detail';
import { TariffDetailResponseInterface, TariffSendingResponseInterface } from '../interfaces/responses/tariff/detail';

import { CardTariffListRequestInterface, CardTariffDetailRequestInterface, CardTariffSendingRequestInterface } from '../interfaces/requests/card/tariff';

import {
  CardBlockingRequestInterface,
  CardLimitOfCashWithdrawalRequestInterface,
  CardLimitStatusRequestInterface,
  CardLimitRiskChangeRequestInterface,
  CardUnlockingFailedRequestInterface,
  CardUnlockingRequestInterface,
  CardUnlockingLockRequestInterface
} from '../interfaces/requests/card/limits';
import {
  CardBlockingResponseInterface,
  CardLimitOfCashWithdrawalResponseInterface,
  CardLimitStatusResponseInterface,
  CardLimitRiskChangeResponseInterface,
  CardUnlockingFailedStatusResponseInterface,
  CardUnlockingStatusResponseInterface,
  CardUnlockingLockStatusResponseInterface
} from '../interfaces/responses/card/limits';
import {
  CardBlockingResponseModel,
  CardLimitOfCashWithdrawalResponseModel,
  CardLimitStatusResponseModel,
  CardLimitRiskChangeResponseModel,
  CardUnlockingFailedStatusResponseModel,
  CardUnlockingStatusResponseModel,
  CardUnlockingLockStatusResponseModel
} from '../models/responses/card/limits';

import {
  CardTokenListRequestInterface,
  CardTokenLastStatusRequestInterface,
  CardTokenDeleteRequestInterface,
  CardTokenActivateRequestInterface
} from '../interfaces/requests/card/token';
import { CardTokenListResponseModel, CardTokenLastStatusResponseModel, CardTokenActionResponseModel } from '../models/responses/card/token';
import { CardTokenListResponseInterface, CardTokenLastStatusResponseInterface, CardTokenActionResponseInterface } from '../interfaces/responses/card/token';

import { CardImageRequestInterface } from '../interfaces/requests/card/cardImage/cardImageRequest.interface';
import { CardImageResponseModel } from '../models/responses/card/cardImage/cardImageResponse.model';
import { CardImageResponseInterface } from '../interfaces/responses/card/cardImage/cardImageResponse.interface';

import { CardDebtToBankRequestInterface } from '../interfaces/requests/card/debtToBank/cardDebtToBankRequest.interface';
import { CardDebtToBankResponseModel } from '../models/responses/card/debtToBank/cardDebtToBankResponse.model';
import { CardDebtToBankResponseInterface } from '../interfaces/responses/card/debtToBank/cardDebtToBankResponse.interface';

class CardApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getCardListAsync (params: CardListRequestModel): Promise<CardListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardListResponseInterface>(url.toString());
    return new CardListResponseModel(response.data.rows);
  }

  public async getDetailAsync (params: CardDetailRequestInterface) : Promise<CardDetailResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardDetailResponseInterface>(url.toString());
    // @ts-ignore
    return new CardDetailResponseModel(response.data);
  }

  public async getBalanceAsync (params: CardBalanceRequestInterface): Promise<CardBalanceResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/balance`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardBalanceResponseInterface>(url.toString());
    // @ts-ignore
    return new CardBalanceResponseModel(response.data);
  }

  public async getStatusHistoryAsync (params: CardStatusHistoryRequestInterface): Promise<CardStatusHistoryResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/status/history`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardStatusHistoryResponseInterface>(url.toString());
    // @ts-ignore
    return new CardStatusHistoryResponseModel(response.data);
  }

  public async getBalanceExtendedAsync (params: CardBalanceExtendedRequestInterface): Promise<CardBalanceExtendedResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/balance/extended`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardBalanceExtendedResponseInterface>(url.toString());
    // @ts-ignore
    return new CardBalanceExtendedResponseModel(response.data);
  }

  public async getFullBalanceAsync (params: CardFullBalanceRequestInterface): Promise<CardFullBalanceResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/balance/full`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardFullBalanceResponseInterface>(url.toString());
    // @ts-ignore
    return new CardFullBalanceResponseModel(response.data);
  }

  public async getSmsInfoAsync (params: CardSmsInfoRequestInterface): Promise<CardSmsInfoResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/sms-info`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardSmsInfoResponseInterface>(url.toString());
    // @ts-ignore
    return new CardSmsInfoResponseModel(response.data);
  }

  public async get3DSecureAsync (params: Card3DSecureRequestInterface): Promise<Card3DSecureResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/3d-secure`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<Card3DSecureResponseInterface>(url.toString());
    // @ts-ignore
    return new Card3DSecureResponseModel(response.data);
  }

  public async getSecure3DStatusHistoryAsync (params: CardSecure3DStatusHistoryRequestInterface): Promise<CardSecure3DStatusHistoryResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/3d-secure/history`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardSecure3DStatusHistoryResponseInterface>(url.toString());
    // @ts-ignore
    return new CardSecure3DStatusHistoryResponseModel(response.data);
  }

  public async getAllTariffAsync (params: CardTariffListRequestInterface): Promise<TariffListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/all`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TariffListResponseInterface>(url.toString());
    return new TariffListResponseModel(response.data.rows);
  }

  public async changeSmsInfoStatusTurnOnAsync (params: ChangeSmsInfoStatusRequestInterface): Promise<ChangeSmsInfoStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/sms-info/turnon`);
    // @ts-ignore
    // url.search = new URLSearchParams(params).toString();
    const response = await baseApi.post<ChangeSmsInfoStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new ChangeSmsInfoStatusResponseModel(response.data);
  }

  public async changeSmsInfoStatusTurnOffAsync (params: ChangeSmsInfoStatusRequestInterface): Promise<ChangeSmsInfoStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/sms-info/turnoff`);
    // @ts-ignore
    // url.search = new URLSearchParams(params).toString();
    const response = await baseApi.post<ChangeSmsInfoStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new ChangeSmsInfoStatusResponseModel(response.data);
  }

  public async getCurrentTariffAsync (params: CardTariffDetailRequestInterface): Promise<TariffDetailResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/current`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TariffDetailResponseInterface>(url.toString());
    // @ts-ignore
    return new TariffDetailResponseModel(response.data);
  }

  public async getSmsInfoStatusHistoryAsync (params: CardSmsInfoHistoryStatusRequestInterface): Promise<CardSmsInfoHistoryStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/service/sms-info/history`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardSmsInfoHistoryStatusResponseInterface>(url.toString());
    // @ts-ignore
    return new CardSmsInfoHistoryStatusResponseModel(response.data);
  }

  public async blockCardAsync (params: CardBlockingRequestInterface): Promise<CardBlockingResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/blocking`);
    // @ts-ignore
    // url.search = new URLSearchParams(params).toString();
    const response = await baseApi.post<CardBlockingResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardBlockingResponseModel(response.data);
  }

  public async getCardLimitOfCashWithdrawalAsync (params: CardLimitOfCashWithdrawalRequestInterface): Promise<CardLimitOfCashWithdrawalResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/limit/cash-withdrawal`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardLimitOfCashWithdrawalResponseInterface>(url.toString());
    // @ts-ignore
    return new CardLimitOfCashWithdrawalResponseModel(response.data);
  }

  public async setCardLimitOfCashWithdrawalAsync (params: CardLimitStatusRequestInterface): Promise<CardLimitStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/limit/cash-withdrawal`);
    // @ts-ignore
    // url.search = new URLSearchParams(params).toString();
    const response = await baseApi.post<CardLimitStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardLimitStatusResponseModel(response.data);
  }

  public async getCurrentStatusAsync (params: CardCurrentStatusRequestInterface): Promise<CardCurrentStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/status`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardCurrentStatusResponseInterface>(url.toString());
    // @ts-ignore
    return new CardCurrentStatusResponseModel(response.data);
  }

  public async getTokenListAsync (params: CardTokenListRequestInterface): Promise<CardTokenListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tokens`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardTokenListResponseInterface>(url.toString());
    // @ts-ignore
    return new CardTokenListResponseModel(response.data.rows);
  }

  public async getTokenLastStatusAsync (params: CardTokenLastStatusRequestInterface): Promise<CardTokenLastStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tokens/laststatus`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardTokenLastStatusResponseInterface>(url.toString());
    // @ts-ignore
    return new CardTokenLastStatusResponseModel(response.data);
  }

  public async deleteTokenAsync (params: CardTokenDeleteRequestInterface): Promise<CardTokenActionResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tokens/deletetoken`);
    const response = await baseApi.post<CardTokenActionResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardTokenActionResponseModel(response.data);
  }

  public async activateTokenAsync (params: CardTokenActivateRequestInterface): Promise<CardTokenActionResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tokens/activatetoken`);
    const response = await baseApi.post<CardTokenActionResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardTokenActionResponseModel(response.data);
  }

  public async sendTariffEmailAsync (params: CardTariffSendingRequestInterface): Promise<TariffSendingResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/send/email`);
    const response = await baseApi.post<TariffSendingResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new TariffSendingResponseModel(response.data);
  }

  public async sendTariffViberAsync (params: CardTariffSendingRequestInterface): Promise<TariffSendingResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/tariff/send/viber`);
    const response = await baseApi.post<TariffSendingResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new TariffSendingResponseModel(response.data);
  }

  public async changeRiskAsync (params: CardLimitRiskChangeRequestInterface): Promise<CardLimitRiskChangeResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/limit/changeRisk`);
    const response = await baseApi.post<CardLimitRiskChangeResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardLimitRiskChangeResponseModel(response.data);
  }

  public async getCardImageAsync (params: CardImageRequestInterface): Promise<CardImageResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/shirt/frontUrl`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardImageResponseInterface>(url.toString());
    // @ts-ignore
    return new CardImageResponseModel(response.data);
  }

  public async getDebtToBankAsync (params: CardDebtToBankRequestInterface): Promise<CardDebtToBankResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/debt-to-bank`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<CardDebtToBankResponseInterface>(url.toString());
    // @ts-ignore
    return new CardDebtToBankResponseModel(response.data);
  }

  public async unlockingFailedAsync (params: CardUnlockingFailedRequestInterface): Promise<CardUnlockingFailedStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/unlocking/notSuccess/finish`);
    // @ts-ignore
    const response = await baseApi.post<CardUnlockingFailedStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardUnlockingFailedStatusResponseModel(response.data);
  }

  public async unlockingAsync (params: CardUnlockingRequestInterface): Promise<CardUnlockingStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/unlocking/on`);
    // @ts-ignore
    const response = await baseApi.post<CardUnlockingStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardUnlockingStatusResponseModel(response.data);
  }

  public async unlockingLockAsync (params: CardUnlockingLockRequestInterface): Promise<CardUnlockingLockStatusResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/unlocking/off`);
    // @ts-ignore
    const response = await baseApi.post<CardUnlockingLockStatusResponseInterface>(url.toString(), params);
    // @ts-ignore
    return new CardUnlockingLockStatusResponseModel(response.data);
  }
}
export const cardApi = new CardApi(constants.api.card_host);
