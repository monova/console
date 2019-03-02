import axios from "axios";
import router from "@/router";
import Vue from 'vue';

export const http = axios.create({
  headers: {
    "Content-Type": "application/json",
    "X-Requested-With": "XMLHttpRequest",
    "X-Application-Name": "vue",
    "Accept-Type" : "application/json"
  }
});

http.interceptors.response.use(
  function (response) {
    if (response.data) {
      if (response.data.success && response.data.message) {
        Vue.notify({
          title: 'Success',
          text: response.data.message
        });
      }
    }
    return response;
  },
  function (error) {
    const statusCode = error.response.status;
    if (statusCode === 401) {
      window.location.href = "/Identity/Account/Login?ReturnUrl=" + encodeURIComponent(window.location.pathname);
      return new Promise(() => {});
    }

    if (statusCode === 403) {
      router.push({
        name: "forbidden"
      });
      return new Promise(() => {});
    }
    const response = error.response;
    if (!response.data.success && response.data.message) {
      Vue.notify({
        title: 'Error',
        text: response.data.message,
        type: 'error'
      });
    }

    return Promise.reject(error);
  }
);
