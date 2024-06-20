<template>
  <div>
    <q-item v-if="profile" clickable v-ripple @click="onShowPanel()">
        <q-item-section side>
          <q-avatar class="bg-white" size="40px">
            <img :src="getAvatar" />
            <q-badge class="bdg-status" floating color="transparent" text-color="transparent" align="bottom">
              <div class="bg-green bdg-status-lens" />
            </q-badge>
          </q-avatar>

        </q-item-section>
        <q-item-section no-wrap>
          <div class="row no-wrap">
            <div class="col-auto text-body1">{{profile.fullName}}</div>
            <div class="col-1 q-ml-xs">
                <template>
                  <q-icon v-if="showPanel" name="arrow_drop_up" size="24px" />
                  <q-icon v-else name="arrow_drop_down" size="24px" />
                </template>
            </div>
          </div>
        </q-item-section>
        <q-popup-proxy ref="qpProxy" :offset="[0, 12]" @hide="showPanel=false">
          <q-card>
            <q-card-section class="row items-center q-px-none q-py-none">
              <q-item class="q-my-sm">
                  <q-item-section side>
                    <q-avatar class="bg-grey-3" size="40px">
                      <img :src="getAvatar" />
                    </q-avatar>
                  </q-item-section>
                  <q-item-section no-wrap>
                      <q-item-label class="col text-body2">{{profile.fullName}}</q-item-label>
                      <q-item-label class="col text-caption text-grey-7">{{profile.email}}</q-item-label>
                  </q-item-section>
              </q-item>
            </q-card-section>
            <!--<q-card-section class="row items-center q-px-none q-py-none">
              <div class="row status rounded-borders q-mx-md q-py-xs q-px-sm q-mb-md">
                    <div class="text-body2 col-auto">{{ $t('components.userProfile.status') }}:</div>
                    <div class="text-body2 text-grey-7 col"><q-icon name="lens" class="text-green q-ml-sm q-mr-xs" size="8px"/>готовий</div>
              </div>
            </q-card-section>-->
            <q-card-section clickable @click="sendFeedback" v-ripple class="row items-center-drop q-px-none q-py-none q-hoverable" >
              <div class="row q-mx-md q-py-xs q-px-sm q-my-sm">
                    <div class="text-body2 col-auto">{{ $t('components.userProfile.sendFeedback') }}</div>
              </div>
            </q-card-section>
            <!--<q-card-section class="row items-center-drop q-px-none q-py-none">
              <div class="row q-mx-md q-py-xs q-px-sm q-my-sm">
                    <div class="text-body2 col-auto">{{ $t('components.userProfile.profile') }}</div>
              </div>
            </q-card-section>-->
            <q-card-section clickable @click="logout" v-ripple class="row logout items-center-drop q-px-none q-py-none q-hoverable">
              <div class="row q-mx-md q-py-xs q-px-sm q-my-sm">
                <div class="text-body2 col-auto">{{ $t('components.userProfile.logout') }}</div>
              </div>
            </q-card-section>
          </q-card>
        </q-popup-proxy>
    </q-item>
    <feedback-component :isShow="showFeedbackDialog" @hide="hideFeedbackDialog"/>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { UserProfileModel } from '../../models/user';
import { UserModule } from '../../store/modules/user.module';

import FeedbackComponent from '../../components/feedback/feedback.component.vue';

@Component({
  components: { FeedbackComponent }
})
export default class UserProfile extends Vue {
private profile?: UserProfileModel | null = null;
  private showPanel:boolean;
  private showFeedbackDialog: boolean;

  constructor () {
    super();
    this.showPanel = false;
    this.showFeedbackDialog = false;
  }

  mounted () {
    this.profile = UserModule.profile;
  }

  onShowPanel () {
    this.showPanel = !this.showPanel;
  }

  get getAvatar () {
    if (this.profile?.avatarBase64) {
      return `data:image/png;base64,${this.profile?.avatarBase64}`;
    } else {
      return require('../../../assets/images/avatars/male.png') || '';
    }
  }

  async logout () {
    await UserModule.SignOut();
    await this.$router.push('/login');
  }

  sendFeedback () {
    this.showFeedbackDialog = true;
    this.$nextTick(function () {
      this.showPanel = false;
      // @ts-ignore
      this.$refs.qpProxy.hide();
    });
  }

  hideFeedbackDialog () {
    this.showFeedbackDialog = false;
  }
}
</script>

<style scoped>
.logout {
  border-top: 1px solid #E9E9EA;
}
.items-center-drop:hover {
  background-color: #F4F4F4;
  cursor: pointer;
}
.status {
  width: 100%;
  border: 1px solid #A8A9AA;
}
.bdg-status {
  top: auto;
  bottom: -2px;
  right: -7px;
}
.bdg-status-lens {
  border: 2px solid #ffffff;
  border-radius: 10px;
  height: 10px;
  width: 10px;
}
</style>
