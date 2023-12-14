<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body p-0">
          <section v-if="keep" class="row">

            <div class="col-12 col-md-5">
              <img class="img-fluid keep-img" :src="keep.img" :alt="keep.name">
            </div>

            <div class="col-12 col-md-7 d-flex flex-column justify-content-between p-4">
              <div class="d-flex justify-content-center">
                <div class="d-flex px-1 align-items-center">
                  <i class="mdi mdi-eye-outline px-1 fs-4"></i>
                  <p>{{ keep.views }}</p>
                </div>
                <div class="d-flex px-1 align-items-center">
                  <i class="mdi mdi-alpha-k-box-outline px-1 fs-4"></i>
                  <p>{{ keep.kept }}</p>
                </div>
              </div>

              <div class="text-center py-3 py-md-0">
                <h1 class="pb-2">{{ keep.name }}</h1>
                <p>{{ keep.description }}</p>
              </div>

              <div class="d-flex justify-content-between align-items-center py-1">
                <div class="dropdown-center">
                  <button v-if="myVaults.length" class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown"
                    aria-expanded="false">
                    SAVE TO VAULT
                  </button>
                  <ul class="dropdown-menu scroll">
                    <li v-for="vault in myVaults" :key="vault.id">
                      <button @click="addKeepToVault(vault, keep)" class="dropdown-item"><img class="dropdown-img me-2"
                          :src="vault.img" alt="vault image">{{
                            vault.name }}</button>
                    </li>
                  </ul>
                </div>

                <div @click="goToProfile()" class="d-flex align-items-center" type="button">
                  <img class="rounded-circle profile-img px-1" :src="keep.creator.picture" :alt="keep.creator.name">
                  <p class="px-1">{{ keep.creator.name }}</p>
                </div>

              </div>

            </div>
          </section>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import Pop from "../utils/Pop";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";

export default {
  setup() {
    const router = useRouter();
    return {
      keep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),
      async addKeepToVault(vault, keep) {
        try {
          const confirmAdd = await Pop.confirm('Would you like to save this keep?', `${keep.name} would be added to the "${vault.name}" vault`, 'Save', 'question')
          if (!confirmAdd) {
            return
          }
          const vaultKeepData = { vaultId: vault.id, keepId: keep.id }
          await vaultKeepsService.addKeepToVault(vaultKeepData)
          keep.kept++
          Pop.toast(`Keep added to ${vault.name}!`, 'success', 'top', 1500, false)
        }
        catch (error) {
          Pop.error(error)
        }
      },
      goToProfile() {
        try {
          Modal.getOrCreateInstance('#keepModal').hide()
          router.push({ name: 'Profile', params: { profileId: this.keep.creatorId } });
        }
        catch (error) {
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
p {
  margin-bottom: 0;
}

.profile-img {
  height: 5vh;
}

.keep-img {
  background-position: center;
  background-size: cover;
  height: 100%;
  border-top-left-radius: 0.375rem;
  border-top-right-radius: 0.375rem;
}

.dropdown-img {
  width: 5rem;
  border-radius: .375rem;
  aspect-ratio: 1/1;
  object-fit: cover;
}

.scroll {
  height: 30vh;
  overflow-y: scroll;
}

@media (min-width: 768px) {
  .keep-img {
    border-top-left-radius: 0.375rem;
    border-bottom-left-radius: 0.375rem;
    border-top-right-radius: 0px;
  }
}
</style>