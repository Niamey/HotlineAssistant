<template>
  <div class="text-h2 txt">
    <q-item-label class="txt-gray">Hotline <span class="txt-orange">Assistant</span></q-item-label>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class IndexPageTitleComponent extends Vue {}
</script>

<style scoped>
.text-h2 {
  font-size: 88px;
  line-height: 24px;
}
.txt {
  font-weight: 800;
}
.txt-gray {
  color: #26272B;
}
.txt-orange {
  color: #f47c20;
}
@media screen and (max-width: 1366px) {
  .text-h2 {
  font-size: 64px;
  line-height: 72px;
  }
}
</style>
