<template>
  <div class="acoes">
    <Vheader title="Carteira"></Vheader>
    <div class="">
      <a class="fab" accesskey="f" data-toggle="modal" data-target="#modalAcoes" v-on:click="limpaBusca()">
        <i class="fas fa-plus" style="font-size:17px; color:#ffffff;margin-top:15px"></i>
        </a>
    </div>
    <div class="container-fluid">
      <div class="row mt-3">
        <div class="col-3 mt-2" v-for="(item,index) in acoesCarteira" v-bind:key="index">
          <linegrafico :title="item['1. symbol']" :Acao="item"></linegrafico>
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
            <div class="input-group mb-3">
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
            <div style="overflow-y:auto; height:50vh">
              <table class="table table-striped table-hover table-sm">
                <thead>
                  <tr>
                    <th scope="col">Código</th>
                    <th scope="col">Descrição</th>
                    <th scope="col">País</th>
                  </tr>
                </thead>
                <tbody>
                  <tr data-dismiss="modal" v-for="(item) in acoesBusca.bestMatches" v-bind:key="item.symbol" v-on:click="addCarteira(item)">
                    <td>{{item['1. symbol']}}</td>
                    <td>{{item['2. name']}}</td>
                    <td>{{item['4. region']}}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <!-- <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Salvar</button>
          </div> -->
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
export default {
  name: 'acoes',
  components: {
    linegrafico,
    Vheader
  },
  data () {
    return {
      acoesBusca: [],
      acoesCarteira: []
    }
  },
  methods: {
    GetAcao: function () {
      let text = document.getElementById('txtBuscaAcao').value
      let url = 'https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=' + text + '&apikey=PTNHBBIDUPE7QRHZ'
      axios.get(url).then((response) => {
        this.acoesBusca = response.data
      })
    },
    addCarteira: function (acao) {
      document.getElementById('txtBuscaAcao').value = null
      this.acoesCarteira.push(acao)
      store.commit('setCarteiraAcoes', this.acoesCarteira)
    },
    limpaBusca: function () {
      this.acoesBusca = []
    },
    ativaEnter: function (event) {
      if (event.key === 'Enter') {
        this.GetAcao()
      }
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
