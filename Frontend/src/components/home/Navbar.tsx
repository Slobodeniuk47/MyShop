import * as React from "react";
import { Link, useNavigate } from 'react-router-dom';
import { http } from "../../http";
import { useDispatch, useSelector } from "react-redux";
import { AuthReducerActionType } from "../../store/reducers/AuthReducer";
import { useTypedSelector } from "../../store/hooks/useTypedSelector";
import { useTypedDispatch } from "../../store/hooks/useTypedDispatch";
export default function Navbar(){
  const { isAuth, user } = useTypedSelector((store) => store.auth); //useTypedSelector -> store -> auth
  const dispatch = useTypedDispatch();
  const navigator = useNavigate();
  const logoutUser = () => {
        delete http.defaults.headers.common["Authorization"];
        localStorage.removeItem("token");
        dispatch({ type: AuthReducerActionType.LOGOUT_USER });
        navigator("/login");
  }
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
            <ul className="navbar-nav me-auto mb-md-0">
              <li className="nav-item">
                {
                 user?.roles == "Admin" || user?.roles == "Editor" ? (
                  <Link
                    className="nav-link active"
                    aria-current="page"
                    to="/admin"
                  >
                    Admin
                  </Link>
                ) : (
                 ""
                )}
              </li>
            </ul>
            {isAuth ? (
              <ul className="navbar-nav">
                <li className="nav-item">
                  <Link className="nav-link" to="/profile">
                    <img src={user?.image} alt="userImage" width="30" height="30" style={{borderRadius: "50%"}}/>
                    {user?.firstname}[{user?.id}]
                  </Link>
                </li>
                <li className="nav-item">
                  <Link 
                  className="nav-link" 
                  to="/"
                  onClick={() => {logoutUser(); }}>
                    Logout
                  </Link>

                </li>
              </ul>
            ) : (
              <ul className="navbar-nav">
                <li className="nav-item">
                  <Link className="nav-link" to="/login">
                    Sign in
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/register">
                    Sign up
                  </Link>
                </li>
              </ul>
            )}
          </div>
        </div>
      </nav>
    </header>
  );
};