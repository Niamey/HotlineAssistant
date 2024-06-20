import { StatementInterface } from '../../../interfaces/agreement/statement/statement.interface';

export class StatementDto implements StatementInterface {
  statementFile!: Uint8Array
}
