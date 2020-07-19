<template>
  <div class="card cardCorpo">
    <div class="card-header cardHeader">{{Title}}</div>
    <div class="card-body" style="padding:0px; min-height: 235px">
      <GChart type="ColumnChart" :data="chartData" :options="chartOptions" />
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'

export default {
  name: 'ColumnChart',
  props: {
    Title: String,
    dados: Array
  },
  components: {
    GChart
  },
  data () {
    return {
      chartOptions: {
        colors: ['#F9A825']
      }
    }
  },
  computed: {
    chartData () {
      let chartData = [['Mes', 'Valor']]
      this.$props.dados.forEach(element => {
        chartData.push([element.mes, Number(element.total)])
      })
      return chartData
    }
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
</style>
