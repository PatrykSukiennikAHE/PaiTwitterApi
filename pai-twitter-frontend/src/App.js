import './App.css';
import React, { useState } from 'react';
import {
  BrowserRouter,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Login from './pages/Login';
import Home from './pages/Home';
import Profile from './pages/Profile';
import Post from './pages/Post';
import useToken from './UseToken';

import "primereact/resources/themes/bootstrap4-light-blue/theme.css";     
import "primereact/resources/primereact.min.css";    

function App() {
  const { token, setToken } = useToken();

  if(!token) {
    return <div className="App"><Login setToken={setToken}/></div>
  } else {
    
  }

  return (
    <BrowserRouter>
    <div className="App">
        <Routes>
          <Route path="/home" element={<Home token={token}/>} />
          <Route path="/profile/:profileId" element={<Profile token={token}/>} />
          <Route path="/post/:postId" element={<Post token={token}/>} />
          <Route index element={<Navigate replace to="/home"/>}/>
        </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;
