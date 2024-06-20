import { TransactionFinancialDto } from '../../dto/transaction';

export class TransactionFinancialModel extends TransactionFinancialDto {
  constructor (dto: TransactionFinancialDto) {
    super();
    Object.assign(this, dto);
  }

  get getFullMerchantName () {
    let merchant = '';
    merchant += (this.merchantName) ? this.merchantName : '';
    merchant += (this.merchantCity) ? `, ${this.merchantCity}` : '';
    merchant += (this.merchantState) ? `, ${this.merchantState}` : '';
    merchant += (this.merchantCountry) ? `, ${this.merchantCountry}` : '';
    return merchant;
  }
}
