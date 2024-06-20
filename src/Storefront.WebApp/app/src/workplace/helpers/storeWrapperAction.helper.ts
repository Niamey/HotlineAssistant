import { AxiosError } from 'axios';
import { BaseMutationInterface } from '../interfaces/store';
import { Context } from 'vuex-smart-module';
import root from '../store/root';
import ExceptionParser from '../utils/exception.parser';

export class StoreWrapperActionHelper {
  public static async runWithTry<T> (rootContext: Context<typeof root>, promise: Promise<T>, mutations: BaseMutationInterface) : Promise<void> {
    mutations.startLoading();

    await promise.catch((error: AxiosError) => {
      void rootContext.actions.addExceptions(error);

      const result = ExceptionParser.errorParse(error);
      if (result.status !== undefined) {
        switch (result.status) {
          case 400: {
            const variableErr = result;
            mutations.setValidationError(variableErr);
            break;
          }
          case 404:
            // mutations.setException(result);
            break;
          case 500: {
            mutations.setException(result);
            break;
          }
        }
      }
    }).finally(() => {
      mutations.stopLoading();
    });
  }
}
