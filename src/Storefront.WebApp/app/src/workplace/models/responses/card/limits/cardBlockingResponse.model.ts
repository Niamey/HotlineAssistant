import { CardBlockingResponseInterface } from '../../../../interfaces/responses/card/limits';
import { CardBlockingStatusResponseModel } from './cardBlockingStatusResponse.model';

export class CardBlockingResponseModel {
  constructor (data: CardBlockingResponseInterface) {
    this.status = new CardBlockingStatusResponseModel(data.status);

    this.operationStatuses = new Array<CardBlockingStatusResponseModel>();
    if (data.operationStatuses) {
      data.operationStatuses.forEach(element => {
        this.operationStatuses.push(new CardBlockingStatusResponseModel(element));
      });
    }
    this.resultForOperator = data.resultForOperator;
  }

  status: CardBlockingStatusResponseModel;
  operationStatuses: Array<CardBlockingStatusResponseModel>;
  resultForOperator: string;
}
