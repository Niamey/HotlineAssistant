<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';

@Component
export default class BaseStepComponent extends Vue {
  @Prop() dynamicData: any;
  @Prop() travelInfo: any;
}
</script>
