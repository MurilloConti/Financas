<template>
  <div class="acoes">
    <Vheader Title="Controle de Carteira"></Vheader>
    <b-container fluid>
      <b-row>
        <b-col>
          <b-row>
           <b-col>
              <Details :Stock="activeStock"></Details>
           </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-card no-body class="shadow-sm mt-2" >
                <AreaChartCard v-if="activeStock" :Stock="activeStock" ></AreaChartCard>
                <b-card-body v-else class="h-100 align-items-center justify-content-center">
                  <i class="far fa-folder-open" style="font-size:50px; color: #E0E0E0"></i>
                  <p>Nada para mostrar aqui.</p>
                </b-card-body>
              </b-card>
            </b-col>
          </b-row>
        </b-col>
        <b-col cols="3" class="h-100 mt-2">
            <extract :Stock="activeStock" @addClicked="handleModal"></extract>
          <b-row>
              <b-col>
                <b-form-select v-model="activeStock">
                  <option disabled value="">Escolha um item</option>
                  <b-form-select-option :value="item" v-for="(item,index) in acoesCarteira" v-bind:key="index">{{item.Name}}</b-form-select-option>
                </b-form-select>
              </b-col>
          </b-row>
        </b-col>
      </b-row>
    </b-container>
        <b-modal id="modalAcoes" size="lg" title="Nova movimentação" header-class="background: #424242" ok-only  ok-title="Salvar" ok-variant="success" @show="setAcao" @ok="handleOk">
          <b-container fluid>
              <b-row>
                  <b-col>
                      <div style=" height:50vh">
                        <b-row>
                          <b-col>
                            <p v-if="this.acaoEdicao != null"><strong>Ativo:</strong>  {{this.acaoEdicao.Name}}</p>
                          </b-col>
                          <b-col>
                          <b-form-radio-group id="tpOper" v-model="tpOper" name="tpOper">
                                <b-form-radio value="C">Compra</b-form-radio>
                                <b-form-radio value="V">Venda</b-form-radio>
                            </b-form-radio-group>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <b-form-group label-for="txtQtdAcoes" description="A quantidade de ativo compradas/vendidas">
                              <b-form-input id="txtQtdAcoes" v-model="acaoEdicao.Qtd" placeholder="Quantidade" lazy-formatter :formatter="formatter">
                              </b-form-input>
                            </b-form-group>
                          </b-col>
                          <b-col>
                            <b-form-group label-for="txtCostAcoes" description="O preço de custo/venda unitario por ativo">
                              <b-form-input id="txtCostAcoes" v-model="acaoEdicao.Cost" placeholder="preço Custo" lazy-formatter :formatter="formatter">
                            </b-form-input>
                            </b-form-group>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <b-form-group label-for="txtDtOperacao" description="Data da operação">
                              <b-form-datepicker id="txtDtOperacao" v-model="acaoEdicao.dtOperation" locale="pt"></b-form-datepicker>
                            </b-form-group>
                          </b-col>
                        </b-row>
                      </div>
                  </b-col>
              </b-row>
          </b-container>
        </b-modal>
  </div>
</template>

<script>
import Vheader from '@/components/Vheader.vue'
import extract from './components/extract'
import Details from './components/stockDetails'
import AreaChartCard from '../../components/AreaChartCard'
import { saveOperationToFirebase, saveAcaoToFirebase } from '../../utils/firebaseUtils'
export default {
  name: 'OverviewStocks',
  components: {
    Vheader,
    extract,
    AreaChartCard,
    Details
  },
  data () {
    return {
      acoesCarteira: [],
      activeStock: null,
      acaoEdicao: {},
      tpOper: null
    }
  },
  methods: {
    handleModal () {
      this.$bvModal.show('modalAcoes')
    },
    handleOk () {
      let operation = {
        Code: this.acaoEdicao.Code,
        Cost: this.acaoEdicao.Cost,
        Qtd: this.acaoEdicao.Qtd,
        Type: this.tpOper,
        dtOperation: this.acaoEdicao.dtOperation,
        idWallet: this.$store.state.Wallets.wallets[0].id
      }
      saveOperationToFirebase(this.$store.state.User.currentUser, operation)
      this.updateStockdueChanges(operation)
      this.acaoEdicao = null
      this.tpOper = null
    },
    updateStockdueChanges (operation) {
      let stocks = this.$store.state.Stocks.stocks
      let stock = stocks.find(x => x.Code === operation.Code)
      if (operation.Type === 'C') {
        stock.Qtd += Number(operation.Qtd)
        stock.Cost = Number(operation.Cost)
      } else {
        stock.Qtd -= Number(operation.Qtd)
      }
      saveAcaoToFirebase(this.$store.state.User.currentUser, stock)
    },
    setAcao () {
      let obj = {
        Code: this.activeStock.Code,
        Name: this.activeStock.Name,
        Qtd: this.activeStock.Qtd,
        dtOperation: null,
        idWallet: this.activeStock.idWallet,
        Cost: null
      }
      this.acaoEdicao = obj
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
</style>
