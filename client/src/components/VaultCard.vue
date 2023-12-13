<template>
  <section @click="goToVault()" type="button" class="row m-1 rounded card-img"
    :style="`background-image: url(${vault.img})`">
    <div v-if="vault.creatorId == account.id">
      <button @click.stop="deleteVault()" class="rounded-circle bg-danger border-0 text-white mdi mdi-close"
        title="Delete Vault"></button>
    </div>
    <div v-if="vault.isPrivate" class="d-flex flex-column justify-content-between">
      <div class="col-12 d-flex justify-content-end text-white pt-1">
        <div class="lock-circle">
          <i class="mdi mdi-lock fs-4"></i>
        </div>
      </div>
      <div class="col-12 text-start text-white fw-bold">
        <p>{{ vault.name }}</p>
      </div>
    </div>
    <div v-else class="d-flex align-items-end">
      <div class="col-12 text-start text-white fw-bold">
        <p>{{ vault.name }}</p>
      </div>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from "../models/Vault";
import { useRouter } from "vue-router";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
export default {
  props: {
    vault: { type: Vault, required: true }
  },
  setup(props) {
    const router = useRouter()
    const vault = props.vault
    return {
      goToVault() {
        router.push({ name: 'Vault', params: { vaultId: vault.id } })
      },
      async deleteVault() {
        try {
          const confirmDelete = await Pop.confirm('Are you sure you want to delete this Vault?', 'It will be gone forever!', 'Delete!', 'warning')
          if (!confirmDelete) {
            return
          }
          vaultsService.deleteVaultById(vault.id)
          Pop.toast('Vault deleted', 'info', 'top', 1500, false)
        }
        catch (error) {
          Pop.error(error)
        }
      },
      account: computed(() => AppState.account)
    }
  }
};
</script>


<style lang="scss" scoped>
p {
  margin-bottom: 0;
}

.card-img {
  background-position: center;
  background-size: cover;
  width: 100%;
  height: 20vh;
}

.lock-circle {
  height: 2rem;
  width: 2rem;
  background-color: rgba(0, 0, 0, 0.5);
  backdrop-filter: blur(3px);
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>