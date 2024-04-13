import React, { useState } from 'react';
import { connect } from 'react-redux';
import MyAlbumsList from './MyAlbumsList';
import PlaylistCreationForm from './PlaylistCreationForm';
import {
    initiateAlbums,
    insertAlbums,
    deleteMyAlbum
} from '../actions/result';

const AlbumsForm = (props) => {
    const [isAlbumVisible, setAlbumVisibility] = useState(false);
    const [isAlbumCreationVisible, setAlbumCreationVisible] = useState(false);
    const { setCreatingAlbum, dispatch, setIsLoading, userId, myalbums, setLoadedAlbums, isAlbumsLoaded } = props;
    const [isFirstLoading, setFirstLoading] = useState(true);

    const handleMyAlbums = () => {
        if (isFirstLoading && !isAlbumsLoaded) {
            setIsLoading(true);
            dispatch(initiateAlbums()).then(() => {
                setIsLoading(false);
                setAlbumVisibility(!isAlbumVisible);
                setFirstLoading(false);
                setLoadedAlbums(true);
            })
        } else {
            setAlbumVisibility(!isAlbumVisible)
        }
    }

    const handleDeleteAlbum = (albumId) => {
        setIsLoading(true);
        dispatch(deleteMyAlbum(albumId)).then(() => {
            setIsLoading(false);
        })
    }

    const handleCreateAlbum = () => {
        setCreatingAlbum(true);
        setAlbumCreationVisible(true);
    }

    const handleStopCreationProcess = () => {
        setCreatingAlbum(false);
        setAlbumCreationVisible(false);
    }

    const handlePlaylistCreation = (name, description) => {
        setIsLoading(true);
        dispatch(insertAlbums(name, description, userId.userId)).then((res) => {
            setIsLoading(false);
            handleStopCreationProcess();
        })
    }

    return (
        <React.Fragment>
            <div className="my-albums">
                <button
                    className={`${
                        isAlbumVisible ? 'btn active' : 'btn'
                    }`}
                    onClick={handleMyAlbums}
                >
                    My Albums
                </button>
                <button 
                    className={isAlbumVisible ? 'btn' : 'btn hide'}
                    onClick={() => handleCreateAlbum()}
                >
                    Create
                </button>
            </div>
            <div className={`${isAlbumVisible ? 'myAlbums' : 'hide'}`}>
                {myalbums && <MyAlbumsList onDeleteAlbum={handleDeleteAlbum} myalbums={myalbums} />}
            </div>
            <PlaylistCreationForm 
                onPlaylistCreation={handlePlaylistCreation} 
                onStopCreationProcess={handleStopCreationProcess} 
                isAlbumCreationVisible={isAlbumCreationVisible} 
            />
        </React.Fragment>
    )
}

const mapStateToProps = (state) => {
    return {
        userId: state.userId,
    };
}

export default connect(mapStateToProps)(AlbumsForm);
