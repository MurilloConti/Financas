<template>
  <div class="register">
        <b-container fluid style="height:100vh;">
    <b-row class="h-100">
            <b-col class="banner">
            </b-col>
            <b-col class="h-100">
                <b-row class="h-100 align-items-center justify-content-center">
                    <b-col cols="6">
                        <p>Cadastro de usuario</p>
                         <b-input-group class="mb-3">
                          <template v-slot:prepend>
                            <b-input-group-text ><i class="fas fa-user"></i></b-input-group-text>
                          </template>
                            <b-form-input v-model="form.name" placeholder="Apelido"></b-form-input>
                        </b-input-group>
                        <b-input-group class="mb-3">
                          <template v-slot:prepend>
                            <b-input-group-text ><i class="fas fa-envelope"></i></b-input-group-text>
                          </template>
                            <b-form-input v-model="form.email" placeholder="Email"></b-form-input>
                        </b-input-group>
                        <b-input-group class="mb-3">
                           <template v-slot:prepend>
                            <b-input-group-text ><i class="fas fa-unlock-alt"></i></b-input-group-text>
                          </template>
                            <b-form-input type="password" v-model="form.password" placeholder="Senha"></b-form-input>
                        </b-input-group>
                        <b-button block clas="botao" v-on:click="submit ()">Cadastrar</b-button>
                    </b-col>
                </b-row>
            </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import firebase from 'firebase'
export default {
  data () {
    return {
      form: {
        name: '',
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
        .createUserWithEmailAndPassword(this.form.email, this.form.password)
        .then(data => {
          data.user
            .updateProfile({
              displayName: this.form.name
            })
            .then(() => {
              alert('cadastro efetuado com sucesso!')
              this.form.email = ''
              this.form.password = ''
              this.$router.push('CreateWallet')
            })
        })
        .catch(err => {
          this.error = err.message
          alert(err.message)
        })
    }
  }
}
</script>

<style>
.banner {
  background: url('../assets/capas-posts-economia-9.jpg') no-repeat right;
  background-size: cover;
}
</style>
