<template>
<div class="frame">
  <h3>評価値計算</h3>
  <h2>{{calcTotal}}</h2>
  <input type="text" value="ウマ娘の名前">
  <br/>
  <h4>レアリティと固有スキルレベル</h4>
    <umaStar @get-star="getStar"></umaStar>
  <!-- <h4>適性</h4> -->
  <tr>
    <td v-for="d in distance" :key="d.name">
      <umaSelect :type="d.name" @get-aptitude="getAptitude" @calc-Skill="calcSkill"></umaSelect>
    </td>
  </tr>
  <tr>
    <td v-for="t in tactics" :key="t.name">
      <umaSelect :type="t.name" @get-aptitude="getAptitude" @calc-Skill="calcSkill"></umaSelect>
    </td>
  </tr>

  <!-- <span @click="toggle">toggle-on</span> テスト -->
  <div><accordion><template v-slot:title>選んだスキル</template>
    <template v-slot:body>
      <input type="checkbox" v-model="deleteCheck" value="削除"><label for="削除">削除</label>
      <span @click="deleteElegidoAll"> 全削除</span>
        <div class="flex-container"  :class="{'obtenido' : elegido.length != 0}">
          <!-- <div class="flex-item" v-for="(skill) in skillsData" :key="skill.Key"> -->
          <div class="flex-item" v-for="(item) in elegido" :key="item.Key" @click="changeE(item)">
            <button tyepe="button"  v-bind:class="{'gold' : item.GradeValue > 300}"> {{item.Name}} </button>
          </div>
        </div>
      </template></accordion>
  </div>

  <ul class="tabs">
    <li v-on:click="change('1')" v-bind:class="{'active': isActive === '1'}">パラメータ</li>
    <li v-on:click="change('2')" v-bind:class="{'active': isActive === '2'}">スキル</li>
  </ul>
  <ul class="contents">
    <li v-show="isActive === '1'">
      <!-- //コンテンツ1 -->
      <tr>
        <td class="para" v-for="(item,index) in items" :key="item">
        <p>{{item}}</p>
        <umaCalc :index="index" @get-score = "getParaScore" @get-input = "getInput"  @get-addscore1 = "getAddScore1" :name="index" @open-modal="openModal" ref="calcMain"></umaCalc>
        </td>
      </tr>
      <h4>加算値 + {{sumScore1}}</h4>
      <tr>
        <td class="para" v-for="(item,index) in items" :key="item.Key">
        <p>{{item}}</p>
        <umaCalcSub :getData=[index,score[index],inputPara[index],1] @get-addscore = "getAddScore1" @get-addinput = "getAddInput1" :name="index" ref="add1" @open-modal="openModalAdd"></umaCalcSub>
        </td>
      </tr>
      <h4>加算値 + {{sumScore2}}</h4>
      <tr>
        <td class="para" v-for="(item,index) in items" :key="item.Key">
        <p>{{item}}</p>
        <umaCalcSub :getData=[index,score[index],inputPara[index],2] @get-addscore = "getAddScore2" @get-addinput = "getAddInput2" :name="index" ref="add2" @open-modal="openModalAdd"></umaCalcSub>
        </td>
      </tr>
    </li>
    <li v-show="isActive === '2'">
      <!-- //コンテンツ2 -->
      <div class="flex-container wordItem">
        <span class="words flex-item" v-for="(word,index) in words" :key="word" @click.prevent="changeF(word)" v-bind:class="{'selected': wordFlag[index] == 1}">{{word}}</span>
        <span class="wordsAnd flex-item" v-for="word3 in words3" :key="word3" @click="setWord3(word3)" :class="{'selected' : word3Flag == word3}">{{word3}}</span>
      </div>
      <div class="flex-container wordItem">
        <span class="wordsAnd flex-item" v-for="word in words2" :key="word" @click="setWord2(word)" :class="{'selected' : word2Flag == word}">{{word}}</span>
        <span class="words" @click="fReset">フィルタリセット</span>
      </div>
      <div class="wordItem">
        <span class="countSkill">スキル<span class="redText">{{filteredSkills.length}}</span>個表示中、</span>
        <span class="countSkill">選択数<span class="redText">{{elegido.length}}</span>個</span>
      </div>
      <div>
        <span @click="deleteElegidoAll">全スキル選択解除</span>      
      </div>
      <div class="flex-container">
        <!-- <div class="flex-item" v-for="(skill) in skillsData" :key="skill.Key"> -->
        <div class="flex-item" v-for="(skill) in filteredSkills" :key="skill.Key">
          <umaSkill :skillName="skill"  @add-skill="addElegido" @delete-skill="deleteElegido" @calc-Skill="calcSkill" ref="skill"></umaSkill>
        </div>
      </div>
      <p class="siempre">{{calcTotal}}</p>
    </li>
</ul>

<!-- モーダル -->
<div class="modal-container" v-show="isVisible">
  <div id="modal-overlay" @click.self="closeModal">
    <div id="modal-content">
      <h3>{{items[indexModal]}} <span class="iText">(入力値)</span><span class="iValue"> {{inputPara[indexModal]}}</span></h3>
        <div class="modalNumAria">
          <button id="modal-button" v-for="num in numbers" :key="num.id" @click="inputModal(num)">{{num}}</button>
        </div>
      <p>
        <span class="allow" @click="()=>{switch(indexModal){case 0:return indexModal += 4; default:return indexModal--}}">＜＜ </span><button class="button" @click="closeModal">閉じる</button><span class="allow" @click="()=>{switch(indexModal){case 4:return indexModal -= 4; default:return indexModal++}}"> ＞＞</span>
      </p>
    </div>
  </div>
</div>
    
<!-- モーダルAdd -->
<div class="modal-container" v-show="isVisibleAdd">
  <div id="modal-overlay" @click.self="closeModalAdd">
    <div id="modal-content">
      <h3>{{items[indexModalAdd]}} <span class="iText">(加算値)</span><span class="iValue"> +{{addFlag=="add1" ?  addInput1[indexModalAdd] : addInput2[indexModalAdd]}}</span></h3>
        <div class="modalNumAria">
            <button id="modal-button" v-for="num in numbers" :key="num.id" @click="inputModalAdd(num)">{{num}}</button>
        </div>
      <span class="allow" @click="()=>{switch(indexModalAdd){case 0:return indexModalAdd += 4; default:return indexModalAdd--}}">＜＜ </span><button class="button" @click="closeModalAdd">閉じる</button><span class="allow" @click="()=>{switch(indexModalAdd){case 4:return indexModalAdd -= 4; default:return indexModalAdd++}}"> ＞＞</span>
    </div>
  </div>
</div>

</div>
</template>

<script>
// import App from '../App.vue'
import umaCalc from "./umaCalcMain.vue"
import umaCalcSub from "./umaCalcSub.vue"
import umaSkill from "./umaSkill.vue"
import umaSelect from "./umaSelect.vue"
import umaStar from "./umaStar.vue"
import accordion from './accordion.vue'

export default {
  name: 'umaLayout',
  components: {
    "umaCalc":umaCalc,
    "umaCalcSub":umaCalcSub,
    "umaSkill":umaSkill,
    "umaSelect":umaSelect,
    "umaStar":umaStar,
    "accordion":accordion,
    
  },data() {
    return {
      items:["スピード","スタミナ","パワー","根性","賢さ"],
      isActive: '1',
      score: [0,0,0,0,0,0,0],
      inputPara:[0,0,0,0,0],
      skillsData:[],
      elegido:[],
      val:0,
      i:0,
      calcTotal:0,
      addScore1:[0,0,0,0,0],
      addScore2:[0,0,0,0,0],
      addInput1:[0,0,0,0,0],
      addInput2:[0,0,0,0,0],
      distance:[{
        name:"長距離",
        rank:"A"
      },{
        name:"中距離",
        rank:"A"
      },{
        name:"マイル",
        rank:"A"
      },{
        name:"短距離",
        rank:"A"
      }],
      tactics:[{
        name:"逃げ",
        rank:"A"
      },{
        name:"先行",
        rank:"A"
      },{
        name:"差し",
        rank:"A"
      },{
        name:"追込",
        rank:"A"
      }],
      words:["長距離","中距離","マイル","短距離","逃げ","先行","差し","追込","レース場","馬場","天候","コツ","デバフ"],
      wordFlag:[0,0,0,0,0,0,0,0,0,0,0],
      filterWords:[],
      isActiveF: 1,
      words2:["常時","序盤","中盤","終盤","展開"],
      word2Flag: "",
      words3:["金","白"],
      word3Flag: "",//金、白用
      deleteCheck: false,
      isVisible:false,
      isVisibleAdd:false,
      numbers: [7,8,9,4,5,6,1,2,3,0,"AC","Max"],
      indexModal: 0,
      indexModalAdd: 0,
      addFlag: String,

    }
  },methods: {
    change: function(num){
      this.isActive = num
    },
    addElegido: function(inputSkill){
      this.elegido.push(inputSkill)
      //familyCheck
      this.checkFamily(inputSkill)
    },
    deleteElegido: function(inputSkill){
      //検索
      if(this.elegido.indexOf(inputSkill) != -1)
        //削除
        this.elegido.splice(this.elegido.indexOf(inputSkill),1);
    },
    checkFamily: function(inputSkill){
      if(inputSkill.Family=="")
        return
      if(this.elegido.length==0)
        return
      for(let i = 0;i<this.elegido.length;i++){
        if(this.elegido[i].Name == inputSkill.Family){
          this.elegido[i].Select = 1
          this.elegido.splice(i,1)
          return
        }
      }
      
    },
    getParaScore: function(value){
      //分離
      this.val = Number(value.substring(0,value.indexOf("index")));
      this.i = Number(value.substr(value.indexOf("index")+5));
      this.$set(this.score,this.i,this.val);
      this.sumUp();
    },
    getInput: function(value){
      //分離
      this.val = Number(value.substring(0,value.indexOf("index")));
      this.i = Number(value.substr(value.indexOf("index")+5));
      this.$set(this.inputPara,this.i, this.val)
    },
    getAddScore1: function(value){
      //分離
      this.val = Number(value.substring(0,value.indexOf("index")));
      this.i = Number(value.substr(value.indexOf("index")+5));
      this.$set(this.addScore1,this.i,this.val)
    },
    getAddScore2: function(value){
      //分離
      this.val = Number(value.substring(0,value.indexOf("index")));
      this.i = Number(value.substr(value.indexOf("index")+5));
      this.$set(this.addScore2,this.i,this.val)
    },
    getAddInput1: function(value){
      //分離
      this.val = Number(value.substring(0,value.indexOf("index")));
      this.i = Number(value.substr(value.indexOf("index")+5));
      this.$set(this.addInput1,this.i,this.val)
    },
    getAddInput2: function(value){
      //分離
      this.val = Number(value.substring(0,value.indexOf("index")));
      this.i = Number(value.substr(value.indexOf("index")+5));
      this.$set(this.addInput2,this.i,this.val)
    },
    updateAdd1:function(){
      for(let i=0 ;i<5;i++){
        //props更新後、計算
        this.$refs.add1[i].getData = [i,this.score[i],this.inputPara[i]]
        this.$refs.add1[i].calcPara(this.addInput1[i])
        this.$refs.add1[i].in1 = this.addInput1[i]
      }
    },updateAdd2:function(){
      for(let i=0 ;i<5;i++){
        //props更新後、計算
        this.$refs.add2[i].getData = [i,this.score[i],this.inputPara[i]]
        this.$refs.add2[i].calcPara(this.addInput2[i])
        this.$refs.add2[i].in1 = this.addInput2[i]
      }
    },
    updateCalc:function(){
      for(let i=0;i<5;i++){
        this.$refs.calcMain[i].in1 = this.inputPara[i]
        this.$refs.calcMain[i].inputItem = this.$refs.calcMain[i].calcPara(this.inputPara[i])
      }
    },
    sumUp: function(){
      this.calcTotal = this.score.reduce(function(sum,element){
        return sum + element;
      },0);
    },
    calcSkill: function(){
      let totalS = 0;
      for(let i = 0; i < this.elegido.length;i++){
        
        totalS += this.checkSkill(this.elegido[i]);
      }
      this.score[5] = totalS;
      this.sumUp();
    },
    checkSkill:function(skill){
      // let skillType = "";
      let rank = "";
      let rate = "";
      let returnV = skill.GradeValue;
      if(skill.Variable){
        //適性による変化確認
        for(let i = 0; i<4;i++){
          if(skill.Aptitude == this.tactics[i].name)
            rank = this.tactics[i].rank;
          else if(skill.Aptitude == this.distance[i].name)
            rank = this.distance[i].rank;
          else
            continue;
          //該当する適性取得
          // skillType = this.tactics[i].name;
          switch(rank){
            case "S":
            case "A":
              rate = 0.1;
              break;
            case "B":
            case "C":
              rate = -0.1;
              break;
            case "D":
            case "E":
            case "F":
              rate = -0.2;
              break;
            case "G":
              rate = -0.3;
              break;
          }
        }
      }
      if(skill.Stage == 3){//◎の場合 現状variableによるボーナスが無いと思いきや、コツに関してはある
        switch(skill.GradeValue){
          case 129:
            returnV = 174;
            break;
          case 174:
            returnV = 217;
            break;
          case 217:
            returnV = 262;
            break;
        }
      }
      //初期値
      if(skill.Variable=="〇")
        returnV = returnV * (1 + rate);
      return Math.round(returnV);

    },
    getAptitude: function(value){
      //分離
      let v = value.substr(-1);
      let t = value.substr(0,value.length-1);
      //チェック
      for(let i = 0;i<4;i++){
        if(t == this.distance[i].name){
          this.distance[i].rank = v;
          return;
        }
        if(t == this.tactics[i].name){
          this.tactics[i].rank = v;
          return;
        }
      }
    },changeF: function(s){
      if(this.filterWords[0] == "リセット")     //一つだけ残ったリセットを消す処理 どうしようもなくやむを得ず入れる
          this.filterWords.length = 0
      //index検索
      let index = 0
      for(let i=0;i<this.words.length;i++){
        if(this.words[i]==s){
          index = i
          //リセット処理
          // if(index == this.words.length - 1){
          //   // this.wordFlag = 
          //   for(let i = 0 ; i<this.wordFlag.length;i++){
          //     if(i == index)//最後のリセットだけフラグ立て
          //       this.wordFlag[i] = 1
          //     this.wordFlag[i] = 0;
          //   }
          //   this.filterWords.length = 0
          //   this.filterWords.push(s)
          //   return
          // }
          break
          }
      }
      if(this.filterWords.length == 0){
        this.filterWords.push(s)
        this.wordFlag[index] = 1
        return
        }
      else
        for(let i=0;i<this.filterWords.length;i++){
          if(this.filterWords[i]==s){
            this.filterWords.splice(i,1)
            this.wordFlag[index] = 0
            return
          }
        }
        this.filterWords.push(s)
        this.wordFlag[index] = 1

    },fReset: function(){
      for(let i = 0 ; i<this.wordFlag.length;i++){
        this.$set(this.wordFlag,i,0)
      }
      this.filterWords.splice(0,this.filterWords.length)
      this.word2Flag = ""
      this.word3Flag = ""

    },setWord2: function(word){
      if(this.word2Flag != word)
        this.word2Flag = word
      else
        this.word2Flag = ""

    },setWord3: function(word){
      if(this.word3Flag != word)
        this.word3Flag = word
      else
        this.word3Flag = ""

    },getStar: function(value){
      this.score[6] = value;
      this.sumUp();
    },toggle: function(){
      for(let i = 0;i<this.$refs.skill.length;i++){
        if(this.$refs.skill[i].skillName.Name == "ホークアイ"){
          if(this.$refs.skill[i].stage2 == true)
            this.$refs.skill[i].stage2 = false //インスタンスだから、スキル画面に出ていないと操作出来ない
          else
            this.$refs.skill[i].stage2 = true
          return
        }

      }
    },changeE: function(item){
      if(this.deleteCheck){//削除
        for(let i=0;i<this.elegido.length;i++){
          if(this.elegido[i].Name == item.Name){
            item.Select = 1
            if(item.Stage == 3){//状態戻して削除
              item.Stage = 2
              this.$set(item,'Name',item.Name.replace("◎","○"));
            }
            this.elegido.splice(i,1)
            break
            }
        }
      }else{//状態変化のみ
        if(item.Stage == 2){
        //〇→◎差し替え
          this.$set(item,'Name',item.Name.replace("○","◎"));
          item.Stage = 3;
        }else{
          //◎→〇差し替え
          this.$set(item,'Name',item.Name.replace("◎","○"));
          item.Stage = 2;
        }
      }
      this.calcSkill()
    },deleteElegidoAll: function(){
      for(let i=0;i<this.elegido.length;i++){
          this.elegido[i].Select = 1
          if(this.elegido[i].Stage == 3){//状態戻して削除
            this.elegido[i].Stage = 2
            this.$set(this.elegido[i],'Name',this.elegido[i].Name.replace("◎","○"));
          }
      }
      this.elegido.splice(0,this.elegido.length)
      this.calcSkill()
    },openModal:function(index){
      this.isVisible = true
      this.indexModal = index
    },closeModal:function(){
      this.isVisible = false
      this.updateCalc()
    },openModalAdd:function(value){
      //分離
      let index = Number(value.substring(0,value.indexOf("order")));
      let order = Number(value.substr(value.indexOf("order")+5));
      this.addFlag = order==1 ? "add1" : "add2"
      this.isVisibleAdd = true
      this.indexModalAdd = index
    },closeModalAdd:function(){
      this.isVisibleAdd = false
      if(this.addFlag=="add1")
        this.updateAdd1()
      else
        this.updateAdd2()
    },inputModal:function(num){
      switch(num){
        case "Max":
          this.$set(this.inputPara,this.indexModal, 1200)
          break
        case "AC":
          this.$set(this.inputPara,this.indexModal, 0)
          break
        default:
          {
            let number = String(num)
            let s = String(this.inputPara[this.indexModal])
            s = (s == "0") || (s == "1200") ? number : s + number
            s = Number(s) > 1200 ? 1200 : Number(s)
            this.$set(this.inputPara,this.indexModal, s)
            break
          }

      }
    },inputModalAdd:function(num){
      switch(num){
        case "Max":
          if(this.addFlag=="add1")
            this.$set(this.addInput1,this.indexModalAdd, 1200 - this.inputPara[this.indexModalAdd])
          else
            this.$set(this.addInput2,this.indexModalAdd, 1200 - this.inputPara[this.indexModalAdd])
          break
         case "AC":
          if(this.addFlag=="add1")
            this.$set(this.addInput1,this.indexModalAdd, 0)
          else
            this.$set(this.addInput2,this.indexModalAdd, 0)
          break
        default:
          {
            let number = String(num)
            let max = 1200 - this.inputPara[this.indexModalAdd]
            let s = this.addFlag=="add1" ? String(this.addInput1[this.indexModalAdd]) : String(this.addInput2[this.indexModalAdd])
            s = (s == "0") || (s == String(max)) ? number : s + number
            s = Number(s) > max ? max : Number(s)
            if(this.addFlag=="add1")
              this.$set(this.addInput1,this.indexModalAdd, s)
            else
              this.$set(this.addInput2,this.indexModalAdd, s)
            break
          }
      }
    }

    
  },mounted: function () {
    // this.$axios.get("/assets/json/hantei.json").then(response => (this.hanteiData = response.data))
    // .catch(function(error){
    //     console.log(error)
    // })
    this.skillsData = require("../assets/json/skills.json");

  },computed: {
    filteredSkills: function() {
      const self = this
      const word2Flag = this.word2Flag
      if(this.filterWords.length != 0 || this.word2Flag != ""){
          //リセット判定
        // if(this.filterWords[0] == "リセット"){
        //   //リセット処理
        //   //フラグ消し
        //   return this.skillsData
        // }
        return this.skillsData.filter(function(skill){

          let countW = 0
          let goldFlag = self.word3Flag == "金" ? true : false
          let whiteFlag = self.word3Flag == "白" ? true : false
          for(let i=0;i<self.filterWords.length;i++){

            switch(self.filterWords[i]){
              case "長距離":
              case "中距離":
              case "マイル":
              case "短距離":
              case "逃げ":
              case "先行":
              case "差し":
              case "追込":
                if(skill.Aptitude == self.filterWords[i])
                  // return true
                  countW++
                break
              case "レース場":
              case "馬場":
              case "天候":
              case "コツ":
              case "デバフ":
                if(skill.Type == self.filterWords[i])
                  // return true
                  countW++
                break
              // case "白":
              // case "金":
              //   switch(self.filterWords[i]){
              //     case "白":
              //       whiteFlag = true
              //       break
              //     case "金":
              //       goldFlag = true
              //       break
              //   }
              //   if((skill.GradeValue < 300)&&(self.filterWords[i]=="白")){
              //     whiteCount++
              //   }
              //   else if((skill.GradeValue >= 300)&&(self.filterWords[i]=="金")){
              //     goldCount++
              //   }
                // break
            }
          }
          //以下の場合について場合分けする
          //1 wordsでフィルタ、更にwords2でフィルタ
          //2 wordsはスルー、word2でフィルタ
          //3 wordsでフィルタ、word2はスルー
          //4 両方スルー
          //5 上記の項目クリア後、更に金、白スキル判定をする

          if(self.filterWords.length!=0){
            if(word2Flag != ""){//1 wordsでフィルタ、更にwords2でフィルタ
              if(countW > 0 && skill.Timing == word2Flag){
                //5 更に金、白スキル判定をする
                if(goldFlag && skill.GradeValue >= 300)
                  return true
                else if(whiteFlag && skill.GradeValue < 300)
                  return true    
                else if(goldFlag || whiteFlag)//金白フラグたってても条件満たさないものはfalse
                  return false           
                else//もともとの1の条件
                  return true
              }
              else
                return false              
            }
            else{//3 wordsでフィルタ、word2はスルー
              if(countW>0){
                //5 更に金、白スキル判定をする
                if(goldFlag && skill.GradeValue >= 300)
                  return true
                else if(whiteFlag && skill.GradeValue < 300)
                  return true              
                else if(goldFlag || whiteFlag)//金白フラグたってても条件満たさないものはfalse
                  return false
                else//もともとの3の条件
                  return true
              }
              else
                return false
            }
          }else{//2 wordsはスルー、word2でフィルタ
            if(skill.Timing == word2Flag){
              //5 更に金、白スキル判定をする
              if(goldFlag && skill.GradeValue >= 300)
                  return true
                else if(whiteFlag && skill.GradeValue < 300)
                  return true         
                else if(goldFlag || whiteFlag)//金白フラグたってても条件満たさないものはfalse
                  return false                
                else//もともとの2の条件
                  return true
            }
            else
              return false
          }
          
          //絞り込み(AND)検索
          // if(countW == self.filterWords.length)
          //   return true
          //OR検索
        })
      }else{//4 両方スルー
      //5 更に金、白スキル判定をする
      if(this.word3Flag=="金"){
          return this.skillsData.filter(function(skill){
            if(skill.GradeValue >= 300)
              return true
            else
              return false
          })
      }
      else if(this.word3Flag=="白"){
          return this.skillsData.filter(function(skill){
            if(skill.GradeValue < 300)
              return true
            else
              return false
          })
      }
      else
        //無選択で全表示
        return this.skillsData
      }
    },
    sumScore1: function(){
      return this.addScore1[0] + this.addScore1[1] +this.addScore1[2] +this.addScore1[3] +this.addScore1[4]  
    },
    sumScore2: function(){
      return this.addScore2[0] + this.addScore2[1] +this.addScore2[2] +this.addScore2[3] +this.addScore2[4]  
    }    
  //   calcTotal: function() {
  //     // return this.score.reduce(function(sum,element){
  //     //   return sum + element;
  //     // },0);
  //     return this.score[0];
  // },
  }, watch:{
    inputPara:function(){
      this.updateAdd1()
      this.updateAdd2()
      this.sumUp()
    },
    

  }
  
 
  
}
</script>

<style scoped>
td {
  width:60px;
}
.frame {
  width: 350px;
  margin: 0 auto;
  float:left;
}
.frame h3{
  margin:0 auto;
}
p {
  font-size:14px;
  font-weight:bold;
}
ul{
    margin: 0;
    padding: 0;
  }
  li{
    list-style: none;
  }
  .tabs {
    overflow: hidden;
  }
  .tabs li,
  .tabs label {
    float: left;
    padding: 10px 20px;
    border: 1px solid #ccc;
    cursor: pointer;
    transition: .3s;
  }
  .tabs li:not(:first-child),
  .tabs label:not(:first-of-type) {
    border-left: none;
  }
  .tabs li.active,
  .tabs :checked + label {
    background-color: #ffccff;
    border-color: #ccccff;
    color: #fff;
    cursor: auto;
  }
  .contents{
    overflow: hidden;
    margin-top: -1px;
  }
  .contents li {
    width: 340px;
    padding: 4px;
    border: 1px solid #ccc;
  }
  .flex-container{
    display:flex;
    flex-wrap:wrap;
    justify-content: flex-end;
  }
  .flex-item{
    margin: 5px, auto;
    padding: 5px 10px;
    box-sizing: border-box;
  }
  .flex-container::after{
    display:block;
    content:"";
    width:170px;/* この数値で最後の行の配置を調節 */
  }
  .words.selected{
    color:yellow;
  }
  .words{
    margin:5px auto;
    font-size:14px;
    background:#5fb3f5;
    color:#fff;
    padding:4px;
    text-align: center;
    font-weight: bold;
    letter-spacing: 0.05em;
  }
  .wordsAnd.selected{
    color:yellow;
  }
  .wordsAnd{
    margin:5px auto;
    font-size:14px;
    background:rgb(197, 137, 197);
    color:#fff;
    padding:4px;
    text-align: center;
    font-weight: bold;
    letter-spacing: 0.05em;
  }
  
  .obtenido {
    width: 340px;
    margin: 0 0 15px 0;
    border: 1px solid #ccc;
  }

  .obtenido button {
    width:150px;
    height:25px;
    padding:0,0;
    margin:0,0;
    color:black;
    background:white;
    font-weight: bold;
  }

  
  .gold{
    background:gold!important;
    color:black!important;
  }
  .siempre{
    position:fixed;
    top:90%;
    left:10%;

    color          : #ffffff;
    font-size      : 20pt;
    letter-spacing : 4px;
    text-shadow    : 
      2px  2px 1px #003366,
    -2px  2px 1px #003366,
      2px -2px 1px #003366,
    -2px -2px 1px #003366,
      2px  0px 1px #003366,
      0px  2px 1px #003366,
    -2px  0px 1px #003366,
      0px -2px 1px #003366;
  }
  .left{
    text-align:left;
  }
  .right{
    text-align: right;
  }
  .countSkill{
    display: inline-block;
    font-size:12px;
    font-weight: bold;
    text-align:left;
  }
  .redText{
    color:red;
  }
  .wordItem{
    text-align: left;
    -webkit-user-select: none;
  }
  
  p {
    margin:0 auto;
  }

  /* モーダル */
#modal-overlay{
  z-index:1;
  position:fixed;
  top:0;
  left:0;
  width:100%;
  height:100%;
  background-color:rgba(0,0,0,0.5);

  display:flex;
  align-items: center;
  justify-content: center;
}
#modal-content{
  z-index: 2;
  width:200px;
  padding:1em;
  background:#fff;
  -webkit-user-select: none;
}
#modal-button{
  font-size: 24px;
  width:60px;
  height:60px;
  float:left;
  text-align: center;

  -webkit-appearance: none;
  border-radius:0;
  display:inline-flex;
  justify-content: center;
  align-items: center;

  touch-action: manipulation;
  
}
#modal-content .iText{
  font-size:18px;
  font-weight: bold;
}
#modal-content .iValue{
  color:red;
}
#modal-content .button{
  margin: 10px auto;
  height: 30px;
}
.modalNumAria{
  width:200px;
  height:250px;
}
.allow{
  font-size:20px;
}
</style>