<template>
  <div>
    <div>
      <div class="card mb-4" v-if="subscription">
        <div class="card-body">
          <h4 class="card-title">
            <strong>Free</strong>
          </h4>
          <div
            class="card-subtitle text-muted mb-3"
          >Current subscription ({{subscription.paymentPeriodText}})</div>
          <ul>
            <li
              v-for="(f, index) in subscription.features"
              :key="'feature-'+index"
            >{{f.valueUsed}}/{{f.value}} {{ f.title }}</li>
          </ul>
        </div>
      </div>
      <div class="row no-gutters row-bordered ui-bordered text-center">
        <div
          v-for="item in subscriptions"
          :key="'subscription'+item.id"
          class="d-flex col-md flex-column py-4"
        >
          <div class="display-1 text-primary my-4">
            <i class="lnr lnr-briefcase text-big"></i>
          </div>
          <h5 class="m-0">{{ item.title }}</h5>
          <div class="flex-grow-1">
            <div class="py-4 my-2">
              <span class="d-inline-block text-muted text-big align-middle mr-2">$</span>
              <span class="display-3 d-inline-block font-weight-bold align-middle">{{ item.price }}</span>
              <span class="d-inline-block text-muted text-big align-middle ml-2">/ mo</span>
            </div>
            <div class="pb-5">
              <p
                v-for="feature in item.features"
                :key="item.id+'-'+feature.name"
                class="ui-company-text mb-2"
              >{{feature.value}} {{feature.title}}</p>
            </div>
          </div>
          <div class="px-md-3 px-lg-5">
            <button @click="subscribe(item.id)" class="btn btn-outline-success">Upgrade</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import service from "service/subscription";

export default {
  data() {
    return {
      subscription: null,
      subscriptions: []
    };
  },
  async mounted() {
    const result = await service.list();
    this.subscriptions = result.data;

    await this.current();
  },
  methods: {
    async subscribe(id) {
      const result = await service.subscribe(id);
      if (result.data) {
        await this.current();
      }
    },
    async current() {
      const current = await service.current();
      this.subscription = current.data;
    }
  }
};
</script>
