<template>
  <h1>Vault Page!</h1>
</template>


<script>
import { useRoute } from "vue-router";
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

export default {
  setup() {
    const route = useRoute()
    const watchableVaultId = route.params.vaultId
    watch(watchableVaultId, () => {
      getVaultById();
      getKeepsInVault();
    }, { immediate: true });

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId
        await vaultsService.getVaultById(vaultId)
      }
      catch (error) {
        Pop.error(error)
      }
    }

    async function getKeepsInVault() {
      try {
        const vaultId = route.params.vaultId
        await vaultKeepsService.getKeepsInVault(vaultId)
      }
      catch (error) {
        Pop.error(error)
      }
    }
    return {
      vault: computed(() => AppState.activeVault)
    }
  }
};
</script>


<style lang="scss" scoped></style>