<template>
  <nav class="navbar bg-white px-3 nav-border">

    <div class="d-flex align-items-center">
      <div class="dropdown">
        <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu">
          <li>
            <button class="dropdown-item" data-bs-toggle="modal" data-bs-target="#createKeepModal">Create Keep</button>
          </li>
          <li>
            <button class="dropdown-item" data-bs-toggle="modal" data-bs-target="#createVaultModal">Create
              Vault</button>
          </li>
        </ul>
      </div>
    </div>
    <div class="navbar-brand m-0 p-0">
      <router-link :to="{ name: 'Home' }">
        <img src="/src/assets/img/keepr-logo.png" alt="keepr logo" height="70">
      </router-link>
    </div>
    <Login />

  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { useRoute } from "vue-router";

export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')
    const route = useRoute()

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
.logo {
  border: 2px solid black;
  border-radius: 0.375rem;
}

.nav-border {
  border-bottom: 1px solid rgb(38, 100, 162);
}
</style>
