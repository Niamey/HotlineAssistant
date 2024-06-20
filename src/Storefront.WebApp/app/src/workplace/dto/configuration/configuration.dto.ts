import { ConfigurationInterface } from '../../interfaces/configuration/configuration.interface';
import { ApiHostSettingInterface } from '../../interfaces/configuration/apiHostSetting.interface';

export default class ConfigurationDto implements ConfigurationInterface {
  public counterpartApiSetting?: ApiHostSettingInterface;
  public accountApiSetting?: ApiHostSettingInterface;
  public agreementApiSetting?: ApiHostSettingInterface;
  public cardApiSetting?: ApiHostSettingInterface;
  public userInterfaceApiSetting?: ApiHostSettingInterface;
  public transactionApiSetting?: ApiHostSettingInterface;
  public modulesApiSetting?: ApiHostSettingInterface;
  public travelApiSetting?: ApiHostSettingInterface;
  public countryApiSetting?: ApiHostSettingInterface;
  public statementApiSetting?: ApiHostSettingInterface;
}
