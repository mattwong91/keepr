<template>
  <section @click="openKeep()" type="button" class="row m-1 rounded card-img"
    :style="`background-image: url(${keep.img})`">
    <div v-if="keep.creatorId == account.id && !isVaultPage" class="text-end delete">
      <button @click.stop="deleteKeep()" class="delete-button d-flex justify-content-center" title="Delete Keep"><i
          class="mdi mdi-close delete-text"></i></button>
    </div>
    <div v-if="activeVault" class="delete">
      <button v-if="activeVault.creatorId == account.id && isVaultPage" @click.stop="removeKeep()"
        class="delete-button d-flex justify-content-center" title="Remove Keep"><i
          class="mdi mdi-close delete-text"></i></button>
    </div>
    <div class="d-flex flex-column justify-content-end pb-1">
      <div class="col-12 d-flex justify-content-between align-items-center">
        <div class="text-white fw-bold">
          {{ keep.name }}
        </div>
        <div v-if="isHomePage">
          <img @click.stop="goToProfile()" class="rounded-circle profile-img" :src="keep.creator.picture"
            :alt="keep.creator.name">
        </div>
      </div>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from "../models/Keep";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import { useRoute, useRouter } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { vaultsService } from "../services/VaultsService";

export default {
  props: {
    keep: { type: Keep, required: true }
  },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const keep = props.keep;
    return {
      async openKeep() {
        try {
          keepsService.clearActiveKeep();
          keepsService.getKeepById(keep.id);
          if (this.account.id) {
            vaultsService.getMyVaults();
          }
          Modal.getOrCreateInstance("#keepModal").show();
        }
        catch (error) {
          Pop.error(error);
        }
      },
      async deleteKeep() {
        try {
          const confirmDelete = await Pop.confirm('Are you sure you want to delete this Keep?', 'It will be gone forever!', 'Delete!', 'warning')
          if (!confirmDelete) {
            return
          }
          keepsService.deleteKeepById(keep.id)
          Pop.toast('Keep deleted', 'info', 'top', 1500, false)
        }
        catch (error) {
          Pop.error(error)
        }
      },
      async removeKeep() {
        try {
          const confirmRemove = await Pop.confirm('Are you sure you want to remove this Keep from your vault?', 'It will be gone forever!', 'Remove!', 'warning')
          if (!confirmRemove) {
            return
          }
          vaultKeepsService.removeKeepFromVault(keep.vaultKeepId)
          Pop.toast('Keep removed from vault', 'info', 'top', 1500, false)
        }
        catch (error) {
          Pop.error(error)
        }
      },
      goToProfile() {
        try {
          router.push({ name: 'Profile', params: { profileId: keep.creatorId } });
        }
        catch (error) {
          Pop.error(error)
        }
      },
      account: computed(() => AppState.account),
      isVaultPage: computed(() => {
        if (route.name == 'Vault') {
          return true
        }
        return false
      }),
      isHomePage: computed(() => {
        if (route.name == 'Home') {
          return true
        }
        return false
      }),
      activeVault: computed(() => AppState.activeVault)
    }
  }
};
</script>


<style lang="scss" scoped>
.card-img {
  background-position: center;
  background-size: cover;
  width: 100%;
  height: 20vh;
  position: relative;
}

.delete {
  position: absolute;
  top: -4%;
  right: -5%;
  justify-content: end;
  display: flex;
}

.delete-button {
  width: 1.2rem;
  height: 1.2rem;
  border-radius: 50%;
  background-color: rgb(214, 30, 30);
  color: white;
  border: 0px;
  justify-content: center;
  align-items: center;
}

.delete-text {
  font-size: small;
}

.profile-img {
  height: 3rem;
  width: 3rem;
}
</style>