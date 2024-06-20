import { PeriodTypeEnum } from '../../../enums/shared/periodType.enum';
import { LimitTypeEnum } from '../../../enums/card/limits/limitType.enum';

export interface AmountCardLimitInterface {
    typeLimit: LimitTypeEnum;
		periodTypeLimit: PeriodTypeEnum;
		limit?: number;
		used?: number;
		currencyCode?: string;
		currencyName?: string;
}