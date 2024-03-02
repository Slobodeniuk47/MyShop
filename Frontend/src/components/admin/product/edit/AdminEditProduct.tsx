import { useFormik } from "formik";
import React, { useRef, LegacyRef, ChangeEvent, FormEvent, useState, useEffect } from "react";
import { IProductEdit, IProductItem, IProductImageItem } from "../types";
import { ICategoryItem } from "../../category/types";
//import {IProductItem} from "../list/types"
import * as yup from "yup";
import classNames from "classnames";
import axios from "axios";
import { useNavigate, Link, useSearchParams } from "react-router-dom";
import { formHttp, http } from "../../../../http";
import { APP_ENV } from "../../../../env";
import { AxiosError } from "axios";
// import add from "../../../assets/add.jpg";

const AdminEditProduct = () => {
    const navigator = useNavigate();
    const fileSelectInputRef = useRef<HTMLInputElement>();
    const [isProcessing, setIsProcessing] = useState<boolean>(false);
    const [imageError, setImageError] = useState<string>();
    const [imagesUrl, setImagesUrl] = useState<string[]>([]);
    const [images, setImages] = useState<File[]>([]);
    const [list, setList] = useState<ICategoryItem[]>([]);
    //const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    const [searchParams, setSearchParams] = useSearchParams();
    const [isLoading, setLoading] = useState<boolean>();
    const [imgViews, setImgViews] = useState<IProductImageItem[]>([]);
    const getProductById = () => {
        console.log("id: " + searchParams.get('id'));
        http.get("api/product/get/"+searchParams.get('id'))
            .then(resp => {
                const data = resp.data.payload;
                setFieldValue("id", searchParams.get('id'));
                setLoading(false);
                //setImage(APP_ENV.BASE_URL + "images/categoryImages/" + json.image);
             
                //formik.setFieldValue("imageUpload", setImage(APP_ENV.BASE_URL + "images/categoryImages/" + json.image));
              //values.images = setImage(APP_ENV.BASE_URL + "images/categoryImages/" + json.image);
              //setFieldValue("images", [...values.images, id]);
              //setFieldValue("imagesUpload", searchParams.get('imagesUpload'));
              //formik.setFieldValue("imagesUpload", [...values.imagesUpload, searchParams.get('get'), searchParams.get('name')]);
              //values.imagesUpload = searchParams.get('imagesUpload');
              // images.forEach((i) => {
              //   formData.append("images", i);
            // });
            
                // if (json.status == 1)
                //     json.status = true;
                // else if (json.status == 0)
                //     json.status = false;
                formik.setValues(data); //Записывает данные в форму для редактирования
            console.log("imagesUpload ", data.imagesUpload);
            // onImageChangeHandler(json.imagesUpload);
            
            // setImgViews(json.imagesUpload)
              console.log("data: ", data);
              // console.log("values ", values);

            })
    }
    // useEffect(() => {
    //     formHttp.get("api/category/get")
    //         .then(resp => {
    //             const data = resp.data;
    //             setList(data);
    //         });

    //     // const updatedImagesId = images.map((image) => image.id);
    //     // setFieldValue('imagesId', updatedImagesId);
    // }, [images]);
    const loadMoreCategoriesAsync = async () => {
    const result = await http.get(
      `${APP_ENV.BASE_URL}api/category/get`
    );
    setList(result.data.payload);
  };
    useEffect(() => {
        getProductById();
    setIsProcessing(true);
    const fetchData = async () => {
      try {
        console.log("create tedas");

        await loadMoreCategoriesAsync();
      } catch (error) {
        setIsProcessing(false);
        console.log("get categories list error: ", error);
      }
    };
    fetchData();
    setIsProcessing(false);
  }, []);

    const initValues: IProductEdit = {
        id: 0,
        name: '',
        description: '',
        price: 0,
        //discountPrice: 0,
        categoryId: 0,
        imagesUpload: []
        //priority: 0,
        //status: true
    };

    const createSchema = yup.object({
        name: yup.string().required("Input name"),
        description: yup.string().required("Input description"),
        categoryId: yup.string().required("Input categoryId")
    });

    const onSubmitFormikData = async (values: IProductEdit) => {
        try {
            var formData = new FormData();
            formData.append("id", values.id.toString());
            formData.append("name", values.name);
            formData.append("description", values.description);
            formData.append("price", values.price.toString());
            formData.append("categoryId", values.categoryId?.toString() as string);
      images.forEach((i) => {
        formData.append("imagesUpload", i);
      });
      var resp = await formHttp.put(
        `${APP_ENV.BASE_URL}api/product/edit`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      var created = resp.data as IProductItem;
      console.log("resp = ", created);

      navigator("..");
      await setIsProcessing(false);
    } catch (e: any) {
      const axiosError = e as AxiosError;
      await setIsProcessing(false);
    }
    }

    const formik = useFormik({
        initialValues: initValues,
        validationSchema: createSchema,
        onSubmit: onSubmitFormikData
    });

    // const onChangeImageHandler = (e: ChangeEvent<HTMLInputElement>) => {
    //     const files = e.target.files;
    //     if (files) {
    //         const file = files[0];
    //         const allowTypes = ["image/jpeg", "image/png", "image/jpg"];
    //         if (!allowTypes.includes(file.type)) {
    //             alert("Невірний формат файлу");
    //             return;
    //         }
    //         const upload: IUploadImage = {
    //             image: file
    //         }
    //         formHttp.post('api/product/uploadProductImage', upload, {
    //         })
    //             .then(resp => {
    //                 setImages([...images, resp.data]);
    //             })
    //             .catch(bad => {
    //                 console.log("Bad request", bad);
    //             })

    //     }

    // }
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
    return (
        <div className='pageList'>
            <div className='ListColumn'>
                <div className='tableHeader'>
                    <h2>Edit product</h2>

                    <Link to=".." className='btn btn-success'>

                        <i className='fa fa-2x fa-chevron-circle-left '></i>
                        <span>Back</span>
                    </Link>
                </div>
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label htmlFor="name" className="form-label">Name</label>
                        <input
                            type="text"
                            className={classNames("form-control", { "is-invalid": errors.name && touched.name })}
                            id="name"
                            name="name"
                            value={values.name}
                            onChange={handleChange}
                        />
                        {errors.name && touched.name && <div className="invalid-feedback">{errors.name}</div>}

                    </div>
                    {/* <div className="mb-3">
                        <label htmlFor="name" className="form-label">Priority</label>
                        <input
                            type="number"
                            className="form-control"
                            id="priority"
                            name="priority"
                            value={values.priority}
                            onChange={handleChange}
                        />
                        {errors.name && touched.name && <div className="invalid-feedback">{errors.name}</div>}

                    </div> */}
                    <div className="mb-3">
                        <label htmlFor="name" className="form-label">Price</label>
                        <input
                            type="number"
                            className="form-control"
                            id="price"
                            name="price"
                            value={values.price}
                            onChange={handleChange}
                        />
                        {errors.price && touched.price && <div className="invalid-feedback">{errors.price}</div>}

                    </div>
                    {/* <div className="mb-3">
                        <label htmlFor="name" className="form-label">Discount price</label>
                        <input
                            type="number"
                            className="form-control"
                            id="discountPrice"
                            name="discountPrice"
                            value={values.discountPrice}
                            onChange={handleChange}
                        />
                        {errors.discountPrice && touched.discountPrice && <div className="invalid-feedback">{errors.discountPrice}</div>}
                    </div> */}

                    <div className="mb-3">
                        <label htmlFor="name" className="form-label">Category</label>
                        <select className="form-select" aria-label="Default select example" id="categoryId" name="categoryId" value={values.categoryId} onChange={handleChange}>
                            {/* <option selected>None</option> */}
                            {list.map(item => {
                                return (
                                    <option value={item.id}>{item.id} - {item.name}</option>
                                )
                            })}
                        </select>
                    </div>
                    <div className="mb-3">
            <div className="form-control">
              <label htmlFor="name" className="form-label">
                Image
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
                id="imagesUpload"
                name="imagesUpload"
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
          </div>
          {/* <button type="submit" className="btn btn-success">
            Додати
          </button> */}
                    <div className="form-floating mb-3">
                    <label htmlFor="description" className="form-label">Description</label>
                        <textarea
                            className={classNames("form-control", { "is-invalid": errors.description && touched.description })}
                            placeholder="Вкажіть опис"
                            id="description"
                            name="description"
                            style={{ height: "100px" }}
                            value={values.description}
                            onChange={handleChange}
                        ></textarea>
                        {errors.description && touched.description && <div className="invalid-feedback">{errors.description}</div>}

                    </div>
                    {/* <div className="mb-3 form-check form-switch">

                        <input type="checkbox" className="form-check-input" onChange={handleChange} checked={values.status} id="status">

                        </input>
                        <label htmlFor="status" className="form-label">Status</label>
                    </div> */}
                    <button type="submit" className="btn btn-primary">
                        Add
                    </button>
                </form>
            </div>
        </div>
    );
};
export default AdminEditProduct;