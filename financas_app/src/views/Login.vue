<template>
  <div class="login">
    <div class="container-fluid" style="height:80vh ;">
        <div class="row h-100">
            <div class="col-md banner">
            </div>
            <div class="col-md h-100">
                <div class="row h-100 align-items-center justify-content-center">
                    <div class="col-6">
                        <img src="https://www.netdente.com.br/images/logonovo.png" alt="" class="img-fluid mb-4">
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="basic-addon1">
                                    <i class="fas fa-user"></i>
                                </span>
                            </div>
                            <input type="text" class="form-control" placeholder="Usuario">
                        </div>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="basic-addon1">
                                    <i class="fas fa-unlock-alt"></i>
                                </span>
                            </div>
                            <input type="password" class="form-control" placeholder="Senha">
                        </div>
                        <input type="button" class="btn btn-primary form-control" value="Entrar">
                        <div class="row">
                            <div class="col-12 mt-4">
                                <div class="alert alert-danger text-center" role="alert">
                                    Usuario ou senha invalidos.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Wating visible='false'></Wating>
  </div>
</template>

<script>
import Wating from '@/components/Wating.vue'
export default {
  name: 'LogIn',
  components: {
    Wating
  }
}
</script>
