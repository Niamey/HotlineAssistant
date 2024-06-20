import { MenuLeftResponseInterface } from '../../../interfaces/responses/menu';
import { MenuLeftDto } from '../../../dto/menu';
import MenuLeftModel from '../../menu/menuLeft.model';
import { MenuLeftInterface } from '../../../interfaces/menu/menuLeft.interface';

export class MenuLeftResponseModel implements MenuLeftResponseInterface {
  constructor (items: Array<MenuLeftInterface>) {
    this.rows = items.map((menuLeftDto: MenuLeftDto) => new MenuLeftModel(menuLeftDto));
  }

  public rows: Array<MenuLeftModel>;
}
