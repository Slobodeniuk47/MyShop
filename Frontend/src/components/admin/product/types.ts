import { ICommentItem } from "../../home/productProfile/types";
export interface IProductImageItem {
    id: number,
    name: string
    productId: number,
    productName: string
}
export interface IProductItem {
    id: number,
    name: string,
    price: number,
    // discountPrice: number|null|undefined,
    images: IProductImageItem[],
    description: string,
    categoryId: number,
    categoryName: string,
    dateCreated: string,
    dateUpdated: string,
    // status:number
    comments: ICommentItem[]
}
export interface IProductCreate {
    name: string;
    price: number;
    description: string;
    categoryId: number | undefined,
    images: File[] | string[] | any
    //discountPrice: number,
    // imagesId: number[],
    // priority: number,
    // status: boolean
}
export interface IProductEdit {
    id: number,
    name: string,
    price: number,
    description: string,
    categoryId: number | undefined,
    imagesUpload: File[] | string[] | any
}