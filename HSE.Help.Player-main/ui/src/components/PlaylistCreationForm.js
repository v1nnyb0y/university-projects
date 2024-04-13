import React, { useState } from 'react';
import Popup from 'reactjs-popup';
import { Form, Button } from 'react-bootstrap';

const PlaylistCreationForm = (props) => {
    const { onPlaylistCreation, isAlbumCreationVisible, onStopCreationProcess } = props;
    const [playlistName, setPlaylistName] = useState('');
    const [playlistDescription, setPlaylistDescription] = useState('');
    const [errorMsg, setErrorMsg] = useState('');

    const handlePlaylistNameChanged = (event) => {
        setPlaylistName(event.target.value);
    }

    const handlePlaylistDescriptionChanged = (event) => {
        setPlaylistDescription(event.target.value);
    }

    const handleSubmitPlaylistCreationForm = () => {
        if (playlistName.trim() !== '') {
            setErrorMsg('')
            onPlaylistCreation(playlistName, playlistDescription);
        } else {
            setErrorMsg('Please fill name of playlist')
        }       
    }

    return (
        <React.Fragment>
            <Popup open={isAlbumCreationVisible} onClose={onStopCreationProcess}>
                <div>
                    <Form className={"popup-form"}>
                        {errorMsg && <p className="errorMsg">{errorMsg}</p>}
                        <Form.Group controlId="formPlaylistName">
                            <Form.Label>Enter playlist name</Form.Label>
                            <Form.Control
                                type="search"
                                name="playlistName"
                                value={playlistName}
                                placeholder="Enter playlist name"
                                onChange={handlePlaylistNameChanged}
                                autoComplete="off"
                            />
                        </Form.Group>
                        <Form.Group controlId="formPlaylistDescription">
                            <Form.Label>Enter playlist name</Form.Label>
                            <Form.Control
                                type="search"
                                name="playlistDescription"
                                value={playlistDescription}
                                placeholder="Enter playlist name"
                                onChange={handlePlaylistDescriptionChanged}
                                autoComplete="off"
                            />
                        </Form.Group>
                        <div className="row">
                            <div className="col-md-6 text-center">
                                <Button variant="info" onClick={handleSubmitPlaylistCreationForm} className="w-75">
                                    Create
                                </Button>
                            </div>
                            <div className="col-md-6 text-center">
                                <Button variant="info" onClick={onStopCreationProcess} className="w-75">
                                    Close
                                </Button>
                            </div>
                        </div>
                    </Form>
                </div>
            </Popup>
        </React.Fragment>
    )
}

export default PlaylistCreationForm;