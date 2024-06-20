import { AgreementInterface } from './agreement.interface';

export interface IAgreementDetailTab {
  agreementDetailData: AgreementInterface | undefined |null,
  hasError: boolean,
  isLoading: boolean
}
