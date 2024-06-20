import { AgreementInterface } from '../../interfaces/agreement';

export class AgreementPageModel {
  constructor () {
    this.filter = { isDebt: false, isActive: false };
    this.pageIndex = 1;
    this.pageSize = 10;
    this.total = 10;
  }

  public items: Array<AgreementInterface> = [];
  public filter: { isDebt: boolean; isActive: boolean };
  public pageIndex: number;
  public pageSize: number;
  public total: number;
}
