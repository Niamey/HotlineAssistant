import { ClientActivityFilterTypeEnum } from '../../../enums/client/activity/ClientActivityFilterType.enum';

export interface ClientActivityFilterDataInterface {
  filterType: ClientActivityFilterTypeEnum,
  filterValueFrom?: number | string;
  filterValueTo?: number | string;
}
