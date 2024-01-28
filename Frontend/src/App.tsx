import React from 'react';
import './App.css';
import { Outlet } from 'react-router-dom';
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";
import Navbar from './components/home/Navbar';
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthReducerActionType } from './store/reducers/AuthReducer';
import { useDispatch, useSelector } from "react-redux";
import { Modal } from "bootstrap"
import { http } from './http';
import { useTypedSelector } from './store/hooks/useTypedSelector';

function App() {

  const { isAuth, user } = useTypedSelector((store: any) => store.auth);
  const navigator = useNavigate();
  const dispatch = useDispatch(); 

  useEffect(() => {
    authCheckState();
  },[])
  const authCheckState = () => { 
//
    if (isAuth) {
      if (user != undefined) {
        //token verification
        if (new Date(user?.exp * 1000) < new Date(Date.now())) {
          logout();
        }
      }
    }
    else {
      console.log("App.tsx => useEffect: user !isAuth")
      //navigator('/login'); 
    }
  }
  const logout = () => {
    delete http.defaults.headers.common["Authorization"];
    localStorage.removeItem("token");
    dispatch({ type: AuthReducerActionType.LOGOUT_USER });
    navigator("/login");
  }

  return (
    <div className="App">
      <Outlet></Outlet>
    </div>
  );
}

export default App;
