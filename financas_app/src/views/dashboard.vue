<template>
  <div class="dashboard">
    <div class="inner">
  <Vheader Title="Resumo Financeiro"></Vheader>
  <b-container>
    <b-row class="mt-3">
      <b-col>
        <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Patrimonio total
            </b-card-header>
              <b-card-body>
                <b-card-text>
                  <p style="font-size: 22px" class="card-text">R$: {{ Number(Patrimonio).toFixed(2)}}</p>
                </b-card-text>
              </b-card-body>
          </b-card>
      </b-col>
      <b-col>
        <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Lucro acumulado
            </b-card-header>
            <b-card-body>
              <b-card-text>
                <p style="font-size: 22px" class="card-text">R$: {{ Number(Patrimonio - TotalAplicado).toFixed(2)}}</p>
              </b-card-text>
            </b-card-body>
        </b-card>
      </b-col>
      <b-col>
        <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Lucro percentual
            </b-card-header>
            <b-card-body>
              <b-card-text>
                <p style="font-size: 22px" class="card-text">{{LucroPercentual}} %</p>
              </b-card-text>
            </b-card-body>
        </b-card>
      </b-col>
    </b-row>
    <b-row class="mt-3">
      <b-col cols="6">
        <bargrafico Title="Evoluçao de patrimonio" :dados="this.evolPatrimonio"></bargrafico>
      </b-col>
      <b-col cols="6">
        <pierafico Title="Distribuição de recursos" :dados="this.carteiraAcoes" ></pierafico>
      </b-col>
    </b-row>
    <b-row class="mt-3">
      <b-col>
        <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Ativos
            </b-card-header>
              <b-card-body>
                <b-table striped hover small sticky-header="230px" :items="carteiraAcoes" :fields="tableFields">
                  <template v-slot:cell(Code)="data">
                    {{data.value.split('.')[0]}}
                  </template>
                  <template v-slot:cell(Valorização)="data">
                    {{ calculateValPerc(data.item) }} %
                  </template>
                </b-table>
              </b-card-body>
          </b-card>
      </b-col>
    </b-row>
  </b-container>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import bargrafico from '@/components/BarGrafico.vue'
import pierafico from '@/components/PieChart.vue'
import Vheader from '@/components/Vheader.vue'
export default {
  name: 'dashboard',
  components: {
    bargrafico,
    pierafico,
    Vheader
  },
  data () {
    return {
      tableFields: ['Code', 'Name', 'Valorização']
    }
  },
  computed: {
    Patrimonio () {
      let Patrimonio = 0
      this.$store.state.Stocks.stocks.forEach(element => {
        Patrimonio += (element.Qtd * element.Price)
      })
      return isNaN(Patrimonio) ? Number(0).toFixed(2) : Patrimonio
    },
    TotalAplicado () {
      let totalAplicado = 0
      this.$store.state.Stocks.stocks.forEach(element => {
        totalAplicado += (element.Qtd * element.Cost)
      })
      return isNaN(totalAplicado) ? Number(0).toFixed(2) : totalAplicado
    },
    LucroPercentual () {
      let GanhoPeriodo = 0
      GanhoPeriodo = Number((Number(this.Patrimonio - this.TotalAplicado) * 100) / this.Patrimonio).toFixed(2)
      return isNaN(GanhoPeriodo) ? Number(0).toFixed(2) : GanhoPeriodo
    },
    carteiraAcoes () {
      return this.$store.state.Stocks.stocks
    },
    evolPatrimonio () {
      return this.$store.state.evolucaoPatrimonio
    }
  },
  methods: {
    calculateGain: function (acao) {
      if (acao) {
        return isNaN(Number((acao.Qtd * acao.Price) - (acao.Qtd * acao.Cost))) ? Number(2).toFixed(2) : Number((acao.Qtd * acao.Price) - (acao.Qtd * acao.Cost)).toFixed(2)
      }
    },
    calculateValPerc: function (acao) {
      if (acao) {
        let totalAtual = acao.Qtd * acao.Price
        return isNaN(Number((this.calculateGain(acao) * 100) / totalAtual)) ? Number(0).toFixed(2) : Number((this.calculateGain(acao) * 100) / totalAtual).toFixed(2)
      }
    }
  },
  beforeCreate () {
    // this.$store.dispatch('fetchEvolPatrimonio')
    // this.$store.dispatch('fetchWallets')
    // this.$store.dispatch('fetchS')
  }
}
</script>

<style>
.dashboard {
  font-family: "Exo", sans-serif;
}
.cardHeader {
  background: #424242 !important;
  color: #ffffff;
  font-weight: bold;
}
.cardCorpo {
  color: #424242;
}
</style>
