import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import CadastroView from '../views/auth/CadastroView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: HomePage,      
    },
    {
      path: "/cadastro",
      name: "cadastro",
      component: CadastroView,
    },
  ],
})

export default router
