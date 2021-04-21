import Vue from 'vue'
import Router from 'vue-router'
import Home from '../components/umaLayout.vue'


Vue.use(Router)

export default new Router({
    mode: 'history',
    // base: process.env.BASE_URL,
    base: '/uma/',
    routes: [
        { path: '/uma', name:'home', component: Home}
    ]
})

const routes = [{
    path: '/',
    name: 'Home',
    componet: Home,
    meta:{title: 'ウマ娘評価値算出アプリ', desc:'ウマ娘の評価値を算出するアプリを提供しています。モバイル用として作成しています'}
}]