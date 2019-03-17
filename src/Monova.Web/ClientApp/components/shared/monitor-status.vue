<template>
  <div>
    <span v-b-tooltip :title="title" :class="'badge badge-'+badgeClass">{{title.substr(0,1)}}</span>
  </div>
</template>


<script>
export default {
  props: {
    title: {
      default: "",
      type: String
    },
    status: {
      default: 0,
      type: Number
    }
  },

  computed: {
    badgeClass() {
      switch (this.status) {
        case 0:
          return "dark";
        case 2:
          return "primary";
        case 3:
          return "success";
        case 4:
          return "warning";
        case 5:
        case 6:
          return "danger";
        default:
          return "light";
      }
    }
  }
};
</script>