import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import Navbar from '../components/Navbar'
import FollowList from '../components/FollowList';
import PostList from '../components/PostList';
import NewPostInput from '../components/NewPostInput';
import UserList from '../components/UsersList';

async function getFeed(token) {
    return await fetch('http://localhost:58820/api/post', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token 
      },
    })
      .then(data => data.json())
   }


export default function Home(props) {
  const [feed, setFeed] = useState();
  const [usersFound, setUsersFound] = useState();

    if (props.token === undefined) {
        return <Navigate to="/login"/>
    } 

    if (!feed) {
      getFeed(props.token).then(
        data => { 
            if (!data) data = [];
            setFeed(data)
        })
    }

    const listToShow = usersFound ? <UserList users={usersFound} /> : <tr><td><PostList token={props.token} feed={feed} /></td></tr>

    return(
        <div className="main-wrapper">
          <Navbar token={props.token} setUsersFound={ setUsersFound }/>
          <table height={"100px"} width={"100%"}><tbody>
            <tr>
              <td width={"70%"}>
                  <table width={"100%"}><tbody>
                    <tr><td><NewPostInput token={props.token} refreshHandler={ setFeed }/> </td></tr>

                    { listToShow }
                    
                    </tbody></table>
                  
                </td>

              <td width={"30%"} height={"100%"}><FollowList token={props.token} /></td>
            </tr>
            </tbody></table>
        </div>
    )
}