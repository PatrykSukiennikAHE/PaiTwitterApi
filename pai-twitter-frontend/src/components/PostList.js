import React, { useState } from 'react';
import { Link } from "react-router-dom";
import NewPostInput from './NewPostInput';
import { format } from 'date-fns'

function getPosts(token) {
    return fetch('http://localhost:58820/api/follow/list/', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token 
      },
    })
      .then(data => { return data.json() } )
   }

export default function PostList(props) {
    const posts = props.feed
    
    return (
    <div className='postList'>
        <table width={"100%"} height={"100%"} align="center"><tbody>
        {
            posts && posts.length > 0 && posts.map(post => {
                    return(
                    <tr key={post.postId}><td>
                        <div className='post-wrapper'>
                        <Link to={"/profile/" + post.creatorId}>{post.creator}</Link> <span className='post-date'>{post.createdDate}</span>
                        <div className='post-text-wrapper'>{post.contentText}</div>
                        </div>
                    </td></tr>
                    )
            })
        }
        {
            posts && posts.length == 0 && 
                <tr><td>Brak postów</td></tr>
        }
        </tbody></table>
        
    </div>
    )
}
