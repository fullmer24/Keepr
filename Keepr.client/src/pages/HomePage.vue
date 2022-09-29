<template>
  <div class="row masonry">
    <div class="col-6 col-md-3" v-for="k in keeps" :key="k.id">
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
.masonry {
  columns: 200px;
  column-gap: 1em;

  div {
    display: block;
    margin-bottom: 1em;
  }
}
</style>
