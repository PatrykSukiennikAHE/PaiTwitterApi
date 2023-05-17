import './App.css';
import React, { useState } from 'react';
import {
  BrowserRouter,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Login from './pages/Login';
import { Home } from './pages/Home';
import { Profile } from './pages/Profile';
import { Post } from './pages/Post';
import useToken from './UseToken';


function App() {
  const { token, setToken } = useToken();

  if(!token) {
    return <Navigate replace to="/login"/>
  }

  return (
    <BrowserRouter>
    <div className="App">
        <Routes>
          <Route path="/login" element={<Login/>} />
          <Route path="/home" element={<Home/>} />
          <Route path="/profile" element={<Profile/>} />
          <Route path="/post" element={<Post/>} />
          <Route index element={<Navigate replace to="/home"/>}/>
        </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;