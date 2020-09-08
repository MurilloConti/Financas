<template>
  <div class="login">
    <b-container fluid style="height:100vh;">
    <b-row class="h-100">
            <b-col class="banner">
            </b-col>
            <b-col class="h-100">
                <b-row class="h-100 align-items-center justify-content-center">
                    <b-col cols="6">
                        <p>LOGIN</p>
                        <b-input-group class="mb-3">
                          <template v-slot:prepend>
                            <b-input-group-text ><i class="fas fa-user"></i></b-input-group-text>
                          </template>
                            <b-form-input v-model="form.email" placeholder="Email"></b-form-input>
                        </b-input-group>
                        <b-input-group class="mb-3">
                           <template v-slot:prepend>
                            <b-input-group-text ><i class="fas fa-unlock-alt"></i></b-input-group-text>
                          </template>
                            <b-form-input type="password" v-model="form.password" placeholder="Senha"></b-form-input>
                        </b-input-group>
                        <b-button block clas="botao" v-on:click="submit ()">Entrar</b-button>
                        <b-row>
                            <b-col>
                              <b-button variant="Link" v-on:click="goToRegister()">Cadastrar</b-button>
                            </b-col>
                        </b-row>
                        <b-row v-if="error">
                          <b-col class="mt-4">
                            <b-alert show variant="danger" fade dismissible class="text-center">{{error}}</b-alert>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-col>
      </b-row>
    </b-container>
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
        .then(user => {
          this.$store.dispatch('setCurrentUser', user.user.uid)
          const userInfo = {
            displayName: user.user.displayName,
            email: user.user.email
          }
          this.$store.dispatch('setUserProfile', userInfo)
          this.$router.push('dashboard')
        })
        .catch(err => {
          this.error = err.message
        })
    },
    goToRegister: function () {
      this.$router.push('register')
    }
  },
  created () {
    if (this.$store.state.User.currentUser) {
      this.$router.push('dashboard')
    }
  }
}
</script>
<style scoped>
 .banner {
            background: url('../assets/capas-posts-economia-9.jpg') no-repeat right;
            background-size: cover;
        }

  .botao {
    background: #424242 !important;
  }
  .pointer {
    cursor: pointer;

  }
</style>
