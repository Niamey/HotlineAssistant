<template>
  <q-card bordered>
    <q-card-section class="justify-start text-weight-bold text-subtitle1">
      {{ $t('components.client.clientSearchAdvance.title') }}
    </q-card-section>
    <q-card-section style="padding-top:0">
      <div class="row items-center q-gutter-md">
        <div class="col-sm-12 col-md-6">
          <q-select outlined
                style="max-width: 450px"
                color="primary" dense options-dense
                :label="$t('components.client.clientSearchAdvance.options.hint')"
                :options="selectOptions"
                :option-disable="opt => Object(opt) === opt ? opt.type === 'soon' : false"
                v-model="selectValue" >

                <template v-slot:option="scope">
                    <q-item
                        v-bind="scope.itemProps"
                        v-on="scope.itemEvents">
                        <q-item-section>
                          <div class="row items-center">
                            <div>{{scope.opt.label}}</div>
                            <div class="col text-right text-green"> {{ scope.opt.type }}</div>
                          </div>
                        </q-item-section>
                      </q-item>
                </template>
          </q-select>

        </div>
        <div class="col-sm-12 col-md-2">
          <q-btn class="bg-primary"
               v-bind:disable="!isValue"
               size="16px"
               no-caps
               text-color="white"
               @click="onPrepareOptions(selectedAdvOption)">{{ $t('components.client.clientSearchAdvance.btnSearchLabel') }}</q-btn>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';
import ClientSearchMixin from '../../../mixins/search/clientSearch.mixin';

@Component
export default class ClientSearchAdvanceComponent extends mixins(ClientSearchMixin, Vue) {
  @Prop() isValue?: boolean;
  public selectValue: any = {};

  mounted () {
    this.selectValue = this.selectedAdvOption;
  }

  @Watch('selectValue')
  onPropertyChanged (value: any) {
    this.selectedAdvOption = value;
  }
}
</script>

<style scoped>

</style>
