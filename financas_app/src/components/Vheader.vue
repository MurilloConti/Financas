<template>
  <div class="Vheader">
    <b-row class="align-items-center justify-content-center m-0" style="background:#424242;height:10vh">
      <b-col cols="2" style="color:#ffffff">
        <b-avatar badge badge-variant="success"></b-avatar> Olá {{name}}
      </b-col>
      <b-col cols="8">
        <h3 style="color:#ffffff">{{Title}} <i class="fas fa-tachometer-alt" style="color:#ffffff"></i></h3>
      </b-col>
      <b-col cols="2">
        <i class="fas fa-home" style="color:#ffffff" v-on:click="goToDashboard()"></i>
        <i class="fas fa-chart-bar botao ml-3"  title="Cotaçoes" v-on:click="goToAcoes()" ></i>
        <i class="fas fa-umbrella botao ml-3" title="Controle de Carteira" v-on:click="goToOtimizaCarteira()" ></i>
        <i class="fas fa-door-open botao ml-3" title="Sair" v-on:click="goToSair()"></i>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import firebase from 'firebase'
export default {
  name: 'Vheader',
  props: {
    Title: String
  },
  computed: {
    name () {
      return this.$store.state.User.userProfile.displayName
    }
  },
  methods: {
    goToAcoes () {
      this.$router.push('acoes').catch(() => {})
    },
    goToOtimizaCarteira () {
      this.$router.push('otimizaCarteira').catch(() => {})
    },
    goToDashboard () {
      this.$router.push('dashboard').catch(() => {})
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
