<template>
  <div class="walletControl">
    <table class="table table-striped table-bordered table-hover" style="font-size: 0.9rem">
                    <thead>
                        <tr>
                        <th scope="col">Ação</th>
                        <th scope="col">Preço atual</th>
                        <th scope="col">Valor total</th>
                        <th scope="col">Quantidade atual</th>
                        <th scope="col">Valorização</th>
                        <th scope="col">(%) Ideal</th>
                        <th scope="col">(%) Atual</th>
                        <th scope="col">Lucro Acum.</th>
                        <th scope="col">Operação</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="acao in this.acoesCarteira" :key="acao.Code">
                        <td class="p-0">{{acao.Code.split('.')[0]}}</td>
                        <td class="p-0">R$: {{Number(acao.Price).toFixed(2)}}</td>
                        <td class="p-0">R$: {{Number(acao.Qtd * acao.Price).toFixed(2)}}</td>
                        <td class="p-0">{{acao.Qtd}}</td>
                        <td class="p-0">{{calculateGainPercentage(acao)}} %</td>
                        <td class="p-0" style="width:150px">
                            <input type="text" class="form-control text-center" v-model="acao.percentualIdeal" />
                        </td>
                        <td class="p-0">{{calculateActualPercentage(acao)}} %</td>
                        <td :class="getLucroPrejuizoClass(acao)">R$: {{calculateGain(acao)}}</td>
                        <td class="p-0">{{ calculateOperation(acao) }}</td>
                        </tr>
                    </tbody>
                    </table>
  </div>
</template>

<script>
import { store } from '@/store/index.js'
export default {
  name: 'walletControlTable',
  props: {
    wallet: Object,
    stocks: Array
  },
  data () {
    return {
      lucroVendas: 0,
      GastosCompra: 0
    }
  },
  methods: {
    calculateGain: function (acao) {
      if (acao) {
        return Number((acao.Qtd * acao.Price) - (acao.Qtd * acao.Cost)).toFixed(2)
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
        return Number((this.calculateGain(acao) * 100) / totalAtual).toFixed(2)
      }
    },
    calculateActualPercentage: function (acao) {
      if (acao) {
        let patrimonio = this.$props.wallet.patrimony
        let valTotAcao = acao.Qtd * acao.Price
        return Number((valTotAcao * 100) / patrimonio).toFixed(2)
      }
    },
    calculateLucroGasto (totalOperacao, acao) {
      if (acao) {
        if (totalOperacao > 0) {
          this.GastosCompra = totalOperacao * acao.Price
          store.dispatch('setGastosCompra', this.GastosCompra)
        } else {
          this.lucroVendas = (totalOperacao * -1) * acao.Price
          store.dispatch('setLucroVendas', this.lucroVendas)
        }
      }
    },
    calculateOperation: function (acao) {
      if (acao) {
        let quantoTenho = acao.Qtd * acao.Price
        let percIdeal = acao.percentualIdeal
        let quantoQuero = (percIdeal / 100) * this.$props.wallet.patrimony
        let quantoFalta = quantoQuero - quantoTenho
        let totalOperacao = quantoFalta / acao.Price
        acao.Operacao = Math.ceil(totalOperacao)
        store.dispatch('setAcaoInCarteira', acao)
        return Math.ceil(totalOperacao)
      }
    }
  },
  computed: {
    acoesCarteira () {
      return store.state.carteiraAcoes
    }
  },
  created () {
    let acoes = []
    store.state.carteiraAcoes.forEach(element => {
      let acao = {
        Code: element.Code,
        Name: element.Name,
        Qtd: element.Qtd,
        Cost: element.Cost,
        Price: element.Price,
        Operacao: 0,
        percentualIdeal: 10
      }
      acoes.push(acao)
    })
    store.dispatch('fetchAcoesCarteira', acoes)
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
