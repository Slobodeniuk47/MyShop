import * as React from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { useEffect, useState } from "react";
import AdminNavbar from './adminNavbar';
import Navbar from '../../home/Navbar';
import { useTypedSelector } from '../../../store/hooks/useTypedSelector';

export default function AdminHomePage() {
    const [isLoading, setLoading] = useState<boolean>(false);
    const { isAuth, user } = useTypedSelector((store) => store.auth);
  return (
      <div className="pageContent mt-5">
            {/* <Navbar/> */}
            <AdminNavbar/>
            {isLoading ? (
                <div className="loader-container">
                    <div className="spinner"></div>
                </div>
            ) : (
                <div className='pageList mt-5'>
                    Admin Home page
                </div>
                
            )}
        </div>
  )
}