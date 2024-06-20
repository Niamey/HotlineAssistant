import { ApiResultInterface } from './../../interfaces/api/apiResult.interface';
import { AxiosError } from 'axios';

export class ApiResultDto<T> implements ApiResultInterface<T> {
  result?: T;
  hasError?: boolean = false;
  error?: AxiosError;
}
