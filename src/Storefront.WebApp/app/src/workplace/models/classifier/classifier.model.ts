import { ClassifierDto } from '../../dto/classifier';

export default class ClassifierModel extends ClassifierDto {
  constructor (dto: ClassifierDto) {
    super();
    Object.assign(this, dto);
  }
}