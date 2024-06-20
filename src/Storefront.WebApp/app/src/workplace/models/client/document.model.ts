import DocumentTypeModel from './documentType.model';
import { DocumentInterface } from '../../interfaces/client';

export default class DocumentModel implements DocumentInterface {
  counterpartDocumentId?: number;
  number?: string;
  series?: string;
  issueDate?: string;
  issueBy?: string;
  photoDate?: string;
  countryId?: number;
  expirationDate?: string;
  closingDate?: string;
  type?: DocumentTypeModel;
}
