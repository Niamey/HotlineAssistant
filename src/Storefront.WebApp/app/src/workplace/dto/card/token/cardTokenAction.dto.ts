import { CardTokenActionInterface } from '../../../interfaces/card/token';

export class CardTokenActionDto implements CardTokenActionInterface {
  public success?: boolean = false;
  public message?: string = '';
}
