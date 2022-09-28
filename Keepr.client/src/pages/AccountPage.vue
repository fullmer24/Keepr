<template>
  <!-- NOTE  profile info-->
  <div class="row p-4 m-2">
    <img class="col-4 img" :src="account.picture" alt="">
    <div class="col-7 ms-3">
      <h1 class="p-2 mt-4"><b>{{account.name}}</b></h1>
      <h3>Vaults: </h3>
      <h3>Keeps: </h3>
    </div>
  </div>
  <!-- NOTE vaults -->
  <div class="row p-2">
    <h1 class="p-2">Vaults<button class="mdi mdi-plus p-2 ms-4"></button></h1>
  </div>
  <!-- NOTE keeps -->
  <div class="row p-2">
    <h1 class="p-2">Keeps<button class="mdi mdi-plus p-2 ms-4"></button></h1>
    <div class="row">
      <!-- FIXME keeps not rendering -->
      <div class="col-6 col-md-3 my-4 p-4" v-for="k in keeps" :key="k.id">
        <!-- <div v-if="account.id == keep?.creatorId"> -->
        <KeepCard :keep="k" />
        {{keep}}
        <!-- </div> -->
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
export default {
  name: "Account",
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        logger.log(error);
      }
    }
    onMounted(() => {
      getKeeps();
    });
    return {
      keep: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped>
img {
  max-width: 250px;
}
</style>
