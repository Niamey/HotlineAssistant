import { SegmentTypeEnum } from '../../enums/client/segmentType.enum';
import { i18n } from '../../../boot/i18n';

export class SegmentTypeEnumHelper {
  public static getTypeName (status: SegmentTypeEnum) : string {
    switch (status) {
      case SegmentTypeEnum.Vip : return i18n.t('enums.segmentTypeEnum.vip').toString();
      case SegmentTypeEnum.Priority: return i18n.t('enums.segmentTypeEnum.priority').toString();
      case SegmentTypeEnum.Employee: return i18n.t('enums.segmentTypeEnum.employee').toString();
      case SegmentTypeEnum.Individual: return i18n.t('enums.segmentTypeEnum.individual').toString();
      case SegmentTypeEnum.Future: return i18n.t('enums.segmentTypeEnum.future').toString();
      case SegmentTypeEnum.Lost: return i18n.t('enums.segmentTypeEnum.lost').toString();
      case SegmentTypeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
