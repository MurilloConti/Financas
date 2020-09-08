<template>
    <div class="Details">
        <b-overlay :show="isShown" opacity="0.9" rounded="sm">
            <b-card no-body class="shadow-sm mt-2" style="height:10vh">
                <b-row v-if="Stock != null" class="h-100 align-items-center justify-content-center">
                    <b-col style="border-right: 2px solid #424242;">
                        <h5 class="mb-1">
                            <strong>{{ Stock.Qtd }}</strong>
                        </h5>
                        <small>Em carteira</small>
                    </b-col>
                    <b-col style="border-right: 2px solid #424242;">
                        <h5 class="mb-1">
                            <strong>R$ {{Stock.Price}}</strong>
                        </h5>
                        <small>Cotação</small>
                    </b-col>
                    <b-col>
                        <h5 class="mb-1">
                            <strong>
                            3.53%
                            <i class="fas fa-long-arrow-alt-down" style="color:red"></i>
                            </strong>
                        </h5>
                        <small>Variação</small>
                    </b-col>
                </b-row>
                 <b-card-body v-else class="h-100 align-items-center justify-content-center">
                  <i class="far fa-folder-open" style="font-size:40px; color: #E0E0E0"></i>
                  <p>Nada para mostrar aqui.</p>
                </b-card-body>
            </b-card>
        </b-overlay>
    </div>
</template>

<script>
export default {
  name: 'Details',
  props: {
    Stock: Object
  },
  components: {},
  data () {
    return {
      isShown: false,
      operations: []
    }
  },
  // watch: {
  //   Stock: function (val) {
  //     this.getOperations()
  //   }
  // },
  methods: {
  }
}
</script>

<style>
</style>
