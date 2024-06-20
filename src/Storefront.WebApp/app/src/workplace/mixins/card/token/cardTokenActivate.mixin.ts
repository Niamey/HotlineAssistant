import { Vue, Component, Prop } from 'vue-property-decorator';

import { CardTokenActionModel } from '../../../models/card/token';
import { CardTokenInterface } from '../../../interfaces/card/token';

import { cardApi } from '../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { CardTokenActionResponseInterface } from '../../../interfaces/responses/card/token';
import { CardTokenActivateRequestInterface } from '../../../interfaces/requests/card/token';

import { CardTokenStatusEnum } from '../../../enums/card/token';
// import { utils } from '../../../utils/utils';

@Component
export class CardTokenActivateMixin extends Vue {
  @Prop() cardId!: number;

  protected tokenToActivate: CardTokenInterface | null;

  protected actionIsLoading: boolean;
  protected actionHasError: boolean;
  protected actionResult?: CardTokenActionModel;

  constructor () {
    super();
    this.actionIsLoading = false;
    this.actionHasError = false;
    this.tokenToActivate = null;
  }

  protected async doActivateRequest () {
    if (this.tokenToActivate) {
      this.actionIsLoading = true;

      const result = await this.activateToken(this.$store, {
        solarId: parseInt(this.$route.params.id),
        cardId: this.cardId,
        tokenUniqueReference: this.tokenToActivate?.tokenId ?? ''
      });

      // await utils.delay(2000);

      // @ts-ignore
      this.actionHasError = result.hasError;

      // @ts-ignore
      if (!this.actionHasError) {
        // @ts-ignore
        this.actionResult = result?.result?.item;

        // @ts-ignore
        if (this.actionResult.success) {
          this.tokenToActivate.tokenStatus = CardTokenStatusEnum.Active;

          this.$q.notify({
            type: 'positive',
            message: this.$t('components.client.detail.cards.tokens.clientCardTokenList.actionResult.positive').toString(),
            position: 'top',
            timeout: 2500
          });
        } else {
          // @ts-ignore
          const msg = this.actionResult?.message || '';
          this.$q.notify({
            type: 'negative',
            message: `${this.$t('components.client.detail.cards.tokens.clientCardTokenList.actionResult.negative').toString()} ${msg}`,
            position: 'top',
            timeout: 2500
          });
        }
      }
      this.actionIsLoading = false;
    }
    this.tokenToActivate = null;
  }

  private async activateToken (store: Store<any>, params: CardTokenActivateRequestInterface) : Promise<void | ApiResultModel<CardTokenActionResponseInterface>> {
    const promise = cardApi.activateTokenAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardTokenActionResponseInterface>(store, promise);
  }
}
