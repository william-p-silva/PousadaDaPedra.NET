import { ListChecksIcon, User } from "@lucide/vue";
import { defineStore } from "pinia";
import { ref } from "vue";



export const useHeaderStore = defineStore(("headerStore"), () => {
    
    const hiddenSide = ref(false);

    const navItems = [
        { to: '/usuario', label: "Usuário", icon: User },
        { to: '/criar/tarefa', label: 'Criar Tarefa', icon: ListChecksIcon },
    ];

    return {
        hiddenSide,
        navItems,
    };
})