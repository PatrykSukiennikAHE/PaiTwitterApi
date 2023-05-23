import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import Navbar from '../components/Navbar'
import FollowList from '../components/FollowList';
import PostList from '../components/PostList';
import NewPostInput from '../components/NewPostInput';

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

    return(
        <div className="main-wrapper">
          <Navbar token={props.token} />
          <table width={"100%"}><tbody>
            <tr>
              <td width={"70%"}>
                  <table width={"100%"}>
                    <tr><td><NewPostInput token={props.token} refreshHandler={getFeed} /> </td></tr>
                    <tr><td><PostList token={props.token} feed={feed} /></td></tr>
                  </table>
                  
                </td>

              <td width={"30%"}><FollowList token={props.token} /></td>
            </tr>
            </tbody></table>
        </div>
    )
}