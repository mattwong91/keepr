<template>
  <div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-body">
          <section class="row">
            <div class="col-12 text-end">
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="col-12 mb-3">
              <h1 class="modal-title fs-5" id="createKeepModalLabel">Add your keep</h1>
            </div>

            <!-- SECTION FORM -->
            <form @submit.prevent="createKeep()">
              <div class="mb-3">
                <input v-model="form.name" type="text" class="form-control" id="title" placeholder="Title..."
                  title="Title" maxlength="50" required>
              </div>
              <div class="mb-3">
                <input v-model="form.img" type="url" class="form-control" id="img" placeholder="Image URL..."
                  title="Image URL" maxlength="1000" required>
              </div>
              <div class="mb-3">
                <textarea v-model="form.description" class="form-control description-box" name="description"
                  id="description" placeholder="Keep Description..." title="Description" maxlength="500"
                  required></textarea>
              </div>
              <div class="text-end">
                <button type="submit" class="btn btn-dark">Create</button>
              </div>
            </form>
            <!-- !SECTION FORM -->

          </section>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
import { useRoute } from "vue-router";

export default {
  setup() {
    const form = ref({})
    const route = useRoute()
    const account = computed(() => AppState.account)
    return {
      async createKeep() {
        try {
          const keepData = form.value
          const newKeep = await keepsService.createKeep(keepData)
          switch (route.name) {
            case 'Profile':
              if (newKeep.creatorId == route.params.profileId) {
                keepsService.addKeep(newKeep)
              }
              break;
            case 'Home':
              keepsService.addKeep(newKeep)
              break;
          }
          Pop.toast('Posted your keep!', 'success', 'top', 1500, false)
          Modal.getOrCreateInstance('#createKeepModal').hide()
          form.value = {}
        }
        catch (error) {
          Pop.error(error)
        }
      },
      form
    }
  }
};
</script>


<style lang="scss" scoped>
.description-box {
  height: 30vh;
}
</style>