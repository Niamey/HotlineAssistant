<template>
<div class="row">
   <q-list class="row inline q-mt-xl">
      <q-item style="max-width: 10.75em" v-for="service in services" :key="service.id">
        <q-item-section class="flex flex-center" >
          <q-item-section>
             <q-btn class="btn .q-btn--outline" flat round>
              <q-skeleton class="circle"  type="circle"/>
            </q-btn>
          </q-item-section>
          <q-item-section>
            <q-item-label class="q-pt-md">
              <q-skeleton class="skeletonText" type="text"/>
              <q-skeleton class="skeletonText" type="text"/>
              </q-item-label>
          </q-item-section>
        </q-item-section>
      </q-item>
    </q-list>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
@Component
export default class ServiceListSkeletonLoaderComponent extends Vue {
 private services = 4
}
</script>

<style scoped>
.skeletonText{
  width: 8em;
  padding-top: 7px;
}
.circle{
  height: 3.99em;
  width: 3.99em;
}
@media screen and (max-width: 1366px) {
    .skeletonText {
      width: 7em;
    }
  }
</style>
