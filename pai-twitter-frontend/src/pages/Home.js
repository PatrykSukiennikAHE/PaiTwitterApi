import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import Navbar from '../components/Navbar'


async function getFeed(token) {
    console.log(token);
    return fetch('http://localhost:58820/api/like/2', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token.token 
      },
    })
      .then(data => data.json())
   }


export default function Home(token) {

    if (token === undefined) {
        return <Navigate to="/login"/>
    } 
    console.log("dupa")
    console.log(token)

    const feed = getFeed(token);

    return(
        <div className="main-wrapper">
          <Navbar token={token} />
            Home page.
        </div>
    )
}