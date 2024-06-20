import { ApiHostSettingInterface } from './apiHostSetting.interface';

export interface ConfigurationInterface {
  counterpartApiSetting?: ApiHostSettingInterface;
  accountApiSetting?: ApiHostSettingInterface;
  agreementApiSetting?: ApiHostSettingInterface;
  cardApiSetting?: ApiHostSettingInterface;
  transactionApiSetting?: ApiHostSettingInterface;
  userInterfaceApiSetting?: ApiHostSettingInterface;
  modulesApiSetting?: ApiHostSettingInterface;
}
