import {
  DocumentInterface,
  SegmentationTypeInterface
} from './index';
import { ClientGenderEnum } from '../../enums/client/clientGender.enum';

export interface CounterpartInterface {
  counterpartId?: number;
  personId?: number;
  solarId?: number;
  srBankId?: number;
  financialPhone?: number;
  inn?: string;
  dateOfBirth?: string;
  birthPlace?: string;
  email?: string;
  firstName?: string;
  middleName?: string;
  lastName?: string;
  fullName?: string;
  gender?: ClientGenderEnum;
  codeWord?: string;
  residentCountry?: string;
  workPlace?: string;
  workPosition?: string;
  isOpen?: boolean;
  isResident?: boolean;
  openedOn?: string;
  closedOn?: string;
  residentialAddress?: string;
  registrationAddress?: string;
  segmentationType?: SegmentationTypeInterface;
  mainDocument?: DocumentInterface;
}
