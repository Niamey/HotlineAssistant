import { ApiResultDto } from '../../dto/api/apiResult.dto';
import ExceptionParser from '../../utils/exception.parser';

export default class ApiResultModel<T> extends ApiResultDto<T> {
  constructor (dto: ApiResultDto<T>) {
    super();
    Object.assign(this, dto);
  }

  get errorText () {
    if (this.error) {
      const result = ExceptionParser.errorParse(this.error);
      return result.message;
    }
    return '';
  }
}
