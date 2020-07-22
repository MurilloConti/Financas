<template>
  <div class="Vheader">
    <div class="row align-items-center justify-content-center m-0" style="background:#217346;height:10vh">
        <div class="col-2" style="color:#ffffff">
          Olá {{name}}
        </div>
      <div class="col-8">
          <h3 style="color:#ffffff">{{Title}} <i class="fas fa-tachometer-alt" style="color:#ffffff"></i></h3>
      </div>
      <div class="col-2">
        <i class="fas fa-home" style="color:#ffffff" v-on:click="goToDashboard()"></i>
        <i class="fas fa-chart-bar botao ml-3"  title="Cotaçoes" v-on:click="goToAcoes()" ></i>
        <i class="fas fa-umbrella botao ml-3" title="Controle de Carteira" v-on:click="goToOtimizaCarteira()" ></i>
        <i class="fas fa-door-open botao ml-3" title="Sair" v-on:click="goToSair()"></i>
      </div>
    </div>
  </div>
</template>

<script>
import { store } from '@/store/index.js'
import firebase from 'firebase'
export default {
  name: 'Vheader',
  props: {
    Title: String
  },
  computed: {
    name () {
      return store.state.userProfile.displayName
    }
  },
  methods: {
    goToAcoes () {
      this.$router.push('acoes')
    },
    goToOtimizaCarteira () {
      this.$router.push('otimizaCarteira')
    },
    goToDashboard () {
      this.$router.push('dashboard')
    },
    goToSair () {
      let that = this
      firebase.auth().signOut().then(function () {
        that.$router.push('/')
      }).catch(function (e) {
        console.log(e)
      })
    }
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
.botao
{
  cursor:pointer;
  color:#ffffff;
  font-size:18px
}
</style>
