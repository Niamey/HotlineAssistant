class Utils {
  public stringToBoolean (value: string): boolean {
    switch (value.toLowerCase().trim()) {
      case 'true':
      case 'yes':
      case '1':
        return true;
      case 'false':
      case 'no':
      case '0':
      case null:
        return false;
      default:
        return Boolean(value);
    }
  }

  // @param value DD.MM.YYYY HH:ii:ss
  /*
  public dotedStringToDate (value: string) : Date | undefined {
    if (!value) return undefined;
    return (value.length === 19)
      ? new Date(value.replace(/^(\d{2}).(\d{2}).(\d{4}) (\d{2}):(\d{2}):(\d{2})$/, '$3-$2-$1T$4:$5:$6'))
      : new Date(value.replace(/^(\d{2}).(\d{2}).(\d{4}).*$/, '$3-$2-$1T00:00:00'));
  }
  */

  // for testing loader animation and other
  public delay (ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}

export const utils = new Utils();
