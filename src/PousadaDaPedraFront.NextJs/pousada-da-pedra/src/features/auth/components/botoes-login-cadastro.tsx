import Link from "next/link";


export function Botoes_Login_Cadastro() {
    return(
        
    <div>
        <Link href={"/auth/cadastro"} >Cadastro |</Link> 
        <Link href={"/auth/login"} > Login</Link>
    </div>
        
        )
}