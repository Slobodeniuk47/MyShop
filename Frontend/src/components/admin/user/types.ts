export interface IPermissions {
    roleName: string;
}
export interface IUserItem {
    id: number;
    email: string;
    firstname: string;
    lastname: string;
    image: string;
    phoneNumber: string;
    permissions: IPermissions[];
}
export interface IEditUser {
    id: number,
    email: string,
    firstname: string,
    lastname: string,
    image: File | string,
    phoneNumber: string,
    password: string,
    confirmPassword: string,
    role: string,
}