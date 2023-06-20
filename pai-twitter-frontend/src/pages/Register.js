import React, { useState } from 'react';
import PropTypes from 'prop-types';
import configData from "../config.json";

async function registerUser(credentials) {
    return fetch(configData.BASE_URL + '/token/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(credentials)
    })
      .then(data => data.json())
   }

export default function Register({ setToken }) {
  
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    const handleSubmit = async e => {
        e.preventDefault();
        const token = await loginUser({
          email,
          password
        });
        setToken(token);
      }
      
    return(
        <div className="login-wrapper">
            <h2>Zaloguj się</h2>
            <form onSubmit={handleSubmit}>
                <table><tbody>
                <tr><td><h5>Email</h5></td></tr>
                <tr><td><input type="text" onChange={e => setEmail(e.target.value)}/></td></tr>
                <tr><td></td></tr>
                <tr><td><h5>Hasło</h5></td></tr>
                <tr><td><input type="password" onChange={e => setPassword(e.target.value)}/></td></tr>
                <tr><td></td></tr>
                <tr><td><button type="submit">Zaloguj</button></td></tr>
                </tbody></table>
            </form>
        </div>
    )
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
  }