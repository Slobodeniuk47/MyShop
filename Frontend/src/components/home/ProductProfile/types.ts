import { IUser } from "../../auth/types";

export interface ICommentCreate {
    title: string;
    message: string;
    stars: number,
    // likes: number,
    // dislikes: number,
    userId: number,
    productId: number | undefined,
    images: File[] | string[] | any
}
export interface ICommentItem {
    id: number,
    title: string;
    message: string;
    dateCreated: string,
    dateUpdated: string,
    stars: number,
    likes: number,
    dislikes: number,
    userId: number,
    username: string,
    productId: number | undefined,
    images: ICommentImageItem[],
    user: IUser
}

export interface ICommentImageItem {
    id: number,
    name: string
    commentId: number | null | undefined,
    commentName: string
}
export interface IServiceResponse{
    payload: ICommentItem | ICommentImageItem
}