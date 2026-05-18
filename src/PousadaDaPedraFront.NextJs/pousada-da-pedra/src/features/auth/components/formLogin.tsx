"use client"
import {useLogin} from "@/features/auth/hooks/useLogin";


export function FormLogin(){
    const {
        user,
        error,
        isLoading,
        handleChange,
        handleSubmit
    } = useLogin();
    return(
        <>
            <form className={`text-black w-80 flex flex-col`} onSubmit={handleSubmit} >
                
                <div className={`p-2 flex flex-col`}>

                    <div className={`p-2 flex flex-col`}>

                        <label htmlFor={`EmaiUser`}>E-mail: </label>
                        <input
                            name={"email"}
                            value={user.email || ""}
                            onChange={handleChange}
                            className={`p-2 text-md inputPadrao`}
                            placeholder={`teste@email.com`} id={`EmaiUser`}/>

                    </div>
                    <div className={`p-2 flex flex-col`}>

                        <label htmlFor={`Senha`}>Senha: </label>
                        <input
                            name={"senha"}
                            value={user.senha || ""}
                            onChange={handleChange}
                            type={"password"}
                            className={`p-2 text-md inputPadrao`}
                            placeholder={`Teste$123`} id={`Senha`}/>

                    </div>
                <div className={`w-full p-2  justify-center flex`}>
                    <button
                        type={"submit"}
                        className={` hover:bg-gray-600
            text-white p-2 text-md font-bold w-full rounded-md cursor-pointer bg-gray-700`}>
                        Entrar
                    </button>
                </div>
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