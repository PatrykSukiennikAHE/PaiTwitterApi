import React, { useState } from 'react';
import { Link } from "react-router-dom";

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

export default function Navbar(props) {
    const [userInfo, setUserInfo] = useState();
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
