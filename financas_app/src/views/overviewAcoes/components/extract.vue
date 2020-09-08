<template>
  <div class="statement">
    <b-card no-body class="shadow-sm" style="height:80vh !important">
      <b-card-header class="text-center">
        <b-row>
          <b-col>
            <p>Extrato</p>
          </b-col>
          <b-col cols="1" class="p-2">
            <b-button pill variant="info" title="nova Movimentação" @click="addClick"><i class="fas fa-plus"></i></b-button>
          </b-col>
        </b-row>
      </b-card-header>
      <b-card-body>
        <b-overlay :show="isShown" opacity="0.9" rounded="sm">
          <b-list-group>
            <b-list-group-item v-for="operation in operations" :key="operation.index">
              <b-row>
                <b-col cols="2">
                    <b-badge pill variant="danger" v-if="operation.Type === 'V'">
                        <i class="fas fa-receipt"></i>
                    </b-badge>
                    <b-badge pill variant="success" v-else>
                        <i class="fas fa-shopping-cart"></i>
                    </b-badge>
                </b-col>
                <b-col>R$ {{ Number(operation.Cost * operation.Qtd).toFixed(2) }}</b-col>
                <b-col cols="3">
                    <small>{{stringFyDate(operation.dtOperation)}}</small>
                </b-col>
              </b-row>
            </b-list-group-item>
          </b-list-group>
        </b-overlay>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
import { getOperationsFromStock } from '../../../utils/firebaseUtils'
import { stringfyDate } from '../../../utils/dateUtils'
export default {
  name: 'statement',
  props: {
    Stock: Object
  },
  components: {},
  data () {
    return {
      isShown: true,
      operations: []
    }
  },
  watch: {
    Stock: function (val) {
      this.getOperations()
    }
  },
  methods: {
    getOperations () {
      getOperationsFromStock(this.$store.state.User.currentUser, this.Stock).then((resp) => {
        this.isShown = false
        this.operations = resp
      })
        .catch((e) => {
        })
    },
    addClick () {
      this.$emit('addClicked')
    },
    stringFyDate (dtOperation) {
      return stringfyDate(dtOperation)
    }
  },
  created () {
  }
}
</script>

<style>
</style>
