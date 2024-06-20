import { SessionStorage } from 'quasar';
import { domainApi } from './domain.api';
import { LocalizationEnum } from '../enums/localization/localization.enum';

export default class CoreApi {
  constructor (key: string) {
    this.key = key;
    this.apiHostAddress = null;
  }

  protected async loadConfig () {
    if (!this.isLoaded) {
      await domainApi.getDomainConfigurationAsync({ localization: LocalizationEnum.Ukraine });
    }
    this.apiHostAddress = SessionStorage.getItem(this.key) || '';
  }

  get isLoaded () {
    return !SessionStorage.isEmpty();
  }

  protected key: string;
  protected apiHostAddress: string | null;
}
