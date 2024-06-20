<template>
  <q-dialog ref="dialog" position="standard" persistent @hide="onDialogHide">
    <q-card class="q-dialog-plugin">
      <q-card-section>
        <div class="text-h6">{{ title }}</div>
      </q-card-section>
      <q-card-section class="q-pt-none">{{ errorTitle }}</q-card-section>
      <q-card-section class="q-pt-none" v-if="this.errorCount > 0">{{this.countLabel}}{{ this.errorCount }}</q-card-section>
      <q-card-section class="q-pt-none">{{ getErrorText }}</q-card-section>
      <q-card-section v-if="this.sessionId" class="q-pt-none">{{ sessionText }}{{ this.sessionId }}</q-card-section>
      <q-card-section v-if="isEnableDetail">
        <a style="color: #0078D4;cursor: pointer;" @click="onClickDetail">{{ detailButtonLabel }}</a>
      </q-card-section>
      <q-card-section class="q-pt-none" v-show="isShowDetail">
       <q-scroll-area style="height: 250px; max-width: 455px;">{{ this.errorDetail }}</q-scroll-area>
      </q-card-section>
      <q-card-actions align="left">
        <q-btn outline :label="buttonSendLabel" color="primary" @click="onOKClick" />
        <q-btn flat :label="buttonCancelLabel" color="gray" @click="onCancelClick" />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { i18n } from 'src/boot/i18n';
import { QDialog } from 'quasar';

@Component
export default class ErrorPopupComponent extends Vue {
  @Prop(String) sessionId?: string;
  @Prop(String) errorText?: string;
  @Prop(String) errorDetail?: string;
  @Prop({ required: true, default: false }) isEnableDetail!: boolean;
  @Prop() errorCount?: number;
  private title: string;
  private countLabel: string;
  private sessionText: string;
  private errorTitle: string;
  private detailButtonLabel: string;
  private buttonSendLabel: string;
  private buttonCancelLabel: string;
  private isShowDetail: boolean;

  constructor () {
    super();
    this.isShowDetail = false;
    this.title = i18n.t('components.shared.errorPopup.title').toString();
    this.countLabel = i18n.t('components.shared.errorPopup.countLabel').toString();
    this.errorTitle = i18n.t('components.shared.errorPopup.text').toString();
    this.sessionText = i18n.t('components.shared.errorPopup.textSession').toString();
    this.detailButtonLabel = i18n.t('components.shared.errorPopup.buttons.showDetail').toString();
    this.buttonCancelLabel = i18n.t('components.shared.errorPopup.buttons.cancel').toString();
    this.buttonSendLabel = i18n.t('components.shared.errorPopup.buttons.send').toString();
  }

  private get getErrorText () {
    return this.errorText === 'RetailApiError' ? i18n.t('components.shared.errorPopup.retailApiError').toString() : this.errorText;
  }

  $refs!: {
    dialog: QDialog;
  }

  show () {
    this.$refs.dialog.show();
  }

  hide () {
    this.$refs.dialog.hide();
  }

  onOKClick () {
    this.$emit('ok');
    this.hide();
  }

  onClickDetail () {
    this.isShowDetail = !this.isShowDetail;
  }

  onCancelClick () {
    this.$emit('cancel');
    this.hide();
  }

  onDialogHide () {
    this.$emit('hide');
  }
}
</script>

<style scoped>
.card-size {
  width: 488px;
  max-height: 484px;
}
</style>
