<template>
  <div class="otimizaCarteira">
    <Vheader Title="Controle de Carteira"></Vheader>
    <b-container fluid>
      <b-row>
        <b-col cols="3" class="my-2">
             <b-form-group
        label-cols-sm="3"
        label="Patrimonio:"
        label-align-sm="right"
        label-for="txtEquity"
      >
        <b-form-input id="txtEquity" v-model="equity" placeholder="0,00" lazy-formatter :formatter="formatter"></b-form-input>
      </b-form-group>
        </b-col>
        <b-col cols="1" offset="8" class="my-2">
          <b-button variant="success" block="" size="sm" v-on:click="saveSimulation()"><i class="fas fa-check"></i> Salvar</b-button>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <walletTable :wallet="this.carteira" />
        </b-col>
      </b-row>
      <b-row>
        <b-col cols="3">
          <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Lucro Com Vendas
            </b-card-header>
              <b-card-body>
                <b-card-text style="font-size: 22px">
                 R$: {{lucroVendas}}
                </b-card-text>
              </b-card-body>
          </b-card>
        </b-col>
        <b-col cols="3">
          <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Gastos com compras
            </b-card-header>
              <b-card-body>
                <b-card-text style="font-size: 22px">
                 R$: {{GastosCompra}}
                </b-card-text>
              </b-card-body>
          </b-card>
        </b-col>
        <b-col cols="3">
          <b-card no-body tag="article" class="mb-2">
            <b-card-header class="cardHeader">
              Gastos com taxas
            </b-card-header>
              <b-card-body>
                <b-card-text style="font-size: 22px">
                 R$: {{GastosTaxas}}
                </b-card-text>
              </b-card-body>
          </b-card>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
// @ is an alias to /src
import Vheader from '@/components/Vheader.vue'
import walletTable from '@/components/walletControlTable.vue'
import { saveAcaoToFirebase } from '../utils/firebaseUtils'
export default {
  name: 'otimizaCarteira',
  components: {
    Vheader,
    walletTable
  },
  data () {
    return {
      acoesCarteira: []
    }
  },
  computed: {
    lucroVendas () {
      let totalGain = 0
      this.$store.state.Stocks.stocks.forEach(element => {
        if (element.Operacao < 0) {
          totalGain += ((element.Operacao * -1) * element.Price)
        }
      })
      return Number(totalGain).toFixed(2)
    },
    GastosCompra () {
      let totalGasto = 0
      this.$store.state.Stocks.stocks.forEach(element => {
        if (element.Operacao > 0) {
          totalGasto += (element.Operacao * element.Price)
        }
      })
      return Number(totalGasto).toFixed(2)
    },
    GastosTaxas () {
      let totalOper = 0
      this.$store.state.Stocks.stocks.forEach(element => {
        if (element.Operacao !== 0 && !isNaN(element.Operacao)) {
          totalOper += 1
        }
      })
      return Number(totalOper * this.$store.state.Wallets.wallets[0].corretagemTax).toFixed(2)
    },
    carteira () {
      return this.$store.state.Wallets.wallets[0]
    },
    equity: {
      get: function () {
        return Number(this.$store.state.Wallets.wallets[0].equity).toFixed(2)
      },
      set: function (newValue) {
        let wallets = this.$store.state.Wallets.wallets
        wallets[0].equity = newValue
        this.$store.dispatch('setWallets', wallets)
      }
    }
  },
  methods: {
    saveSimulation () {
      this.$store.state.Stocks.stocks.forEach(element => {
        saveAcaoToFirebase(this.$store.state.User.currentUser, element)
      })
    },
    formatter (value) {
      if (isNaN(Number(value.replace(/,/g, '.')))) {
        return Number(0).toFixed(2)
      } else {
        return Number(value.replace(/,/g, '.')).toFixed(2)
      }
    }
  },
  created () {
    this.acoesCarteira = this.$store.state.Stocks.stocks
  }
}
</script>

<style>
.cardHeader {
  background: #424242 !important;
  color: #ffffff;
  font-weight: bold;
}
</style>
