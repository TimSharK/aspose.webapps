<template>
  <div class="asposetools">
    <h1 class="title">{{ title }}</h1>
    <h2 class="subtitle">{{ subtitle }}</h2>
    <div class="tool">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "ToolContainer",
  props: ["title", "subtitle"],
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
.asposetools {
  display: flex;
  flex-direction: column;
  justify-content: center;
  background: url(https://www.aspose.cloud/templates/asposeapp/images/bg/header-app.png)
    top center no-repeat;
  background-size: cover;
  padding: 40px 0 80px 0;

  .title {
    font-size: 45px;
    font-weight: 700;
    color: #fff;
    align-self: center;
  }

  .subtitle {
    color: #fff;
    font-weight: 500;
    font-size: 18px;
    margin: 5px;
    align-self: center;
  }

  .tool {
    margin-top: 40px;
    width: 540px;
    align-self: center;
  }
}
</style>
