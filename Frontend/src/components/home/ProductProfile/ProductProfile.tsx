import { useParams,useNavigate, Link, useSearchParams, Navigate } from "react-router-dom";
import img from '../images/t-shirt-png.webp'
import { useFormik } from "formik";
import classNames from "classnames";
//import { useGetProductByIdQuery, useGetProductsQuery } from '../features/user/apiProductSlice';
import {IProductItem} from '../../admin/product/types';
//import { useAppSelector } from '../app/hooks';
import { useDispatch } from 'react-redux';
//import { addOrder } from '../features/user/ordersStateSlice';
//import { v4 as uuidv4 } from 'uuid';
import { useEffect, useState, useRef, LegacyRef } from "react";
import imgStar from "../../../assets/star.png"
import imgEmptyStar from "../../../assets/emptyStar.png"
import * as yup from "yup";
import StarComponent from '../StarComponent';
import { AxiosError } from "axios";
import { APP_ENV } from "../../../env";
import { http } from "../../../http";
import { ICommentCreate, ICommentItem } from "./types";
import { useTypedSelector } from "../../../store/hooks/useTypedSelector";
import Navbar from "../Navbar";
import { ICategoryItem } from "../../admin/category/types";
import Slider from "../Slider";
import { IUser } from "../../auth/types";
import { Modal } from "bootstrap"
import CommentView from "../comment/CommentView";

const ProductProfile=()=>{
    const { isAuth, user } = useTypedSelector((store) => store.auth);
    const [product, setProduct] = useState<IProductItem>();
    const [isLoading, setLoading] = useState<boolean>(true);
    const [searchParams, setSearchParams] = useSearchParams();
    useEffect(() => {
        getProductById();
        
    }, []);
    const getProductById = () => {
      console.log("id: " + searchParams.get('id'));
      http.get('api/product/get/' + searchParams.get('id'))
        .then((res) =>
        {
          const product = res.data.payload;
            setLoading(false);                
          setProduct(product);
          console.log("json: ", product);

        })
    }

  const showProductImage = (product: IProductItem) => {
    var jsx_stars: JSX.Element[] = [];
      for(var i = 0;i<product.images.length;i++)
      {
          jsx_stars.push( <img src={`${product.images[i].name}`} width={150} height={150} className=' mr-1 hover:contrast-75 image-container'/>);
      }
      return jsx_stars;
  }
  const getProductRating = (comments: ICommentItem[]) => {
    var star = 0;
    comments.map((comment: ICommentItem) => (
      star += comment.stars / comments.length
    ));
    console.log(`Star: ${star}`)
    return star;
  }
    return <>
        {/* {console.log("Log", product)} */}
      <div className='pageContent'>
        <Navbar />
        <div className='pageList mt-5 mb-5'>
          <h1 className="text-center">ProductProfile</h1>
          {/* <Slider /> */}
          {product ? (
            <div className="text-center mt-5">
              <div>{ showProductImage(product) }</div>
              <div>ProductID: {product.id}</div>
              <div>Name: {product.name}</div>
              <div>Description: {product.description}</div>
              <div>Category: [{product.categoryId}]{product.categoryName}</div>
              <div><StarComponent Count={5} Rating={getProductRating(product.comments)} /></div>
              <div>Comments: { product.comments.length}</div>

            </div>
          ) : "null"}
        </div>

        <CommentView />
        
      </div>
    </>
}
    
export default ProductProfile