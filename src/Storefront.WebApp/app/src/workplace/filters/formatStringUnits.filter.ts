import { Vue } from 'vue-property-decorator';
import { i18n } from '../../boot/i18n';

export const FormatStringUnitsFilter = Vue.filter('stringFormat', function (value:number, unit: string) {
  if (!value && value !== 0) return '';
  const loc = i18n.t(`units.${unit}`);
  return `${value} ${loc}`;
});
