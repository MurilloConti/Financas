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
                        <h2>Criação de Carteira</h2>
                    </b-col>
                </b-row>
                <b-row class="align-items-center"  style="height:90vh" >
                    <b-col cols="6" offset-md="3">
                        <b-row>
                            <b-col class="text-right align-baselin">
                                <b>Corretora:</b>
                            </b-col>
                            <b-col class="text-left">
                                <b-form-select v-model="selectedBrokerId" :options="corretoras"></b-form-select>
                            </b-col>
                        </b-row>
                        <b-row class="mt-5" v-if="selectedBrokerId != null">
                            <b-col class="text-right">
                                <p>Taxa de corretagem:</p>
                            </b-col>
                            <b-col class="text-left">
                                <b v-if="taxaCorretagem > 0">R$ {{Number(taxaCorretagem).toFixed(2)}}</b>
                                <b v-if="taxaCorretagem <= 0">Gratis</b>
                            </b-col>
                        </b-row>
                        <b-row class="mt-3" v-if="selectedBrokerId != null">
                            <b-col class="text-right">
                                <p>Taxa de custódia:</p>
                            </b-col>
                            <b-col class="text-left">
                                <b v-if="taxaCustodia > 0">R$ {{Number(taxaCustodia).toFixed(2)}}</b>
                                <b v-if="taxaCustodia <= 0">Gratis</b>
                            </b-col>
                        </b-row>
                        <b-row class="mt-3" v-if="selectedBrokerId != null">
                            <b-col class="text-right">
                                <p>Apelido da carteira:</p>
                            </b-col>
                            <b-col class="text-left">
                                <b-form-input v-model="apelido" placeholder="minha carteira"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                          <b-col cols="6" offset-md="4" class="mt-5">
                            <b-button class="mt-5" v-if="selectedBrokerId != null" block clas="botao" v-on:click="createWallet ()">Próximo</b-button>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
import { CORRETORAS } from '../constants/corretoras'
import { createWalletFireStore } from '../utils/firebaseUtils'
export default {
  name: 'CreateWallet',
  data () {
    return {
      selectedBrokerId: null,
      apelido: null
    }
  },
  computed: {
    corretoras () {
      let obj = []
      CORRETORAS.forEach(corretora => {
        obj.push({ value: corretora.id, text: corretora.name })
      })
      return obj
    },
    taxaCorretagem () {
      let corretora = CORRETORAS.find(x => x.id === this.selectedBrokerId)
      let tax = 0
      if (corretora) {
        tax = corretora.corretagemTax
      }
      return tax
    },
    taxaCustodia () {
      let corretora = CORRETORAS.find(x => x.id === this.selectedBrokerId)
      let tax = 0
      if (corretora) {
        tax = corretora.custodiaTax
      }
      return tax
    }
  },
  methods: {
    createWallet: function () {
      let corretora = CORRETORAS.find(x => x.id === this.selectedBrokerId)
      createWalletFireStore(this.$store.state.User.currentUser, corretora, this.apelido).then(() => {
        this.$store.dispatch('fetchWallets')
        this.$router.push('addAtivos')
      })
    }
  }
}
</script>

<style scoped>
 .banner {
            background: url('../assets/capas-posts-economia-9.jpg') no-repeat right;
            background-size: cover;
        }

  .botao {
    background: #424242 !important;
  }
  .pointer {
    cursor: pointer;

  }
</style>
