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
        ['Jan', 83719.88],
        ['Fev', 83719.88],
        ['Mar', 83719.88],
        ['Abr', 83719.88],
        ['Mai', 83719.88],
        ['Jun', 83719.88],
        ['Jul', 83719.88],
        ['Ago', 83719.88],
        ['Set', 83719.88],
        ['Out', 83719.88],
        ['Nov', 83719.88],
        ['Dez', 83719.88]
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
