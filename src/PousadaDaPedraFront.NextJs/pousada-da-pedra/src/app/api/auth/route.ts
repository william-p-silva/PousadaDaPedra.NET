import {NextRequest, NextResponse} from "next/server";
import {cookies} from "next/headers";


export async function POST(req: NextRequest) {
    const {token} = await req.json();
    
    if(!token){
        return NextResponse.json({ error: "Token não informado" }, { status: 400 });
    }

    const cookieStore = await cookies();
    cookieStore.set("token", token, {
        httpOnly: true,   
        secure: process.env.NODE_ENV === "production",
        sameSite: "strict",
        path: "/",
        maxAge: 60 * 60 * 8, // 8 horas
    });

    return NextResponse.json({ ok: true });
    
}

export async function DELETE() {
    const cookieStore = await cookies();
    cookieStore.delete("token");

    return NextResponse.json({ ok: true });
}