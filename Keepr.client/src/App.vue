<template>
  <header>
    <Navbar />
  </header>
  <main class="main-body container-fluid bg-white">
    <router-view />
  </main>

  <KeepForm />
  <VaultForm />
</template>

<script>
import { computed } from 'vue';
import { AppState } from './AppState';
import KeepForm from './components/KeepForm.vue';
import VaultForm from './components/VaultForm.vue';

export default {
  name: "App",
  setup() {
    return {
      appState: computed(() => AppState)
    };
  },
  components: { KeepForm, VaultForm }
}
</script>
<style lang="scss">
.main-body {
  width: 100vw;
}

@import "./assets/scss/main.scss";
</style>
