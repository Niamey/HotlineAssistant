import { LocalizationEnum } from '../../enums/localization/localization.enum';
import { MenuLeftInterface } from '../../interfaces/menu/menuLeft.interface';
import { MenuItemPositionEnum } from '../../enums/sidebarMenuItemPosition.enum';

export class MenuLeftDto implements MenuLeftInterface {
  id!: number;
  url?: string;
  iconUrl?: string;
  defaultModuleImage?: string;
  moduleName?: string;
  localization!: LocalizationEnum;
  align!: MenuItemPositionEnum;
}
