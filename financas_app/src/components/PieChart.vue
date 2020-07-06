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
import { store } from '@/store/index.js'
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
        ['Fundo', 'Valor']
      ],
      chartOptions: {
        pieHole: 0.38
      }
    }
  },
  methods: {
    montaGrafico: function (event) {
      let carteira = store.state.carteiraAcoes
      carteira.forEach(element => {
        this.chartData.push([element.Name, Number((element.Qtd * element.Price))])
      })
    }
  },
  created () {
    this.montaGrafico()
  }
}
</script>
<style scoped>
</style>
