<template>
  <div class="row">
    <div class="col-6 col-md-3 my-2" v-for="k in keeps" :key="k.id">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';

export default {
  name: "Home",
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
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
};
</script>


<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
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
