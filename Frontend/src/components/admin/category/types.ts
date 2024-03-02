export interface ICategoryItem {
    id: number,
    name: string,
    image: string,
    imageURL: string,
    description: string,
    parentId: number | null | undefined,
}
export interface ICategoryCreate {
    name: string,
    image: File | string,
    description: string,
    parentId: number | null | undefined,
}
export interface ICategoryEdit {
    id: number,
    name: string,
    imageUpload: any,
    description: string,
    parentId: number | null | undefined,
}