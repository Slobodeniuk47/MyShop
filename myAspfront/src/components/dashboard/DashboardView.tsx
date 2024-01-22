import React from 'react'
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { Modal } from "bootstrap"
import Navbar from '../home/Navbar';

export default function DashboardView() {
  const [isLoading, setLoading] = useState<boolean>(false);

  return (
    <div className="pageContent">
            <Navbar/>
            {isLoading ? (
                <div className="loader-container">
                    <div className="spinner"></div>
                </div>
            ) : (
                <div className='pageList'>
                    
                </div>
                
            )}
        </div>
  )
}
