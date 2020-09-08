<template>
  <div class="card cardCorpo shadow-sm">
    <div class="card-header cardHeader">
     <div class="row">
      <div class="col-10" v-if="Stock">
        {{Stock.Code.split('.')[0]}}
      </div>
     </div>
    </div>
    <div class="card-body">
      <GChart
      style="height:600px"
       type="CandlestickChart"
       :data="stockState"
       :options="chartOptions"/>
    </div>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'
import axios from 'axios'
import { API_URL, ALPHAVANTAGE_KEY } from '../constants/config'
export default {
  name: 'AcaoCard',
  props: {
    Stock: {
      type: Object,
      required: true
    }
  },
  components: {
    GChart
  },
  data () {
    return {
      chartOptions: {
        legend: 'none',
        candlestick: {
          fallingColor: { strokeWidth: 0, fill: '#E53935' },
          risingColor: { strokeWidth: 0, fill: '#00E676' } // green
        },
        colors: ['#BDBDBD']
      }
    }
  },
  computed: {
    stockState () {
      let arr = ['Data', 'low', 'open', 'close', 'high']
      arr = this.GetCotacao(arr)
      return arr
    }
  },
  methods: {
    GetCotacao: function (arr) {
      if (this.Stock) {
        arr = []
        let url = API_URL + '?function=TIME_SERIES_DAILY_ADJUSTED&symbol=' + this.Stock.Code + '&apikey=' + ALPHAVANTAGE_KEY
        axios.get(url).then((response) => {
          let a = response.data['Time Series (Daily)']
          if (Object.keys(a)) {
            Object.keys(a).forEach(element => {
              let low = a[element]['3. low']
              let open = a[element]['1. open']
              let close = a[element]['4. close']
              let high = a[element]['2. high']
              let obj = [
                String(element),
                Number(low),
                Number(open),
                Number(close),
                Number(high)
              ]
              arr.push(obj)
            })
          }
          arr.push(['Data', 'low', 'open', 'close', 'high'])
          arr = arr.reverse()
        },
        (error) => { alert(error) })
      }
      return arr
    }
  },
  created () {
    this.GetCotacao()
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
</style>
