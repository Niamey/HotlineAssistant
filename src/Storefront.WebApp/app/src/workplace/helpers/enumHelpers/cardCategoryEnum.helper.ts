import { CardCategoryEnum } from '../../enums/card/cardCategory.enum';
import { i18n } from '../../../boot/i18n';

export class CardCategoryEnumHelper {
  public static getCategoryName (status: CardCategoryEnum) : string {
    switch (status) {
      case CardCategoryEnum.Primary : return i18n.t('enums.cardCategoryEnum.primary').toString();
      case CardCategoryEnum.Supplementary: return i18n.t('enums.cardCategoryEnum.supplementary').toString();
      case CardCategoryEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
