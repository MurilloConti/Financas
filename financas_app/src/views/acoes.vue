<template>
    <div class="acoes">
      <div class="row align-items-center justify-content-center m-0" style="background:#217346;height:10vh">
        <h3 style="color:#ffffff">Carteira</h3>
      </div>
      <div class="container-fluid">
            <div class="row mt-3">
             <div class="col-3 mt-2">
            <linegrafico title="VVAR3"></linegrafico>
            </div>
             <div class="col-3 mt-2">
            <linegrafico title="MGLU3"></linegrafico>
            </div>
            <div class="col-3 mt-2">
            <linegrafico title="JBSS3"></linegrafico>
            </div>
            <div class="col-3 mt-2">
            <linegrafico title="LCAM3"></linegrafico>
            </div>
            <div class="col-3 mt-2">
            <linegrafico title="WEGE3"></linegrafico>
            </div>
        </div>
      </div>
    </div>
</template>

<script>
import linegrafico from '@/components/LineChart.vue'
export default {
  name: 'acoes',
  components: {
    linegrafico
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
</style>
