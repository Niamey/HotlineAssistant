import { AccountTypeEnum } from '../../enums/account/accountType.enum';
import { i18n } from '../../../boot/i18n';

export class AccountTypeEnumHelper {
  public static getTypeName (type: AccountTypeEnum) : string {
    switch (type) {
      // case AccountTypeEnum.Main: return i18n.t('components.client.detail.account.clientAccountList.accountType.main').toString();
      // case AccountTypeEnum.Additional : return i18n.t('components.client.detail.account.clientAccountList.accountType.additional').toString();

      case AccountTypeEnum.Device1004: return i18n.t('components.client.detail.account.clientAccountList.accountType.device1004').toString();
      case AccountTypeEnum.Int2608: return i18n.t('components.client.detail.account.clientAccountList.accountType.int2608').toString();
      case AccountTypeEnum.Transit2920: return i18n.t('components.client.detail.account.clientAccountList.accountType.transit2920').toString();
      case AccountTypeEnum.Dispute2924: return i18n.t('components.client.detail.account.clientAccountList.accountType.dispute2924').toString();
      case AccountTypeEnum.Shortage2924: return i18n.t('components.client.detail.account.clientAccountList.accountType.shortage2924').toString();
      case AccountTypeEnum.Surplus2924: return i18n.t('components.client.detail.account.clientAccountList.accountType.surplus2924').toString();
      case AccountTypeEnum.Transit2924: return i18n.t('components.client.detail.account.clientAccountList.accountType.transit2924').toString();
      case AccountTypeEnum.Fee3570: return i18n.t('components.client.detail.account.clientAccountList.accountType.fee3570').toString();
      case AccountTypeEnum.FeeOvd3570: return i18n.t('components.client.detail.account.clientAccountList.accountType.feeOvd3570').toString();
      case AccountTypeEnum.Fee3578: return i18n.t('components.client.detail.account.clientAccountList.accountType.fee3578').toString();
      case AccountTypeEnum.FeeOvd3578: return i18n.t('components.client.detail.account.clientAccountList.accountType.feeOvd3578').toString();
      case AccountTypeEnum.Cashback3678: return i18n.t('components.client.detail.account.clientAccountList.accountType.cashback3678').toString();
      case AccountTypeEnum.Limit9129: return i18n.t('components.client.detail.account.clientAccountList.accountType.limit9129').toString();
      case AccountTypeEnum.Damping96019618: return i18n.t('components.client.detail.account.clientAccountList.accountType.damping96019618').toString();
      case AccountTypeEnum.Reserve9601: return i18n.t('components.client.detail.account.clientAccountList.accountType.reserve9601').toString();
      case AccountTypeEnum.Acc2924AdviceOffus: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc2924AdviceOffus').toString();
      case AccountTypeEnum.Acc3622Ndfl: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc3622Ndfl').toString();
      case AccountTypeEnum.Acc6050: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc6050').toString();
      case AccountTypeEnum.Acc6052: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc6052').toString();
      case AccountTypeEnum.Acc2924P2PVb: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc2924P2PVb').toString();
      case AccountTypeEnum.Acc2924Transit29: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc2924Transit29').toString();
      case AccountTypeEnum.Acc3622MilitaryFee: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc3622MilitaryFee').toString();
      case AccountTypeEnum.Acc3800: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc3800').toString();
      case AccountTypeEnum.Acc3801Eur: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc3801Eur').toString();
      case AccountTypeEnum.Acc3801Usd: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc3801Usd').toString();
      case AccountTypeEnum.Acc6397: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc6397').toString();
      case AccountTypeEnum.Acc6510: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc6510').toString();
      case AccountTypeEnum.Acc6510Cash: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc6510Cash').toString();
      case AccountTypeEnum.Acc6514: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc6514').toString();
      case AccountTypeEnum.Acc7020: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc7020').toString();
      case AccountTypeEnum.Acc7020Val: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc7020Val').toString();
      case AccountTypeEnum.Acc7040: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc7040').toString();
      case AccountTypeEnum.Acc7040Val: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc7040Val').toString();
      case AccountTypeEnum.Acc9900: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc9900').toString();
      case AccountTypeEnum.Acc9910: return i18n.t('components.client.detail.account.clientAccountList.accountType.acc9910').toString();
      case AccountTypeEnum.Over: return i18n.t('components.client.detail.account.clientAccountList.accountType.over').toString();
      case AccountTypeEnum.OverExpire2809Bpk: return i18n.t('components.client.detail.account.clientAccountList.accountType.overExpire2809Bpk').toString();
      case AccountTypeEnum.OverInt: return i18n.t('components.client.detail.account.clientAccountList.accountType.overInt').toString();
      case AccountTypeEnum.OverIntOvd: return i18n.t('components.client.detail.account.clientAccountList.accountType.overIntOvd').toString();
      case AccountTypeEnum.OverOvd: return i18n.t('components.client.detail.account.clientAccountList.accountType.overOvd').toString();
      case AccountTypeEnum.OverOvdInt: return i18n.t('components.client.detail.account.clientAccountList.accountType.overOvdInt').toString();
      case AccountTypeEnum.OverOvdIntOvd: return i18n.t('components.client.detail.account.clientAccountList.accountType.overOvdIntOvd').toString();
      case AccountTypeEnum.OverRest2809Bpk: return i18n.t('components.client.detail.account.clientAccountList.accountType.overRest2809Bpk').toString();
      case AccountTypeEnum.Sks: return i18n.t('components.client.detail.account.clientAccountList.accountType.sks').toString();
      case AccountTypeEnum.SksInt: return i18n.t('components.client.detail.account.clientAccountList.accountType.sksInt').toString();
      case AccountTypeEnum.Асс2924P2P: return i18n.t('components.client.detail.account.clientAccountList.accountType.асс2924P2P').toString();

      case AccountTypeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
