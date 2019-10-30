<template>
  <div class="dashboard container">
    <div class="row mt-3">
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Patrimonio total</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">0</p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >Lucro no mês {{ new Date().getMonth() + 1 }}/{{ new Date().getFullYear() }}</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text"></p>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card text-center cardCorpo">
          <div
            class="card-header cardHeader"
          >Ganho em relação ultimo periodo {{ new Date().getMonth() }}/{{ new Date().getFullYear() }}</div>
          <div class="card-body">
            <p style="font-size: 22px" class="card-text">{{this.total}}</p>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-6">
          <bargrafico title="Evoluçao de patrimonio" tipo="bar"></bargrafico>
      </div>
      <div class="col">

      </div>
    </div>
    <div class="row mt-3">
      <div class="col">
        <div class="card text-center cardCorpo">
          <div class="card-header cardHeader">Desempenho de fundos</div>
          <div class="card-body">
            <table class="table table-light" style="font-size: 0.8rem">
              <thead>
                <tr>
                  <th scope="col">Status</th>
                  <th scope="col">Fundo</th>
                  <th scope="col">Valorização</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>
                    <i></i>
                  </td>
                  <td>Daycoval Debêntures Incentiva FIM C Priv</td>
                  <td>3.25%</td>
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
            <p style="font-size: 22px" class="card-text">R$20.000,00</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import bargrafico from '@/components/BarGrafico.vue'
export default {
  name: 'dashboard',
  components: {
    bargrafico
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
  color: #ffffff;
}
</style>
