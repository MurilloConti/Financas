<template>
  <div class="card cardCorpo">
    <div class="card-header cardHeader">{{Title}}</div>
    <div class="card-body">
      <!-- <GChart type="LineChart" :data="chartData" :options="chartOptions" /> -->
       <GChart
       type="AreaChart"
       :data="chartData"
       :options="chartOptions"
      />
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'

export default {
  name: 'LineChart',
  props: {
    Title: String
  },
  components: {
    GChart
  },
  data () {
    return {
      chartData: [
        ['Data', 'VVAR3'],
        ['2019/2', 4.5800],
        ['2019/3', 4.2200],
        ['2019/4', 4.1000],
        ['2019/5', 4.6900],
        ['2019/6', 5.0700],
        ['2019/7', 7.7200],
        ['2019/8', 7.7300],
        ['2019/9', 7.9200],
        ['2019/10', 7.4200],
        ['2019/11', 8.8100],
        ['2019/12', 11.1700],
        ['2020/01', 14.0000],
        ['2020/02', 14.9700]
      ],
      chartOptions: {
        chart: {
          title: 'Company Performance',
          subtitle: 'Sales, Expenses, and Profit: 2014-2017'
        }
      }
    }
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
</style>
