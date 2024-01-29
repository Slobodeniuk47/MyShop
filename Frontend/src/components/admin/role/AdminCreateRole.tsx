import { Link, useNavigate } from "react-router-dom";
import * as yup from "yup";
import { useFormik } from "formik";
import classNames from "classnames";
import { formHttp, http } from "../../../http";
import { useState, useEffect } from "react";
import { IRoleCreate } from "./types";

const AdminCreateRole = () => {

  const navigator = useNavigate();

  useEffect(() => {
    
  }, [])
  const [message, setMessage] = useState<string>("");
  const [isLoading, setLoading] = useState<boolean>();

  const initValues: IRoleCreate = {
    roleName: ""
  };
  const createSchema = yup.object({
    roleName: yup
      .string()
      .required("Enter name")
  });


  const onSubmitFormikData = (values: IRoleCreate) => {
    setLoading(true);
      formHttp.post("api/Role/create", values).then(() => {
        navigator("..");
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
            <h2>Create role</h2>

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
export default AdminCreateRole;