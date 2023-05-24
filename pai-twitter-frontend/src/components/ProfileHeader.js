import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { useParams } from 'react-router-dom';

async function getUserInfo(token, profileId) {
    return await fetch('http://localhost:58820/api/users/' + profileId, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      }
    })
      .then(data => data.json())
   }

   async function follow(token, profileId) {
    return await fetch('http://localhost:58820/api/follow/' + profileId, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      }
    })
      .then(data => data.json())
   }

   async function unfollow(token, profileId) {
    return await fetch('http://localhost:58820/api/unfollow/' + profileId, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      }
    })
      .then(data => data.json())
   }

export default function ProfileHeader(props) {
    const [userInfo, setUserInfo] = useState();
    const [profileIdState, setProfileIdState] = useState();
    const { profileId } = useParams();

    if (profileIdState != profileId) {
        setProfileIdState(profileId);
        setUserInfo(undefined);
    }

    if (!userInfo) {
      getUserInfo(props.token, profileIdState).then(
        data => { 
            if (!data) data = [];
            console.log(data)
            setUserInfo(data)
        })
    }

    const button = userInfo ?
                            userInfo.isSelf ? <></> 
                                
                                : userInfo.isFollowed ? <button onClick={() => {unfollow(props.token, profileIdState); setUserInfo(undefined)}} style={{float: "right"}}>Unfollow</button> 
                                : <button onClick={() => {follow(props.token, profileIdState); setUserInfo(undefined)}} style={{float: "right"}}>Follow</button>

                            : <></>

    return(
      userInfo &&
        <div className="profileheader-wrapper">
            <h4>{userInfo.firstName + " " + userInfo.lastName} {button}</h4>
            <p>{userInfo.description}</p>
        </div>
    )
}

