



<template>
    <nav>
        <ul class="flex gap-4 text-zinc-700 font-semibold ease-in-out duration-300">
            <li>
                <RouterLink to="/usuario" 
                class=" border-b-2 border-transparent transition-colors"
                active-class="border-zinc-600 text-zinc-600">
                usuario</RouterLink>
            </li>
            <li>
                <RouterLink to="/criar/tarefa" 
                class=" border-b-2 border-transparent transition-colors"
                active-class="border-zinc-600 text-zinc-600"
                >
                    Cadastrar Tarefa</RouterLink>
            </li>
        </ul>
    </nav>
</template>