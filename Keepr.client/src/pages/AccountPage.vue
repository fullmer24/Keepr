<template>
  <!-- NOTE  profile info-->
  <div class="row p-4 m-4">
    <img class="col-4 img" :src="account.picture" alt="">
    <div class="col-7">
      <h1 class="p-2 mt-4"><b>{{account.name}}</b></h1>
      <h3>Vaults: </h3>
      <h3>Keeps: </h3>
    </div>
  </div>
  <!-- NOTE vaults -->
  <div class="row p-2"></div>
  <!-- NOTE keeps -->
  <div class="row p-2"></div>
  <!-- <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div> -->
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 250px;
}
</style>
