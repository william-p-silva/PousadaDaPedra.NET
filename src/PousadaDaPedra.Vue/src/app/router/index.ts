import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import CadastroView from '../views/auth/CadastroView.vue'
import LoginView from '../views/auth/LoginView.vue'
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore.ts'
import UsuarioView from '@/app/views/usuario/UsuarioView.vue'

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
    {
      path: "/login",
      name: "login",
      component: LoginView,
    },
    {
      path: "/usuario",
      name: "usuario",
      component: UsuarioView,
    },
  ],
})

router.beforeEach(async (to) => {
  const authStore = loginUsuarioStore()

  const publicRoutes = ['/login', '/cadastro', '/']
  if (publicRoutes.includes(to.path)) return true

  await authStore.verificarAuth();

  if (!authStore.isAuthenticated) {
    return '/login'
  }
  return true;
});


export default router
