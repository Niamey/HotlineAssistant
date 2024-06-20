import ClassifierModel from '../../../classifier/classifier.model';
import { ClassifierListResponseInterface } from '../../../../interfaces/responses/classifier/list/classifierListResponse.interface';
import { ClassifierInterface } from '../../../../interfaces/classifier/classifier.interface';
import { ClassifierDto } from '../../../../dto/classifier';

export class ClassifierListResponseModel implements ClassifierListResponseInterface {
  constructor (items: Array<ClassifierInterface>) {
    this.rows = items.map((classifierDto: ClassifierDto) => new ClassifierModel(classifierDto));
  }

  public rows: Array<ClassifierModel>;
}