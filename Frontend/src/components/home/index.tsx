import * as React from 'react';
import { Link, Outlet } from 'react-router-dom';
import { useEffect, useState } from "react";
import Navbar from './Navbar';
import { IProductItem } from "../admin/product/types"
import { http } from "../../http";
import { APP_ENV } from "../../env";
import Slider from './Slider';
import StarComponent from './StarComponent';
import { ICommentItem } from './productProfile/types';

export interface ServiceResponce{
  payload: IProductItem[]
}
export default function HomePage() {
    const [isLoading, setLoading] = useState<boolean>(false);
  const [products, setProducts] = useState<IProductItem[]>();
    useEffect(() => {
    setLoading(true);
    http.get<ServiceResponce>(`api/product/get`).then((resp) => {
      const { data } = resp;
      //console.log("----Products---", data);
      //setHome(data);
      console.log()
      setProducts(data.payload);
      setLoading(false);
    });
    }, []);
  const getProductRating = (comments: ICommentItem[]) => {
    var star = 0;
    comments.map((comment: ICommentItem) => (
      star += comment.stars / comments.length
    ));
    console.log(`Star: ${star}`)
    return star;
  }
  return (
    <div className="pageContent">
          <Navbar />
          
            {isLoading ? (
                <div className="loader-container">
                    <div className="spinner"></div>
                </div>
            ) : (
                // <div className='pageList mt-5'>
                //       Home Page
                  // </div>
                  
                  <div className='pageList mt-5 mb-5'>
                      
                      <h1 className="text-center">Products</h1>
      <div className="row" style={{marginLeft: 200}}>
        {products?.map((product) => (
          <div className="card col-md-2 h-150 mt-5">
            <Link to={"/product?id=" + product.id} style={{textDecoration:"none", color: 'black'}}><i className='fa fa-heart'></i></Link>
            <Link to={"/product?id=" + product.id} style={{textDecoration:"none", color: 'black'}}><i className='fa fa-heart-o'></i></Link>
            <img 
              style={{ width: "100%", height: "120px", objectFit: "contain" }}
              src={`${product.images[0].name}`}
              className=""
              alt="productImage"
            />
            <div className="card-body" style={{width: 360, height: 360}}>
              <p>{product.dateCreated}</p>
              <h5 className="card-title">[{product.id}]{product.name}</h5>
              <p className="card-text"> [{product.categoryId}]{product.categoryName}</p>
              <Link to={"/product?id=" + product.id} style={{textDecoration:"none", color: 'black'}}>
                {product.comments.length} <i className='fa fa-comments'></i>
                <StarComponent Count={5} Rating={getProductRating(product.comments)}/>
              </Link>
              {/* <Link to={"/product?id=" + product.id} style={{textDecoration:"none", color: 'black'}}>{product.comments.length} <i className='fa fa-comments'></i></Link>
              <StarComponent LinkTo={"/product?id=" + product.id}  Count={5} Rating={getProductRating(product.comments)} /> */}
              
              <p className="card-text"><h1>{product.price}$</h1></p>
              <p className="card-text">Description: <small>{product.description}</small></p>
              {/* <Link className='fa fa-shopping-cart' to="/basket" style={{ textDecoration: "none", color: 'black' }}>
              </Link> */}
              <a href={`/cart`} className="btn btn-primary">Add to cart <i className="fa fa-shopping-cart" aria-hidden="true"></i></a>
              </div>
          </div>
        ))}
      </div>
                </div>
                
                
            )}
        </div>
  )
}