<template>
  <header class="sticky-top">
    <Navbar />
  </header>
  <main class="">
    <router-view />
  </main>
  <KeepModal></KeepModal>
  <CreateKeepModal></CreateKeepModal>
  <CreateVaultModal></CreateVaultModal>
  <EditProfileOffCanvas></EditProfileOffCanvas>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepModal from "./components/KeepModal.vue"
import CreateKeepModal from "./components/CreateKeepModal.vue"
import CreateVaultModal from "./components/CreateVaultModal.vue"
import EditProfileOffCanvas from "./components/EditProfileOffCanvas.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal, CreateKeepModal, CreateVaultModal, EditProfileOffCanvas }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
