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

export default function ProfileHeader(props) {
    const [userInfo, setUserInfo] = useState();
    const { profileId } = useParams();

    if (!userInfo) {
      getUserInfo(props.token, profileId).then(
        data => { 
            if (!data) data = [];
            console.log(data)
            setUserInfo(data)
        })
    }

    return(
      userInfo &&
        <div className="profileheader-wrapper">
            <h4>{userInfo.firstName + " " + userInfo.lastName}</h4>
            <p>{userInfo.description}</p>

        </div>
    )
}

