<template>
  <div class="container-fluid pt-5">
    <section class="row m-2">
      <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-4 col-lg-3 p-3">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";

export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
    });
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
