import React, { useState } from 'react';
import Popup from 'reactjs-popup';
import { Form, Button } from 'react-bootstrap';

const AddingTrackToPlaylist = (props) => {
    const { addingTrackPopup, setAddingTrackPopup, setTrackUri, onTrackAdding, myalbums } = props;
    const [selectedPlaylist, setSelectedPlaylist] = useState(myalbums && myalbums.items && myalbums.items.length > 0 ? myalbums.items[0].id : '');
    const [errorMsg, setErrorMsg] = useState('');

    const handleClosingPopup = () => {
        setAddingTrackPopup(false);
        setTrackUri('');
    }

    const handleChangeSelectedPlaylist = (event) => {
        setSelectedPlaylist(event.target.value);
        console.log(selectedPlaylist);
        console.log(event.target.value);
    }

    const handleAddTrackToPlaylist = () => {
        if (selectedPlaylist) {
            setErrorMsg('');
            onTrackAdding(selectedPlaylist);
        } else {
            setErrorMsg('Please choose playlist');
        }
    }

    return (
        <React.Fragment>
            <Popup open={addingTrackPopup} onClose={handleClosingPopup}>
                <div>
                    <Form className={"popup-form"}>
                        {errorMsg && <p className="errorMsg">{errorMsg}</p>}
                        <Form.Group controlId="formPlaylistName">
                            <Form.Label>Choose source playlist</Form.Label> <br />
                            <select  className={'w-100'} value={selectedPlaylist} onChange={handleChangeSelectedPlaylist}>
                                <option value="">Enter playlist</option>
                                {myalbums.items.map((item,index) => {
                                    return (
                                        <option key={item.id} value={item.id}>{item.name}</option>
                                    )
                                })}
                            </select>
                        </Form.Group>
                        <div className="row">
                            <div className="col-md-6 text-center">
                                <Button variant="info" onClick={handleAddTrackToPlaylist} className="w-75">
                                    Add
                                </Button>
                            </div>
                            <div className="col-md-6 text-center">
                                <Button variant="info" onClick={handleClosingPopup} className="w-75">
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

export default AddingTrackToPlaylist;
