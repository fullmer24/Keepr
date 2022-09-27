<template>
  <div class="row">
    <div class="col-md-3" v-for="k in keeps" :key="k.id">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';

export default {
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


<style >

</style>
