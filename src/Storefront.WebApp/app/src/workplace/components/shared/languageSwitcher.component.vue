<template>
  <q-select
    v-model="lang"
    :options="langOptions"
    :label="$t('languageSwitcher.label')"
    dense
    borderless
    emit-value
    map-options
    options-dense
    style="min-width: 150px"
    @input="onSelect"
  />
</template>

<script lang="ts">
import { SessionStorage } from 'quasar';
import { Component, Vue } from 'vue-property-decorator';
const Watch = Vue.extend({
  watch: {
    lang (lang) {
      this.$i18n.locale = lang;
    }
  }
});

@Component
export default class LanguageSwitcher extends Watch {
  private readonly lang: any;
  private langOptions: any[];

  constructor () {
    super();
    this.lang = this.$i18n.locale;

    this.langOptions = [
      { value: 'uk', label: 'Українська' },
      { value: 'ru', label: 'Русский' }
    ];
  }

  onSelect () {
    SessionStorage.set('CurrentLang', this.lang);
  }
}
</script>
