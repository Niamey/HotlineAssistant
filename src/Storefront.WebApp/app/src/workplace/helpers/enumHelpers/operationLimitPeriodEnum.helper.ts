import { PeriodTypeEnum } from '../../enums/shared/periodType.enum';
import { i18n } from '../../../boot/i18n';

export class OperationLimitPeriodEnumHelper {
  public static getResult (period: PeriodTypeEnum) : string {
    switch (period) {
      case PeriodTypeEnum.Day : return i18n.t('components.client.detail.cards.limits.cardLimits.operationsPerDay').toString();
      case PeriodTypeEnum.Week : return i18n.t('components.client.detail.cards.limits.cardLimits.operationsPerWeek').toString();
      case PeriodTypeEnum.Month : return i18n.t('components.client.detail.cards.limits.cardLimits.operationsPerMonth').toString();
      case PeriodTypeEnum.Year : return i18n.t('components.client.detail.cards.limits.cardLimits.operationsPerYear').toString();
      default: return '';
    }
  }
}
