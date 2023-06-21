import React, { useState, useRef } from 'react';
import PropTypes from 'prop-types';
import configData from "../config.json";

import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Toast } from 'primereact/toast';




async function loginUser(credentials, showError) {
    return fetch(configData.BASE_URL + '/token/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(credentials)
    })
      .then(data => {
          if (data.status == 200) {
            return data.json()
          }
          else {
            data.json().then(j => {
              showError(j.error);
            });
          }
      })
   }

   async function registerUser(newUserInfo, showError, showSuccess, setRegistration) {
    return fetch(configData.BASE_URL + '/users/register/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        FirstName: newUserInfo.registerFirstName,
        LastName: newUserInfo.registerLastName,
        UserName: newUserInfo.registerUserName,
        Email: newUserInfo.registerEmail,
        Password: newUserInfo.registerPassword
      })
    })
      .then(data => {
        if (data.status == 200) {
          showSuccess();
          setRegistration(false);
        }
        else {
          data.json().then(j => {
            showError(j.error);
          });
        }
    })
   }

export default function Login({ setToken }) {
    const toast = useRef(null);
    const showError = (message) => {
      toast.current.show({severity:'error', summary:message, life: 3000});
    }
    const showRegistered = () => {
      toast.current.show({severity:'success', summary:"Zarejestrowano pomyślnie!", detail:"Zaloguj się", life: 3000});
    }
    
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    const [registerFirstName, setRegisterFirstName] = useState();
    const [registerLastName, setRegisterLastName] = useState();
    const [registerUserName, setRegisterUserName] = useState();
    const [registerEmail, setRegisterEmail] = useState();
    const [registerPassword, setRegisterPassword] = useState();
    const [isRegistration, setRegistration] = useState();

    const handleLogin = async e => {
        e.preventDefault();
        const token = await loginUser({
          email,
          password
        }, showError);
        setToken(token);
      }

      const handleRegister = async e => {
        e.preventDefault();
        await registerUser({
          registerFirstName,
          registerLastName,
          registerUserName,
          registerEmail,
          registerPassword
        }, showError, showRegistered, setRegistration);
        setEmail("");
        setPassword("");
      }

    if (isRegistration) return(
      <div className="login-wrapper">
            <Toast ref={toast} />
            <h2>Rejestracja</h2>
            <form onSubmit={handleRegister}>
                <table><tbody>
                <tr><td><InputText placeholder="Imie" type="text" onChange={e => setRegisterFirstName(e.target.value)}/></td></tr>
                <tr><td><InputText placeholder="Nazwisko" type="text" onChange={e => setRegisterLastName(e.target.value)}/></td></tr>
                <tr><td><InputText placeholder="Nazwa użytkownika" type="text" onChange={e => setRegisterUserName(e.target.value)}/></td></tr>
                <tr><td><InputText placeholder="Email" type="text" onChange={e => setRegisterEmail(e.target.value)}/></td></tr>
                <tr><td><InputText placeholder="Hasło" type="password" onChange={e => setRegisterPassword(e.target.value)}/></td></tr>
                <tr><td></td></tr>
                <tr><td><Button type="submit">Zarejestruj</Button></td></tr>
                <tr><td><Button onClick={() => setRegistration(false)}>Powrót</Button></td></tr>
                </tbody></table>
            </form>
        </div>
    )
      
    return(
        <div className="login-wrapper">
            <Toast ref={toast} />
            <h2>Zaloguj się</h2>
            <form onSubmit={handleLogin}>
                <table><tbody>
                <tr><td><InputText placeholder="Email" type="text" onChange={e => setEmail(e.target.value)}/></td></tr>
                <tr><td><InputText placeholder="Hasło" type="password" onChange={e => setPassword(e.target.value)}/></td></tr>
                <tr><td></td></tr>
                <tr><td><Button type="submit">Zaloguj</Button></td></tr>
                <tr><td><Button onClick={() => {
                    setRegistration(true);
                    setRegisterFirstName("");
                    setRegisterLastName("");
                    setRegisterUserName("");
                    setRegisterEmail("");
                    setRegisterPassword("");
                }}> Zarejestruj</Button></td></tr>
                </tbody></table>
            </form>
        </div>
    )
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
  }