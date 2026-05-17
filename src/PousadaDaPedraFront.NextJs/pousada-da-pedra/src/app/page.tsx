"use client"

import {useState} from "react";
import {UsuarioRequest} from "@/Types/Usuario/UsuarioRequest";

export default function Home() {
  
  const [user, setUser] = useState<UsuarioRequest | null>(null);
  
  return (
    <>
      <section className={`min-h-screen  bg-gray-200 flex justify-center items-center`}>
        <form className={`text-black w-80`}>
          
          <div className={`p-2 flex flex-col`}>
            
            <label htmlFor={`NomeUser`}>Nome Completo: </label>
            <input className={`px-4 py-2 text-md inputPadrao`} 
                   placeholder={`Teste da Silva`} form={`NomeUser`}/>
            
          </div>
          <div className={`p-2 flex flex-col`}>
  
            <label htmlFor={`EmaiUser`}>E-mail: </label>
            <input className={`p-2 text-md inputPadrao`}
                   placeholder={`teste@email.com`} form={`EmaiUser`}/>
  
          </div>
          <div className={`p-2 flex flex-col`}>
  
            <label htmlFor={`Senha`}>Senha: </label>
            <input className={`p-2 text-md inputPadrao`}
                   placeholder={`Teste$123`} form={`Senha`}/>
  
          </div>
          <div className={`p-2 flex flex-col`}>

            <label htmlFor={`ConfirmSenha`}>Confirme a Senha: </label>
            <input className={`p-2 text-md inputPadrao`}
                   placeholder={`Teste$123`} form={`ConfirmSenha`}/>

          </div>
          <div className={`w-full p-2  justify-center flex`}>
            <button type={`submit`} className={` hover:bg-gray-600
            text-white p-2 text-md font-bold w-full rounded-md cursor-pointer bg-gray-700`}>
              Cadastrar
            </button>
          </div>
          
        </form>
        
      </section>
    </>
  );
}
