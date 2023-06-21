import React, { useState } from 'react';
import { Link } from "react-router-dom";
import NewPostInput from './NewPostInput';
import { format } from 'date-fns'
import { Card } from 'primereact/card';
import { Divider } from 'primereact/divider';

export default function UserList(props) {
    const users = props.users

    console.log(users)

    return (
    <div className='postList'>
        <table width={"100%"} height={"100%"} align="center"><tbody>
        {
            users && users.length > 0 && users.map(user => {
                    return(
                    <tr key={user.userId}><td>
                        <div className='post-wrapper'>
                        <Link to={"/profile/" + user.userId}><span color='grey'>{user.name}</span> @{user.userName}</Link>
                        </div>
                    </td></tr>
                
                    )
            })
        }
        {
            users && users.length == 0 && 
                <tr><td>Brak wyników</td></tr>
        }
        </tbody></table>
        
    </div>
    )
}
