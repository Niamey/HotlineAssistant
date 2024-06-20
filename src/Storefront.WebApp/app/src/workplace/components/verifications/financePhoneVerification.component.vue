<template>
  <q-dialog ref="verify" position="center">
    <q-card>
      <q-card-section>
        <div class="text-h6">{{$t('verification.title')}}</div>
      </q-card-section>
      <q-card-section>
      </q-card-section>
      <q-card-actions align="right" class="text-primary">
        <q-btn flat :label="$t('verification.btnClose')" v-close-popup />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import CounterpartModel from '../../models/client/counterpart.model';

@Component
export default class FinancePhoneVerification extends Vue {
  private data?: CounterpartModel;
}
</script>
