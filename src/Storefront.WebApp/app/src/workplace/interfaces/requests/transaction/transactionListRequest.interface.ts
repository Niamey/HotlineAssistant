export interface TransactionListRequestInterface {
  solarId: number;
  dateFrom?: string;
  dateTo?: string;
  amountFrom?: number;
  amountTo?: number;
  cardNumber?: string;
  pageIndex: number;
  pageSize: number;
}
