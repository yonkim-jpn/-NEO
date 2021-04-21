<template>
  <div class="my-area">
    <p>{{type}}</p>
    <span class="my-button">
    <span @click="upRank">△</span><span class="rank" :class="this.rank">{{rank}}</span><span @click="downRank">▽</span>
    </span>
  </div>
</template>

<script>


export default {
  name: 'umaSelect',
  props: {
    type: String
  },
  data() {
    return{
    rank: "A",
    }
  },methods:{
    upRank: function(){
      let grade = ["S","A","B","C","D","E","F","G"];
      let index = 0;
      for(let i = 0;i<8;i++){
        if(grade[i] == this.rank){
          index = i;
          index = index != 0 ? index -1: index;
          this.rank = grade[index];
          this.$emit('get-aptitude', this.type + this.rank)
          this.$emit('calc-Skill');
          return; 
        }
      }
    },downRank: function(){
      let grade = ["S","A","B","C","D","E","F","G"];
      let index = 0;
      for(let i = 0;i<8;i++){
        if(grade[i] == this.rank){
          index = i;
          index = index != 7 ? index +1: index;
          this.rank = grade[index];
          this.$emit('get-aptitude', this.type + this.rank)
          this.$emit('calc-Skill');
          return; 
        }
      }
    }
  }
  
}
</script>
<style scoped>
.my-area{
  width:80px;
  height:55px;
  -webkit-user-select: none;
}
.my-button{
  width:25px;
  height:25px;
  font-size:20px;
}
p{
  margin:0 auto;
  font-size:14px;
  font-weight:bold;
}
.rank{
  font-weight: bolder;
}
.S{
  color:#ffdd1a;
}
.A{
  color:#ffaf1a;
}
.B{
  color:#ffb3bf;
}
.C{
  color:#00e600;
}
.D{
  color:#6666ff;
}
.E{
  color:#cc00cc;
}
.F{
  color:#b3b3ff;
}
.G{
  color:gray;
}
</style>