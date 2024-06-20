import { PersonDataTypeEnum } from '../../enums/modules';
import { i18n } from '../../../boot/i18n';

export class PersonDataTypeEnumHelper {
  public static getTypeName (status: PersonDataTypeEnum) : string {
    switch (status) {
      case PersonDataTypeEnum.Personal : return i18n.t('enums.personalDataTypeEnum.personal').toString();
      case PersonDataTypeEnum.Identification: return i18n.t('enums.personalDataTypeEnum.identification').toString();
      case PersonDataTypeEnum.Application: return i18n.t('enums.personalDataTypeEnum.application').toString();
      case PersonDataTypeEnum.Card: return i18n.t('enums.personalDataTypeEnum.card').toString();
      case PersonDataTypeEnum.Code: return i18n.t('enums.personalDataTypeEnum.code').toString();
      case PersonDataTypeEnum.Other: return i18n.t('enums.personalDataTypeEnum.other').toString();
      case PersonDataTypeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
