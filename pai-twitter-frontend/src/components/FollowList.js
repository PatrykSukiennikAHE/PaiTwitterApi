import React, { useState } from 'react';
import { Link } from "react-router-dom";

function getFollowingInfo(token) {
    return fetch('http://localhost:58820/api/follow/list/', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      },
    })
      .then(data => { return data.json() } )
   }

export default function FollowList(props) {
    const [followingInfo, setFollowingInfo] = useState();
    if (!followingInfo) {
        getFollowingInfo(props.token).then(
            data => { 
                if (!data) data = [];
                setFollowingInfo(data)
            })
    }

    return (
    <div className='followingList'>
        <table width={"100%"} height={"100%"}><tbody>
        {
            followingInfo && followingInfo.map(following => {
                return(
                    <tr className='followlist-item-wrapper'><td>
                        <Link to={"/profile/" + following.followedId}>
                            {following.followedName}
                        </Link>
                    </td></tr>
                )
            })
        }
        {
            followingInfo && followingInfo.length == 0 && 
                <tr><td>Nikogo nie śledzisz</td></tr>
        }
        
        </tbody></table>
        
    </div>
    )
}
