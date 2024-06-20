import { ExceptionType } from '../../../types';
import { BaseErrorInterface } from './baseError.interface';

export interface ResponseErrorInterface extends BaseErrorInterface {
  exception?: ExceptionType;
}
