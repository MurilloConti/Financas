<template>
  <div class="card cardCorpo">
    <div class="card-header cardHeader">{{Title}}</div>
    <div class="card-body" style="padding:0px; min-height: 235px">
      <GChart type="PieChart" :data="chartData" :options="chartOptions" />
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'
export default {
  name: 'PieChart',
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
        pieHole: 0.38
      }
    }
  },
  computed: {
    chartData () {
      let chartData = [['Fundo', 'Valor']]
      this.$props.dados.forEach(element => {
        chartData.push([element.Name, Number((element.Qtd * element.Price))])
      })
      return chartData
    }
  }
}
</script>
<style scoped>
</style>
