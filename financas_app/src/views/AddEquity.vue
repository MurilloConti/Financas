<template>
  <div>
    <b-container fluid style="height:100vh;">
      <b-row class="h-100">
        <b-col class="banner"></b-col>
        <b-col class="h-100">
          <b-row class="h-100">
            <b-col>
                <b-row class="align-items-center justify-content-center" style="background: #424242; color:#ffffff; height:10vh">
                    <b-col>
                        <h2>Adicione ativos a sua carteira!</h2>
                    </b-col>
                </b-row>
                <b-row class="mt-5"  style="height:90vh" >
                    <b-col>
                      <b-table striped bordered small :items="acoesCarteira" :fields="AcoesColumns" show-empty empty-text="Nenhum ativo na carteira">
                        <template v-slot:cell(Code)="data">
                          {{data.item.Code.split('.')[0]}}
                        </template>
                        <template v-slot:cell(Name)="data">
                          {{data.item.Name}}
                        </template>
                        <template v-slot:cell(Qtd)="data">
                          {{data.item.Qtd}}
                        </template>
                        <template v-slot:cell(Price)="data">
                          {{data.item.Price}}
                        </template>
                        <template v-slot:cell(dtOperation)="data">
                          {{data.item.dtOperation}}
                        </template>
                        <template v-slot:cell(Action)="data" class="text-right" style="max-width:50px">
                          <b-button variant="danger" size="sm" v-on:click="removeAcao(data.index)"><i class="fas fa-trash"></i></b-button>
                        </template>
                      </b-table>
                      <b-button class="success" block v-if="acoesCarteira.length > 0" @click="saveStocks()">Concluir</b-button>
                    </b-col>
                    <a class="fab" accesskey="f" v-b-modal.modalAcoes >
                        <i class="fas fa-plus" style="font-size:17px; color:#ffffff;margin-top:15px"></i>
                      </a>
                </b-row>
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </b-container>
    <b-modal id="modalAcoes" size="lg" title="Adição de ativos" header-class="background: #424242" ok-only @show="limpaBusca" @ok="handleOk" ok-title="Salvar" ok-variant="success">
        <b-container fluid>
            <b-row>
                <b-col>
                   <b-input-group class="mb-3" v-if="!editando">
                    <b-form-input id="txtBuscaAcao" type="text" placeholder="Buscar uma ação" v-model="acaoText" v-on:keyup="ativaEnter" />
                      <b-input-group-append>
                          <b-button variant="primary" accesskey="13" v-on:click="GetAcao ()"><i class="fas fa-search" style="color:#ffffff"></i></b-button>
                      </b-input-group-append>
                    </b-input-group>
                    <div style="overflow-y:auto; height:50vh" v-if="!editando">
                      <b-table striped bordered small :busy="isBusy" :items="acoesBusca" :fields="tableColumns" show-empty empty-text="Nenhum registro encontrado.">
                        <template v-slot:table-busy>
                          <div class="text-center text-danger my-2">
                            <b-spinner class="align-middle"></b-spinner>
                            <strong>Loading...</strong>
                          </div>
                        </template>
                        <template v-slot:empty="scope">
                          <p>{{scope.emptyText}}</p>
                        </template>
                        <template v-slot:cell(Code)="data">
                          {{data.item.Code.split('.')[0]}}
                        </template>
                        <template v-slot:cell(Name)="data">
                          {{data.item.Name}}
                        </template>
                        <template v-slot:cell(Action)="data" class="text-right" style="max-width:50px">
                          <b-button variant="success" v-on:click="changeEditando(data.item)"><i class="fas fa-check"></i></b-button>
                        </template>
                      </b-table>
                    </div>
                    <div style=" height:50vh" v-if="editando">
                      <p><strong>Ação:</strong>  {{this.acaoEdicao.Name}}</p>
                      <b-row>
                        <b-col>
                           <b-form-group label-for="txtQtdAcoes" description="A quantidade de ações compradas">
                            <b-form-input id="txtQtdAcoes" v-model="acaoEdicao.Qtd" placeholder="Quantidade" lazy-formatter :formatter="formatter">
                            </b-form-input>
                          </b-form-group>
                        </b-col>
                        <b-col>
                          <b-form-group label-for="txtCostAcoes" description="O preço de custo unitario por ação">
                            <b-form-input id="txtCostAcoes" v-model="acaoEdicao.Cost" placeholder="preço Custo" lazy-formatter :formatter="formatter">
                          </b-form-input>
                          </b-form-group>
                        </b-col>
                      </b-row>
                      <b-row>
                        <b-col>
                          <b-form-group label-for="txtDtOperacao" description="Data da compra das ações">
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
import axios from 'axios'
import { saveAcaoToFirebase, createWalletFireStore, saveOperationToFirebase } from '../utils/firebaseUtils'
import { API_URL, ALPHAVANTAGE_KEY } from '../constants/config'
export default {
  name: 'AddEquity',
  data () {
    return {
      acoesBusca: [],
      acoesCarteira: [],
      acaoText: null,
      editando: false,
      CostAcoes: null,
      QtdAcoes: null,
      dtOperacao: null,
      acaoEdicao: {},
      isBusy: false,
      tableColumns: [
        {
          key: 'Code',
          label: 'Codigo'
        },
        {
          key: 'Name',
          label: 'Nome'
        },
        {
          key: 'Action',
          label: ''
        }
      ],
      AcoesColumns: [
        {
          key: 'Code',
          label: 'Codigo'
        },
        {
          key: 'Name',
          label: 'Nome'
        },
        {
          key: 'Qtd',
          label: 'Quantidade'
        },
        {
          key: 'dtOperation',
          label: 'data'
        },
        {
          key: 'Price',
          label: 'Cotação'
        },
        {
          key: 'Action',
          label: ''
        }
      ]
    }
  },
  methods: {
    GetAcao: function () {
      this.isBusy = true
      let url = API_URL + '?function=SYMBOL_SEARCH&keywords=' + this.acaoText + '&apikey=' + ALPHAVANTAGE_KEY
      axios.get(url).then((response) => {
        response.data.bestMatches.forEach((element, index) => {
          this.acoesBusca.push({ Code: element['1. symbol'], Name: element['2. name'], Qtd: null, Cost: null, Price: null, dtOperation: null, idWallet: 0, idealPerc: 0 })
        })
        this.isBusy = false
      })
        .catch((e) => {
          this.isBusy = false
        })
    },
    ativaEnter: function (event) {
      if (event.key === 'Enter') {
        this.GetAcao()
      }
    },
    limpaBusca: function () {
      this.acoesBusca = []
      this.editando = false
      this.acaoText = null
      this.acaoEdicao = { Code: null, Name: null, Qtd: null, Cost: null, Price: null, dtOperation: null }
    },
    changeEditando: function (acao) {
      this.acaoEdicao = acao
      this.editando = true
    },
    formatter (value) {
      if (isNaN(Number(value.replace(/,/g, '.')))) {
        return Number(0).toFixed(2)
      } else {
        return Number(value.replace(/,/g, '.')).toFixed(2)
      }
    },
    handleOk () {
      this.acaoEdicao.idWallet = this.$store.state.Wallets.wallets[0].id
      if (!this.acoesCarteira.find(x => x.Code === this.acaoEdicao.Code)) {
        this.acoesCarteira.push(this.acaoEdicao)
        this.getCotacaoAtual()
      } else {
        if (this.acoesCarteira) {
          this.acoesCarteira.forEach((element, index) => {
            if (element.Code === this.acaoEdicao.Code) {
              this.acoesCarteira[index] = this.acaoEdicao
            }
          })
        }
      }
    },
    removeAcao (index) {
      this.acoesCarteira.splice(index, 1)
    },
    saveStocks () {
      this.acoesCarteira.forEach(element => {
        let operation = {
          Code: element.Code,
          Cost: element.Cost,
          Qtd: element.Qtd,
          Type: 'C',
          dtOperation: element.dtOperation,
          idWallet: this.$store.state.Wallets.wallets[0].id
        }
        saveAcaoToFirebase(this.$store.state.User.currentUser, element)
        saveOperationToFirebase(this.$store.state.User.currentUser, operation)
      })
      this.updateWalletPatrimony()
    },
    updateWalletPatrimony () {
      let wallets = this.$store.state.Wallets.wallets
      this.acoesCarteira.forEach(element => {
        wallets[0].equity += (element.Price * element.Qtd)
      })
      createWalletFireStore(this.$store.state.User.currentUser, wallets[0], wallets[0].nickName).then(() => {
        this.$router.push('dashboard')
      })
    },
    getCotacaoAtual () {
      let url = API_URL + '?function=GLOBAL_QUOTE&symbol=' + this.acaoEdicao.Code + '&apikey=' + ALPHAVANTAGE_KEY
      axios.get(url).then((response) => {
        let a = response.data['Global Quote']
        this.acaoEdicao.Price = a['05. price']
      },
      (error) => { alert(error) })
    }
  }
}
</script>

<style scoped>
 .banner {
    background: url('../assets/capas-posts-economia-9.jpg') no-repeat right;
    background-size: cover;
  }
 .modalHeader {
    background: #424242 !important;
    color:#ffffff
 }
  .pointer {
    cursor: pointer;
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
  background-color: #04c92f;
  color: white;
  border: none;
  box-shadow: 0 4px 5px rgba(0,0,0,.4);
}
</style>
