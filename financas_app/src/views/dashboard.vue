<template>
  <div class="dashboard">
  <Vheader Title="DashBoard"></Vheader>
      <div class="container">
    <div class="row mt-3">
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Patrimonio total</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">{{Patrimonio}}</p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >Lucro no mês {{ new Date().getMonth() + 1 }}/{{ new Date().getFullYear() }}</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">{{LucroMes}}</p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >ultimo periodo {{ new Date().getMonth() }}/{{ new Date().getFullYear() }}</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">{{GanhoPeriodo}}</p>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-6">
        <bargrafico title="Evoluçao de patrimonio"></bargrafico>
      </div>
      <div class="col">
        <pierafico title="Distribuição de recursos"></pierafico>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Desempenho de fundos</div>
          <div class="card-body">
            <table class="table table-striped" style="font-size: 0.8rem">
              <thead>
                <tr>
                  <th scope="col">Status</th>
                  <th scope="col">Fundo</th>
                  <th scope="col">Valorização</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="fundo in Fundos" :key="fundo">
                  <td class="p-0">
                    <i></i>
                  </td>
                  <td class="p-0">{{fundo}}</td>
                  <td class="p-0">3.25%</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Lucro no mês</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">- R$14.300,00</p>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script>
// @ is an alias to /src
import bargrafico from '@/components/BarGrafico.vue'
import pierafico from '@/components/PieChart.vue'
import Vheader from '@/components/Vheader.vue'
export default {
  name: 'dashboard',
  components: {
    bargrafico,
    pierafico,
    Vheader
  },
  data () {
    return {
      Patrimonio: 999999.99,
      LucroMes: 9999.99,
      GanhoPeriodo: '-3,25%',
      Fundos: [
        'Daycoval Debêntures Incentiva FIM C Priv',
        'LCI/DAYCOVAL 97.5% CDI',
        'LCI/DAYCOVAL 100% CDI',
        'Constância Fundamento FIA',
        'Daycoval Ibovespa Ativo FIA',
        'ALASKA BLACK Institucional FIA',
        'Constellation Institucional FIA'
      ]
    }
  }
}
</script>

<style>
.dashboard {
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
