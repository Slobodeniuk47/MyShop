import * as React from 'react';
import { Link, Outlet } from 'react-router-dom';
import { useEffect, useState } from "react";
import Navbar from './Navbar';
import { IProductItem } from "../admin/product/types"
import { http } from "../../http";
import { APP_ENV } from "../../env";
import Slider from './Slider';
import StarComponent from './StarComponent';
import { ICommentItem } from './ProductProfile/types';

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
            
            {/* <div className="card width: 18rem;">
              <img className="card-img-top" style={{ width: "100%", height: "100px", objectFit: "contain" }} src={`${APP_ENV.BASE_URL}Images/productImages/${product.images[0].name}`} alt="Card image cap"/>
              <div className="card-body">
                <h5 className="card-title">[{product.id}]{product.name}</h5>
              <p className="card-text">[Id]CategoryName: [{product.categoryId}]{product.categoryName}</p>
              <div>Comments: { product.comments.length}</div>
              <div><StarComponent Count={5} Rating={getProductRating(product.comments)} /></div>
              
                    <p className="card-text"><h1>{product.price}$</h1></p>
              <p className="card-text">Description: <small>{product.description}</small></p>
              <div>
                <Link to={"/product?id=" + product.id}><i className='fa fa-edit btnEdit'></i></Link>
              </div>
                <a href={`/product?id=${product.id}`} className="btn btn-primary">Comments</a>
              </div>
            </div> */}
          
            <img 
              style={{ width: "100%", height: "120px", objectFit: "contain" }}
              src={`${APP_ENV.BASE_URL}Images/productImages/${product.images[0].name}`}
              className=""
              alt="Козачка"
            />
            <div className="card-body" style={{width: 360, height: 360}}>
              <p>{product.dateCreated}</p>
              <h5 className="card-title">[{product.id}]{product.name}</h5>
              <p className="card-text">[Id]CategoryName: [{product.categoryId}]{product.categoryName}</p>
              <div>Comments: { product.comments.length}</div>
              <div><StarComponent Count={5} Rating={getProductRating(product.comments)} /></div>
              
                    <p className="card-text"><h1>{product.price}$</h1></p>
              <p className="card-text">Description: <small>{product.description}</small></p>
              <div>
                <Link to={"/product?id=" + product.id}><i className='fa fa-edit btnEdit'></i></Link>
              </div>
                <a href={`/product?id=${product.id}`} className="btn btn-primary">Comments</a>
              </div>
          </div>
        ))}
      </div>
                </div>
                
                
            )}
        </div>
  )
}