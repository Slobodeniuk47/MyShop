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
import { ICommentCreate, ICommentItem } from "../productProfile/types";
import { useTypedSelector } from "../../../store/hooks/useTypedSelector";
import Navbar from "../Navbar";
import { ICategoryItem } from "../../admin/category/types";
import Slider from "../Slider";
import { IUser } from "../../auth/types";
import { Modal } from "bootstrap"

const CommentView=()=>{
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
        console.log("Searchid: " + searchParams.get('id'));
        http.get('api/Comment/GetCommentsByProductId/'+ searchParams.get('id'))
            .then((res) => res.data.payload)
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
            .then((res) =>
           {
              const product = res.data.payload;
                setLoading(false);                
              setProduct(product);
              console.log("json: ", product);
              console.log("res: ", res);
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
    const initValues: ICommentCreate = {
        title: '',
        message: '',
        stars: 5,
        productId: 0,
        userId: 0,
        images: []
  };
    const onSubmitFormikData = async (values: ICommentCreate) => {
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
          await http.post(
            `${APP_ENV.BASE_URL}api/Comment/create`,
            formData,
            {
              headers: {
                "Content-Type": "multipart/form-data",
              },
            }
      )//.then(() => {
      //           loadCommentsByProductId();
      //       });

    //   var created = resp.data as IProductItem;
          //   console.log("resp = ", created);
          console.log("SubloadCommentsByProductId()")
          loadCommentsByProductId();
      await setIsProcessing(false);
    } catch (e: any) {
      const axiosError = e as AxiosError;
      await setIsProcessing(false);
    }
    }
    const createSchema = yup.object({
        // title: yup.string().required("Input title"),
        // message: yup.string().required("Input message"),
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
  const showCommentImage = (comment: ICommentItem) => {
    // console.log(comment.images.length);
    var jsx_stars: JSX.Element[] = [];
      for(var i = 0;i<comment.images.length;i++)
      {
          jsx_stars.push( <img src={`${comment.images[i].name}`} width={100} height={100} className=' mr-1 hover:contrast-75 image-container'/>);
      }
      return jsx_stars;
  }
  const deleteConfirmed = () => {
        console.log(1);

        http.delete('api/Comment/delete/' + deleteId)
            .then(() => {
                loadCommentsByProductId();
            });
  }
  const deleteCommentModel = (id: number) => {
        console.log(id);
        const myElement = document.getElementById("commentDeleteModel") as HTMLElement;
        setId(id);
        const myModal = new Modal(myElement);
        myModal.show();
  }
    return <>
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

            

            {/* <CommentView/> */}



            <div>
            {comments ?
            (
              comments.map((item: ICommentItem) => (                          
                <div className="mb-5 mt-5" style={{ background: "#FFCB66", borderRadius: "10px", padding: "20px", width: "40%" }}>

                  {
                    user?.roles == "Admin" ||
                    user?.roles == "Editor" ||
                    user?.roles == "Moder" || 
                    user?.id == item.userId ? <a onClick={() => deleteCommentModel(item.id)} ><i className='fa fa-trash btnDelete'></i></a> : null}
                <div><img src={item.user.imageURL} alt="userImage" width="50" height="50" style={{ borderRadius: "8%" }} /> {item.user.firstname} {item.user.lastname}</div>
                <div>Comment ID: {item.id}</div>
                <h5>{item.dateCreated}</h5>
                    <div>Title: {item.title}</div>
                    <div style ={{background: "#fff", width:"100%", padding: "15px", borderRadius: "10px"}}>Message: {item.message}</div>
                    <div>Stars: ({item.stars})<StarComponent  Count={5} Rating={item.stars} Event={setStars}/></div>
                {item.images.length > 0 ? <div style={{background: "#aaa", borderRadius: "10px", padding: "20px", width: "100%"}} >{ showCommentImage(item) }</div> : <div style={{background: "#FF6347", width:"100%", padding: "5px", borderRadius: "10px"} }>Don't have images</div>}
                </div>
              ))
            ) : null}

                                    
            </div>
          

            <div className=' mt-2'>
                <div className='bg-slate-400'>
                        asasd
                </div>
            </div>
            </div>

        </div>
            {/*  */}
            <div className="modal fade" id="commentDeleteModel" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title" id="exampleModalLabel">Confirm delete</h5>
                                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>

                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" >Cancel</button>
                                    <button type="button" onClick={() => deleteConfirmed()} className="btn btn-primary" data-bs-dismiss="modal">Delete</button>
                                </div>
                            </div>
                        </div>
                    </div>
            {/*  */}
    </>
}
    
export default CommentView