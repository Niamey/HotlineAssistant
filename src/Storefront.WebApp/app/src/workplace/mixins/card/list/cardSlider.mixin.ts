import { Vue } from 'vue-property-decorator';
import Component from 'vue-class-component';
import { cardListMapper } from '../../../store/modules/cardList.module';

const Mapper = Vue.extend({
  computed: cardListMapper.mapGetters(['CardsForSlider'])
});

@Component
export default class CardSliderMixin extends Mapper {
  public getCardsForSlider() {
    return this.CardsForSlider.items;
  }
}
