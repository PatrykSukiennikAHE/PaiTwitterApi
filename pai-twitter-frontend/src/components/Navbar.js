import React, { useState } from 'react';
import { Link } from "react-router-dom";
import { InputText } from "primereact/inputtext";
import { Button } from 'primereact/button';

function getUserInfo(token) {
    return fetch('http://localhost:58820/api/users/', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token 
      },
    })
      .then(data => { return data.json() } )
   }

function searchUsers(token, searchText, setUsersFound) {
    return fetch('http://localhost:58820/api/usersearch/' + searchText, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token 
      },
    })
      .then(data => { return data.json() } )
      .then(jsonData => setUsersFound(jsonData) )
   }


export default function Navbar(props) {
    const [userInfo, setUserInfo] = useState();
    const [userSearchText, setUserSearchText] = useState();

    if (!userInfo) {
        getUserInfo(props.token).then(data => setUserInfo(data))
    }

    return (
    <div className='navbar'>
        <table width={"100%"} height={"100%"} align="center"><tbody>
        <tr>
            <td>
                <Link to={"/home"}><img height={"40px"} src={"../logo.png"}/></Link>
            </td>
            <td>
                <Link to={"/home"}>PaiTwitter</Link>
            </td>

            <td>
            <span className="p-input-icon-left">
                <i className="pi pi-search" />
                <InputText placeholder="Wyszukaj" onChange={e => setUserSearchText(e.target.value)}/>
            </span>
            </td>

            <td>
            <Button onClick={() => searchUsers(props.token, userSearchText, props.setUsersFound)}>Ok</Button>
            </td>
                

            <td width={"100%"}>
                
            </td>
            <td width={"100px"}>
                <Link to={"/profile/" + userInfo?.id}>
                    <span style={{ whiteSpace: "nowrap" }}>
                    {userInfo?.firstname + " " + userInfo?.surname}
                    </span>
                </Link>
            </td>
        </tr>
        </tbody></table>
        
    </div>
    )
}
