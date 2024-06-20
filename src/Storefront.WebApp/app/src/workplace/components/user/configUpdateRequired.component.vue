<template>
  <q-dialog ref="dialog" v-model="isShowDialog" @hide="hideDialog()" persistent>
    <q-card style="width: 612px">
      <q-card-section class="row items-center q-px-lg q-pt-lg q-pb-none">
        <div class="title text-weight-medium">{{ $t('components.authorization.update.newVersionAvailable') }}</div>
        <q-space />
      </q-card-section>

      <q-card-section class="q-px-lg">
        <div v-if="this.isLoading">
          <block-spinner-component :loaded="this.isLoading" />
        </div>
        <template v-else>
          <div class="text-body1">{{ $t('components.authorization.update.newVersionInfoText') }}</div>
          <q-btn class="float-left q-my-lg"
                      @click="closePopup"
                      color="primary"
                      :label="$t('components.authorization.update.continue')"
                      no-caps
                    />
        </template>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { QDialog } from 'quasar';
import { domainApi } from '../../api/domain.api';
import { LocalizationEnum } from '../../enums/localization/localization.enum';

@Component
export default class ConfigUpdateRequiredComponent extends Vue {
  @Prop({ default: false }) isShow?: boolean;
  private isShowDialog: boolean;
  private isLoading = false;

  $refs!: {
    dialog: QDialog;
  }

  constructor () {
    super();
    this.isShowDialog = this.isShow || false;
  }

  async created () {
    this.isLoading = true;
    await domainApi.getDomainConfigurationAsync({ localization: LocalizationEnum.Ukraine });
    this.isLoading = false;
  }

  @Watch('isShow')
  private isShowChanged (value: boolean) {
    this.isShowDialog = value;
  }

  private hideDialog () {
    this.$emit('hide');
  }

  closePopup () {
    document.location.reload();
    this.$refs.dialog.hide();
  }
}
</script>

<style scoped>
.title {
  font-size: 18px;
}
</style>
