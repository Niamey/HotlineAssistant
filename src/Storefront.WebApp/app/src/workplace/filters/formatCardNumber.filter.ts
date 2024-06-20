import { Vue } from 'vue-property-decorator';

export const FormatCardNumberFilter = Vue.filter('formatCardNumber', function (val:string, mask = true) {
  if (!val) return '';
  let regex;
  switch (val.length) {
    case 16:
      regex = /^(\d{4})(\d{2})(\d{2})(\d+)(\d{4})$/gi;
      return val.replace(regex, mask ? '$1 $2•• •••• $5' : '$1 $2$3 $4 $5');
    case 18: // PriorityPass
      regex = /^(\d{6})(\d+)(\d{4})(\d{4})$/gi;
      return val.replace(regex, mask ? '$1 •••• ••••$4' : '$1 $2 $3$4');
    default: return val;
  }
});
