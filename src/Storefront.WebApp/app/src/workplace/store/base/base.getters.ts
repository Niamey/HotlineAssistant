import { Getters } from 'vuex-smart-module';
import State from './base.state';

export default class BaseGetters<TState extends State> extends Getters<TState> {
  public get Loading () {
    return this.state.isLoading;
  }

  public get Success () {
    return this.state.isSuccess;
  }

  public get HasError () {
    return this.state.hasError;
  }

  public get Exception () {
    return this.state.exception;
  }

  public get ValidationError () {
    return this.state.validationError;
  }
}
