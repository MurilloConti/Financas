<template>
  <div class="acoes">
    <Vheader Title="Controle de Carteira"></Vheader>
    <b-container fluid>
      <b-row>
        <b-col>
          <b-row>
            <b-col cols="4">
              <b-form-select v-model="activeStock">
                <option disabled value="">Escolha um item</option>
                <b-form-select-option :value="item" v-for="(item,index) in acoesCarteira" v-bind:key="index">{{item.Name}}</b-form-select-option>
              </b-form-select>
            </b-col>
          </b-row>
          <AcaoCard :Stock="activeStock" style="height:70vh"></AcaoCard>
        </b-col>
        <b-col cols="3" class="h-100">
            <b-card no-body class="shadow" style="height:90vh !important">
              <b-card-header class="text-center">
                <p>Extrato</p>
              </b-card-header>
              <b-card-body>
                <b-overlay :show="show" opacity="0.9" rounded="sm">
                  <b-list-group>
                    <b-list-group-item>
                      <b-row>
                        <b-col cols="2">
                          <b-badge pill  variant="success"><i class="fas fa-shopping-cart"></i></b-badge>
                        </b-col>
                          <b-col>
                            Compra de R$250.000
                          </b-col>
                      </b-row>
                      </b-list-group-item>
                    <b-list-group-item>
                      <b-row>
                        <b-col cols="2">
                          <b-badge pill  variant="danger"><i class="fas fa-receipt"></i></b-badge>
                        </b-col>
                          <b-col>
                            <h5 class="mb-1">R$250.000</h5>
                          </b-col>
                          <b-col cols="3">
                            <small>25/10/2020</small>
                          </b-col>
                      </b-row>
                    </b-list-group-item>
                    <b-list-group-item>Morbi leo risus</b-list-group-item>
                    <b-list-group-item>Porta ac consectetur ac</b-list-group-item>
                    <b-list-group-item>Vestibulum at eros</b-list-group-item>
                  </b-list-group>
                </b-overlay>
              </b-card-body>
            </b-card>
        </b-col>
      </b-row>
    </b-container>
    <b-modal id="modalAcoes" size="lg" title="Adicionar nova ação a carteira">
      <b-input-group class="mb-3" v-if="!editando">
        <b-form-input id="txtBuscaAcao" type="text" placeholder="Buscar uma ação" v-on:keyup="ativaEnter" />
          <b-input-group-append>
              <b-button variant="success" accesskey="13" v-on:click="GetAcao ()"><i class="fas fa-search" style="color:#ffffff"></i></b-button>
          </b-input-group-append>
      </b-input-group>
            <div style="overflow-y:auto; height:50vh" v-if="!editando">
              <table class="table table-striped table-hover table-sm">
                <thead>
                  <tr>
                    <th scope="col">Código</th>
                    <th scope="col">Descrição</th>
                    <th scope="col">País</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item) in acoesBusca" v-bind:key="item.Code" v-on:click="changeEditando(item)">
                    <td>{{item.Code}}</td>
                    <td>{{item.Name}}</td>
                    <td>{{item.Region}}</td>
                  </tr>
                </tbody>
              </table>
            </div>
            <div style=" height:50vh" v-if="editando">
              <p><strong>Ação:</strong>  {{this.acaoEdicao.Name}}</p>
              <div class="row">
                <div class="col-6">
                <input
                id="txtqtdAcoes"
                type="text"
                class="form-control"
                placeholder="Quantidade"/>
              </div>
                <div class="col-6">
                <input
                id="txtCostAcoes"
                type="text"
                class="form-control"
                placeholder="preço Custo"/>
              </div>
              </div>
            </div>
        <b-button @click="preencheDadosAcao()">Salvar</b-button>
    </b-modal>
  </div>
</template>

<script>
import Vheader from '@/components/Vheader.vue'
// import AreaChartCard from '../components/AreaChartCard'
import AcaoCard from '../components/AcaoCard'
import axios from 'axios'
import { store } from '@/store/index.js'
const fb = require('@/firebaseConfig.js')
export default {
  name: 'acoes',
  components: {
    Vheader,
    // AreaChartCard
    AcaoCard
  },
  data () {
    return {
      acoesBusca: [],
      acoesCarteira: [],
      activeStock: null,
      editando: false,
      acaoEdicao: {}
    }
  },

  beforeCreate () {
    if (store.state.carteiraAcoes.length === 0) {
      store.dispatch('fetchAcoesCarteira')
    }
  },
  methods: {
    GetAcao: function () {
      let text = document.getElementById('txtBuscaAcao').value
      let url = 'https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=' + text + '&apikey=PTNHBBIDUPE7QRHZ'
      axios.get(url).then((response) => {
        response.data.bestMatches.forEach((element, index) => {
          let acao = {
            Code: element['1. symbol'],
            Name: element['2. name'],
            Region: element['4. region'],
            Qtd: 0,
            Cost: 0,
            Price: 0
          }
          this.acoesBusca.push(acao)
        })
      })
    },
    preencheDadosAcao: function () {
      this.acaoEdicao.Qtd = document.getElementById('txtqtdAcoes').value
      this.acaoEdicao.Cost = document.getElementById('txtCostAcoes').value
      this.addCarteira(this.acaoEdicao)
      this.editando = false
      this.acaoEdicao = {}
    },
    addCarteira: function (acao) {
      if (!this.acoesCarteira.find(x => x.Code === acao.Code)) {
        this.acoesCarteira.push(acao)
        this.saveToFirebase(acao)
      } else {
        if (this.acoesCarteira) {
          this.acoesCarteira.forEach((element, index) => {
            if (element.Code === acao.Code) {
              this.acoesCarteira[index] = acao
            }
          })
        }
      }
      store.commit('fetchAcoesCarteira', this.acoesCarteira)
    },
    limpaBusca: function () {
      this.acoesBusca = []
    },
    ativaEnter: function (event) {
      if (event.key === 'Enter') {
        this.GetAcao()
      }
    },
    changeEditando: function (acao) {
      this.acaoEdicao = acao
      this.editando = true
    },
    saveToFirebase (acao) {
      fb.usuariosCollection.doc(store.state.currentUser).collection('Acoes').doc(acao.Code)
        .set({
          Cod: acao.Code,
          Cost: acao.Cost,
          Name: acao.Name,
          Price: acao.Price,
          Qtd: acao.Qtd
        }, { merge: true })
    }
  },
  created () {
    if (store.state.carteiraAcoes.length > 0) {
      this.acoesCarteira = store.state.carteiraAcoes
    }
  }
}
</script>

<style>
.acoes {
  font-family: "Exo", sans-serif;
}
.cardHeader {
  background: #424242;
  color: #ffffff;
  font-weight: bold;
}
.cardCorpo {
  /* background: #424242; */
  color: #424242;
}
.fab{
  z-index: 20;
  position: fixed;
  bottom:30px;
  right:35px;
  cursor: pointer;
  width: 48px;
  height: 48px;
  border-radius: 30px;
  background-color: #c91104;
  color: white;
  border: none;
  box-shadow: 0 4px 5px rgba(0,0,0,.4);
}
</style>
