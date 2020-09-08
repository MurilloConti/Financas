<template>
  <b-card no-body tag="article" class="mb-2">
    <b-card-header class="cardHeader">
      {{Title}}
    </b-card-header>
    <b-card-body>
      <GChart type="ColumnChart" :data="chartData" :options="chartOptions" />
    </b-card-body>
  </b-card>
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
      if (this.$props.dados) {
        this.$props.dados.forEach(element => {
          chartData.push([element.mes, Number(element.total)])
        })
      }
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
