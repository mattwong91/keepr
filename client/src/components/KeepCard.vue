<template>
  <section class="row m-1 rounded card-img" :style="`background-image: url(${keep.img})`">
    <div class="col-12 d-flex justify-content-between align-items-center">
      <div class="text-white fw-bold">
        {{ keep.name }}
      </div>
      <div>
        <img class="rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
      </div>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from "../models/Keep";
export default {
  props: {
    keep: { type: Keep, required: true }
  },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>
.card-img {
  background-position: center;
  background-size: cover;
  width: 100%;
  height: auto;
}
</style>