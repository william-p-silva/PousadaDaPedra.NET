// features/auth/components/FormCadastro.tsx
"use client"

import {useCadastro} from "@/features/auth/hooks/useCadastro";


export function FormCadastro(){

    const {
        user,
        confirmSenha,
        error,
        isLoading,
        handleSubmit,
        HandleChange
    } = useCadastro();
   
    return(
        <>
            
        <form className={`text-black w-80 flex flex-col`} onSubmit={handleSubmit} >
            
        

            <div className={`p-2 flex flex-col`}>

                <label htmlFor={`NomeUser`}>Nome Completo: </label>
                <input
                    name={"nome"}
                    value={user.nome || ""}
                    onChange={HandleChange}
                    className={`px-4 py-2 text-md inputPadrao`}
                    placeholder={`Teste da Silva`} id={`NomeUser`}/>

            </div>
            <div className={`p-2 flex flex-col`}>

                <label htmlFor={`EmaiUser`}>E-mail: </label>
                <input
                    name={"email"}
                    value={user.email || ""}
                    onChange={HandleChange}
                    className={`p-2 text-md inputPadrao`}
                    placeholder={`teste@email.com`} id={`EmaiUser`}/>

            </div>
            <div className={`p-2 flex flex-col`}>

                <label htmlFor={`Senha`}>Senha: </label>
                <input
                    name={"senha"}
                    value={user.senha || ""}
                    onChange={HandleChange}
                    type={"password"}
                    className={`p-2 text-md inputPadrao`}
                    placeholder={`Teste$123`} id={`Senha`}/>

            </div>
            <div className={`p-2 flex flex-col`}>

                <label htmlFor={`ConfirmSenha`}>Confirme a Senha: </label>
                <input
                    name={"confirmSenha"}
                    value={confirmSenha || ""}
                    onChange={HandleChange}
                    type={"password"}
                    className={`p-2 text-md inputPadrao`}
                    placeholder={`Teste$123`} id={`ConfirmSenha`}/>

            </div>
            <div className={`w-full p-2  justify-center flex`}>
                <button
                    type={"submit"}
                    className={` hover:bg-gray-600
            text-white p-2 text-md font-bold w-full rounded-md cursor-pointer bg-gray-700`}>
                    Cadastrar
                </button>
            </div>
        </form>
            {error && (
                <div className="text-red-500 text-sm font-bold p-2 text-center">
                    {error}
                </div>
            )}
        </>

    )
}