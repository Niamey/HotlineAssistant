import { boot } from 'quasar/wrappers';
import Vue from 'vue';
import VueI18n from 'vue-i18n';
import messages from 'src/workplace/i18n';

declare module 'vue/types/vue' {
  interface Vue {
    i18n: VueI18n;
  }
}

Vue.use(VueI18n);

export const i18n = new VueI18n({
  locale: 'uk',
  fallbackLocale: 'ru',
  silentFallbackWarn: true,
  silentTranslationWarn: true,
  messages,
  pluralizationRules: {
    /*
    * @param choice {number} индекс выбора, переданный в $tc: `$tc('path.to.rule', choiceIndex)`
    * @param choicesLength {number} общее количество доступных вариантов
    * @returns финальный индекс для выбора соответственного варианта слова
    */
    uk: function (choice, choicesLength) {
      // this === VueI18n экземпляра, так что свойство locale также существует здесь

      if (choice === 0) {
        return 0;
      }

      const teen = choice > 10 && choice < 20;
      const endsWithOne = choice % 10 === 1;

      if (choicesLength < 4) {
        return (!teen && endsWithOne) ? 1 : 2;
      }
      if (!teen && endsWithOne) {
        return 1;
      }
      if (!teen && choice % 10 >= 2 && choice % 10 <= 4) {
        return 2;
      }

      return (choicesLength < 4) ? 2 : 3;

      // 0 вещей | количество вещей заканчивается на 1 | количество вещей заканчивается на 2-4 | количество вещей заканчивается на 5-9, 0 и числа от 11 до 19
    }
  }
});

export default boot(({ app }) => {
  // Set i18n instance on app
  app.i18n = i18n;
});
