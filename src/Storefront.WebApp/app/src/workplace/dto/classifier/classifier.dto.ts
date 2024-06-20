import { ClassifierInterface } from '../../interfaces/classifier/classifier.interface';

export class ClassifierDto implements ClassifierInterface {
  classifierName?: string | undefined;
}