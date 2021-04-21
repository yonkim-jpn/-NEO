<template>
  <div id="app">
    <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
    <!--<HelloWorld msg="yahoo!!!Welcome to Your Vue.js App"/>-->
      <umaLayout></umaLayout>
      <!-- <router-link to="/uma">Home</router-link>
      <router-view/> -->
  </div>
</template>




<script>
    import umaLayout from "./components/umaLayout.vue"
    import Vue from 'vue'
    import VueHead from 'vue-head'

    Vue.use(VueHead)

export default {
  name: 'App',
  components: {
    umaLayout
        },
  mounted: function(){
    document.querySelector("meta[name='description").setAttribute('content',"ウマ娘アプリの評価値計算が目的のWEBアプリです。タブレット等でゲームプレイ中にスマホで評価値を算出する使い方を想定しているため、スマホ用の画面構成になってます")
    document.title = "ウマ娘評価値計算"
  }  
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
