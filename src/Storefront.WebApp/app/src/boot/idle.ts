import Vue from 'vue';
import store from '../workplace/store';
import constants from '../workplace/common/constants';
// @ts-ignore
import IdleVue from 'idle-vue';

const eventsHub = new Vue();

Vue.use(IdleVue, {
  eventEmitter: eventsHub,
  store,
  idleTime: constants.auth.idleTime,
  startAtIdle: false
});
