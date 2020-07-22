<template>
  <div class="card cardCorpo shadow-sm">
    <div class="card-header cardHeader">
     <div class="row">
      <div class="col-10">
        {{Title.split('.')[0]}}
      </div>
      <div class="col-1">
        <a style="cursor:pointer" v-on:click="removeAcao()"><i class="fas fa-times"></i></a>
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
        <div class="col-6 align-items-center justify-content-center">
          <p style="color:#424242;font-size:20px;font-weight:bold">R$: {{ Number(valorAtual).toFixed(2) }}</p>
        </div>
        <div class="col-6 align-items-center justify-content-center">
          <p style="color:#424242;font-size:20px;font-weight:bold">{{ changePerc }}
            <i v-if="change < 0" class="fas fa-long-arrow-alt-down" style="color:red"></i>
            <i v-if="change > 0" class="fas fa-long-arrow-alt-up" style="color:green"></i>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'
import { store } from '@/store/index.js'
import axios from 'axios'
import { API_URL, ALPHAVANTAGE_KEY } from '../constants/config'
export default {
  name: 'AreaChart',
  props: {
    Title: String
  },
  components: {
    GChart
  },
  data () {
    return {
      chartData: [],
      acao: null,
      valorAtual: 0,
      changePerc: 0,
      change: 0,
      chartOptions: {
        chart: {
          title: 'Company Performance',
          subtitle: 'Sales, Expenses, and Profit: 2014-2017'
        }
      }
    }
  },
  methods: {
    getAcaoFromStore: function () {
      let acao = store.state.rawAcoesData.find(x => x.code === this.Title)
      if (acao) {
        this.chartData = [['Data', 'Fechamento']]
        acao.data.forEach(element => {
          this.chartData.push([element.date, Number(element.close)])
        })
      }
    },
    getCotacaoActual: function () {
      let url = API_URL + '?function=GLOBAL_QUOTE&symbol=' + this.Title + '&apikey=' + ALPHAVANTAGE_KEY
      axios.get(url).then((response) => {
        if (response.status === 200) {
          let a = response.data['Global Quote']
          this.valorAtual = a['05. price']
          this.changePerc = a['10. change percent']
          this.change = a['09. change']
        }
      },
      (error) => { alert(error) })
    }
  },
  created () {
    store.dispatch('fetchRawAcoesData', store.state.carteiraAcoes.find(x => x.Code === this.Title))
    this.getAcaoFromStore()
    this.getCotacaoActual()
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
</style>
