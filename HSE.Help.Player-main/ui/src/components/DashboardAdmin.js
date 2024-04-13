import React, { useState } from 'react';

import {
  initiateAlbums,
  initiateGetResult,
  initiateLoadMoreAlbums,
  initiateLoadMorePlaylist,
  initiateLoadMoreArtists,
  initiateLoadMoreTracks,
  updateMyAlbum
} from '../actions/result';
import { connect } from 'react-redux';
import SearchResult from './SearchResult';
import SearchForm from './SearchForm';
import AlbumsForm from './AlbumsForm';
import Loader from './Loader';
import AddingTrackToPlaylist from './AddingTrackToPlaylist';

const DashboardAdmin = (props) => {
  const [isLoading, setIsLoading] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState('albums');
  const [isCreatingAlbum, setCreatingAlbum] = useState(false);
  const { isValidSession, history } = props;
  const [trackUri, setTrackUri] = useState('');
  const [addingTrackPopup, setAddingTrackPopup] = useState(false);
  const [isAlbumsLoaded, setLoadedAlbums] = useState(false);

  const handleSearch = (searchTerm) => {
    if (isValidSession()) {
      setIsLoading(true);
      if (!isAlbumsLoaded) {
        props.dispatch(initiateAlbums()).then(() => {
          setLoadedAlbums(true);
        });
      }
      props.dispatch(initiateGetResult(searchTerm)).then(() => {
        setIsLoading(false);
        setSelectedCategory('albums');
      });
    } else {
      history.push({
        pathname: '/',
        state: {
          session_expired: true
        }
      });
    }
  };

  const handleAddTrackToPlaylist = (uri) => {
    setAddingTrackPopup(true);
    setTrackUri(uri);
  }

  const handleAddTrackToPlaylistApprove = (playlistId) => {
    setIsLoading(true);
    props.dispatch(updateMyAlbum(trackUri, playlistId)).then((res) => {
      setIsLoading(false);
      setAddingTrackPopup(false);
      setTrackUri('');
    })
  }

  const loadMore = async (type) => {
    if (isValidSession()) {
      const { dispatch, albums, artists, playlist, tracks } = props;
      setIsLoading(true);
      
      switch (type) {
        case 'albums':
          await dispatch(initiateLoadMoreAlbums(albums.next));
          break;
        case 'artists':
          await dispatch(initiateLoadMoreArtists(artists.next));
          break;
        case 'playlist':
          await dispatch(initiateLoadMorePlaylist(playlist.next));
          break;
        case 'tracks':
          await dispatch(initiateLoadMoreTracks(tracks.next));
          break;
        default:
      }
      setIsLoading(false);
    } else {
      history.push({
        pathname: '/',
        state: {
          session_expired: true
        }
      });
    }
  };

  const setCategory = (category) => {
    setSelectedCategory(category);
  };

  const { albums, artists, playlist, tracks, myalbums } = props;
  const result = { albums, artists, playlist, tracks, myalbums };

  console.log(myalbums);
  return (
    <div>
        {myalbums && myalbums.items && <AddingTrackToPlaylist addingTrackPopup={addingTrackPopup} myalbums={myalbums} onTrackAdding={handleAddTrackToPlaylistApprove} setTrackUri={setTrackUri} setAddingTrackPopup={setAddingTrackPopup} /> }
        <AlbumsForm isAlbumsLoaded={isAlbumsLoaded} setLoadedAlbums={setLoadedAlbums} setCreatingAlbum={setCreatingAlbum} setIsLoading={setIsLoading} myalbums={myalbums} />
        <div className={`${!isCreatingAlbum ? 'search' : 'hide'}`}>
          <SearchForm handleSearch={handleSearch} />
          <Loader show={isLoading}>Loading...</Loader>
          <SearchResult
              result={result}
              loadMore={loadMore}
              setCategory={setCategory}
              selectedCategory={selectedCategory}
              isValidSession={isValidSession}
              onAddTrackToPlaylist={handleAddTrackToPlaylist}
          />
        </div>
    </div>
  );
};

const mapStateToProps = (state) => {
  return {
    albums: state.albums,
    artists: state.artists,
    playlist: state.playlist,
    tracks: state.tracks,
    myalbums: state.myalbums, 
  };
};

export default connect(mapStateToProps)(DashboardAdmin);
