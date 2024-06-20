import { TransactionDto } from '../../dto/transaction';

export class TransactionModel extends TransactionDto {
  constructor (dto: TransactionDto) {
    super();
    Object.assign(this, dto);

    this.childs = this.childs?.map((dto:TransactionDto) => new TransactionModel(dto));
  }

  get getFullMerchantName () {
    let merchant = '';
    merchant += (this.merchant) ? this.merchant : '';
    merchant += (this.merchantName) ? `, ${this.merchantName}` : '';
    merchant += (this.merchantCity) ? `, ${this.merchantCity}` : '';
    merchant += (this.merchantState) ? `, ${this.merchantState}` : '';
    merchant += (this.merchantCountry) ? `, ${this.merchantCountry}` : '';
    return merchant;
  }

  get getTransactionInfo () {
    let info = '';
    info += (this.category) ? this.category : '';
    info += (this.class) ? `, ${this.class}` : '';
    info += (this.direction) ? `, ${this.direction}` : '';
    info += (this.directionClass) ? `, ${this.directionClass}` : '';
    return info;
  }
}
