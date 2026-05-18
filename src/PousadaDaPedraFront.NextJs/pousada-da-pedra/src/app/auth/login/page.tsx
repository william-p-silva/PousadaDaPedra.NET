import {FormLogin} from "@/features/auth/components/formLogin";
import {Botoes_Login_Cadastro} from "@/features/auth/components/botoes-login-cadastro";


export default function Login(){
    return (
        <div className={`w-full h-screen flex flex-col justify-center items-center`}>
            <FormLogin />
            <Botoes_Login_Cadastro />
        </div>
    )
}