<template>
  <div class="card cardCorpo">
    <div class="card-header cardHeader">{{Title}}</div>
    <div class="card-body" style="padding:0px">
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
      chartData: [
        ['Fundo', 'Valor'],
        ['Daycoval Debêntures Incentiva FIM C Priv', 83719.88],
        ['LCI/DAYCOVAL 97.5%', 38191.19],
        ['LCI/DAYCOVAL 100%', 9291.02],
        ['Constância Fundamento FIA', 10181.65],
        ['Daycoval Ibovespa Ativo FIA', 16417.96],
        ['LCI Banco do Brasil', 0],
        [' BP Debêntures Incentiv 45 FIC FIM C Priv', 0]
      ],
      chartOptions: {
        pieHole: 0.38
      }
    }
  }
}
</script>
<style scoped>
</style>
