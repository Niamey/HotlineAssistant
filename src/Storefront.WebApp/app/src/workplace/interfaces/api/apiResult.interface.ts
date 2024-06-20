import { AxiosError } from 'axios';

export interface ApiResultInterface<T> {
  result?: T;
  hasError?: boolean;
  error?: AxiosError;
}
