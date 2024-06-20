import { MenuLeftDto } from '../../dto/menu';

export default class MenuLeftModel extends MenuLeftDto {
  constructor (dto: MenuLeftDto) {
    super();
    Object.assign(this, dto);
  }
}
