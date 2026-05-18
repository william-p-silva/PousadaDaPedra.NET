"use client"

import { useRouter } from "next/navigation";
import { useEffect } from "react";

export default function Home() {
  const router = useRouter();

  useEffect(() => {
    // O useEffect garante que isso só roda DEPOIS que o componente foi renderizado
    router.push("/auth/login");
  }, [router]);

  return (
      <>
        {/* Você pode deixar um esqueleto de loading visual aqui se quiser */}
        <div className="flex h-screen items-center justify-center">
          <p className="text-gray-500">Redirecionando...</p>
        </div>
      </>
  );
}