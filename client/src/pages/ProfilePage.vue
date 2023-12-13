<template>
  <div class="container-fluid text-center px-md-5">

    <section v-if="profile" class="row justify-content-center pt-2">
      <div class="col-12 col-md-8 p-2">
        <div class="d-flex flex-column text-center">
          <img v-if="profile.coverImg" class="img-fluid cover-img" :src="profile.coverImg" alt="Cover image">
          <div class="pt-2">
            <img class="rounded-circle profile-img" :src="profile.picture" :alt="profile.name">
          </div>
        </div>
      </div>
      <div class="col-12">
        <div v-if="account" class="dropdown">
          <button v-if="account.id == profile.id" class="btn" type="button" data-bs-toggle="dropdown"
            aria-expanded="false" title="options">
            ...
          </button>
          <ul class="dropdown-menu">
            <li><button class="dropdown-item" data-bs-toggle="offcanvas" data-bs-target="#editOffCanvas"
                aria-controls="editOffCanvas">Edit Profile</button></li>
          </ul>
        </div>
      </div>
      <div class="col-12">
        <h1>{{ profile.name }}</h1>
        <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
      </div>
    </section>

    <section class="row p-2 p-md-5">
      <div class="col-12 text-start">
        <h2>Vaults</h2>
      </div>
      <div v-for="vault in vaults" :key="vault.id" class="col-6 col-md-4 col-lg-3 col-xl-2">
        <VaultCard :vault="vault" />
      </div>
    </section>

    <section class="row p-2 p-md-5">
      <div class="col-12 text-start">
        <h2>Keeps</h2>
      </div>
      <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-4 col-lg-3">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>


<script>
import { useRoute } from "vue-router";
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import Pop from "../utils/Pop";
import { profilesService } from "../services/ProfilesService.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";

export default {
  setup() {
    const route = useRoute();
    const watchableProfileId = computed(() => route.params.profileId);

    watch(watchableProfileId, () => {
      profilesService.clearActiveProfile();
      keepsService.clearKeeps();
      vaultsService.clearVaults();
      getProfileById();
      getKeepsByProfileId();
      getVaultsByProfileId();
    }, { immediate: true });

    async function getProfileById() {
      try {
        const profileId = route.params.profileId;
        await profilesService.getProfileById(profileId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getKeepsByProfileId() {
      try {
        const profileId = route.params.profileId;
        await keepsService.getKeepsByProfileId(profileId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getVaultsByProfileId() {
      try {
        const profileId = route.params.profileId
        await vaultsService.getVaultsByProfileId(profileId)
      }
      catch (error) {
        Pop.error(error)
      }
    }
    return {
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account)
    };
  },
  components: { KeepCard, VaultCard }
};
</script>


<style lang="scss" scoped>
.cover-img {
  height: 40vh;
  object-fit: cover;
  object-position: center;
}

.profile-img {
  height: 10rem;
  width: 10rem;
}
</style>