import { Vue, Component, Prop } from 'vue-property-decorator';

import { cardApi } from '../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { CardTokenListResponseInterface, CardTokenLastStatusResponseInterface } from '../../../interfaces/responses/card/token';
import { CardTokenListRequestInterface, CardTokenLastStatusRequestInterface } from '../../../interfaces/requests/card/token';
import { CardTokenLastStatusModel, CardTokenModel } from '../../../models/card/token';

import { utils } from '../../../utils/utils';

@Component
export class CardTokenListMixin extends Vue {
  @Prop() cardId!: number;

  protected tokenIsLoading: boolean;
  protected tokenHasError: boolean;
  protected tokenData: Array<CardTokenModel> = [];

  protected statusData: CardTokenLastStatusModel | null;
  protected statusIsLoading: boolean;
  protected statusHasError: boolean;

  constructor () {
    super();
    this.tokenIsLoading = false;
    this.tokenHasError = false;

    this.statusData = null;
    this.statusIsLoading = false;
    this.statusHasError = false;
  }

  async created () {
    await this.doTokenListRequest();
  }

  protected async doTokenListRequest () {
    this.tokenIsLoading = true;
    const result = await this.loadTokenList(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });
    await utils.delay(500);
    // @ts-ignore
    this.tokenHasError = result.hasError;
    // @ts-ignore
    this.tokenData = result.result?.rows;

    this.tokenIsLoading = false;
  }

  private async loadTokenList (store: Store<any>, params: CardTokenListRequestInterface) : Promise<void | ApiResultModel<CardTokenListResponseInterface>> {
    const promise = cardApi.getTokenListAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardTokenListResponseInterface>(store, promise);
  }

  protected async doTokenLastStatusRequest (tokenId: string) {
    this.statusData = null;
    this.statusHasError = false;

    this.statusIsLoading = true;
    const result = await this.loadTokenLastStatus(this.$store, {
      solarId: parseInt(this.$route.params.id),
      cardId: this.cardId,
      tokenUniqueReference: tokenId
    });
    // @ts-ignore
    this.statusHasError = result.hasError;
    if (!this.statusHasError) {
      // @ts-ignore
      this.statusData = result.result?.item;
    }
    this.statusIsLoading = false;
  }

  private async loadTokenLastStatus (store: Store<any>, params:CardTokenLastStatusRequestInterface) : Promise<void | ApiResultModel<CardTokenLastStatusResponseInterface>> {
    const promise = cardApi.getTokenLastStatusAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardTokenLastStatusResponseInterface>(store, promise);
  }
}
