import { CardBlockingStatusResponseInterface } from './cardBlockingStatusResponse.interface';

export interface CardBlockingResponseInterface {
  status: CardBlockingStatusResponseInterface;
  operationStatuses?: Array<CardBlockingStatusResponseInterface>;
  resultForOperator: string;
}
