import { ValidationDataType } from '../../../types';
import { BaseErrorInterface } from './baseError.interface';

export interface ValidationErrorInterface extends BaseErrorInterface {
  errors?: Array<ValidationDataType>
}
