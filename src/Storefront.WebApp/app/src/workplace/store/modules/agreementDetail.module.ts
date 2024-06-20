import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { AgreementInterface } from '../../interfaces/agreement';
import { agreementApi } from '../../api/agreement.api';
import { AgreementDetailResponseInterface } from '../../interfaces/responses/agreement/detail';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { AgreementDetailRequestInterface } from '../../interfaces/requests/agreement/agreementDetailRequest.interface';
import { ClassifierTypeEnum } from '../../enums/classifier/classifierType.enum';
import { ClassifierListRequestInterface } from '../../interfaces/requests/classifier/classifierListRequest.interface';
import { AgreementClassifierTypeCodeEnum } from '../../enums/agreement/agreementClassifierTypeCode.enum';
import { AgreementClassifierInterface } from '../../interfaces/agreement/agreementClassifier.interface';

class State extends BaseState {
  constructor () {
    super();
    this.detail = undefined;
  }

  public detail?: AgreementInterface;
}

class AgreementDetailGetters extends BaseGetters<State> {
  public get Detail () {
    return this.state.detail;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class AgreementDetailMutations extends BaseMutations<State> {
  public updateDetail (payload: AgreementInterface) {
    this.state.detail = payload;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.detail = undefined;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class AgreementDetailActions extends Actions<State, AgreementDetailGetters, AgreementDetailMutations, AgreementDetailActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public startLoading () : void {
    this.mutations.startLoading();
  }

  public stopLoading () : void {
    this.mutations.stopLoading();
  }

  public updateDetail (payload: AgreementInterface) : void {
    this.mutations.startLoading();
    if (payload) {
      if (!payload.classifiers) {
        this.dispatch('getClassifiers', { entityId: payload.agreementId }).then((resp: Array<AgreementClassifierInterface>) => {
          payload.classifiers = new Array<AgreementClassifierInterface>();
          payload.classifiers?.push(...resp);
          this.mutations.updateDetail(payload);
        });
      } else {
        this.mutations.updateDetail(payload);
      }
    } else {
      this.mutations.updateDetail(payload);
    }
    this.mutations.stopLoading();
  }

  public async getClassifiers (params: ClassifierListRequestInterface) : Promise<Array<AgreementClassifierInterface>> {
    const classifiers = new Array<AgreementClassifierInterface>();
    const result = await agreementApi.getClassifiersAsync(params);
    if (result.rows.length > 0) {
      result.rows.forEach((value) => {
        if (value.classifierName) {
          const className = Object.values(AgreementClassifierTypeCodeEnum).find(x => x === value.classifierName);
          if (className) {
            classifiers.push({ classifierName: className });
          }
        }
      });
    }
    return classifiers;
  }

  public async getDetail (params: AgreementDetailRequestInterface) : Promise<void> {
    const promise = agreementApi.getDetailAsync(params).then((resp: AgreementDetailResponseInterface) => {
      this.dispatch('getClassifiers', { entityId: params.agreementId || 0 }).then((respClassifiers: Array<AgreementClassifierInterface>) => {
        resp.item.classifiers = new Array<AgreementClassifierInterface>();
        resp.item.classifiers?.push(...respClassifiers);
      });
      this.mutations.updateDetail(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const agreementDetailStore = new Module({
  namespaced: true,
  state: State,
  getters: AgreementDetailGetters,
  mutations: AgreementDetailMutations,
  actions: AgreementDetailActions
});

export const agreementDetailMapper = createMapper(agreementDetailStore);

export default agreementDetailStore;
