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
              <div class="text-center">
                <h1>{{ keep.name }}</h1>
                <p>{{ keep.description }}</p>
              </div>
              <div class="d-flex justify-content-between">
                <div v-if="isVaultPage" class="d-flex align-items-center">
                  <button @click="removeKeep()" class="btn btn-danger"><i class="mdi mdi-cancel"></i> Remove</button>
                </div>
                <div v-else class="d-flex align-items-center">
                  <p class="pe-2">VAULTS DROPDOWN</p>
                  <button class="btn btn-primary">SAVE</button>
                </div>
                <div class="d-flex align-items-center">
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
import { useRoute } from "vue-router";
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

export default {
  setup() {
    const route = useRoute()
    return {
      keep: computed(() => AppState.activeKeep)
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

@media (min-width: 768px) {
  .keep-img {
    border-top-left-radius: 0.375rem;
    border-bottom-left-radius: 0.375rem;
    border-top-right-radius: 0px;
  }
}
</style>