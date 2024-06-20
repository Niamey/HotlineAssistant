import { LocalStorage } from 'quasar';
import constants from '../common/constants';

export class UserSessionTimerHelper {
  private static idleTimeStart : string = 'idleTimeStart';
  private static idleTimer : string = 'idleTimer';
    
  public static initIdleTimer () : void  {
    LocalStorage.set(this.idleTimeStart, Date.now());
    LocalStorage.set(this.idleTimer, constants.auth.idleWarningTime);
  }

  public static changeIdleTimer (value: number) : number  {
    const timer : number | null = LocalStorage.getItem(this.idleTimer);
    const result = (timer || 0) - value;
    LocalStorage.set(this.idleTimer, result);
    return result;
  }

  public static get getIdleTimeStart () : number  {
    return LocalStorage.getItem(this.idleTimeStart) || 0;
  }

  public static get getIdleTimer () : number  {
    return LocalStorage.getItem(this.idleTimer) || 0;
  }
}
