import { Vue } from 'vue-property-decorator';
import { date as QuasarDate } from 'quasar'; // https://quasar.dev/quasar-utils/date-utils
import constants from '../common/constants';

export const FormatDateFilter = Vue.filter('formatDate', function (val: string, format = constants.formatting.dateVue) {
  if (!val || !format) return '';

  const date = new Date(val);
  if (!date) return '';

  return QuasarDate.formatDate(date, format);
});
