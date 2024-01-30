import { Link, useNavigate, useSearchParams } from "react-router-dom";
import * as yup from "yup";
import { useFormik } from "formik";
import classNames from "classnames";
import { formHttp, http } from "../../../../http";
import { useState, ChangeEvent, useEffect } from "react";
import { APP_ENV } from "../../../../env";
import { IEditUser, IPermissions } from "../types";
import { IRoleItem } from "../../role/types";

const AdminEditUser = () => {

  const navigator = useNavigate();

  useEffect(() => {
    setLoading(true);
    loadRoles();
    loadingUserOnFormik();
  }, [])
  const [searchParams, setSearchParams] = useSearchParams();
  const [message, setMessage] = useState<string>("");
  const [isLoading, setLoading] = useState<boolean>();
  const [image, setImage] = useState<string>();
  const [roles, setRoles] = useState<IPermissions[]>([]);

  const initValues: IEditUser = {
    id: 0,
    email: "",
    image: "",
    firstname: "",
    lastname: "",
    phoneNumber: "",
    password: "",
    confirmPassword: "",
    role: "",
  };
  const createSchema = yup.object({
    // email: yup
    //   .string()
    //   .required("Enter name")
    //   .email("Wrong email"),
    // image: yup.mixed().required("Choose image"),
    // firstName: yup.string().required("Enter Firstname").min(2),
    // lastName: yup.string().required("Enter Lastname").min(2),
    // phoneNumber: yup.string().required("Enter Lastname").min(10),
  });

  const loadingUserOnFormik = () => {
    console.log("id: " + searchParams.get('id'));
    http.get('api/Account/get/' + searchParams.get('id'))
      .then((res) =>
      {
        {
          var { payload } = res.data;
          setLoading(false);
          setImage(APP_ENV.BASE_URL + "Images/userImages/" + payload.image)
          formik.setValues(payload);
          formik.setFieldValue("role", payload.permissions[0].roleName); //Init role on the formik
          console.log("payload ", payload);
        }
      })
  }

  const loadRoles = () => {

    http.get("api/Role/get")
      .then(resp => {
        const {payload} = resp.data;

        setRoles(payload);
        console.log(payload);
        
      });
  }


  const onSubmitFormikData = (values: IEditUser) => {
    console.log("das" + values.image);
    setLoading(true);
    formHttp.put("api/Account/edit", values).then(() => {
        navigator("..");
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

const changeImage = (event: ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.files);
    if (event.target.files) {
        console.log("set");
        formik.setFieldValue("image", event.target.files[0]);
        console.log(values);
        values.image = event.target.files[0];
        setImage(URL.createObjectURL(event.target.files[0]));
        formik.setFieldValue("imgChange", true);
    }
}

  const { values, errors, touched, handleSubmit, handleChange } = formik;

  return (
    isLoading ? (
      <div className="loader-container">
        <div className="spinner"></div>
      </div>
    ) : (
      <div className='pageList'>
        <div className='ListColumn'>
          <div className='tableHeader'>
            <h2>Create user</h2>

            <Link to=".." className='btn btn-success'>

              <i className='fa fa-2x fa-chevron-circle-left '></i>
              <span>Back</span>
            </Link>
          </div>


          <form onSubmit={handleSubmit}>
            {message && (
              <div className="alert alert-danger" role="alert">
                {message}
              </div>
            )}
            <div className="mb-2">
              <input type="file" id="selectedFile" className='selectInp' name="img" accept="image/*" onChange={changeImage}></input>
              <input type="button" className='btn btn-primary btnSelect' value="Select image" onClick={clickSelect} />
              <img className='selectedImg' src={image} height={100}></img>
              {values.image === undefined ? (
                <div className="img">Choose image</div>
              ) : <></>}
            </div>

            <div className="mb-2">
              <label htmlFor="email" className="form-label">
                Email
              </label>
              <input
                type="email"
                className={classNames("form-control", {
                  "is-invalid": errors.email && touched.email,
                })}
                id="email"
                name="email"
                value={values.email}
                onChange={handleChange}
              />
              {errors.email && touched.email && (
                <div className="invalid-feedback">{errors.email}</div>
              )}
            </div>
            <div className="mb-2">
              <label htmlFor="name" className="form-label">
                Firstname
              </label>
              <input
                type="text"
                className={classNames("form-control", {
                  "is-invalid": errors.firstname && touched.firstname,
                })}
                id="firstname"
                name="firstname"
                value={values.firstname}
                onChange={handleChange}
              />
              {errors.firstname && touched.firstname && (
                <div className="invalid-feedback">{errors.firstname}</div>
              )}
            </div>
            <div className="mb-2">
              <label htmlFor="lastName" className="form-label">
                Lastname
              </label>
              <input
                type="text"
                className={classNames("form-control", {
                  "is-invalid": errors.lastname && touched.lastname,
                })}
                id="lastname"
                name="lastname"
                value={values.lastname}
                onChange={handleChange}
              />
              {errors.lastname && touched.lastname && (
                <div className="invalid-feedback">{errors.lastname}</div>
              )}
              </div>
              <div className="mb-2">
              <label htmlFor="lastName" className="form-label">
                Phone number
              </label>
              <input
                type="text"
                className={classNames("form-control", {
                  "is-invalid": errors.phoneNumber && touched.phoneNumber,
                })}
                id="phoneNumber"
                name="phoneNumber"
                value={values.phoneNumber}
                onChange={handleChange}
              />
              {errors.phoneNumber && touched.phoneNumber && (
                <div className="invalid-feedback">{errors.phoneNumber}</div>
              )}
            </div>
            
            <div className="mb-3">
              <label htmlFor="role" className="form-label">Role</label>
              <select className="form-select" aria-label="Default select example" id="role" name="role" value={values.role} onChange={handleChange} >
                <option value="None">None</option>
                {roles.map(item => {
                  return (
                    <option value={item.roleName} >{item.roleName}</option>
                  )
                })}
              </select>
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
export default AdminEditUser;