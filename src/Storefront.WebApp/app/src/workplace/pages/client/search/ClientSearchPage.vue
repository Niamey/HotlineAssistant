<template>
  <q-page padding>
    <div class="col-md-6 offset-md-3">
      <div class="column items-center">
        <index-page-title-component class="col q-mb-xl" style="padding-top: 2em;"/>
        <client-search-component class="col" v-bind:bordered="true" />
       <service-list-component class="col" />
      </div>
    </div>
  </q-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import ServiceListComponent from '../../../components/shared/serviceList.component.vue';
import IndexPageTitleComponent from '../../../components/shared/indexPageTitle.component.vue';
import ClientSearchComponent from '../../../components/client/search/clientSearch.component.vue';

@Component({
  components: { ServiceListComponent, IndexPageTitleComponent, ClientSearchComponent }
})
export default class ClientSearchPage extends Vue {
}
</script>
