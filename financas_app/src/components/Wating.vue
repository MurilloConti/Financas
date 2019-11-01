<template>
  <div v-if="visible === 'true'">
    <div class="dvWait">
      <i
        class="fas fa-redo-alt fa-spin"
        style="left: 50%;position: absolute;top: 50%;font-size: 70px;color: #ffffff"
      ></i>
    </div>
  </div>
</template>
<script>
export default {
  name: 'dvWait',
  props: {
    visible: Boolean
  }
}
</script>
<style scoped>
.dvWait {
  display: block;
  position: fixed;
  z-index: 1003;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  background: rgba(4, 146, 146, 0.651);
}
</style>
