<template>
  <div class="otimizaCarteira">
    <Vheader Title="Controle de Carteira"></Vheader>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <walletTable :wallet="this.carteira" />
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <div class="card text-center cardCorpo">
                        <div class="card-header cardHeader">Lucro Com Vendas</div>
                        <div class="card-body">
                            <p style="font-size: 22px" class="card-text">{{lucroVendas}}</p>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="card text-center cardCorpo">
                        <div class="card-header cardHeader">Gastos com compras</div>
                        <div class="card-body">
                            <p style="font-size: 22px" class="card-text">{{GastosCompra}}</p>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="card text-center cardCorpo">
                        <div class="card-header cardHeader">Gastos com taxas</div>
                        <div class="card-body">
                            <p style="font-size: 22px" class="card-text">{{GastosTaxas}}</p>
                        </div>
                    </div>
                </div>
            </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import Vheader from '@/components/Vheader.vue'
import walletTable from '@/components/walletControlTable.vue'
import { store } from '@/store/index.js'
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
      store.state.carteiraAcoes.forEach(element => {
        if (element.Operacao < 0) {
          totalGain += ((element.Operacao * -1) * element.Price)
        }
      })
      return Number(totalGain).toFixed(2)
    },
    GastosCompra () {
      let totalGasto = 0
      store.state.carteiraAcoes.forEach(element => {
        if (element.Operacao > 0) {
          totalGasto += (element.Operacao * element.Price)
        }
      })
      return Number(totalGasto).toFixed(2)
    },
    GastosTaxas () {
      let totalOper = 0
      store.state.carteiraAcoes.forEach(element => {
        if (element.Operacao !== 0 && !isNaN(element.Operacao)) {
          totalOper += 1
        }
      })
      return Number(totalOper * 6.9).toFixed(2)
    },
    carteira () {
      return store.state.carteiras[0]
    }
  },
  methods: {
  },
  created () {
    this.acoesCarteira = store.state.carteiraAcoes
  }
}
</script>
