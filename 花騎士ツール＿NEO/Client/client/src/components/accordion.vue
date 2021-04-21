<template>
    <div class="accordion">
        <h4 class="accordion_title" @click="toggle">
            <slot name="title"></slot>{{delta}}
        </h4>
        <div class="accordion_body" v-show="expand">
            <slot name="body"></slot>
        </div>
    </div>
</template>

<script>
export default{
    data(){
        return{
            expand:false,
            delta:"▽",
        }
    },methods:{
        toggle: function(){
        this.expand = !this.expand
        this.delta = this.expand ? "△" : "▽"
        }
    }
}
</script>
