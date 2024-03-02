import { Link, useNavigate, useSearchParams } from "react-router-dom";
import * as yup from "yup";
import { useFormik } from "formik";
import classNames from "classnames";
import { formHttp, http } from "../../../http";
import { useState, useEffect } from "react";
import { IRoleEdit } from "./types";

const AdminEditRole = () => {

  const navigator = useNavigate();

  useEffect(() => {
    setLoading(true);
    loadingRoleOnFormik();
  }, [])
  const [searchParams, setSearchParams] = useSearchParams();
  const [message, setMessage] = useState<string>("");
  const [isLoading, setLoading] = useState<boolean>();

  const initValues: IRoleEdit = {
    id: 0,
    roleName: "",
    concurrencyStamp: ""
  };
  const createSchema = yup.object({
    roleName: yup
      .string()
      .required("Enter name")
  });

  const loadingRoleOnFormik = () => {
    console.log("id: " + searchParams.get('id'));
    http.get('api/Role/get/' + searchParams.get('id'))
      .then((res) =>
      {
        {
          
          var data = res.data.payload;
          console.log("loasd", data)
          setLoading(false);
          formik.setValues(data);
        }
      })
  }


  const onSubmitFormikData = (values: IRoleEdit) => {
    setLoading(true);
      formHttp.put("api/Role/edit", values).then(() => {
        navigator("..");
        console.log("Submit", values);
        navigator(0);
    });
}

const formik = useFormik({
    initialValues: initValues,
    validationSchema: createSchema,
    onSubmit: onSubmitFormikData
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
            <h2>Edit role</h2>

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
              <label htmlFor="roleName" className="form-label">
                Role name
              </label>
              <input
                type="roleName"
                className={classNames("form-control", {
                  "is-invalid": errors.roleName && touched.roleName,
                })}
                id="roleName"
                name="roleName"
                value={values.roleName}
                onChange={handleChange}
              />
              {errors.roleName && touched.roleName && (
                <div className="invalid-feedback">{errors.roleName}</div>
              )}
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
export default AdminEditRole;