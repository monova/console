<template>
  <div>
    <page-head
      icon="chart-line"
      title="All Monitorings"
    />
    <div class="row">
      <div
        class="col-md-3"
        v-for="(item, index) in monitorings"
        :key="index"
      >
        <div class="card mr-4 mb-4">
          <img
            class="card-img-top shadow-sm"
            src="https://api.apiflash.com/v1/urltoimage?access_key=1607d65bfb7543e19b50d13b5ae96dd5&url=http://selcukermaya.com/tr"
            alt=""
          >
          <div class="card-body">
            <h4 class="card-title">{{ item.name }}</h4>
          </div>
          <div class="card-body">
            <router-link
              :to="{name: 'monitoring-view', params:{id:item.monitorId}}"
              class="card-link"
            >
              View Dashboard
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import service from "service/monitoring";
export default {
  data() {
    return {
      monitorings: []
    };
  },
  async mounted() {
    var result = await service.list();
    if (result.data && result.data.length) {
      this.monitorings.push(...result.data);
    }
  }
};
</script>