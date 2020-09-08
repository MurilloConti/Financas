<template>
  <b-card no-body tag="article" class="mb-2">
    <b-card-header class="cardHeader">
      {{Title}}
    </b-card-header>
    <b-card-body>
        <GChart type="PieChart" :data="chartData" :options="chartOptions" />
    </b-card-body>
  </b-card>
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
      if (this.$props.dados) {
        this.$props.dados.forEach(element => {
          chartData.push([element.Name, Number((element.Qtd * element.Price))])
        })
      }
      return chartData
    }
  }
}
</script>
<style scoped>
</style>
