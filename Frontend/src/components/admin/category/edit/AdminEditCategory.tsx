import { useFormik } from "formik";
import React, { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { ICategoryEdit, ICategoryItem } from "../types";
import * as yup from "yup";
import classNames from "classnames";
import axios from "axios";
import { useNavigate, Link, useSearchParams, useParams } from "react-router-dom";
import { APP_ENV } from "../../../../env";
import { formHttp, http } from "../../../../http";

const AdminEditCategory = () => {
    const navigator = useNavigate();
    const [image, setImage] = useState<string>();
    const [list, setList] = useState<ICategoryItem[]>([]);

    const [searchParams, setSearchParams] = useSearchParams();
    const [isLoading, setLoading] = useState<boolean>();
    const loadingCategoryOnFormik = () => {
        console.log("id: " + searchParams.get('id'));
        formHttp.get("api/category/get/"+ searchParams.get('id'))
            .then(resp => {
                const data = resp.data.payload;
                setLoading(false);
                setImage(data.image);
                formik.setValues(data);
                console.log("values ", data);

            })
    }
    const initValues: ICategoryEdit = {
        id: 0,
        name: "",
        imageUpload: new File(["fs"], "", {
            type: "text/plain"
        }),
        description: "",
        parentId: null
    };

    const createSchema = yup.object({
        name: yup.string().required("Input name"),
        image: yup.mixed().required("Choose image"),
        description: yup.string().required("Input description"),
    });

    const onSubmitFormikData = (values: ICategoryEdit) => {
        setLoading(true);
        formHttp.put("api/category/edit", values).then(() => {
            navigator("/admin/categories");
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
            formik.setFieldValue("imageUpload", event.target.files[0]);
            console.log(values);
            values.imageUpload = event.target.files[0];
            setImage(URL.createObjectURL(event.target.files[0]));
        }   
    }
    useEffect(() => {
        formHttp.get("api/category/get")
            .then(resp => {
                const data = resp.data.payload;
                setList(data);                
            });
        loadingCategoryOnFormik();
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
                            <label htmlFor="name" className="form-label">Parent category</label>
                            <select className="form-select" aria-label="Default select example" id="parentId" name="parentId" value={values.parentId===null ? 0 : values.parentId} onChange={handleChange}>
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
export default AdminEditCategory;