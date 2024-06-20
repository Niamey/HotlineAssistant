import { FoundbByAnotherPersonReasonInterface } from '../../../interfaces/card/limits';

export class FoundbByAnotherPersonReasonDto implements FoundbByAnotherPersonReasonInterface {
  fullName?: string = '';
  phone?: string = '';
  foundThings?: boolean = false;
  comments?: string = '';
}
