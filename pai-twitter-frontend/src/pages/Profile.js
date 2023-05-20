import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import Navbar from '../components/Navbar'

export default function Profile(token) {

    if (!token) {
        return <Navigate to="/login"/>
    } 

    return(
        <div className="main-wrapper">
          <Navbar token={token} />
            Profile page.
        </div>
    )
}