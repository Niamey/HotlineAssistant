import { ServiceInterface } from '../../interfaces/service/service.interface';

export class ServiceDto implements ServiceInterface {
  id!: number;
  imageUrl!: string;
  isCustomIcon!: boolean;
  title!: string;
  description?: string;
  url!: string;
}
