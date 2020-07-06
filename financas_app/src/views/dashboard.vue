<template>
  <div class="dashboard">
  <Vheader Title="Resumo Financeiro"></Vheader>
      <div class="container">
    <div class="row mt-3">
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Patrimonio total</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">R$: {{ Number(Patrimonio).toFixed(2)}}</p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >Lucro no mês {{ new Date().getMonth() + 1 }}/{{ new Date().getFullYear() }}</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">R$: {{ Number(Patrimonio - totalAplicado).toFixed(2)}}</p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >no periodo {{ new Date().getMonth() + 1 }}/{{ new Date().getFullYear() }}</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">{{GanhoPeriodo}} %</p>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-6">
        <bargrafico title="Evoluçao de patrimonio"></bargrafico>
      </div>
      <div class="col">
        <pierafico title="Distribuição de recursos"></pierafico>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Ativos</div>
          <div class="card-body">
            <table class="table table-striped" style="font-size: 0.8rem">
              <thead>
                <tr>
                  <th scope="col">Codigo</th>
                  <th scope="col">Nome</th>
                  <th scope="col">Valorização</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="acao in carteiraAcoes" :key="acao.Code">
                  <td class="p-0">
                    {{acao.Code.split('.')[0]}}
                  </td>
                  <td class="p-0">{{acao.Name}}</td>
                  <td class="p-0">3.25%</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script>
// @ is an alias to /src
import bargrafico from '@/components/BarGrafico.vue'
import pierafico from '@/components/PieChart.vue'
import Vheader from '@/components/Vheader.vue'
import { store } from '@/store/index.js'
export default {
  name: 'dashboard',
  components: {
    bargrafico,
    pierafico,
    Vheader
  },
  data () {
    return {
      Patrimonio: 0,
      LucroMes: 9999.99,
      totalAplicado: 0,
      GanhoPeriodo: 0,
      carteiraAcoes: []
    }
  },
  methods: {
    calculaPatrimonio: function (event) {
      let carteira = store.state.carteiraAcoes
      carteira.forEach(element => {
        this.Patrimonio = this.Patrimonio + (element.Qtd * element.Price)
      })
    },
    calculaInvestimento: function (event) {
      let carteira = store.state.carteiraAcoes
      carteira.forEach(element => {
        this.totalAplicado = this.totalAplicado + (element.Qtd * element.Cost)
      })
    },
    calculaLucroPercentual: function (event) {
      this.GanhoPeriodo = Number((Number(this.Patrimonio - this.totalAplicado) * 100) / this.Patrimonio).toFixed(2)
    }
  },
  created () {
    this.calculaPatrimonio()
    this.calculaInvestimento()
    this.calculaLucroPercentual()
    this.carteiraAcoes = store.state.carteiraAcoes
  }
}
</script>

<style>
.dashboard {
  font-family: "Exo", sans-serif;
}
.cardHeader {
  background: #00796b;
  color: #ffffff;
  font-weight: bold;
}
.cardCorpo {
  /* background: #424242; */
  color: #424242;
}
</style>
