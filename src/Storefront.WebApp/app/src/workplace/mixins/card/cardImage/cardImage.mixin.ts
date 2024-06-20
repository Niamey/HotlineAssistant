import { Vue, Component } from 'vue-property-decorator';
import { cardImageMapper } from '../../../store/modules/cardImage.module';
import CardImageModel from '../../../models/card/cardImage/cardImage.model';

const Mapper = Vue.extend({
  computed: cardImageMapper.mapGetters({
    CardImageList: 'List',
    CardImageLoading: 'Loading',
    CardImageSuccess: 'Success',
    CardImageHasError: 'HasError'
  }),
  methods: cardImageMapper.mapActions({
    updateCardImageList: 'updateList',
    getCardImageDetail: 'getImage',
    startLoadingCardImageDetail: 'startLoading',
    stopLoadingCardImageDetail: 'stopLoading'
  })
});

@Component
export default class CardImageMixin extends Mapper {
  startLoadingCardImageDetail!: () => any;
  stopLoadingCardImageDetail!: () => any;

  public onCardImageListUpdate (payload : CardImageModel) {
    void this.updateCardImageList(payload);
  }

  public async loadCardImage (cardCode: string) : Promise<void> {
      await this.getCardImageDetail({ cardCode });
  }

}
