import * as React from 'react';
import { Outlet } from 'react-router-dom';
import { useEffect, useState } from "react";
import AdminNavbar from './adminNavbar';
import Navbar from '../../home/Navbar';

export default function AdminHomePage() {
  const [isLoading, setLoading] = useState<boolean>(false);

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