<template>
  <div>
      <p>{{inputItem}}</p>
      <input type="text" v-bind:value="in1" v-on:change="getInput" @click="openModal" readonly="readonly"/>    
    
  </div>
  
</template>

<script>

export default {
  name: "umaCalcMain",
  props: {
    index: Number,
    score: Number,
  },
  components: {
  },
  data() {
    return {
      in1: 0,
      inputItem: 0,
      hanteiData: [],
    };
  },
  methods:{
    getInput:function($event){
      var a = $event.target.value;
      this.in1 = Number($event.target.value);
      // this.inputItem = this.hanteiData.length + a;
      this.inputItem = this.calcPara(a);
    },
    calcPara:function(para){
      let result;
      //判定
      for(let i = 0;i<this.hanteiData.length;i++){
        let maxZone = 0;
        if(i == 0)
          maxZone = 1201;
        else
          maxZone = Number(this.hanteiData[i-1].zone);
        if((Number(this.hanteiData[i].zone)<para) && (para <= maxZone)){
          result = Number(this.hanteiData[i].suma) + (para - Number(this.hanteiData[i].zone))* Number(this.hanteiData[i].ratio)
          result = Math.floor(result)
          this.$emit('get-score',String(result) + "index" + String(this.index))
          this.$emit('get-input',String(para) + "index" + String(this.index))
          this.$emit('get-addscore1',"0" + "index" + String(this.index))
          return result;
        }
        else if(para > 1202 || para < 0)
          return "範囲外";
        else if(para == 0){
          result = 0
          this.$emit('get-score',String(result) + "index" + String(this.index))
          this.$emit('get-input',String(para) + "index" + String(this.index))
          return 0;
        }
      }
    },
    openModal:function(){
      this.$emit('open-modal',this.index)
    },
  },
  mounted: function () {
    // this.$axios.get("/assets/json/hantei.json").then(response => (this.hanteiData = response.data))
    // .catch(function(error){
    //     console.log(error)
    // })
    this.hanteiData = require("../assets/json/hantei.json");

  }
  
}
                                                            
</script>

<style scoped>
  input{
    text-align:center;
    width:45px;
  }
  p {
    margin:5px auto;
  }
  


</style>