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
      series: [{
        name: 'Net Profit',
        data: [1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000]
      }],
      options: {
        chart: {
          height: 350,
          type: 'bar'
        },
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: '55%',
            endingShape: 'rounded'
          }
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          show: true,
          width: 2,
          colors: ['transparent']
        },
        xaxis: {
          categories: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
        },
        yaxis: {
          title: {
            text: 'R$ (milhares)'
          }
        },
        fill: {
          opacity: 1
        },
        tooltip: {
          y: {
            formatter: function (val) {
              return 'R$ ' + val + ' milhares'
            }
          }
        }
      }
    }
  }
}
</script>
<style scoped>
 .apexcharts-tooltip{
   color: #424242;
 }
</style>
