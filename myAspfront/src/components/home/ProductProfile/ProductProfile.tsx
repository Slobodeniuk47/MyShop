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
import internal from 'stream';
import StarComponent from '../StarComponent';
import { AxiosError } from "axios";
import { APP_ENV } from "../../../env";
import { http } from "../../../http";
import { ICommentCreate, ICommentItem } from './types';
import { useTypedSelector } from "../../../store/hooks/useTypedSelector";
import Navbar from "../Navbar";
import { ICategoryItem } from "../../admin/category/types";
import Slider from "../../home/Slider";
import { IUser } from "../../auth/types";

const ProductProfile=()=>{
    const dispatch = useDispatch();
    const navigator = useNavigate();
    const params = useParams();
    //const orders = useAppSelector((state)=>state.orders);
    var [stars, setStars] = useState(0);
    const { isAuth, user } = useTypedSelector((store) => store.auth);
    const [comments, setComments] = useState<ICommentItem[]>([]);
    const [product, setProduct] = useState<IProductItem>();
    const [isLoading, setLoading] = useState<boolean>(true);
    const [imageError, setImageError] = useState<string>();
    const [imagesUrl, setImagesUrl] = useState<string[]>([]);
    const [images, setImages] = useState<File[]>([]);
    const fileSelectInputRef = useRef<HTMLInputElement>();
    const [isProcessing, setIsProcessing] = useState<boolean>(false);
    const [searchParams, setSearchParams] = useSearchParams();
    //const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    const [deleteId, setId] = useState<number>();
    // const handleAddNewOrder=(data:any)=>{
    //     const newOrder:Order = {id:uuidv4(), name:data.name,product_id:data.id};
    //     dispatch(addOrder(newOrder));
    // }
    useEffect(() => {
        //getProductById();
        getProductById();
        loadCommentsByProductId();
        
    }, []);
    
    const loadCommentsByProductId = () => {
        console.log("id: " + searchParams.get('id'));
        http.get('api/Comment/GetCommentsByProductId/'+ searchParams.get('id'))
            .then((res) => res.data)
            .then(async (json) => {
                console.log("CommentsJson", json);
                setLoading(false);
                setComments(json);
                setStars(5);
            });
    }
    const getProductById = () => {
        console.log("id: " + searchParams.get('id'));
        http.get('api/product/get/' + searchParams.get('id'))
            .then((res) => res.data)
          .then(async (json) => {
              const product = json;
                setLoading(false);                
                // if (json.status == 1)
                //     json.status = true;
                // else if (json.status == 0)
                //     json.status = false;
              setProduct(json);
              console.log("imagesUpload ", json.imagesUpload);  
              console.log("json: ", json);

            })
    }
    const changeStars = (stars: number,star_id: number) => {
        setStars(star_id);
        console.log("changeStars: " + star_id)
        for(var i = 1;i<=stars;i++){  
            var star: any = document.getElementById(i.toString());   
            //ternary operator  if i<=star_id star.src = imgStar, else star.src = imgEmptyStar          
            star.src = i <= star_id ? imgStar : imgEmptyStar;
        }
    }
  const getStarsForProduct = (count: number) => {
      
      var jsx_stars: JSX.Element[] = [];
      for(var i = 1;i<=count;i++)
      {
        const s = i
          jsx_stars.push(<img onClick={() => changeStars(count, s)} id={`${i}`} width={50} height={50} className=' mr-1 hover:contrast-75 image-container' src={imgStar} />);
        }
      return jsx_stars;
    }
    // const createNewComment = (data: React.FormEvent<HTMLFormElement>) => {
    //     data.preventDefault();
    //     var curentData = new FormData(data.currentTarget);
    //     var title = curentData?.get("Title")?.toString()!;
    //     var text = curentData?.get("Text")?.toString()!;
    // }
    const initValues: ICommentCreate = {
        title: '',
        message: '',
        stars: 5,
        //discountPrice: 0,
        productId: product?.id,
        userId: 0,
        images: []
        //priority: 0,
        //status: true
  };
    const onSubmitFormikData = async (values: ICommentCreate) => {
        // console.log(values);       
        // formHttp.post('api/product/get', values, {
        // }).then(resp => {navigator("..");})
        // navigator("..");
        try {
        var formData = new FormData();
            formData.append("title", values.title);
            formData.append("message", values.message);
            formData.append("stars", stars.toString());
            formData.append("productId", product?.id.toString() as string);
            formData.append("userId", user?.id.toString() as string);
        images.forEach((i) => {
        formData.append("images", i);
        });
          console.log(values.title);
          console.log(values.message);
          console.log(values.stars.toString());
          console.log(product?.id);
          console.log(values.userId?.toString() as string);
          console.log(images);
          console.log("FormData: ", formData);
      var resp = await http.post(
        `${APP_ENV.BASE_URL}api/Comment/create`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      var created = resp.data as IProductItem;
      console.log("resp = ", created);

      
      navigator("../")
      await setIsProcessing(false);
    } catch (e: any) {
      const axiosError = e as AxiosError;
      await setIsProcessing(false);
    }
    }
    const createSchema = yup.object({
        title: yup.string().required("Input title"),
        message: yup.string().required("Input message"),
        // categoryId: yup.string().required("Input categoryId")
    });
     const formik = useFormik({
        initialValues: initValues,
        validationSchema: createSchema,
        onSubmit: onSubmitFormikData
    });
    const { values, errors, touched, handleSubmit, handleChange, setFieldValue } = formik;
    const onAddImageClick = async () => {
    await fileSelectInputRef.current?.click();
};
    const onImageChangeHandler = async (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const files = e.target.files;
    if (!files || !files.length) {
      return;
    }

    const file = files[0];
    if (!/^image\/\w+/.test(file.type)) {
      setImageError("Select correct image!");
      return;
    }
    const url = URL.createObjectURL(file);
    setImages([...images, file]);
    setImagesUrl([...imagesUrl, url]);
    console.log("images:", images, imagesUrl);
    };
    const removeImage = (index: number) => {
    const updatedImages = [...images];
    updatedImages.splice(index, 1);
    setImages(updatedImages);

    const updatedImagesUrl = [...imagesUrl];
    updatedImagesUrl.splice(index, 1);
    setImagesUrl(updatedImagesUrl);
    };
    const imagesPreviewData = imagesUrl?.map((url, index) => (
    <div className="img" key={url}>
      <button
        type="button"
        className="btn-close position-relative z-index-100 top-0 start-100"
        aria-label="Remove image"
        title="Remove image"
        onClick={() => removeImage(index)}
      ></button>

      <div className="card m-2" style={{ width: "14rem", height: "14rem" }}>
        <img src={url} className="card-img-top" alt={"image"}></img>
      </div>
    </div>
  ));
    // const { data, isSuccess } = useGetProductByIdQuery({ Id: params.productId });
    
    // const { data, isSuccess }: { data?: { payload: IProductItem }, isSuccess: boolean } = useGetProductByIdQuery({ Id: params.productId });

    // console.log(data);

  const showProductImage = (product: IProductItem) => {
    // console.log(product.images.length);
    var jsx_stars: JSX.Element[] = [];
      for(var i = 0;i<product.images.length;i++)
      {
          jsx_stars.push( <img src={`${APP_ENV.BASE_URL}Images/productImages/${product.images[i].name}`} width={150} height={150} className=' mr-1 hover:contrast-75 image-container'/>);
      }
      return jsx_stars;
  }
  const showCommentImage = (comment: ICommentItem) => {
    // console.log(comment.images.length);
    var jsx_stars: JSX.Element[] = [];
      for(var i = 0;i<comment.images.length;i++)
      {
          jsx_stars.push( <img src={`${APP_ENV.BASE_URL}Images/commentImages/${comment.images[i].name}`} width={100} height={100} className=' mr-1 hover:contrast-75 image-container'/>);
      }
      return jsx_stars;
  }
  const bg = (imgName: string) => {
    //console.log(user);
        return `${APP_ENV.BASE_URL}Images/userImages/${imgName}`;
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
          {/* {Slider(product?.images)} */}
            {product ? (
                <div className="text-center mt-5">
                    <div>{ showProductImage(product) }</div>
                    {/* <div>{product.images.length > 0 ? <img src={`${APP_ENV.BASE_URL}Images/productImages/${product.images[0].name}`} height={60}></img> : null}</div> */}
                    <div>ProductID: {product.id}</div>
                    <div>Name: {product.name}</div>
                    <div>Description: {product.description}</div>
              <div>Category: [{product.categoryId}]{product.categoryName}</div>
              <div><StarComponent Count={5} Rating={getProductRating(product.comments)} /></div>
              <div>Comments: { product.comments.length}</div>

                </div>
            ) : "null"}
        </div>

        <div>
            <div className='mb-10'>
            <div className="flex items-center justify-between">
                <label htmlFor="password" className="block text-sm font-medium leading-6 text-gray-900">
                            Add Comment
                </label>
                
                
              </div>
            <form className='flex' onSubmit={handleSubmit}>

                <div className='w-full flex bg-slate-200 rounded-xl p-2'>

                    <div className='flex flex-col w-[90%] mt-4'>
                        <label htmlFor='Title' className='text-sm mb-1'>Title</label>
                        <input 
                          id="title"
                          name="title"
                          value={values.title}
                          onChange={handleChange}
                          required
                          className="shadow-xl outline-0 text-[12px] w-full mr-2 p-3 block rounded-md  py-1.5 text-gray-900 focus:shadow-xl ring-gray-300 placeholder:text-gray-400 " />
                        <label htmlFor='Text'  className='text-sm mb-1'>Text</label>
                        <textarea 
                          id="message"
                          name="message"
                          value={values.message}
                          onChange={handleChange}
                          required
                          className="shadow-xl outline-0 text-[12px] w-full mr-2 p-3 block rounded-md  py-1.5 text-gray-900 focus:shadow-xl ring-gray-300 placeholder:text-gray-400 " />
                    </div>
                    
                    <div className='ml-2'>
                                <div className='p-2 w-full flex rounded-full self-end'>
                                  {getStarsForProduct(5)}
                                  {/* <StarComponent Count={5} Rating={4} Event={setStars} /> */}
                            {/* <img onClick={()=>changeStars("1")} id='1' className='h-4 mr-1 hover:contrast-75 image-container' src={imgStar} />
                            <img onClick={()=>changeStars("2")} id='2' className='h-4 mr-1 hover:contrast-75 image-container' src={imgStar} />
                            <img onClick={()=>changeStars("3")} id='3' className='h-4 mr-1 hover:contrast-75 image-container' src={imgStar} />
                            <img onClick={()=>changeStars("4")} id='4' className='h-4 mr-1 hover:contrast-75 image-container' src={imgStar} />
                            <img onClick={()=>changeStars("5")} id='5' className='h-4 mr-1 hover:contrast-75 image-container' src={imgStar} /> */}
                  </div>
                  <div className="form-control">
              <label htmlFor="name" className="form-label">
                Images
              </label>
              <button
                type="button"
                onClick={onAddImageClick}
                className="btn btn-primary"
              >
                Add image
              </button>
              {/* hidden file input */}
              <input
                type="file"
                accept="image/*"
                className={classNames("form-control d-none")}
                id="image"
                name="image"
                ref={fileSelectInputRef as LegacyRef<HTMLInputElement>}
                onChange={onImageChangeHandler}
              />
              <div className="container">
                <div className="d-flex flex-wrap">{imagesPreviewData}</div>
              </div>
              {imageError && (
                <div className="invalid-feedback">{imageError}</div>
              )}
                  </div>
                  {/* <a type="submit" href={"/product?id=" + searchParams.get("id")}><i className='fa fa-edit btnEdit'></i></a> */}
                  
                        <button type="submit" className="btn btn-primary">Add Comment</button>
                    </div>                   
                </div>
            </form>
            <div>
                        {comments ? (
                            comments.map((item: ICommentItem) => (
                              <div className="mb-5 mt-5" style={{background: "#FFCB66", borderRadius: "10px", padding: "20px", width: "40%"}}>
                                <div><img src={bg(item.user.image)} alt="userImage" width="50" height="50" style={{ borderRadius: "8%" }} /> {item.user.firstname} {item.user.lastname}</div>
                                <div>Comment ID: {item.id}</div>
                                <h5>{item.dateCreated}</h5>
                                    <div>Title: {item.title}</div>
                                    <div style ={{background: "#fff", width:"100%", padding: "15px", borderRadius: "10px"}}>Message: {item.message}</div>
                                    <div>Stars: ({item.stars})<StarComponent  Count={5} Rating={item.stars} Event={setStars}/></div>
                                    {/* <div>{item.images.length > 0 ? <img src={`${APP_ENV.BASE_URL}Images/commentImages/${item.images[0].name}`} height={60}></img> : "Don't have images"}</div> */}
                                {item.images.length > 0 ? <div style={{background: "#aaa", borderRadius: "10px", padding: "20px", width: "100%"}} >{ showCommentImage(item) }</div> : <div style={{background: "#FF6347", width:"100%", padding: "5px", borderRadius: "10px"} }>Don't have images</div>}
                                </div>
                            ))) : null}

                                    
            </div>
            <div className=' mt-2'>
                <div className='bg-slate-400'>
                        asasd
                </div>
            </div>
            </div>

        </div>

        </div>
    </>
}
    
export default ProductProfile