import { AxiosError } from 'axios';
import ResponseError from '../models/error/responseError.model';
import ValidationError from '../models/error/validationError.model';
import { BaseErrorModel } from '../models/error/baseError.model';

export default class ExceptionParser {
  public static errorParse (error: AxiosError): ResponseError {
    const base = new BaseErrorModel();
    if (error !== undefined) {
      if (error.response !== undefined) {
        base.sessionId = error.response.data?.sessionId;
        base.status = error.response.status;
        base.statusCode = error.response.statusText;
        switch (base.status) {
          case 404:
            base.message = `${error.message} Url ${error.response?.config?.url}`;
            return base;
          case 500: {
            const errorRes = new ResponseError(base);
            if (error.response.data?.exception?.exception !== undefined) {
              errorRes.message = error.response.data.exception.exception.message;
              errorRes.exception = error.response.data.exception.exception.stackTrace;
              errorRes.request = error.response.data.exception.request;
            } else {
              errorRes.message = error.response.data?.exception?.message;
              errorRes.request = error.response.data?.exception?.request;
            }
            return errorRes;
          }
          case 400: {
            base.message = error.response.data.message;
            const validationRes = new ValidationError(base);
            if (error.response.data.errors !== undefined) {
              validationRes.errors = [];
              for (let i = 0; i <= error.response.data.errors.length - 1; i++) {
                validationRes.errors.push({
                  field: error.response.data.errors[i].field,
                  message: error.response.data.errors[i].message
                });
              }
            }
            return validationRes;
          }
        }
      } else {
        base.message = error.message;
      }
    }
    return base;
  }
}
