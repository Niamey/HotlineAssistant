import ClassifierModel from '../../../../models/classifier/classifier.model';

export interface ClassifierListResponseInterface {
  rows: Array<ClassifierModel>;
}