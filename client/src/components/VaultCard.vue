<template>
  {{ vault.name }}
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from "../models/Vault";
export default {
  props: {
    vault: { type: Vault, required: true }
  },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped></style>