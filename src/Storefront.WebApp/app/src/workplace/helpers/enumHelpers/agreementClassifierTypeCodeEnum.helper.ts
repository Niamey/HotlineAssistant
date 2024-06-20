import { AgreementClassifierTypeCodeEnum } from '../../enums/agreement/agreementClassifierTypeCode.enum';
import { i18n } from '../../../boot/i18n';

export class AgreementClassifierTypeCodeEnumHelper {
  public static getTypeCodeName (type: AgreementClassifierTypeCodeEnum) : string {
    switch (type) {
      case AgreementClassifierTypeCodeEnum.InactiveAgreement: return i18n.t('enums.agreementCategoryTypeCodeEnum.inactiveAgreement').toString();
      case AgreementClassifierTypeCodeEnum.BlockLimitArrest: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockLimitArrest').toString();
      case AgreementClassifierTypeCodeEnum.BlockFullArrest: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockFullArrest').toString();
      case AgreementClassifierTypeCodeEnum.BlockFmDebit: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockFmDebit').toString();
      case AgreementClassifierTypeCodeEnum.BlockFmCredit: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockFmCredit').toString();
      case AgreementClassifierTypeCodeEnum.BlockFmFull: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockFmFull').toString();
      case AgreementClassifierTypeCodeEnum.BlockFmIdentification: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockFmIdentification').toString();
      case AgreementClassifierTypeCodeEnum.BlockDecisionBank: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockDecisionBank').toString();
      case AgreementClassifierTypeCodeEnum.BlockGrodi: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockGrodi').toString();
      case AgreementClassifierTypeCodeEnum.BlockGna: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockGna').toString();
      case AgreementClassifierTypeCodeEnum.BlockTerrorist: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockTerrorist').toString();
      case AgreementClassifierTypeCodeEnum.BlockPep: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockPep').toString();
      case AgreementClassifierTypeCodeEnum.BlockDeathClient: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockDeathClient').toString();
      case AgreementClassifierTypeCodeEnum.BlockOverdueLoan: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockOverdueLoan').toString();
      case AgreementClassifierTypeCodeEnum.BlockCustom: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockCustom').toString();
      case AgreementClassifierTypeCodeEnum.BlockResidentStatusChange: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockResidentStatusChange').toString();
      case AgreementClassifierTypeCodeEnum.BlockClientMissinLists: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockClientMissinLists').toString();
      case AgreementClassifierTypeCodeEnum.BlockInvalidPassport: return i18n.t('enums.agreementCategoryTypeCodeEnum.blockInvalidPassport').toString();
      case AgreementClassifierTypeCodeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
