export interface GridColumnBaseInterface {
  name: string,
  align?: string,
  label?: string,
  // eslint-disable-next-line
  field?: string | Function,
  classes?: string,
  // eslint-disable-next-line
  format?: Function,
  headerStyle?: string,
}
