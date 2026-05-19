import {useEffect, useState} from "react";
import {UserResponse} from "@/shared/types/userResponse";
import {buscarUserId} from "@/features/tarefa/hooks/useTarefa";


interface Props {
    id: number;
}

export function ResponsaveisTarefa({ id }: Props){
    
    const [user, setUser] = useState<UserResponse | null >(null);
    
    useEffect(() => {
        async function buscar(Id: number){
            const data = await buscarUserId(Id);
            setUser(data)
        }
        buscar(Number(id))
    }, []);
    
    if(user == null)
        return <p>Carregando</p>
    
    return (
            <p>{user.email}</p>
    )
}