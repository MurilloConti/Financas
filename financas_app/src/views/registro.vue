<template>
  <div class="register">
    <div class="container-fluid" style="height:100vh ;">
      <div class="row h-100">
        <div class="col-md banner"></div>
        <div class="col-md h-100">
          <div class="row h-100 align-items-center justify-content-center">
            <div class="col-6">
              <h2>Cadastro de usuario.</h2>
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="basic-addon1">
                    <i class="fas fa-user"></i>
                  </span>
                </div>
                <input type="text" class="form-control" placeholder="Apelido" v-model="form.name"  required autofocus />
              </div>
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="basic-addon1">
                    <i class="fas fa-envelope"></i>
                  </span>
                </div>
                <input type="text" class="form-control" placeholder="Email" required v-model="form.email" />
              </div>
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text" id="basic-addon1">
                    <i class="fas fa-unlock-alt"></i>
                  </span>
                </div>
                <input type="password" class="form-control" placeholder="Senha" required v-model="form.password" />
              </div>
              <input
                type="submit"
                class="btn btn-primary form-control"
                value="Cadastrar"
                v-on:click="submit()"
              />
              <!-- <div class="row">
                            <div class="col-12 mt-4">
                                <div class="alert alert-danger text-center" role="alert">
                                    Usuario ou senha invalidos.
                                </div>
                            </div>
              </div>-->
            </div>
          </div>
        </div>
      </div>
    </div>
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
      console.log(this.form.email)
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
  background: url(https://www.netdente.com.br/images/banner_home_003.jpg)
    no-repeat right;
  background-size: cover;
}
</style>
