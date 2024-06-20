import { AxiosError } from 'axios';
import { Store } from 'vuex';
import ApiResultModel from '../models/api/apiResult.model';

export class ApiWrapperActionHelper {
  public static async runWithTry<T> (rootContext: Store<any>, promise: Promise<T>) : Promise<void | ApiResultModel<T>> {
    let result;
    await promise.then((response: T) => {
      result = new ApiResultModel<T>({ result: response });
    }).catch((error: AxiosError) => {
      result = new ApiResultModel<T>({ hasError: true, error: error });
      void rootContext.dispatch('addExceptions', error);
    });
    return result;
  }
}
