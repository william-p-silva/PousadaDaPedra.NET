import Cookies from "js-cookie";

const TOKEN_KEY = "tokenPousada";

export const cookieArmazenado = {
    setToken: (token: string) => {
        Cookies.set(TOKEN_KEY, token, { expires: 1, secure: true, sameSite: "strict" })
    },
    
    getToken: () => {
        return Cookies.get(TOKEN_KEY);
    },
    
    removeToken: () => {
        Cookies.remove(TOKEN_KEY);
    },
}