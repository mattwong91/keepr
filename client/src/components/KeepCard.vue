<template>
  <section @click="openKeep()" type="button" class="row m-1 rounded card-img"
    :style="`background-image: url(${keep.img})`">
    <div v-if="keep.creatorId == account.id && !isVaultPage" class="text-end delete">
      <button @click.stop="deleteKeep()" class="rounded-circle bg-danger border-0 text-white mdi mdi-close"
        title="Delete Keep"></button>
    </div>
    <div class="col-12 d-flex justify-content-between align-items-center">
      <div class="text-white fw-bold">
        {{ keep.name }}
      </div>
      <div v-if="isHomePage">
        <img @click.stop="goToProfile()" class="rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
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
      goToProfile() {
        try {
          router.push({ name: 'Profile', params: { profileId: keep.creator.id } });
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
      })
    }
  }
};
</script>


<style lang="scss" scoped>
.card-img {
  background-position: center;
  background-size: cover;
  width: 100%;
  height: auto;
  position: relative;
}

.delete {
  position: absolute;
  top: -10%;
  right: -5%;
}
</style>