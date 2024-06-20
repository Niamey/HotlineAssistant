import { Component } from 'vue-mixin-decorator';
import constants from '../common/constants';
import CardImageMixin from '../mixins/card/cardImage/cardImage.mixin';
import { CardImageRequestInterface } from '../interfaces/requests/card/cardImage/cardImageRequest.interface';
import { utils } from '../utils/utils';

@Component
export default class CardImageHelper extends CardImageMixin {
  public getCardImageHelper (params: CardImageRequestInterface) {
    let cardImage = this.getImageModel(params.cardCode);
    if (!cardImage) {
      this.loadCardImage(params.cardCode).then(() => {
        cardImage = this.getImageModel(params.cardCode);
        if (!cardImage) {
          cardImage = {cardCode: params.cardCode, frontUrl: constants.defaultCardShirtUrl};
          this.updateCardImageList(cardImage).then(() => {
            return cardImage?.frontUrl;
          });
        }
      });
    }
    return cardImage?.frontUrl;
  }

  private getImageModel(cardCode: string) {
    return this.CardImageList.find(x => x.cardCode === cardCode)
  }
}
