export interface IRoleItem {
    id: number,
    roleName: string
    concurrencyStamp: string
}
export interface IRoleCreate {
    roleName: string
}
export interface IRoleEdit {
    id: number,
    roleName: string,
    concurrencyStamp: string | null
}