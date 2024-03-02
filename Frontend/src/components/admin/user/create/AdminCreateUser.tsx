import { Link, useNavigate } from "react-router-dom";
import { IRegistration } from "../../../auth/types";
import * as yup from "yup";
import { useFormik } from "formik";
import classNames from "classnames";
import { formHttp, http } from "../../../../http";
import { useState, ChangeEvent, useEffect } from "react";
import { IRoleItem } from "../../role/types";

const AdminCreateUser = () => {

  const navigator = useNavigate();

  useEffect(() => {
    setLoading(true);
    loadRoles();
  }, [])

  
  const [message, setMessage] = useState<string>("");
  const [isLoading, setLoading] = useState<boolean>();
  const [image, setImage] = useState<string>();
  const [roles, setRoles] = useState<IRoleItem[]>([]);

  const initValues: IRegistration = {
    email: "",
    image: "",
    firstname: "",
    lastname: "",
    phoneNumber: "",
    password: "",
    confirmPassword: "",
    role: ""
  };
  const createSchema = yup.object({
    email: yup
      .string()
      .required("Enter name")
      .email("Wrong email"),

    password: yup.string().required("Enter password"),
    confirmPassword: yup.string().required("Confirm password").oneOf([yup.ref('password'), ""], 'Passwords must match'),
    image: yup.mixed().required("Choose image"),
    firstname: yup.string().required("Enter Firstname").min(2),
    lastname: yup.string().required("Enter Lastname").min(2),
    phoneNumber: yup.string().required("Enter Lastname").min(10),
  });

  const loadRoles = () => {

    http.get("api/Role/get")
      .then(resp => {
        const {payload} = resp.data;

        setRoles(payload);
        setLoading(false);
        console.log(payload);
      });
  }

  const clickSelect = () => {
    const myElement = document.getElementById("selectedFile") as HTMLInputElement;
    myElement.click();
    console.log(myElement);
  }

  const changeImage = (event: ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.files);
    if (event.target.files) {
      console.log("set");
      formik.setFieldValue("image", event.target.files[0]);
      values.image = event.target.files[0];
      setImage(URL.createObjectURL(event.target.files[0]));
    }
  }

  const onSubmitFormikData = async (values: IRegistration) => {
    try {
      setLoading(true);
      console.log("Send", values);

      const result = await formHttp.post("api/Account/register", values);
      navigator("..");
    }
    catch (error) {
      setMessage("Wrong data!");
      console.log("Error", error);
    }
  }

  const validateConfirmPassword = (pass: string, value: string) => {

    let error = "";
    if (pass && value) {
      if (pass !== value) {
        error = "Password not matched";
      }
    }
    return error;
  };

  const formik = useFormik({
    initialValues: initValues,
    validationSchema: createSchema,
    onSubmit: onSubmitFormikData,
  });

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
              <label htmlFor="firstname" className="form-label">
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
              <label htmlFor="lastname" className="form-label">
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
            <div className="mb-2">
              <label htmlFor="password" className="form-label">
                Password
              </label>
              <input
                type="password"
                className={classNames("form-control", {
                  "is-invalid": errors.password && touched.password,
                })}
                id="password"
                name="password"
                value={values.password}
                onChange={handleChange}
              />
              {errors.password && touched.password && (
                <div className="invalid-feedback">{errors.password}</div>
              )}
            </div>
            <div className="mb-2">
              <label htmlFor="password_confirmation" className="form-label">
                Password Confirmation
              </label>
              <input
                type="password"
                className={classNames("form-control", {
                  "is-invalid": errors.confirmPassword && touched.confirmPassword,
                })}
                id="confirmPassword"
                name="confirmPassword"
                value={values.confirmPassword}
                onChange={handleChange}

              />
              {errors.confirmPassword && touched.confirmPassword && (
                <div className="invalid-feedback">{errors.confirmPassword}</div>
              )}
            </div>
            <div className="mb-3">
              <label htmlFor="role" className="form-label">Role</label>
              <select className="form-select" aria-label="Default select example" id="role" name="role" value={values.role} onChange={handleChange} >
                {/* <option value="None">None</option> */}
                {roles.map(item => {
                  return (
                    <option value={item.roleName} >{item.roleName}</option>
                  )
                })}
              </select>
            </div>
            <button type="submit" className="btn btn-primary">
              Sign up
            </button>
          </form>
        </div>
      </div>
    )
  );

};
export default AdminCreateUser;