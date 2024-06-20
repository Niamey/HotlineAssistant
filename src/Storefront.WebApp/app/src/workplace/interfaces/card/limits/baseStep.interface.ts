export interface BaseStepInterface {
  validateStep () : string | string[] | undefined;
  collectData () : void;
}
