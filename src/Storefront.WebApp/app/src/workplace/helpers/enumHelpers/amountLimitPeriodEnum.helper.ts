import { PeriodTypeEnum } from '../../enums/shared/periodType.enum';
import { i18n } from '../../../boot/i18n';

export class AmountLimitPeriodEnumHelper {
  public static getResult (period: PeriodTypeEnum) : string {
    switch (period) {
      case PeriodTypeEnum.Day : return i18n.t('components.client.detail.cards.limits.cardLimits.limitPerDay').toString();
      case PeriodTypeEnum.Week : return i18n.t('components.client.detail.cards.limits.cardLimits.limitPerWeek').toString();
      case PeriodTypeEnum.Month : return i18n.t('components.client.detail.cards.limits.cardLimits.limitPerMonth').toString();
      case PeriodTypeEnum.Year : return i18n.t('components.client.detail.cards.limits.cardLimits.limitPerYear').toString();
      default: return '';
    }
  }
}