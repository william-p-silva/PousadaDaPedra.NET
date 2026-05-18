import {ChangeEvent, FormEvent, useState} from "react";
import {UserFormCadastro, UserFormLogin} from "@/features/auth/types";
import {cadastroUser, loginUser} from "@/features/auth/services/authService";
import {useRouter} from "next/navigation";


export function useLogin() {
    const [user, setUser] = useState<UserFormLogin>({
        email: "",
        senha: "",
    })

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false)
    
    const router = useRouter();
    
    const handleChange = (e: ChangeEvent<HTMLInputElement>)=> {
        const {name, value} = e.target;

        setUser((prevUSer) => ({
            ...prevUSer,
            [name as keyof UserFormCadastro]: value,
        }));
    }
    
    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        
        if(user.email.trim() === "" || user.senha.trim() === "") {
            setError("Campos vazios")
            return;
        }

        try {
            setIsLoading(true);

            const data = await loginUser(user);

            if(data != null) {
                alert("Login realizado")
                router.push(`/${data.data.cargo}`)
            }
            

        }catch (err) {
            setError("Erro ao realizar o login. Tente novamente.");
        }finally {
            setIsLoading(false);
        }
        
    }
    
    return {
        user,
        error,
        isLoading,
        handleChange,
        handleSubmit
    }
}