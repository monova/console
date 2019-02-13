<template>
  <div class="card mb-4">
    <h6 class="card-header">
      <div>{{title}}</div>
    </h6>
    <div class="card-body"><input
        type="text"
        :placeholder="placeholder"
        class="form-control"
        v-bind:value="value"
        v-on:input="$emit('input', $event.target.value)"
      ></div>
  </div>
</template>

<script>
export default {
  props: {
    value: {
      default: null,
      type: String
    },
    placeholder: {
      default: "",
      type: String
    },
    title: {
      default: "",
      type: String
    }
  }
};
</script>