<template>
  <div class="acoes">
    <Vheader Title="Carteira"></Vheader>
    <div class="">
      <a class="fab" accesskey="f" data-toggle="modal" data-target="#modalAcoes" v-on:click="limpaBusca()">
        <i class="fas fa-plus" style="font-size:17px; color:#ffffff;margin-top:15px"></i>
        </a>
    </div>
    <div class="container-fluid">
      <div class="row mt-3">
        <div class="col-3 mt-2" v-for="(item,index) in acoesCarteira" v-bind:key="index">
          <linegrafico :title="item.Code" :Acao="item"></linegrafico>
        </div>
      </div>
    </div>
    <div class="modal" tabindex="-1" role="dialog" id="modalAcoes">
      <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header" style="background: #217346;">
            <h6 class="modal-title" style="color:#ffffff">Adicionar nova ação a carteira</h6>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true" style="color:#ffffff">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="input-group mb-3" v-if="!editando">
              <input
              id="txtBuscaAcao"
                type="text"
                class="form-control"
                placeholder="Buscar uma ação"
                aria-label="Recipient's username"
                aria-describedby="basic-addon2"
                v-on:keyup="ativaEnter"
              />
              <div class="input-group-append">
                <a class="btn btn-success" accesskey="13" v-on:click="GetAcao ()">
                  <i class="fas fa-search" style="color:#ffffff"></i>
                </a>
              </div>
            </div>
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
          </div>
          <div class="modal-footer" v-if="editando">
            <button type="button" class="btn btn-secondary" data-dismiss="modal" v-on:click="preencheDadosAcao()">Salvar</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import linegrafico from '@/components/LineChart.vue'
import Vheader from '@/components/Vheader.vue'
import axios from 'axios'
import { store } from '@/store/index.js'
const fb = require('@/firebaseConfig.js')
export default {
  name: 'acoes',
  components: {
    linegrafico,
    Vheader
  },
  data () {
    return {
      acoesBusca: [],
      acoesCarteira: [],
      editando: false,
      acaoEdicao: {}
    }
  },

  beforeCreate () {
    let that = this
    fb.usuariosCollection.doc(store.state.currentUser).collection('Acoes')
      .onSnapshot(function (querySnapshot) {
        querySnapshot.forEach(function (doc) {
          let acao = {
            Code: doc.data().Cod,
            Name: doc.data().Name,
            Qtd: doc.data().Qtd,
            Cost: doc.data().Cost,
            Price: doc.data().Price
          }
          that.addCarteira(acao)
        })
      })
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
        this.acoesCarteira.forEach((element, index) => {
          if (element.Code === acao.Code) {
            this.acoesCarteira[index] = acao
          }
        })
      }
      store.commit('setCarteiraAcoes', this.acoesCarteira)
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
  background: #217346;
  color: #ffffff;
  font-weight: bold;
}
.cardCorpo {
  /* background: #424242; */
  color: #424242;
}
.fab{
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
