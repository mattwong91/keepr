<template>
  <div class="container text-center px-5">

    <section class="row">
      <div class="col-12">
        <img class="img-fluid" :src="profile.coverImg" alt="Cover image">
      </div>
      <div class="col-12">
        <img class="rounded-circle" :src="profile.picture" :alt="profile.name">
      </div>
      <div class="col-12">
        <h1>{{ profile.name }}</h1>
        <p>vaults.length Vaults | {{ keeps.length }} Keeps</p>
      </div>
    </section>

    <section class="row p-5">
      <div class="col-12 text-start">
        <h2>Vaults</h2>
      </div>
      <div v-for="vault in vaults" :key="vault.id" class="col-6 col-md-4 col-lg-3 col-xl-2">
        <VaultCard :vault="vault" />
      </div>
    </section>

    <section class="row p-5">
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
      vaults: computed(() => AppState.vaults)
    };
  },
  components: { KeepCard, VaultCard }
};
</script>


<style lang="scss" scoped></style>