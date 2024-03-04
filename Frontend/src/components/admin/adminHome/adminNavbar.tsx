import * as React from "react";
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from "react-redux";
import { http } from "../../../http";
import { useTypedSelector } from "../../../store/hooks/useTypedSelector";
import { useTypedDispatch } from "../../../store/hooks/useTypedDispatch";
//export default function adminNavbar(){
  const AdminNavbar : React.FC = () => {
  const { isAuth, user } = useTypedSelector((store: any) => store.auth);
  //const { isAuth, user } = useTypedSelector((store) => store.auth);
  const dispatch = useTypedDispatch();

    const navigator = useNavigate();
    // React.useEffect(() => {
    //     if (user?.roles != "Admin") {
    //         navigator("/");
    //     }
        
    // }, []);
  return (
    <header>
      <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <div className="container">
          <Link className="navbar-brand" to="/">
            MyShop
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarCollapse"
            aria-controls="navbarCollapse"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarCollapse">
            <ul className="navbar-nav me-auto mb-2 mb-md-0">
              <li className="nav-item">
                  <Link className="nav-link" to="/profile">
                    <img src={user?.image} alt="userImage" width="30" height="30" style={{borderRadius: "50%"}}/>
                    {user?.firstname}
                  </Link>
                </li>
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/admin"
                >
                  Back
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/admin/categories"
                >
                  Categories
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/admin/products"
                >
                  Products
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/admin/users"
                >
                  Users
                </Link>
              </li>
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/admin/roles"
                >
                  Roles
                </Link>
              </li>
            </ul>           
          </div>
        </div>
      </nav>
    </header>
  );
}
export default AdminNavbar;