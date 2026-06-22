import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import CadastroView from '../views/auth/CadastroView.vue'
import LoginView from '../views/auth/LoginView.vue'
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore.ts'
import UsuarioView from '@/app/views/usuario/UsuarioView.vue'
import AppLayout from '../views/layout/AppLayout.vue'
import CriarTarefaView from '../views/usuario/gerente/CriarTarefaView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: AppLayout,
      children: [
        {
          path: "/",
          name: "Inicio",
          component: HomePage,
        },
        {
          path: "/usuario",
          name: "usuario",
          component: UsuarioView,
        },
        {
          path: "/criar/tarefa",
          name: "cadastrarTarefa",
          component: CriarTarefaView,
        },
      ],
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
