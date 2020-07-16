<template>
  <div class="result">
    <tool-container
      title="Free online converter"
      subtitle="Convert JPG, SVG, DICOM documents to PDF, PSD, PNG."
    >
      <div>
        <div class="result-content">
          <div>
            <h3 class="result-header">
              Your files have been processed successfully
            </h3>
            <a class="btn-primary" v-bind:href="endpoint">Download</a>
            <div class="tip">
              Press <span class="tip-btn">Ctrl</span>+<span class="tip-btn"
                >D</span
              >
              to store it in your bookmarks in order not to search it again
            </div>
          </div>
        </div>
        <div class="result-actions">
          <router-link class="back-link" to="/">← Convert other documents</router-link>
          <a href="#"></a>
        </div>
      </div>
    </tool-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import ToolContainer from "./tools/ToolContainer.vue";
import config from "../config";

@Component({
  components: {
    ToolContainer,
  },
})
class Result extends Vue {
  endpoint = "";

  mounted() {
    this.endpoint = config.endpoints.downloadResult + `${this.$route.params.id}?fileName=${this.$route.params.fileName}`;
  }
}

export default Result;
</script>

<style lang="scss" scoped>
.result-content {
  color: #155724;
  background-color: #d4edda;
  border: 1px solid #c3e6cb;
  position: relative;
  padding: 0.75rem 1.25rem;
  margin-bottom: 1rem;
  border-radius: 4px;
  text-align: center;
}

.result-header {
  font-size: 17px;
  font-weight: 500;
  margin-top: 10px;
}

.btn-primary {
  display: block;
}

.tip {
  margin-top: 20px;
  font-size: 14px;

  .tip-btn {
    font-weight: 500;
  }
}

.back-link {
  color: white;
  text-decoration: none;
  font-weight: 500;

  &:hover {
    text-decoration: underline;
  }
}
</style>
