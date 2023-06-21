import React, { useState } from 'react';

import { InputTextarea } from 'primereact/inputtextarea';
import { Button } from 'primereact/button';

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
        props.refreshHandler(undefined);
      }

    const handleChange = (event) => {
        setContent(event.target.value);
    };
      
    return(
        <div className="newpost-wrapper">
            <h4>Dodaj nowy post</h4>
            <form onSubmit={handleSubmit}>
                <InputTextarea type="textarea" onChange={handleChange} rows={"3"} cols={"50"} maxLength={"100"}/>
                <br />
                <Button type="submit">Dodaj post</Button>
            </form>
        </div>
    )
}

