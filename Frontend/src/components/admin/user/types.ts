export interface IUserItem {
    id: number;
    email: string;
    firstname: string;
    lastname: string;
    image: string;
    phoneNumber: string;
    roles: string;
}
export interface IUserResult {
    payload: IUserItem[]
}
export interface IEditUser {
    id: number,
    email: string,
    firstName: string,
    lastName: string,
    image: File | string,
    phoneNumber: string,
    password: string,
    confirmPassword: string,
}