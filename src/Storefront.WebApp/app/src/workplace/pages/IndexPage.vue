<template>
  <q-page padding>
    <div class="col-md-6 offset-md-3">
      <div class="column items-center">
        <index-page-title-component class="col q-mb-xl" style="padding-top: 2em;"/>
        <client-search-component class="col" v-bind:bordered="true" />
        <service-list-component class="col" v-bind:listData="this.ServiceList"
                                      v-bind:hasError="this.ServiceListHasError"
                                      v-bind:isLoading="this.ServiceListLoading"/>
      </div>
    </div>
  </q-page>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';

import ServiceListComponent from '../components/shared/serviceList.component.vue';
import IndexPageTitleComponent from '../components/shared/indexPageTitle.component.vue';
import ClientSearchComponent from '../components/client/search/clientSearch.component.vue';
import ServiceListMixin from './../mixins/service/serviceList.mixin';

@Component({
  components: { ServiceListComponent, IndexPageTitleComponent, ClientSearchComponent }
})

export default class Index extends mixins(ServiceListMixin) {
  async created () {
    await this.getServicesList(4);
  }
}
</script>

<style scoped>
.txt-gray {
  color: #26272B;
}
.txt-orange {
  color: #f47c20;
}
</style>
