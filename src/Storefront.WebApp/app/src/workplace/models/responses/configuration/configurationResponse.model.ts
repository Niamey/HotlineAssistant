import ConfigurationModel from '../../configuration/configuration.model';
import { ConfigurationResponseInterface } from '../../../interfaces/responses/configuration/configurationResponse.interface';

export class ConfigurationResponseModel implements ConfigurationResponseInterface {
  constructor (data: ConfigurationResponseInterface) {
    this.settings = new ConfigurationModel(data.settings);
    this.versionId = data.versionId;
  }

  public settings: ConfigurationModel;
  public versionId: string;
}
