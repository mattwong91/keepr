<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body p-0">
          <section v-if="keep" class="row">
            <div class="col-12 col-md-5">
              <img class="img-fluid keep-img" :src="keep.img" :alt="keep.name">
            </div>
            <div class="col-7 d-flex flex-column justify-content-between">
              <div>
                <div class="d-flex">
                  <i class="mdi mdi-eye-outline"></i>
                  <p>{{ keep.views }}</p>
                </div>
                <div class="d-flex">
                  <i class="mdi mdi-alpha-k-box-outline"></i>
                  <p>{{ keep.kept }}</p>
                </div>
              </div>
              <div>
                <h1>{{ keep.name }}</h1>
                <p>{{ keep.description }}</p>
              </div>
              <div class="d-flex justify-content-between">
                <div>
                  <p>VAULTS DROPDOWN</p>
                  <button class="btn btn-primary">SAVE</button>
                </div>
                <div class="d-flex">
                  <div>
                    <img class="rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
                  </div>
                  <div>
                    {{ keep.creator.name }}
                  </div>
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
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
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