// features/auth/hooks/useCadastro.t
import {ChangeEvent, FormEvent, useState} from "react";
import {Cargo, UserFormCadastro} from "@/features/auth/types";
import {cadastroUser} from "@/features/auth/services/authService";

export function useCadastro() {
    const [user, setUser] = useState<UserFormCadastro>({
        nome: "",
        email: "",
        senha: "",
        cargo: Cargo.funcionario,
    });

    const [confirmSenha, setConfirmSenha] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false)

    const HandleChange = (e: ChangeEvent<HTMLInputElement>) => {
        const {name, value} = e.target

        if (name === "confirmSenha"){
            setConfirmSenha(value);
            return
        }
        
        setUser((prevUSer) => ({
            ...prevUSer,
            [name as keyof UserFormCadastro]: value,
        }));
    }

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);
        
        if(user.nome.trim() === "" || user.email.trim() === "" || user.senha.trim() === "" || confirmSenha.trim() == ""){
            setError("Campos em Branco");
            return;
        }
        
        if(user.senha != confirmSenha){
            setError("Senhas Incompativeis");
            return;
        }
        
        try {
            setIsLoading(true);
            
            const data = await cadastroUser(user);
            
            if(data != null)
                alert("Cadastro realizado")
            
        }catch (err) {
            setError("Erro ao realizar o cadastro. Tente novamente.");
        }finally {
            setIsLoading(false);
        }
        

    }
    return {
        user,
        confirmSenha,
        error,
        isLoading,
        handleSubmit,
        HandleChange
    }
}