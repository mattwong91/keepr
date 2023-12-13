<template>
  <div class="offcanvas offcanvas-start" tabindex="-1" id="editOffCanvas" aria-labelledby="editOffCanvasLabel">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="editOffCanvasLabel">Edit Profile</h5>
      <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
    </div>
    <div v-if="profile" class="offcanvas-body text-center">
      <div class="mb-3">
        <img class="rounded-circle" :src="profile.picture" :alt="profile.name">
        <p>{{ profile.name }}</p>
      </div>
      <form @submit.prevent="editProfile()" class="text-start">
        <div class="mb-3">
          <label for="Name" class="form-label">Name</label>
          <input v-model="form.name" type="text" class="form-control" id="Name" aria-describedby="emailHelp"
            maxlength="255" :placeholder="profile.name">
        </div>
        <div class="mb-3">
          <label for="Picture" class="form-label">Profile Picture</label>
          <input v-model="form.picture" type="url" class="form-control" id="Picture" placeholder="Image URL...">
        </div>
        <div class="mb-3">
          <label for="CoverImage" class="form-label">Cover Image</label>
          <input v-model="form.coverImg" type="url" class="form-control" id="CoverImage" placeholder="Cover Image URL...">
        </div>
        <div class="text-end">
          <button type="submit" class="btn btn-primary">Save changes</button>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop";
import { profilesService } from "../services/ProfilesService";
import { Offcanvas } from "bootstrap";
export default {
  setup() {
    const form = ref({})
    return {
      profile: computed(() => AppState.activeProfile),
      form,
      async editProfile() {
        try {
          const profileData = form.value
          const profileId = this.profile.id
          await profilesService.editProfile(profileId, profileData)
          Pop.toast('Profile updated!', 'success', 'top', 1500, false)
          Offcanvas.getOrCreateInstance('#editOffCanvas').hide()
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