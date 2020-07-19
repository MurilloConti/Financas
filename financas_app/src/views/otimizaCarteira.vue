<template>
  <div class="otimizaCarteira">
    <Vheader Title="Controle de Carteira"></Vheader>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <walletTable :wallet="this.carteira" :stocks="this.acoesCarteira" />
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
    }
  },
  computed: {
    lucroVendas () {
      return Number(store.state.lucroVendas).toFixed(2)
    },
    acoesCarteira () {
      return store.state.carteiraAcoes
    },
    GastosCompra () {
      return Number(store.state.GastosCompra).toFixed(2)
    },
    GastosTaxas () {
      return Number(store.state.totalOperacoes * 6.9).toFixed(2)
    },
    carteira () {
      return store.state.carteiras[0]
    }
  },
  methods: {
  },
  created () {
  }
}
</script>
