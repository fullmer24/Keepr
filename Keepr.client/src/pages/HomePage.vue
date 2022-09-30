<template>
  <div class="masonry-with-columns mt-4">
    <div v-for="k in keeps" :key="k.id">
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
.masonry-with-columns {
  columns: 4 100px;
  column-gap: 1rem;
  width: 100%;
  padding-left: 4px;
  padding-right: 8px;


  div {
    width: 150px;
    background: #EC985A;
    background-size: contain;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }
}
</style>
