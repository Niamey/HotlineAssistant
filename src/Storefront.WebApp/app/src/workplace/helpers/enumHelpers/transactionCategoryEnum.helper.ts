import { CategoryEnum } from '../../enums/transaction/category.enum';
import { i18n } from '../../../boot/i18n';

export class TransactionCategoryEnumHelper {
  public static getCategoryName (category: CategoryEnum) : string {
    switch (category) {
      case CategoryEnum.Advice : return i18n.t('enums.transaction.сategoryEnum.advice').toString();
      case CategoryEnum.Request: return i18n.t('enums.transaction.сategoryEnum.request').toString();
      case CategoryEnum.Notification : return i18n.t('enums.transaction.сategoryEnum.notification').toString();
      case CategoryEnum.Check : return i18n.t('enums.transaction.сategoryEnum.check').toString();
      case CategoryEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
