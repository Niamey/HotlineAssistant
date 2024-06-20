import { MenuItemPositionEnum } from '../../enums/sidebarMenuItemPosition.enum';
import { LocalizationEnum } from '../../enums/localization/localization.enum';
import { BaseCollectionInterface } from '../base/baseCollection.interface';

export interface MenuLeftInterface extends BaseCollectionInterface {
  id: number;
  url?: string;
  iconUrl?: string;
  defaultModuleImage?: string;
  moduleName?: string;
  localization: LocalizationEnum;
  align: MenuItemPositionEnum;
}
