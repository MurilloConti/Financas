<template>
  <div class="Vheader">
    <div class="row align-items-center justify-content-center m-0" style="background:#217346;height:10vh">
        <div class="col-2">
<i class="fas fa-home" style="color:#ffffff"></i>
        </div>
      <div class="col-8">
          <h3 style="color:#ffffff">{{Title}} <i class="fas fa-tachometer-alt" style="color:#ffffff"></i></h3>
      </div>
      <div class="col-2">
<a title="Carteiras" v-on:click="goToAcoes()"><i class="fas fa-chart-bar botao" ></i></a>
<a class="ml-5" title="Sair" v-on:click="goToSair()"><i class="fas fa-door-open botao"></i></a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Vheader',
  props: {
    Title: String
  },
  methods: {
    goToAcoes () {
      this.$router.push('acoes')
    },
    goToDashboard () {
      this.$router.push('dashboard')
    },
    goToSair () {
      this.$router.push('/')
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
