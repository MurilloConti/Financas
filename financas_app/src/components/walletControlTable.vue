<template>
  <div class="walletControl">
    <b-table striped hover fixed bordered sticky-header="600px" small :busy="isBusy" :items="acoesCarteira" :fields="tableColumns" style="font-size: 0.9rem">
      <template v-slot:table-busy>
        <div class="text-center text-danger my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </template>
      <template v-slot:cell(Code)="data">
        {{data.item.Code.split('.')[0]}}
      </template>
      <template v-slot:cell(CCompra)="data">
       R$: {{Number(data.item.Cost).toFixed(2)}}
      </template>
      <template v-slot:cell(actualPrice)="data">
        R$: {{Number(data.item.Price).toFixed(2)}}
      </template>
      <template v-slot:cell(TotalValue)="data">
        R$: {{Number(data.item.Qtd * data.item.Price).toFixed(2)}}
      </template>
      <template v-slot:cell(actualQuantity)="data">
        {{data.item.Qtd}}
      </template>
      <template v-slot:cell(valorization)="data">
        {{calculateGainPercentage(data.item)}} %
      </template>
      <template v-slot:cell(idealPerc)="data">
        <input type="text" class="form-control text-center" v-model="data.item.idealPerc" />
      </template>
      <template v-slot:cell(actualPerc)="data">
        {{calculateActualPercentage(data.item)}} %
      </template>
      <template v-slot:cell(LucroAcum)="data" :class="getLucroPrejuizoClass(data.item)">
        R$: {{calculateGain(data.item)}}
      </template>
      <template v-slot:cell(Operation)="data">
        {{ calculateOperation(data.item) }}
      </template>
  </b-table>
  </div>
</template>

<script>
export default {
  name: 'walletControlTable',
  props: {
    wallet: Object,
    stocks: Array
  },
  data () {
    return {
      isBusy: false,
      lucroVendas: 0,
      GastosCompra: 0,
      tableColumns: [
        {
          key: 'Code',
          label: 'Ativo'
        },
        {
          key: 'CCompra',
          label: 'Custo Compra'
        },
        {
          key: 'actualPrice',
          label: 'Preço atual'
        },
        {
          key: 'TotalValue',
          label: 'Valor total'
        },
        {
          key: 'actualQuantity',
          label: 'Quantidade atual'
        },
        {
          key: 'valorization',
          label: 'Valorização'
        },
        {
          key: 'idealPerc',
          label: '(%) Ideal'
        },
        {
          key: 'actualPerc',
          label: '(%) Atual'
        },
        {
          key: 'LucroAcum',
          label: 'Lucro Acum.'
        },
        {
          key: 'Operation',
          label: 'Operação'
        }
      ]
    }
  },
  methods: {
    calculateGain: function (acao) {
      if (acao) {
        return isNaN(Number((acao.Qtd * acao.Price) - (acao.Qtd * acao.Cost))) ? 0 : Number((acao.Qtd * acao.Price) - (acao.Qtd * acao.Cost)).toFixed(2)
      }
    },
    getLucroPrejuizoClass: function (acao) {
      if (acao) {
        return this.calculateGain(acao) > 0 ? 'p-0 corLucro' : 'p-0 corPrejuizo'
      }
    },
    calculateGainPercentage: function (acao) {
      if (acao) {
        let totalAtual = acao.Qtd * acao.Price
        return isNaN(Number((this.calculateGain(acao) * 100) / totalAtual)) ? Number(0).toFixed(2) : Number((this.calculateGain(acao) * 100) / totalAtual).toFixed(2)
      }
    },
    calculateActualPercentage: function (acao) {
      if (acao) {
        let patrimonio = this.$props.wallet.equity
        let valTotAcao = acao.Qtd * acao.Price
        return isNaN(Number((valTotAcao * 100) / patrimonio)) ? 0 : Number((valTotAcao * 100) / patrimonio).toFixed(2)
      }
    },
    calculateLucroGasto (totalOperacao, acao) {
      if (acao) {
        if (totalOperacao > 0) {
          this.GastosCompra = totalOperacao * acao.Price
          this.$store.dispatch('setGastosCompra', isNaN(this.GastosCompra) ? 0 : this.GastosCompra)
        } else {
          this.lucroVendas = (totalOperacao * -1) * acao.Price
          this.$store.dispatch('setLucroVendas', isNaN(this.lucroVendas) ? 0 : this.lucroVendas)
        }
      }
    },
    calculateOperation: function (acao) {
      if (acao) {
        let quantoTenho = acao.Qtd * acao.Price
        let percIdeal = acao.idealPerc
        let quantoQuero = (percIdeal / 100) * this.$props.wallet.equity
        let quantoFalta = quantoQuero - quantoTenho
        let totalOperacao = quantoFalta / acao.Price
        acao.Operacao = Math.ceil(totalOperacao)
        this.$store.dispatch('setAcaoInCarteira', acao)
        return isNaN(Math.ceil(totalOperacao)) ? Number(0) : Math.ceil(totalOperacao)
      }
    }
  },
  computed: {
    acoesCarteira () {
      return this.$store.state.Stocks.stocks
    }
  },
  created () {
    this.isBusy = true
    let acoes = []
    this.$store.state.Stocks.stocks.forEach(element => {
      let acao = {
        Code: element.Code,
        Name: element.Name,
        Qtd: element.Qtd,
        Cost: element.Cost,
        Price: element.Price,
        dtOperation: element.dtOperation,
        idWallet: element.idWallet,
        Operacao: 0,
        idealPerc: element.idealPerc
      }
      acoes.push(acao)
    })
    this.$store.dispatch('setStocks', acoes)
    this.isBusy = false
  }
}
</script>
<style scoped>
.corLucro {
    color:#4CAF50
}
.corPrejuizo {
    color:#b92512
}
</style>
