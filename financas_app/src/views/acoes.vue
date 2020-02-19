<template>
    <div class="acoes container">
            <div class="row mt-3 ">
             <div class="col-6 mt-2">
            <linegrafico title="VVAR3"></linegrafico>
            </div>
             <div class="col-6 mt-2">
            <linegrafico title="VVAR3"></linegrafico>
            </div>
            <div class="col-6 mt-2">
            <linegrafico title="VVAR3"></linegrafico>
            </div>
            <div class="col-6 mt-2">
            <linegrafico title="VVAR3"></linegrafico>
            </div>
            <div class="col-6 mt-2">
            <linegrafico title="VVAR3"></linegrafico>
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
  background: #00796b;
  color: #ffffff;
  font-weight: bold;
}
.cardCorpo {
  /* background: #424242; */
  color: #424242;
}
</style>
