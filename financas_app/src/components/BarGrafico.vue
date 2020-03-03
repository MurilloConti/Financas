<template>
  <div class="card cardCorpo">
    <div class="card-header cardHeader">{{Title}}</div>
    <div class="card-body">
      <GChart type="ColumnChart" :data="chartData" :options="chartOptions" />
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'

export default {
  name: 'ColumnChart',
  props: {
    Title: String
  },
  components: {
    GChart
  },
  data () {
    return {
      // Array will be automatically processed with visualization.arrayToDataTable function
      chartData: [
        ['Mes', 'Valor'],
        ['Jan', 10000.88],
        ['Fev', 20000.88],
        ['Mar', 30000.88],
        ['Abr', 40000.88],
        ['Mai', 50000.88],
        ['Jun', 60000.88],
        ['Jul', 70000.88],
        ['Ago', 80000.88],
        ['Set', 90000.88],
        ['Out', 100000.88],
        ['Nov', 110000.88],
        ['Dez', 120000.88]
      ],
      chartOptions: {
        colors: ['#F9A825']
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
