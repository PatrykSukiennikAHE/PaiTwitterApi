import React, { useState } from 'react';
import { Navigate, useParams } from 'react-router-dom';
import Navbar from '../components/Navbar'
import FollowList from '../components/FollowList';
import PostList from '../components/PostList';
import ProfileHeader from '../components/ProfileHeader';

async function getFeed(token, userId) {
    return await fetch('http://localhost:58820/api/post/' + userId, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token 
      },
    })
      .then(data => data.json())
   }


export default function Profile(props) {
  const [feed, setFeed] = useState();
  const { profileId } = useParams();

    if (props.token === undefined) {
        return <Navigate to="/login"/>
    } 

    if (!feed) {
      getFeed(props.token, profileId).then(
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
                    <tr><td><ProfileHeader token={props.token} /> </td></tr>
                    <tr><td><PostList token={props.token} feed={feed} /></td></tr>
                </table>
              </td>
              <td width={"30%"}><FollowList token={props.token} /></td>
            </tr>
            </tbody></table>
        </div>
    )
}