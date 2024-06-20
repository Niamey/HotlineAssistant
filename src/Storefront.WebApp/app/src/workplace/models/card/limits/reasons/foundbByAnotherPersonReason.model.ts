import { FoundbByAnotherPersonReasonDto } from '../../../../dto/card/limits';

export class FoundbByAnotherPersonReasonModel {
  constructor (dto: FoundbByAnotherPersonReasonDto | undefined = undefined) {
    if (dto !== undefined) Object.assign(this, dto);
  }
}
