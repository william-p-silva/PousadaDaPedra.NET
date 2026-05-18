//src/app/auth/cadastro/page.tsx

import {FormCadastro} from "@/features/auth/components/formCadastro";


export default function Cadastro(){
    return (
        <div className={`w-full h-screen flex flex-col justify-center items-center`}>
            <FormCadastro />
        </div>
    )
}