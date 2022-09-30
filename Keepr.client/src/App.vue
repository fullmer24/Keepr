<template>
  <header>
    <Navbar />
  </header>
  <main class="main-body container-fluid bg-white">
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue';
import { AppState } from './AppState';

export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
.main-body {
  width: 100vw;
}

@import "./assets/scss/main.scss";
</style>
