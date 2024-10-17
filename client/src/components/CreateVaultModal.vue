<template>
  <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-body">
          <section class="row">
            <div class="col-12 text-end">
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="col-12 mb-3">
              <h1 class="modal-title fs-5" id="createVaultModalLabel">Add your vault</h1>
            </div>

            <form @submit.prevent="createVault()">
              <div class="mb-3">
                <input v-model="form.name" type="text" class="form-control" id="vaultTitle" placeholder="Title..."
                  title="Title" maxlength="50" required>
              </div>
              <div class="mb-5">
                <input v-model="form.img" type="url" class="form-control" id="vaultImg" placeholder="Cover Image URL..."
                  title="Cover Image URL" maxlength="1000" required>
              </div>
              <div class="mb-3 form-check d-flex justify-content-end">
                <input v-model="form.isPrivate" class="form-check-input mx-1" type="checkbox" value="" id="isPrivate"
                  checked>
                <label class="form-check-label" for="isPrivate">Make Vault Private?</label>
              </div>
              <div class="text-end">
                <button type="submit" class="btn btn-dark">Create</button>
              </div>
            </form>
          </section>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from 'vue';
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { useRoute } from "vue-router";
import { Modal } from "bootstrap";
export default {
  setup() {
    const form = ref({ isPrivate: false })
    const route = useRoute()
    return {
      form,
      async createVault() {
        try {
          const vaultData = form.value
          const newVault = await vaultsService.createVault(vaultData)
          if (route.name == "Profile") {
            if (newVault.creatorId == route.params.profileId) {
              vaultsService.addVault(newVault)
            }
          }
          Pop.toast('Vault created!', 'success', 'top', 1500, false)
          Modal.getOrCreateInstance('#createVaultModal').hide()
          form.value = {}
        }
        catch (error) {
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped></style>