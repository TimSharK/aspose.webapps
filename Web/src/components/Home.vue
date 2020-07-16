<template>
  <tool-container
    title="Free online converter"
    subtitle="Convert JPG, SVG, DICOM documents to PDF, PSD, PNG."
  >
    <image-tool />
  </tool-container>
</template>

<script>
import Vue from "vue";
import ToolContainer from "./tools/ToolContainer.vue";
import ImageTool from "./tools/ImageTool.vue";

export default {
  name: "Home",
  components: {
    ToolContainer,
    ImageTool
  },
};
</script>
