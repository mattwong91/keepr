<template>
  <section @click="goToVault()" type="button" class="row m-1 rounded card-img align-items-end"
    :style="`background-image: url(${vault.img})`">
    <div class="col-12 text-start text-white fw-bold">
      <p>{{ vault.name }}</p>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from "../models/Vault";
import { useRouter } from "vue-router";
export default {
  props: {
    vault: { type: Vault, required: true }
  },
  setup(props) {
    const router = useRouter()
    const vault = props.vault
    return {
      goToVault() {
        router.push({ name: 'Vault', params: { vaultId: vault.id } })
      }
    }
  }
};
</script>


<style lang="scss" scoped>
p {
  margin-bottom: 0;
}

.card-img {
  background-position: center;
  background-size: cover;
  width: 100%;
  height: 20vh;
}
</style>