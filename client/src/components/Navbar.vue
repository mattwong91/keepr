<template>
  <nav class="navbar bg-light px-3 nav-border">

    <div class="d-flex align-items-center">
      <router-link :to="{ name: 'Home' }">
        <button class="btn rounded-pill btn-dark py-1 me-2">Home</button>
      </router-link>
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
    <div>
      <p class="logo ps-1 pe-4">the<br>keepr<br>co.</p>
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
