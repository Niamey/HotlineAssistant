import { TravelStatusEnum } from '../../enums/travel/travelStatus.enum';
import { i18n } from '../../../boot/i18n';

export class TravelStatusEnumHelper {
  public static getStatusName (status: TravelStatusEnum) : string {
    switch (status) {
      case TravelStatusEnum.Active: return i18n.t('enums.travelStatusEnum.active').toString();
      case TravelStatusEnum.Finished: return i18n.t('enums.travelStatusEnum.finished').toString();
      case TravelStatusEnum.Deleted: return i18n.t('enums.travelStatusEnum.deleted').toString();
      case TravelStatusEnum.Planned: return i18n.t('enums.travelStatusEnum.planned').toString();
      case TravelStatusEnum.Error: return i18n.t('enums.travelStatusEnum.error').toString();
      default: return '';
    }
  }
}
