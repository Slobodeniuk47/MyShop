//if we modify it, we must also modify it in the reducer
export interface IUser {
    id: number;
    email: string;
    firstname: string;
    lastname: string;
    image: string;
    phoneNumber: string;
    exp: number;
    roles: string;
}

export interface ILogin {
    email: string,
    password: string
}
export interface ILoginResult {
    payload: string //Token
}

export interface IRegistration {
    email: string,
    firstName: string,
    lastName: string,
    image: File | string,
    phoneNumber: string,
    password: string,
    confirmPassword: string,
}


