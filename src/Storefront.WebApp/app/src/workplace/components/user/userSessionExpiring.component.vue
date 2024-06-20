<template>
  <q-dialog ref="dialog" v-model="isShowDialog" @hide="hideDialog()" persistent>
    <q-card style="width: 612px">
      <q-card-section class="row items-center q-px-lg q-pt-lg q-pb-none">
        <div class="title text-weight-medium">{{ $t('components.authorization.logout.sessionPaused') }}</div>
        <q-space />
      </q-card-section>

      <q-card-section class="q-px-lg">
        <div class="text-body1">{{ $t('components.authorization.logout.youWillBeLogout') }} <b>{{ getTime }}</b>. {{ $t('components.authorization.logout.clickContinueForStayInSystem') }}</div>
        <q-btn class="float-left q-my-lg"
                    @click="closePopup"
                    color="primary"
                    :label="$t('components.authorization.logout.continue')"
                    no-caps
                  />
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { QDialog } from 'quasar';
import { UserModule } from '../../store/modules/user.module';
import { UserSessionTimerHelper } from '../../helpers/userSessionTimer.helper';
import constants from '../../common/constants';

@Component
export default class UserSessionExpiringComponent extends Vue {
  @Prop({ default: false }) isShow?: boolean;
  private isShowDialog: boolean;
  private time: number;
  private timerId: any;

  $refs!: {
    dialog: QDialog;
  }

  constructor () {
    super();
    this.isShowDialog = this.isShow || false;
    this.time = constants.auth.idleWarningTime;
  }

  @Watch('isShow')
  private isShowChanged (value: boolean) {
    this.isShowDialog = value;
    if (value) {
      this.startTimer();
    }
  }

  private hideDialog () {
    this.$emit('hide');
  }

  private initData () {
    clearInterval(this.timerId);
    this.time = constants.auth.idleWarningTime;
  }

  closePopup () {
    this.initData();
    this.$refs.dialog.hide();
  }

  startTimer () {
    UserSessionTimerHelper.initIdleTimer();

    this.timerId = setInterval(() => {
      this.time = UserSessionTimerHelper.changeIdleTimer(1000);

      if (this.time < 1 || Date.now() - UserSessionTimerHelper.getIdleTimeStart > constants.auth.idleWarningTime) {
        void this.logout();
        this.closePopup();
      }
    }, 1000);
  }

  async logout () {
    await UserModule.SignOut();
    await this.$router.push(
      {
        name: 'login',
        params: {
          message: this.$t('components.authorization.logout.sessionClosed').toString()
        },
        query: { returnUrl: this.$router.currentRoute.fullPath }
      }
    );
  }

  get getTime () {
    const mins = ~~((this.time / 1000) / 60);
    const secs = ~~((this.time / 1000) % 60);

    let result = '';
    if (mins >= 1) {
      result += `${(mins < 10 ? `0${mins}` : `${mins}`)}:`;
    } else {
      result += '00:';
    }
    result += `${(secs < 10 ? `0${secs}` : `${secs}`)}`;
    return result;
  }
}
</script>

<style scoped>
.title {
  font-size: 18px;
}
</style>
