<template>
  <div class="fixed-center text-center">
    <p>
      <img
        src="~assets/sad.svg"
        style="width:30vw;max-width:150px;">
    </p>
    <p class="text-faded">{{$t('errorPage.message')}}<strong>(404)</strong></p>
    <q-btn
      color="secondary"
      style="width:200px;"
      to="/">{{$t('errorPage.btnBack')}}</q-btn>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
@Component
export default class Error404 extends Vue {}
</script>
