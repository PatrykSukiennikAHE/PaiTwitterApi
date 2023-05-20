import React, { useState } from 'react';
import PropTypes from 'prop-types';

async function loginUser(credentials) {
    return fetch('http://localhost:58820/api/token/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(credentials)
    })
      .then(data => data.json())
   }

export default function Login({ setToken }) {
  
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
                <table>
                <tr><td><h5>Email</h5></td></tr>
                <tr><td><input type="text" onChange={e => setEmail(e.target.value)}/></td></tr>
                <tr><td></td></tr>
                <tr><td><h5>Hasło</h5></td></tr>
                <tr><td><input type="password" onChange={e => setPassword(e.target.value)}/></td></tr>
                <tr><td></td></tr>
                <tr><td><button type="submit">Zaloguj</button></td></tr>
                </table>
            </form>
        </div>
    )
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
  }