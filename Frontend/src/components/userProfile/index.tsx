import * as React from 'react';
import { Outlet, Link, useNavigate } from 'react-router-dom';
import { useEffect, useState } from "react";
import Navbar from '../home/Navbar';
import AdminNavbar from '../admin/adminHome/adminNavbar';
import { useDispatch, useSelector } from "react-redux";
import { AuthReducerActionType } from '../../store/reducers/AuthReducer';
import { useTypedSelector } from "../../store/hooks/useTypedSelector";
import { useTypedDispatch } from "../../store/hooks/useTypedDispatch";

//export default function ProfilePage() {
const ProfilePage: React.FC = () => {
    const [isLoading, setLoading] = useState<boolean>(false);
    const dispatch = useTypedDispatch();
    //const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    const { isAuth, user } = useTypedSelector(store => store.auth);
    const auth = useTypedSelector(store => store.auth)
    const navigator = useNavigate();
  return (
      <div className="pageContent mt-5">
          {/* <Navbar/> */}
          {user?.roles == "Admin" ? <AdminNavbar /> : <Navbar />}
            {isLoading ? (
                <div className="loader-container">
                    <div className="spinner"></div>
                </div>
            ) : (
                <div className='pageList mt-5'>
                      Profile page
                      <div className='iconMenu profileImg' style={{ backgroundRepeat: "no-repeat", backgroundSize: "500px", width: "500px", height: "500px", backgroundImage: `url(${user?.image})` }}></div>
                      <span className="nav-item log">
                                <div>ID: {auth.user?.id}</div>
                                <div>ID: {user?.id}</div>
                                <div>Email: {user?.email}</div>
                                <div>Role: {user?.roles}</div>
                                <div>Firstname: {user?.firstname}</div>
                                <div>Lastname: {user?.lastname}</div>
                                <div>ImageName: {user?.image}</div>
                                <div>Phone: {user?.phoneNumber}</div>
                            </span>
                      <div className="main-menu">
                          <ul className="logout">
                          <li>
                            <span className="nav-item log">
                                <div>[Sidebar.tsx]</div>
                                <div>{user?.email}</div>
                                <div>{user?.roles}</div>
                                <div>{user?.firstname}</div>
                                <div>{user?.lastname}</div>
                                <div>{user?.image}</div>
                                <div>{user?.phoneNumber}</div>
                            </span>
                        </li>
                        <li className="nav-item profile">
                            <a href="#">
                                <div className='iconMenu profileImg' style={{ backgroundSize: "100px", backgroundImage: `url(${user?.image})` }}>

                                </div>
                                <span className="nav-text email">
                                    <div>{user?.email}</div>
                                    <div>{user?.roles}</div>
                                    {/* <div>{user?.firstname}</div>
                                    <div>{user?.image}</div> */}
                                </span>
                                {/* <img className='profileImg' src={'http://bevl.com/storage/images/users/' + user?.image}></img> */}
                            </a>
                        </li>
                      </ul>
                      </div>
                </div>
                
            )}
        </div>
  )
}
export default ProfilePage;