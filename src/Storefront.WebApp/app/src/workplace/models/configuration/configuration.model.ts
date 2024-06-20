import ConfigurationDto from '../../dto/configuration/configuration.dto';

export default class ConfigurationModel extends ConfigurationDto {
  constructor (dto: ConfigurationDto) {
    super();
    Object.assign(this, dto);
  }
}
