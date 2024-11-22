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

<style scoped lang="scss"></style>
