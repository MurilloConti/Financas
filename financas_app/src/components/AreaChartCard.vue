<template>
<div>
    <b-overlay :show="show" opacity="0.9" rounded="sm">
      <GChart
      style="min-height:550px"
       type="AreaChart"
       :data="chartData"
       :options="chartOptions"/>
    </b-overlay>
  </div>
</template>

<script>
import { GChart } from 'vue-google-charts'
import { getOperationsFromStock } from '../utils/firebaseUtils'
import { stringfyDateUTC, stringfyDate } from '../utils/dateUtils'
import axios from 'axios'
import { API_URL, ALPHAVANTAGE_KEY } from '../constants/config'
export default {
  name: 'AreaChart',
  props: {
    Stock: Object
  },
  components: {
    GChart
  },
  data () {
    return {
      chartData: [],
      show: true,
      chartOptions: {
        chart: {
          title: 'Company Performance',
          subtitle: 'Sales, Expenses, and Profit: 2014-2017'
        },
        colors: ['#69F0AE', '#5E35B1']
      }
    }
  },
  methods: {
    getOperations () {
      getOperationsFromStock(this.$store.state.User.currentUser, this.Stock).then((resp) => {
        let data = []
        data.push(['Data', 'Valor investido', 'Saldo bruto'])
        this.getHistoryQuot().then((history) => {
          let totalQtd = 0
          resp.forEach(element => {
            totalQtd += Number(element.Qtd)
            let datestring = stringfyDateUTC(element.dtOperation)
            let value = this.getValueFromHistory(history, datestring)
            data.push([stringfyDate(element.dtOperation), Number(element.Cost * totalQtd), Number(value * totalQtd)])
          })
          this.show = false
          this.chartData = data
        })
      })
        .catch((e) => {
        })
    },
    getHistoryQuot () {
      return new Promise((resolve, reject) => {
        let url = API_URL + '?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=' + this.Stock.Code + '&apikey=' + ALPHAVANTAGE_KEY
        axios.get(url).then((response) => {
          let a = response.data['Monthly Adjusted Time Series']
          resolve(a)
        },
        (error) => { reject(error) })
      })
    },
    getValueFromHistory (history, dateToFind) {
      let dateFound = Object.keys(history).find(x => x.substring(0, 7) === dateToFind.substring(0, 7))
      return Number(history[dateFound]['5. adjusted close'])
    }
  },
  watch: {
    Stock: function (val) {
      this.getOperations()
      this.getHistoryQuot()
    }
  }
}
</script>
<style scoped>
.apexcharts-tooltip {
  color: #424242;
}
</style>
