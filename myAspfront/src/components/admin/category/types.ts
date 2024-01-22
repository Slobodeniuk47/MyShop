export interface ICategoryItem {
    "id": number,
    "name": string,
    "image": string,
    "description": string,
    "parentId": number | null | undefined,
    // status: boolean,
    // priority:number
}
export interface ICategoryCreate {
    name: string,
    image: File | string,
    description: string,
    parentId: number | null | undefined,
    // status: boolean,
    // priority:number
}
export interface ICategoryEdit {
    id: number,
    name: string,
    imageUpload: any,
    description: string,
    parentId: number | null | undefined,
    // imgChange: boolean,
    // status: boolean,
    // priority:number
}