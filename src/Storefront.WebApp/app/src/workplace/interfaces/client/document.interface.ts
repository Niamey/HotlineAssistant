import { DocumentTypeInterface } from './documentType.interface';

export interface DocumentInterface {
  counterpartDocumentId?: number;
  number?: string;
  series?: string;
  issueDate?: string;
  issueBy?: string;
  photoDate?: string;
  countryId?: number;
  expirationDate?: string;
  closingDate?: string;
  documentType?: DocumentTypeInterface;
}
