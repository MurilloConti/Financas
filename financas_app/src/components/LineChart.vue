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
<div class="col-6 align-items-center justify-content-center"><p style="color:#424242;font-size:20px;font-weight:bold">R$: {{ Number(valorAtual['05. price']).toFixed(2) }}</p></div>
<div class="col-6 align-items-center justify-content-center"> <p style="color:#424242;font-size:20px;font-weight:bold">{{ valorAtual['10. change percent']}} <i class="fas fa-long-arrow-alt-down" style="color:red"></i></p></div>
      </div>
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'
import axios from 'axios'
import { store } from '@/store/index.js'
const fb = require('@/firebaseConfig.js')
export default {
  name: 'LineChart',
  props: {
    Title: String,
    Acao: Object
  },
  components: {
    GChart
  },
  data () {
    return {
      chartData: [],
      acao: null,
      valorAtual: [],
      chartOptions: {
        chart: {
          title: 'Company Performance',
          subtitle: 'Sales, Expenses, and Profit: 2014-2017'
        }
      }
    }
  },
  methods: {
    GetCotacao: function () {
      let url = 'https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=' + this.acao.Code + '&interval=15min&apikey=PTNHBBIDUPE7QRHZ'
      axios.get(url).then((response) => {
        for (let i in response.data['Time Series (15min)']) {
          let a = response.data['Time Series (15min)'][i]
          let obj = [i, Number(a['4. close'])]
          this.chartData.push(obj)
        }
        this.chartData.push(['Data', this.acao.Code])
        this.chartData = this.chartData.reverse()
      },
      (error) => { alert(error) })
    },
    GetAtual: function () {
      let url = 'https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=' + this.acao.Code + '&apikey=PTNHBBIDUPE7QRHZ'
      axios.get(url).then((response) => {
        let a = response.data['Global Quote']
        this.valorAtual = a
      },
      (error) => { alert(error) })
    },
    removeAcao: function () {
      if (window.confirm('Quer mesmo excluir?')) {
        fb.usuariosCollection
          .doc(store.state.currentUser)
          .collection('Acoes')
          .doc(this.acao.Code)
          .delete().then(function () {
            var acoesCarteira = store.state.carteiraAcoes
            console.log(acoesCarteira.findIndex(x => x.Code === this.acao.Code))
            acoesCarteira.splice(acoesCarteira.findIndex(x => x.Code === this.acao.Code), 1)
            store.commit('setCarteiraAcoes', acoesCarteira)
          }).catch(function (e) {
            console.error('Error removing document: ', e)
          })
      }
    }
  },
  created () {
    this.acao = this.$props.Acao
    this.GetCotacao()
    this.GetAtual()
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
</style>
