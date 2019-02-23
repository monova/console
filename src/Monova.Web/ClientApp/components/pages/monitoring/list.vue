<template>
  <div>
    <page-head icon="chart-line" title="All Monitorings"/>
    <div class="row">
      <div v-for="(item, index) in monitorings" :key="'monitoring-' + index">
        <div class="card mr-2 ml-2 mb-4 monitor-card">
          <div class="card-header pr-3 pl-3 d-flex">
            <div class="card-title mb-0 with-elements">
              <router-link :to="{name:'monitoring-view', params: {id:item.monitorId}}">
                <icon icon="chart-line"/>
                {{item.name}}
              </router-link>
              <div class="card-title-elements ml-md-auto">
                <router-link
                  class="hover-show"
                  :to="{name:'monitoring-save', params: {id:item.monitorId}}"
                >
                  <icon icon="edit"/>Edit
                </router-link>
                <mvv-monitor-status :status="item.stepStatus" :title="item.stepStatusText"/>
              </div>
            </div>
          </div>
          <apexchart
            type="area"
            :options="item.uptimeChart"
            :series="item.uptimeChart.series"
            class="m-2 mt-4"
            height="140"
          />
          <apexchart
            type="area"
            :options="item.loadtimeChart"
            :series="item.loadtimeChart.series"
            class="m-2 mt-4"
            height="140"
          />
          <div class="card-body">
            <div class="card-subtitle text-muted">
              <a
                :href="item.url"
                target="_blank"
                class="text-muted font-weight-light d-block text-ellipsis"
              >
                <icon icon="external-link-alt"/>
                {{ item.url }}
              </a>
            </div>
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
      result.data.map(item => {
        item.loadtimeChart = this.chart(
          `${item.loadTime.toFixed(2)} ms`,
          "Load time",
          item.loadTimes
        );
        item.uptimeChart = this.chart(
          `${item.upTime.toFixed(2)} %`,
          "Uptime",
          item.upTimes
        );
        this.monitorings.push(item);
      });
    }
  },
  methods: {
    chart(title, subtitle, data) {
      return {
        chart: {
          type: "area",
          height: 160,
          sparkline: {
            enabled: true
          }
        },
        stroke: {
          curve: "straight"
        },
        fill: {
          opacity: 0.3
        },
        series: [
          {
            name: subtitle,
            data: data
          }
        ],
        yaxis: {
          min: 0
        },
        colors: ["#DCE6EC"],
        title: {
          text: title,
          offsetX: 0,
          style: {
            fontSize: "16pt",
            cssClass: "apexcharts-yaxis-title"
          }
        },
        subtitle: {
          text: subtitle,
          offsetX: 0,
          style: {
            fontSize: "10pt",
            cssClass: "apexcharts-yaxis-title"
          }
        }
      };
    },
    randomize() {
      let arg = [
        47,
        45,
        54,
        38,
        56,
        24,
        65,
        31,
        37,
        39,
        62,
        51,
        35,
        41,
        35,
        27,
        93,
        53,
        61,
        27,
        54,
        43,
        19,
        46
      ];
      var array = arg.slice();
      var currentIndex = array.length,
        temporaryValue,
        randomIndex;

      while (0 !== currentIndex) {
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
      }

      return array;
    }
  }
};
</script>