<template>
  <div class="card cardCorpo shadow-sm">
    <div class="card-header cardHeader">
     <div class="row">
      <div class="col-10">
        {{Title}}
      </div>
      <div class="col-1">
        <a style="cursor:pointer"><i class="fas fa-times"></i></a>
      </div>
     </div>
    </div>
    <div class="card-body">
      <GChart
       type="AreaChart"
       :data="chartData"
       :options="chartOptions"/>
    </div>
    <div class="card-footer">
      <div class="row">
<div class="col-6 align-items-center justify-content-center"><p style="color:#424242;font-size:22px;font-weight:bold">R$: 14.70</p></div>
<div class="col-6 align-items-center justify-content-center"> <p style="color:#424242;font-size:20px;font-weight:bold">- 8.99% <i class="fas fa-long-arrow-alt-down" style="color:red"></i></p></div>
      </div>
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
