<template>
  <div>
     <p>+{{result - resultOri}}</p>
    <input type="text" v-bind:value="in1" v-on:change="getInput" @click="openModal" readonly="readonly"/>
  </div>
</template>
<script>

export default {
    name:"umaCalcSub",
    props: {
    getData:Array, //index,score,inputPara,order  
  },data() {
    return {
      in1: 0,
      hanteiData: [],
      result: 0,
      resultOri: 0,
    };
  },methods:{
    getInput:function($event){
      var a = Number($event.target.value);
      this.in1 = $event.target.value;
      // this.inputItem = this.hanteiData.length + a;
      this.inputItem = this.calcPara(a);
    },
    calcPara:function(input){
      let para = input + Number(this.getData[2])//入力値＋上段の既に入力済みの値
      if(this.getData[2]==0 || input == 0 || this.getData[2] == 1200 || para >= 1200){
        this.result = 0
        this.resultOri = 0
        this.$emit('get-addscore',String(this.result - this.resultOri) + "index" + String(this.getData[0]));
        this.$emit('get-addinput',String(input) + "index" + String(this.getData[0]));
        return
      }
      this.resultOri = Number(this.getData[1])
      
      
      //判定
      for(let i = 0;i<this.hanteiData.length;i++){
        let maxZone = 0;
        if(i == 0)
          maxZone = 1201;
        else
          maxZone = Number(this.hanteiData[i-1].zone);
        if((Number(this.hanteiData[i].zone)<para) && (para <= maxZone)){
          this.result = Number(this.hanteiData[i].suma) + (para - Number(this.hanteiData[i].zone))* Number(this.hanteiData[i].ratio)
          this.result = Math.floor(this.result);
          this.$emit('get-addscore',String(this.result - this.resultOri) + "index" + String(this.getData[0]));
          this.$emit('get-addinput',String(input) + "index" + String(this.getData[0]));
          return
        }
        else if(para > 1202 || para < 0){
          this.result = 3838
          this.$emit('get-addscore',String(this.result - this.resultOri) + "index" + String(this.getData[0]));
          this.$emit('get-addinput',String(input) + "index" + String(this.getData[0]));
          return
        }
        else if(para == 0){
          this.result = 0
          this.$emit('get-addscore',String(this.result - this.resultOri) + "index" + String(this.getData[0]));
          this.$emit('get-addinput',String(input) + "index" + String(this.getData[0]));
          return
        }
      }
    },openModal:function(){
      this.$emit('open-modal',this.getData[0] + "order" + this.getData[3])
    }

  },mounted: function () {
    this.hanteiData = require("../assets/json/hantei.json");

  },computed:{
    
  },
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
