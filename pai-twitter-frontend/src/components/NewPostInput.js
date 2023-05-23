import React, { useState } from 'react';
import PropTypes from 'prop-types';

async function newPost(token, contentText) {
    return fetch('http://localhost:58820/api/post/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      },
      body: JSON.stringify({
        contentText
      })
    })
      .then(data => data.json())
   }

export default function NewPostInput(props) {
    const [content, setContent] = useState();

    const handleSubmit = async e => {
        e.preventDefault();
        await newPost(props.token, content);
        props.refreshHandler(props.token);
      }

    const handleChange = (event) => {
        setContent(event.target.value);
    };
      
    return(
        <div className="newpost-wrapper">
            <h4>Dodaj nowy post</h4>
            <form onSubmit={handleSubmit}>
                <textarea type="textarea" onChange={handleChange} rows={"3"} cols={"50"} maxLength={"100"}/>
                <br />
                <button type="submit">Dodaj post</button>
            </form>
        </div>
    )
}

