import { CounterpartDto } from '../../dto/client';

export default class CounterpartModel extends CounterpartDto {
  constructor (dto: CounterpartDto) {
    super();
    Object.assign(this, dto);
  }
}
