<template>
  <!-- NOTE  account info-->
  <div class="row p-4 m-2">
    <img class="col-4 img" :src="account.picture" alt="">
    <div class="col-7 ms-3">
      <h1 class="p-2 mt-4"><b>{{account.name}}</b></h1>
      <h3>Vaults: {{vaults.length}} </h3>
      <h3>Keeps: {{keeps.length}}</h3>
    </div>
  </div>
  <!-- NOTE vaults -->
  <!-- Add modal, and form-->
  <div class="row p-2">
    <h1 class="p-2">Vaults<button data-bs-toggle="modal" data-bs-target="#vaultFormModal"
        class="mdi mdi-plus p-2 ms-4"></button></h1>
  </div>
  <div class="row">
    <!-- FIXME vaults not getting accessed -->
    <div class="col-6 col-mdm-3 my-4 p-4" v-for="v in vaults" :key="v.id">
      <div v-if="account.id == v?.creatorId">
        <VaultCard :vault="v" />
      </div>
    </div>

  </div>
  <!-- NOTE keeps -->
  <div class="row p-2">
    <!-- NOTE add modal and form -->
    <h1 class="p-2">Keeps<button data-bs-toggle="modal" data-bs-target="#keepFormModal"
        class="mdi mdi-plus p-2 ms-4"></button></h1>
    <div class="row">
      <!-- FIXME keeps not rendering and v-if not working-->
      <div class="col-6 col-md-3 my-4 p-4" v-for="k in keeps" :key="k.id">
        <div v-if="account.id == k?.creatorId">
          <KeepCard :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import VaultCard from '../components/VaultCard.vue';
export default {
  name: "Account",
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeepsByProfileId(id);
        await vaultsService.getAccountVaults();
      }
      catch (error) {
        logger.error(error.message);
      }
    }
    onMounted(() => {
      getKeeps();
    });
    return {
      keeps: computed(() => AppState.myKeeps),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
    };
  },
  components: { KeepCard, VaultCard }
}
</script>

<style scoped lang="scss">
img {
  max-width: 250px;
}

.account {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .account-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
