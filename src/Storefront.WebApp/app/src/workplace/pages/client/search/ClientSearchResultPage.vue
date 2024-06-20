<template>
  <q-page padding>
    <div class="q-pa-md row justify-center">
      <div class="col-md-8">
        <q-card class="column">
          <q-card-section class="column items-start">
            <div class="text-h5 q-mb-lg">{{$t('search.result.title')}}</div>
            <client-search-component class="justify-start" v-on:onSearchClient="onSearch" v-bind:bordered="true" />
          </q-card-section>
          <q-card-section horizontal class="column m-h q-mx-lg">
            <div class="row inline justify-between items-center">
              <div class="col-6">
                <client-search-category-component
                  v-bind:category="this.selectedCategoryName"
                  v-bind:selected="true"/>
              </div>
              <div class="col-6">
                <pager-component class="pager"
                                 v-bind:loading="this.SearchCountLoading"
                                 v-bind:page-index="this.searchCounterpartParams.pageIndex"
                                 v-bind:page-size="this.searchCounterpartParams.pageSize"
                                 v-bind:total="this.TotalRows"
                                 v-bind:is-available-next-page="this.AvailableNextPage"
                                 v-on:onNavigate="onPagerNavigate" />
              </div>
            </div>
            </q-card-section>
          <q-card-section class="column table-column">
            <client-search-result-component
                                v-bind:listData="this.Clients"
                                v-bind:hasError="this.SearchClientHasError"
                                v-bind:isLoading="this.SearchClientLoading"
                                v-bind:page-size="this.searchCounterpartParams.pageSize"/>
          </q-card-section>
        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';
import PagerComponent from '../../../components/shared/pager.component.vue';
import BlockSpinnerComponent from '../../../components/shared/blockSpinner.component.vue';
import ClientSearchComponent from '../../../components/client/search/clientSearch.component.vue';
import ClientSearchCategoryComponent from '../../../components/client/search/clientSearchCategory.component.vue';
import ClientSearchResultComponent from '../../../components/client/search/clientSearchResult.component.vue';
import ErrorPopupComponent from '../../../components/shared/errorPopup.component.vue';
import SearchCounterpart from '../../../models/search/searchCounterpart.model';
// Mixins
import RouteHelper from '../../../mixins/routing/router.mixin';
import ClientSearchMixin from '../../../mixins/search/clientSearch.mixin';
import SearchCountMixin from '../../../mixins/search/searchCount.mixin';
import { SearchCountCounterpartRequestInterface } from '../../../interfaces/requests/client/search';
import constants from '../../../common/constants';

@Component({
  components: {
    ClientSearchResultComponent,
    ClientSearchCategoryComponent,
    ClientSearchComponent,
    PagerComponent,
    BlockSpinnerComponent,
    ErrorPopupComponent
  }
})
export default class ClientSearchResultPage extends mixins(RouteHelper, SearchCountMixin, ClientSearchMixin) {
  private readonly searchCounterpartParams: SearchCounterpart;
  private readonly currentPage: number;
  private selectedCategoryName: string;

  constructor () {
    super();
    this.currentPage = 1;
    this.selectedCategoryName = '';
    this.searchCounterpartParams = new SearchCounterpart(constants.paging.pageIndex, constants.paging.pageSize);
  }

  async mounted () {
    await this.onSearch({ searchFilter: this.$route.query.filter, searchType: this.$route.query.type });
  }

  async onSearch (params: SearchCountCounterpartRequestInterface) {
    void this.startLoadingTotalRows();
    this.startLoadingClients();

    this.selectedCategoryName = this.getCategory(params.searchType);

    await this.getClientLists(params.searchFilter, params.searchType, this.currentPage);
    if (!this.SearchClientHasError || this.Clients?.length > 0) {
      await this.getTotalRows(params.searchFilter, params.searchType);
    } else {
      void this.stopLoadingTotalRows();
    }
  }

  async onPagerNavigate (data: number) {
    await this.getClientsByPage(data);
  }
}
</script>

<style scoped lang="css">
.pager {
  float: right;
}
.m-h {
  max-height: 3.5em;
}
.q-card__section--vert {
  padding: 24px 24px 16px;
}
.q-card__section--vert.table-column {
  padding: 24px 0;
}
.q-item__label,
.pager >>> .q-item__label{
  font-size: 16px;
  line-height: 24px!important;
  margin-right: 1em;
}
.col-4 >>> .row {
  float: right;
}
@media screen and (max-width: 1366px) {
  .q-item__label,
  .pager >>> .q-item__label{
    font-size: 14px;
    line-height: 20px!important;
  }
  .text-h5 {
    font-size: 1.25rem;
    line-height: 28px;
    font-weight: 500;
  }
}
</style>
