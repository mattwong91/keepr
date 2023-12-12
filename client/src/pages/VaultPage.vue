<template>
  <div class="container-fluid">
    <section class="row justify-content-center my-3">
      <div class="col-6">
        <div v-if="vault" class="d-flex justify-content-center align-items-end vault-img rounded"
          :style="`background-image: url(${vault.img})`">
          <div class="text-center pb-4 text-white">
            <h2>{{ vault.name }}</h2>
            <p>by {{ vault.creator.name }}</p>
          </div>
        </div>
        <div class="text-end my-1">
          <button class="btn btn-dark rounded-pill">...</button>
        </div>
        <div class="text-center d-flex justify-content-center">
          <p class="rounded-pill text-white bg-dark p-1">{{ keeps.length }} Keeps</p>
        </div>
      </div>
    </section>

    <section class="row m-2">
      <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-4 col-lg-3">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router";
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import KeepCard from "../components/KeepCard.vue";
import { keepsService } from "../services/KeepsService";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const watchableVaultId = route.params.vaultId;

    watch(watchableVaultId, () => {
      keepsService.clearKeeps();
      getVaultById();
      getKeepsInVault();
    }, { immediate: true });

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId;
        await vaultsService.getVaultById(vaultId);
      }
      catch (error) {
        Pop.error(error);
        if (error.response.data.includes('ACCESS DENIED')) {
          router.push({ name: 'Home' })
        }
      }
    }

    async function getKeepsInVault() {
      try {
        const vaultId = route.params.vaultId;
        await vaultKeepsService.getKeepsInVault(vaultId);
      }
      catch (error) {
        Pop.error(error);
      }
    }

    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.vault-img {
  background-position: center;
  background-size: cover;
  height: 30vh;
}
</style>