import {
  CounterpartInterface,
  DocumentInterface,
  SegmentationTypeInterface
} from '../../interfaces/client';
import { ClientGenderEnum } from '../../enums/client/clientGender.enum';

export class CounterpartDto implements CounterpartInterface {
  counterpartId?: number;
  personId?: number;
  solarId?: number;
  srBankId?: number;
  financialPhone?: number;
  inn?: string = '';
  dateOfBirth?: string = '';
  birthPlace?: string = '';
  email?: string = '';
  firstName?: string = '';
  middleName?: string = '';
  lastName?: string = '';
  fullName?: string = '';
  gender?: ClientGenderEnum;
  codeWord?: string = '';
  residentCountry?: string = '';
  workPlace?: string = '';
  workPosition?: string = '';
  isOpen?: boolean = false;
  isResident?: boolean = false;
  openedOn?: string = '';
  closedOn?: string = '';
  residentialAddress?: string = '';
  registrationAddress?: string = '';
  segmentationType?: SegmentationTypeInterface = {};
  mainDocument?: DocumentInterface = {};
}
