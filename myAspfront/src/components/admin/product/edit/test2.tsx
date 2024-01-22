import { useFormik } from "formik";
import React, { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { ICategoryItem } from "./../../category/types"; 
import { IProductEdit } from "./../types"; 
import * as yup from "yup";
import classNames from "classnames";
import axios from "axios";
import { useNavigate, Link, useSearchParams, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { APP_ENV } from "../../../../env";
import { formHttp, http } from "../../../../http";

const EditProduct = () => {
    const navigator = useNavigate();
    const [image, setImage] = useState<string>();
    const [searchParams, setSearchParams] = useSearchParams();
    const [isLoading, setLoading] = useState<boolean>();
  const [list, setList] = useState<ICategoryItem[]>([]);
  const [images, setImages] = useState<File[]>([]);
  const { id } = useParams();
    const getProducts = () => {
        console.log("id: " + searchParams.get('id'));
        http.get('api/product/get/' + searchParams.get('id'))
            .then((res) => res.data)
          .then(async (json) => {
            console.log("json: ", json);
                //setFieldValue("id", searchParams.get('id'));
                setLoading(false);
                //setImage(APP_ENV.BASE_URL + "images/categoryImages/" + json.image);
                
                //formik.setFieldValue("imageUpload", setImage(APP_ENV.BASE_URL + "images/categoryImages/" + json.image));
              //values.images = setImage(APP_ENV.BASE_URL + "images/categoryImages/" + json.image);
              //setFieldValue("images", [...values.images, id]);
              images.forEach((i) =>
              {
                setFieldValue("images", [...values.imagesUpload, id])
              });
              // images.forEach((i) => {
              //   formData.append("images", i);
              // });
                if (json.status == 1)
                    json.status = true;
                else if (json.status == 0)
                    json.status = false;
                formik.setValues(json); //Записывает данные в форму для редактирования
                console.log("values ", values);

            })
    }
    const initValues: IProductEdit = {
        id: id ? Number(id) : 0,
        name: "",
        imagesUpload: [],
        description: "",
        price: 0,
        categoryId: 0
    };

    const createSchema = yup.object({
      name: yup.string().required("Input name"),
      price: yup.number().required("Input price"),
      //images: yup.mixed().required("Choose image"),
      description: yup.string().required("Input description"),
    });

    const onSubmitFormikData = (values: IProductEdit) => {
        console.log(values.imagesUpload);
        setLoading(true);
        var check = "false";
        // if (values.imgChange)
        //     check = "true";
        
        formHttp.put("api/product/edit", values).then(() => {
            navigator("/products");
            navigator(0);
        });
    }
    const clickSelect = () => {
        const myElement = document.getElementById("selectedFile") as HTMLInputElement;
        myElement.click();
        console.log(myElement);
    }
    
    const formik = useFormik({
        initialValues: initValues,
        validationSchema: createSchema,
        onSubmit: onSubmitFormikData
    });
    const { setFieldValue,
        values,
        errors,
        touched,
        handleSubmit,
        handleChange } = formik;

    
    
    const changeImage = (event: ChangeEvent<HTMLInputElement>) => {
        console.log(event.target.files);
        if (event.target.files) {
            console.log("set");
            formik.setFieldValue("images", event.target.files[0]);
            console.log(values);
            //values.images = event.target.files[0];
            setImage(URL.createObjectURL(event.target.files[0]));
            // formik.setFieldValue("imgChange", true);
        //formik.setFieldValue("imageUpload", event.target.files[0])
        }
    }
    useEffect(() => {
        //getCategory();
        
        formHttp.get("api/category/get")
            .then(resp => {
                const data = resp.data;               
                setList(data);
                getProducts();
                
            });
    }, []);

    return (
        isLoading ? (
            <div className="loader-container">
                <div className="spinner"></div>
            </div>
        ) : (
            <div className='pageList'>
                <div className='ListColumn'>
                    <div className='tableHeader'>
                        <h2>Edit category</h2>

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
                        <div className="mb-3">
                            <input type="file" id="selectedFile" className='selectInp' name="img" accept="image/*" onChange={changeImage}></input>
                            <input type="button" className='btn btn-primary btnSelect' value="Select image" onClick={clickSelect} />
                            <img className='selectedImg' src={image} height={100}></img>
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
                                type="text"
                                className={classNames("form-control", { "is-invalid": errors.price && touched.price })}
                                id="price"
                                name="price"
                                value={values.price}
                                onChange={handleChange}
                            />
                            {errors.name && touched.name && <div className="invalid-feedback">{errors.name}</div>}

                        </div>
                        <div className="mb-3">
                            <label htmlFor="name" className="form-label">Parent category</label>
                            <select className="form-select" aria-label="Default select example" id="categoryId" name="categoryId" value={values.categoryId===null ? 0 : values.categoryId} onChange={handleChange}>
                                <option value={0} selected>None</option>
                                {list.map(item => {
                                    return (
                                        item.id != Number(searchParams.get('id')) ?
                                            (
                                                <option value={item.id}>{item.id} - {item.name}</option>
                                            )
                                            :
                                            (<></>)
                                    )

                                })}
                            </select>
                        </div>
                        <div className="mb-3 form-check form-switch">

                            {/* <input type="checkbox" className="form-check-input" onChange={handleChange} checked={values.status} id="status"></input> */}
                            <label htmlFor="status" className="form-label">Status</label>
                        </div>
                        <div className=" mb-3">
                            <label htmlFor="description" className="form-label">Description</label>
                            <textarea
                                className={classNames("form-control", { "is-invalid": errors.description && touched.description })}

                                id="description"
                                name="description"
                                style={{ height: "100px" }}
                                value={values.description}
                                onChange={handleChange}
                            ></textarea>
                            {errors.description && touched.description && <div className="invalid-feedback">{errors.description}</div>}

                        </div>

                        <button type="submit" className="btn btn-primary">
                            Save
                        </button>
                    </form>
                </div>

            </div>
        )
    );
};
export default EditProduct;