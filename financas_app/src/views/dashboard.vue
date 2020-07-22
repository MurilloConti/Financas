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
          >Lucro acumulado</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">R$: {{ Number(Patrimonio - TotalAplicado).toFixed(2)}}</p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >Lucro percentual</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">{{LucroPercentual}} %</p>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-6">
        <bargrafico Title="Evoluçao de patrimonio" :dados="this.evolPatrimonio"></bargrafico>
      </div>
      <div class="col">
        <pierafico Title="Distribuição de recursos" :dados="this.carteiraAcoes" ></pierafico>
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
                  <td class="p-0">{{calculateValPerc(acao)}} %</td>
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
    }
  },
  computed: {
    Patrimonio () {
      let Patrimonio = 0
      store.state.carteiraAcoes.forEach(element => {
        Patrimonio = Patrimonio + (element.Qtd * element.Price)
      })
      return Patrimonio
    },
    TotalAplicado () {
      let totalAplicado = 0
      store.state.carteiraAcoes.forEach(element => {
        totalAplicado = totalAplicado + (element.Qtd * element.Cost)
      })
      return totalAplicado
    },
    LucroPercentual () {
      let GanhoPeriodo = 0
      GanhoPeriodo = Number((Number(this.Patrimonio - this.TotalAplicado) * 100) / this.Patrimonio).toFixed(2)
      return GanhoPeriodo
    },
    carteiraAcoes () {
      return store.state.carteiraAcoes
    },
    evolPatrimonio () {
      return store.state.evolucaoPatrimonio
    }
  },
  methods: {
    calculateGain: function (acao) {
      if (acao) {
        return Number((acao.Qtd * acao.Price) - (acao.Qtd * acao.Cost)).toFixed(2)
      }
    },
    calculateValPerc: function (acao) {
      if (acao) {
        let totalAtual = acao.Qtd * acao.Price
        return Number((this.calculateGain(acao) * 100) / totalAtual).toFixed(2)
      }
    }
  },
  beforeCreate () {
    store.dispatch('fetchAcoesCarteira')
    store.dispatch('fetchEvolPatrimonio')
    store.dispatch('fetchCarteiras')
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
