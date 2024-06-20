<template>
  <q-dialog v-model="isShowDialog" @hide="hideDialog()">
    <q-card style="width: 450px" class="q-pa-sm">
      <q-card-section class="row items-center q-pb-none">
        <div class="title text-weight-medium col">{{ $t('components.feedback.title') }}</div>
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup text-color="grey-7"/>
      </q-card-section>
      <q-card-section>
        <div class="text-subtitle1 q-mb-md">{{$t('components.feedback.rateInterface')}}</div>
        <div class="row">
          <q-img
            :ref="'emoji_' + item.id"
            v-for="item in emojiIcons" :key="item.id"
            class="emoji-icon q-mr-md cursor-pointer"
            :src="getEmoji(item.icon)"
            @mouseover="mouseOver(item)"
            @mouseout="mouseOut()"
            @click="emojiSelect(item)">
            <q-tooltip
              transition-show="scale"
              transition-hide="rotate"
            >
              {{item.title}}
            </q-tooltip>
          </q-img>
        </div>
        <q-input v-model="comment"
                filled
                type="textarea"
                class="q-my-lg"
                :placeholder="$t('components.feedback.comment')"
        />
        <q-btn :disabled="!selectedEmoji" color="primary" size="14px" no-caps :label="$t('components.feedback.send')" :loading="isLoading" @click="sendFeedback"/>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import { UserModule } from '../../store/modules/user.module';

@Component
export default class FeedbackComponent extends Vue {
  @Prop({ default: false }) isShow?: boolean;
  private isShowDialog: boolean;
  private comment: string;
  private emojiIcons: Array<any>;
  private selectedEmoji: any;
  private isLoading = false;
  constructor () {
    super();
    this.selectedEmoji = null;
    this.isShowDialog = false;
    this.comment = '';
    this.emojiIcons = [
      {
        id: 0,
        icon: 'rage',
        title: this.$t('components.feedback.status.terribly').toString()
      },
      {
        id: 1,
        icon: 'confused',
        title: this.$t('components.feedback.status.badly').toString()
      },
      {
        id: 2,
        icon: 'neutral_face',
        title: this.$t('components.feedback.status.normally').toString()
      },
      {
        id: 3,
        icon: 'smile',
        title: this.$t('components.feedback.status.fine').toString()
      },
      {
        id: 4,
        icon: 'heart_eyes',
        title: this.$t('components.feedback.status.perfectly').toString()
      }
    ];
  }

  @Watch('isShow')
  isShowChanged (newVal: boolean) {
    this.comment = '';
    this.isShowDialog = newVal;
  }

  getEmoji (val: string) {
    return require(`../../../statics/icons/emoji/${val}.png`);
  }

  hideDialog () {
    this.$emit('hide');
  }

  mouseOver (item: any) {
    this.emojiIcons.forEach((emoji: any) => {
      if (emoji.id !== item.id) {
        const ref = this.$refs[`emoji_${emoji.id}`];
        // @ts-ignore
        ref[0].$el.classList.add('fade');
      }
    });
  }

  mouseOut () {
    this.emojiIcons.forEach((emoji: any) => {
      const ref = this.$refs[`emoji_${emoji.id}`];
      // @ts-ignore
      ref[0].$el.classList.remove('fade');
    });
  }

  emojiSelect (item: any) {
    this.selectedEmoji = item;
    this.emojiIcons.forEach((emoji: any) => {
      const ref = this.$refs[`emoji_${emoji.id}`];
      // @ts-ignore
      ref[0].$el.classList.add('grayscale');
      if (emoji.id === item.id) {
        // @ts-ignore
        ref[0].$el.classList.remove('grayscale');
      }
    });
  }

  async sendFeedback () {
    this.isLoading = true;
    const result = await UserModule.SendFeedback({
      login: UserModule.profile?.login || '',
      fullName: UserModule.profile?.fullName || '',
      position: UserModule.profile?.position || '',
      email: UserModule.profile?.email || '',
      rating: this.selectedEmoji.id,
      message: this.comment
    });

    this.isLoading = UserModule.isLoading;

    if (result.success) {
      this.$q.notify({
        type: 'positive',
        message: result.message,
        position: 'top'
      });
      this.isShowDialog = false;
    } else {
      this.$q.notify({
        type: 'negative',
        message: result.message,
        position: 'top'
      });
    }
  }
}
</script>

<style scoped>
.title {
  font-size: 18px;
}
.emoji-icon {
  width: 32px;
  height: 32px;
  transition: all 300ms;
}
.fade {
  opacity: 0.5;
}
.grayscale {
  filter: grayscale(1);
}
</style>
