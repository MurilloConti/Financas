<template>
  <div class="card cardCorpo">
    <div class="card-header cardHeader">{{Title}}</div>
    <div class="card-body">
      <apexchart width="100%" :type="tipo" :options="options" :series="series"></apexchart>
    </div>
  </div>
</template>

<script>
import apexchart from 'vue-apexcharts'
export default {
  name: 'BarGrafico',
  props: {
    Title: String,
    tipo: String,
    name: String
  },
  components: {
    apexchart
  },

  data () {
    return {
      options: {
        chart: {
          id: name
        },
        xaxis: {
          categories: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
        },
        theme: {
          palette: 'palette1',
          monochrome: {
            enabled: true,
            color: '#424242',
            shadeTo: 'dark',
            shadeIntensity: 1
          }
        }
      },
      series: [{
        name: 'series-1',
        data: [121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52, 121532.52]
      }]
    }
  }
}
</script>
<style scoped>
</style>
