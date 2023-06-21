import React, { useState } from 'react';
import { Link } from "react-router-dom";
import NewPostInput from './NewPostInput';
import { format } from 'date-fns'
import { Card } from 'primereact/card';
import { Divider } from 'primereact/divider';

export default function PostList(props) {
    const posts = props.feed
    
    return (
    <div className='postList'>
        <table width={"100%"} height={"100%"} align="center"><tbody>
        {
            posts && posts.length > 0 && posts.map(post => {
                    return(
                    <tr key={post.postId}><td>
                    <Divider  type="dashed"/>
                        <div className='post-wrapper'>
                        <span color='grey'>{post.creator}</span><Link to={"/profile/" + post.creatorId}>{post.creatorUserName}</Link> <span className='post-date'>{post.createdDate}</span>
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
