import { CardFullBalanceInterface } from '../../../../interfaces/card/balance/full';

export class CardFullBalanceDto implements CardFullBalanceInterface {
currency?: string = '';
available?: number;
own?: number;
blocked?: number;
// delayed?: number; (todo check this later)
loan?: number;
overlimit?: number;
overdue?: number;
creditLimit?: number;
finBlocking?: number;
interests?: number;
penalty?: number;
minimalPayment?: number;
mandatoryPayment?: number;
unusedCreditLimit?: number;
overdraft?: number;
debt?: number;
fullOwn?: number;
fee?: number;
}
