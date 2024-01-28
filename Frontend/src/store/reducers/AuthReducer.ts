import { IUser } from '../../components/auth/types';
export enum AuthReducerActionType {
    LOGIN_USER = "AUTH_LOGIN_USER",
    LOGOUT_USER = "AUTH_LOGOUT_USER"
}

export const IUserPayload = (user:IUser) => {
    const payload =
        {
            id: user.id,
            email: user.email,
            firstname: user.firstname,
            lastname: user.lastname,
            image: user.image,
            phoneNumber: user.phoneNumber,
            exp: user.exp,
            roles: user.roles// != null ? user.roles : "User"          
        } as IUser
    return payload;
}
       

export interface IAuthReducerState {
    isAuth: boolean,
    user?: IUser | null,
}

const initState: IAuthReducerState = {
    isAuth: false,
    user: null,
}

export const AuthReducer = (state=initState, action: any) : IAuthReducerState => {
    
    switch(action.type) {
        case AuthReducerActionType.LOGIN_USER: {

            return {
                isAuth: true,
                user: action.payload //as IUser
            };
        }
        case AuthReducerActionType.LOGOUT_USER: {
            return {
                isAuth: false,
                user: null
            };
        }
        //default: return state;
    }
    return state;
}


// import {Dispatch} from "react";
// import {AuthUserActionType, IUser, LoginSuccessAction} from "../../entities/Auth.ts";
// import {http} from '../../http.js';
// import jwtDecode from "jwt-decode";

// export const LoginUserAction = (dispatch: Dispatch<LoginSuccessAction>, token: string) => {
//     http.defaults.headers.common["Authorization"]=`Bearer ${token}`;
//     localStorage.token=token;
//     const user = jwtDecode(token) as IUser;
//     dispatch({
//        type: AuthUserActionType.LOGIN_USER,
//        payload: {
//            email: user.email,
//            roles: user.roles
//        }
//     });
// }