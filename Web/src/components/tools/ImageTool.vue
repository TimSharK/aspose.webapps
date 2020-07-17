<template>
  <div class="image-tool position-relative">
    <form
      id="image-tool-form"
      enctype="multipart/form-data"
      method="POST"
      v-bind:action="endpoint"
    >
      <validation-observer ref="observer" v-slot="{ invalid, validated }">
        <div class="row">
          <validation-provider rules="required">
            <input type="hidden" name="fileName" v-bind:value="fileName" />
          </validation-provider>
          <validation-provider rules="required">
            <input type="hidden" name="fileData" v-bind:value="fileData" />
          </validation-provider>
          <file-pond
            ref="filePond"
            class="file-drop"
            v-bind:class="{ 'file-drop-invalid': invalid && validated }"
            maxFileSize="5MB"
            v-bind:accepted-file-types="acceptFileTypes"
            v-bind:fileValidateTypeDetectType="detectCustomType"
            labelIdle='Drag &amp; Drop your file or <span class="filepond--label-action"> Browse </span>'
            v-on:addfile="onAddFile"
            v-on:removefile="onRemoveFile"
          />
        </div>
        <div class="row">
          <input type="hidden" name="format" v-model="format" />
          <v-select
            class="format-select"
            v-bind:options="options"
            v-model="format"
            placeholder="Please select output format"
            :searchable="false"
            :clearable="false"
          ></v-select>
        </div>
        <div class="row validation-msg" v-if="invalid && validated">
          <span>Select source file</span>
        </div>
        <div class="row align-center">
          <button class="btn-primary" type="button" v-on:click="onSubmit">
            Convert
          </button>
        </div>
      </validation-observer>
    </form>
    <div class="loading-overlay" v-bind:visible="busy">
      <div class="blur-glass"></div>
      <div class="rotate-shadows"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop, Ref } from "vue-property-decorator";
import vueFilePond from "vue-filepond";
import FilePondPluginFileValidateType from "filepond-plugin-file-validate-type";
import FilePondPluginFileValidateSize from "filepond-plugin-file-validate-size";
import FilePondPluginFileEncode from "filepond-plugin-file-encode";
import "filepond/dist/filepond.min.css";
import vSelect from "vue-select";
import "vue-select/dist/vue-select.css";
import { ValidationProvider, ValidationObserver, extend } from "vee-validate";
import { required } from "vee-validate/dist/rules";

import config from "../../config";

const FilePond = vueFilePond(
  FilePondPluginFileValidateType,
  FilePondPluginFileValidateSize,
  FilePondPluginFileEncode
);

extend("required", required);

@Component({
  components: {
    FilePond,
    vSelect,
    ValidationProvider,
    ValidationObserver,
  },
})
class ImageTool extends Vue {
  @Ref() readonly observer!: InstanceType<typeof ValidationObserver>;

  endpoint = config.endpoints.convert;
  options = config.outputFormats;
  acceptFileTypes = config.inputFormats;

  format = "PDF";
  fileName = "";
  fileData = "";
  busy = false;

  detectCustomType(source: any, type: any) {
    return new Promise((resolve, reject) => {
      if (type) resolve(type);

      const re = /(?:\.([^.]+))?$/;
      const matches = re.exec(source.name);
      const ext = matches == null ? null : matches[1].toLowerCase();

      if (ext && ext === "dcm") type = "application/dicom";

      resolve(type);
    });
  }

  onAddFile(e: any, file: any) {
    this.fileName = file.file.name;
    this.fileData = file.getFileEncodeBase64String();
  }

  onRemoveFile() {
    this.fileName = "";
    this.fileData = "";
  }

  onSubmit() {
    this.observer.validate().then((success) => {
      if (!success) {
        return;
      }

      this.busy = true;

      const form =
        document.querySelector<HTMLFormElement>("#image-tool-form") ??
        undefined;

      const formData = new FormData(form);

      fetch(this.endpoint, { method: "POST", body: formData })
        .then((response) => {
          if (!response.ok) {
            alert("Oops! Something went wrong. Please try again later");
            throw new Error("Convert failed " + response.status);
          }

          return response.json();
        })
        .then((data) => {
          this.$router.push({
            name: "result",
            params: { id: data.id, fileName: data.fileName },
          });
        })
        .catch((e) => {
          this.busy = false;
          alert("Failed to convert. Try again later")
          console.log("Error: " + e.message);
          console.log(e.response);
        });
    });
  }
}

export default ImageTool;
</script>

<style lang="scss"></style>
