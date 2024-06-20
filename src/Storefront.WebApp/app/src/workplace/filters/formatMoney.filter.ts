import { Vue } from 'vue-property-decorator';

// добавляем копейки + разделяем тысячи пробелами, например "1 234.00" "12 345.00" "123 456.00" "1 234 567.00"
export const FormatMoneyFilter = Vue.filter('currencyFormat', function (value:number, capacity = 2) {
  if (!value && value !== 0) return '';
  return value.toFixed(capacity).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');
});
