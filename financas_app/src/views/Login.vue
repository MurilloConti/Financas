<template>
  <div class="login">
    <div class="container-fluid" style="height:100vh ;">
        <div class="row h-100">
            <div class="col-md banner">
            </div>
            <div class="col-md h-100">
                <div class="row h-100 align-items-center justify-content-center">
                    <div class="col-6">
                        <p>LOGIN</p>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="basic-addon1">
                                    <i class="fas fa-user"></i>
                                </span>
                            </div>
                            <input type="text" class="form-control" placeholder="Email" v-model="form.email">
                        </div>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="basic-addon1">
                                    <i class="fas fa-unlock-alt"></i>
                                </span>
                            </div>
                            <input type="password" class="form-control" placeholder="Senha" v-model="form.password">
                        </div>
                        <input type="button" class="btn btn-primary form-control" value="Entrar" v-on:click="submit ()">
                        <div class="row">
                            <div class="col-12">
                                <a>Cadastrar</a>
                            </div>
                            </div>
                        <div class="row" v-if="error">
                            <div class="col-12 mt-4">
                                <div class="alert alert-danger text-center" role="alert">
                                  {{error}}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Wating :visible='false'></Wating>
  </div>
</template>

<script>
import Wating from '@/components/Wating.vue'
import firebase from 'firebase'
export default {
  name: 'LogIn',
  components: {
    Wating
  },
  data () {
    return {
      form: {
        email: '',
        password: ''
      },
      error: null
    }
  },
  methods: {
    submit () {
      firebase
        .auth()
        .signInWithEmailAndPassword(this.form.email, this.form.password)
        .then(data => {
          this.$router.push('dashboard')
        })
        .catch(err => {
          this.error = err.message
        })
    },
    goToRegister: function () {
      this.$router.push('dashboard')
    }
  }
}
</script>
<style scoped>
 .banner {
            background: url(https://www.netdente.com.br/images/banner_home_003.jpg) no-repeat right;
            background-size: cover;
        }
</style>
