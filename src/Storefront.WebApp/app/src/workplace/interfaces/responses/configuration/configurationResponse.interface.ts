import ConfigurationModel from '../../../models/configuration/configuration.model';

export interface ConfigurationResponseInterface {
  settings: ConfigurationModel;
  versionId: string;
}
