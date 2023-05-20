import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import Navbar from '../components/Navbar'

export default function Post(token) {

    if (!token) {
        return <Navigate to="/login"/>
    } 

    return(
        <div className="main-wrapper">
          <Navbar token={token} />
            Post page.
        </div>
    )
}