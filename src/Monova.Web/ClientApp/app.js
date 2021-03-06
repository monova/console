import Vue from 'vue';
import axios from 'axios';
import router from './router/index';
import store from './store';
import {
  sync
} from 'vuex-router-sync';
import App from 'components/root/app-root';
import {
  FontAwesomeIcon
} from './icons';
import PageHead from 'components/shared/page-head';
import Notifications from 'vue-notification';

import BootstrapVue from 'bootstrap-vue';
Vue.use(BootstrapVue);

// Stripe
import VueStripeCheckout from 'vue-stripe-checkout';
Vue.use(VueStripeCheckout, monova.config.stripePublicKey);

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import VueContentPlaceholders from "vue-content-placeholders";

Vue.use(VueContentPlaceholders)

import VueApexCharts from "vue-apexcharts";
Vue.use(VueApexCharts);

Vue.component('apexchart', VueApexCharts);

// Input Controls
import MVIText from 'components/input/text';

// View Components
import MVVMonitorStatus from 'components/shared/monitor-status';

Vue.use(Notifications);

Vue.use(require('vue-moment'));

// Registration of global components
Vue.component('icon', FontAwesomeIcon);
Vue.component('page-head', PageHead);
Vue.component('mvv-monitor-status', MVVMonitorStatus);

// Registration for Input Controls
Vue.component('mvi-text', MVIText);

Vue.prototype.$http = axios;

sync(store, router);

const app = new Vue({
  store,
  router,
  ...App
});

export {
  app,
  router,
  store
};
