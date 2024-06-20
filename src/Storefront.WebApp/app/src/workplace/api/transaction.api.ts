import baseApi from './base.api';
import { TransactionListResponseModel, TransactionListFinancialResponseModel, TransactionReversalResponseModel } from '../models/responses/transaction';
import { TransactionListResponseInterface, TransactionListFinancialResponseInterface, TransactionReversalResponseInterface } from '../interfaces/responses/transaction';
import {
  TransactionListRequestInterface,
  TransactionListFinancialBlockingRequestInterface,
  TransactionListFinancialUnblockingRequestInterface,
  TransactionReversalRequestInterface
} from '../interfaces/requests/transaction';
import constants from '../common/constants';
import CoreApi from './core.api';

class TransactionApi extends CoreApi {
  // eslint-disable-next-line no-useless-constructor
  constructor (host: string) {
    super(host);
  }

  public async getTransactionListAsync (params: TransactionListRequestInterface): Promise<TransactionListResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TransactionListResponseInterface>(url.toString());
    return new TransactionListResponseModel(response.data.rows, response.data.isNextPageAvailable);
  }

  public async getTransactionListFinancialForBlockingAsync (params: TransactionListFinancialBlockingRequestInterface): Promise<TransactionListFinancialResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list/financial/forBlocking`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TransactionListFinancialResponseInterface>(url.toString());
    return new TransactionListFinancialResponseModel(response.data.rows, response.data.isNextPageAvailable);
  }

  public async getTransactionListFinancialForUnBlockingAsync (params: TransactionListFinancialUnblockingRequestInterface): Promise<TransactionListFinancialResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/list/financial/forUnBlocking`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TransactionListFinancialResponseInterface>(url.toString());
    return new TransactionListFinancialResponseModel(response.data.rows, response.data.isNextPageAvailable);
  }

  public async getTransactionReversalAsync (params: TransactionReversalRequestInterface): Promise<TransactionReversalResponseModel> {
    await this.loadConfig();
    const url = new URL(`${this.apiHostAddress}/reversal/list`);
    // @ts-ignore
    url.search = new URLSearchParams(params).toString();
    const response = await baseApi.get<TransactionReversalResponseInterface>(url.toString());
    // @ts-ignore
    return new TransactionReversalResponseModel(response.data);
  }
}

export const transactionApi = new TransactionApi(constants.api.transaction_host);
