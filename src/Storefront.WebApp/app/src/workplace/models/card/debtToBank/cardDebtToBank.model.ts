import { CardDebtToBankDto } from '../../../dto/card/debtToBank/cardDebtToBank.dto';

export class CardDebtToBankModel {
  constructor (dto: CardDebtToBankDto) {
    Object.assign(this, dto);
  }
}