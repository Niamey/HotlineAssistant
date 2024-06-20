import { ClientGenderEnum } from '../../enums/client/clientGender.enum';
import { i18n } from '../../../boot/i18n';

export class ClientGenderEnumHelper {
  public static getGender (gender: ClientGenderEnum) : string {
    switch (gender) {
      case ClientGenderEnum.Female : return i18n.t('components.client.detail.clientRegistrationData.gender.female').toString();
      case ClientGenderEnum.Male: return i18n.t('components.client.detail.clientRegistrationData.gender.male').toString();
      default: return '';
    }
  }
}
