<template>
    <div>
        <!-- <button type="button" @click.prevent="onClick" :class="{'stage-1':stage1,'stage-2':stage2,'stage-3':stage3}">{{skillName.Name}}</button> -->
        <button type="button" @click.prevent="onClick" :class="{'stage-1':skillName.Select == 1,'stage-2':skillName.Select == 2,'stage-3':skillName.Select == 3,'goldSkill' : skillName.GradeValue > 300}">{{skillName.Name}} </button>
    </div>
</template>
<script>

export default {
  name: "umaSkill",
  data(){
    return{
      stage1:true,
      stage2:false,
      stage3:false
    }
  },
  props: {
      skillName:{    
        name:String,
        base:Number,
        variable:Boolean,
        stage:Number,

        default:function(){
          return{
            name:""
          }
        },
        required:true,
        },
  },methods: {
    onClick: function() {
      // if(this.stage1){
      //   this.stage1 = false;
      //   this.stage2 = true;
      if(this.skillName.Select == 1){
        this.skillName.Select = 2
        //計算データ追加
          this.$emit('add-skill',this.skillName);
          this.$emit('calc-Skill');
      // }else if(this.stage2){
      }else if(this.skillName.Select == 2){
        if(this.skillName.Stage==2){
          //計算データ削除
          this.$emit('delete-skill',this.skillName);
          //〇→◎差し替え
          this.$set(this.skillName,'Name',this.skillName.Name.replace("○","◎"));
          this.skillName.Stage = 3;
          // this.stage1 = false;
          // this.stage2 = false;
          // this.stage3 = true;
          this.skillName.Select = 3
          //計算データ追加
          this.$emit('add-skill',this.skillName);
          this.$emit('calc-Skill');
        }else{
          // this.stage1 = true;
          // this.stage2 = false;     
          this.skillName.Select = 1     
          //配列をonにする
          //計算データ削除
          this.$emit('delete-skill',this.skillName);
          this.$emit('calc-Skill');
        }
      }else{//stage3
          //◎は〇に変更
          // this.skillName.Name = this.skillName.Name.replace("◎","○");
         this.$set(this.skillName,'Name',this.skillName.Name.replace("◎","○"));
         this.skillName.Stage = 2;
        // this.stage1 = true;
        // this.stage2 = false;
        // this.stage3 = false;
        this.skillName.Select = 1
          //配列をonにする
          //計算データ削除
          this.$emit('delete-skill',this.skillName);
          this.$emit('calc-Skill');
      }
    },
  },
}
</script>
<style scoped>
button{
  width:150px;
  height:30px;
  padding:0,0;
  margin:0,0;
  font-weight: bold;
}
.stage-1{
  background-color: azure;
  color:black;
}
.stage-2{
  background-color: darkorange!important;
  color:white!important;
}
 .stage-3{
  background-color:darkorange!important;
  color:white!important;
}
.goldSkill{
    background:gold;
    color:black;
}


</style>