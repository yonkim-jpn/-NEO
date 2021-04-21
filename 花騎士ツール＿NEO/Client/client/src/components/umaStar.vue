<template>
    <div class="stars">
        <img @click="change(0)" :src="[imgActive[0]==1 ? onImage : offImage]"/>
        <img @click="change(1)" :src="[imgActive[1]==1 ? onImage : offImage]"/>
        <img @click="change(2)" :src="[imgActive[2]==1 ? onImage : offImage]"/>
        <img @click="change(3)" :src="[imgActive[3]==1 ? onImage : offImage]"/>
        <img @click="change(4)" :src="[imgActive[4]==1 ? onImage : offImage]"/>
        <span class="lvText">LV</span><span class="updown" @click="upLv">△</span><span class="lv" :class="{upper:upper,lower:lower}">{{lv}}</span><span class="updown" @click="downLv">▽</span>
    </div>
</template>

<script>

export default{
    name: "umaStar",
    
    data(){
        return{
            activeStar:0,
            onImage:"/Static/on.png",
            offImage:"/Static/off.png",
            imgActive:[1,1,1,0,0],
            lp:0,
            lv:1,
            maxLv:[4,5,4,5,6],
            lvGradeV:[120,120,170,170,170],
            upper:false,
            lower:false,
        }

    },methods:{
        change: function(num){
            if(this.imgActive[num]==1){
                for( this.lp=1;this.lp<5;this.lp++){
                    if(num > this.lp)
                        this.$set(this.imgActive,this.lp,1)
                    else
                        this.$set(this.imgActive,this.lp,0)
                }
            }
            else{
                for( this.lp=1;this.lp<5;this.lp++){
                    if(num >= this.lp)
                        this.$set(this.imgActive,this.lp,1)
                    else
                        this.$set(this.imgActive,this.lp,0)
                }
            }
            this.calcGradeV()
            this.checkLimit()
            return
        },
        upLv: function(){
            let total = this.imgActive.reduce(function(sum, element){
                return sum + element;
            }, 0);
            let max = this.maxLv[total - 1]
            if(this.lv < max)
                this.lv++
            this.calcGradeV()
            this.checkLimit()
            return
        },
        downLv: function(){
            let total = this.imgActive.reduce(function(sum, element){
                return sum + element;
            }, 0);
            let min = this.maxLv[total - 1] -3
            if(this.lv > min)
                this.lv--
            this.calcGradeV()
            this.checkLimit()
            return
        },
        calcGradeV: function(){
            let total = this.imgActive.reduce(function(sum, element){
                return sum + element;
            }, 0);
            let returnV = this.lvGradeV[total-1] * this.lv
            this.$emit('get-star', returnV);
        },
        checkLimit: function(){
            let total = this.imgActive.reduce(function(sum, element){
                return sum + element;
            }, 0);
            if(this.lv > this.maxLv[total-1])
                this.upper = true
            else
                this.upper = false
            if(this.lv < this.maxLv[total-1]-3)
                this.lower = true
            else
                this.lower = false
        }

    },
}
</script>
<style scoped>
.stars{    
  -webkit-user-select: none;
  font-size:20px;
}
.lv{
    font-weight: bolder;
}
.updown{
    font-weight: bold;
}
.upper{
    color:red;
}
.lower{
    color:blue;
}
.lvText{
    margin:0 0 0 20px;
}
</style>

