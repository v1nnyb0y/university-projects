import {
  SET_ALBUMS,
  ADD_ALBUMS,
  SET_ARTISTS,
  ADD_ARTISTS,
  SET_PLAYLIST,
  ADD_PLAYLIST,
  ADD_TRACK,
  SET_TRACK,
  ADD_USERS_DATA,
  SET_MY_ALBUM,
  ADD_MY_ALBUM,
  DEL_MY_ALBUM
} from '../utils/constants';
import { get, post } from '../utils/api';

export const delAlbum = (albumId) => ({
  type: DEL_MY_ALBUM,
  albumId,
});

export const addTrack = (tracks) => ({
  type: ADD_TRACK,
  tracks,
});

export const setTrack = (tracks) => ({
  type: SET_TRACK,
  tracks,
});

export const addMyAlbum = (albums) => ({
  type: ADD_MY_ALBUM,
  albums
})

export const setMyAlbum = (album)  => ({
  type: SET_MY_ALBUM,
  album
});

export const setAlbums = (albums) => ({
  type: SET_ALBUMS,
  albums
});

export const addAlbums = (albums) => ({
  type: ADD_ALBUMS,
  albums
});

export const setArtists = (artists) => ({
  type: SET_ARTISTS,
  artists
});

export const addArtists = (artists) => ({
  type: ADD_ARTISTS,
  artists
});

export const setPlayList = (playlists) => ({
  type: SET_PLAYLIST,
  playlists
});

export const addPlaylist = (playlists) => ({
  type: ADD_PLAYLIST,
  playlists
});

export const addUsersData = (userId) => ({
  type: ADD_USERS_DATA,
  userId
});

export const initiateGetResult = (searchTerm) => {
  return async (dispatch) => {
    try {
      const API_URL = `https://api.spotify.com/v1/search?query=${encodeURIComponent(
        searchTerm
      )}&type=album,playlist,artist,track`;
      const result = await get(API_URL);
      console.log(result);
      const { albums, artists, playlists, tracks } = result;
      dispatch(setAlbums(albums));
      dispatch(setArtists(artists));
      dispatch(setTrack(tracks));
      return dispatch(setPlayList(playlists));
    } catch (error) {
      console.log('error', error);
    }
  };
};

export const initiateLoadMoreTracks = (url) => {
  return async (dispatch) => {
    try {
      const result = await get(url);
      return dispatch(addTrack(result.tracks))
    } catch(error) {
      console.log('error', error);
    }
  }
}

export const initiateLoadMoreAlbums = (url) => {
  return async (dispatch) => {
    try {
      const result = await get(url);
      return dispatch(addAlbums(result.albums));
    } catch (error) {
      console.log('error', error);
    }
  };
};

export const initiateLoadMoreArtists = (url) => {
  return async (dispatch) => {
    try {
      const result = await get(url);
      return dispatch(addArtists(result.artists));
    } catch (error) {
      console.log('error', error);
    }
  };
};

export const initiateLoadMorePlaylist = (url) => {
  return async (dispatch) => {
    try {
      const result = await get(url);
      return dispatch(addPlaylist(result.playlists));
    } catch (error) {
      console.log('error', error);
    }
  };
};

export const initiateUsersData = (token) => {
  return async (dispatch) => {
    try {
      const API_URL = 'https://api.spotify.com/v1/me';
      const result = await get(API_URL);
      return dispatch(addUsersData(result.id));
    } catch (error) {
      console.log('error', error);
    }
  }
}

export const insertAlbums = (name, description, id) => {
  return async (dispatch) => {
    try {
      const API_URI = 'http://localhost:2093/api/post/myalbums';
      return await dispatch(insertAlbumsSpotiry(name, description, id)).then((response) => {
        fetch(API_URI, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            id: response.id,
          })
        }).then((res) => {
          return dispatch(setMyAlbum({
            msg: res.text(),
            name: name,
            description: description, 
            url: response.external_links.spotify,
            id: response.id,
            displayName: response.owner.display_name,
            images: response.images
          }))
        });
      })
    } catch (error) {
      console.log('error', error);
    }
  }
}

export const insertAlbumsSpotiry = (name, description, id) => {
  return async (dispatch) => {
    try {
      const API_URI = `https://api.spotify.com/v1/users/${id}/playlists`
      const result = await post(API_URI, {
        name,
        description,
        public: true,
      });
      return result;
    } catch (error) {
      console.log('error', error);
    }
  }
}

export const initiateAlbums = () => {
  return async (dispatch) => {
    try {
      const API_URI = 'http://localhost:2093/api/get/myalbums';
      await fetch(API_URI, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        }
      })
        .then((res) => {
          return res.text()
        })
        .then((res) => {
          res = JSON.parse(res);
          dispatch(initiateAlbumsSpotify(res));
        })
    } catch(error) {
      console.log('error', error);
    }
  }
}

export const initiateAlbumsSpotify = (ids) => {
  return async (dispatch) => {
    try {
      const playlists = [];
      for (var id in ids) {
        var el = ids[id];
        const API_URI = `https://api.spotify.com/v1/playlists/${el.id}`
        const result = await get(API_URI)
        console.log(result);
        if (result.public) {
          playlists.push({
            name: result.name,
            description: result.description,
            url: result.external_urls.spotify,
            id: result.id,
            displayName: result.owner.display_name,
            images: result.images
          });
        }
      }
      dispatch(addMyAlbum(playlists))
    } catch (error) {
      console.log('error', error);
    }
  }
}

export const updateMyAlbum = (tractUri, albumId) => {
  return async (dispatch) => {
    try {
      const API_URI = `https://api.spotify.com/v1/playlists/${albumId}/tracks?uris=${encodeURI(tractUri)}`;
      await post(API_URI);
      dispatch(updateMyAlbumAPI(albumId));
    } catch (error) {
      console.log('error', error);
    }
  }
}

export const updateMyAlbumAPI = (albumId) => {
  return async (dispatch) => {
    try {
      const API_URI = 'http://localhost:2093/api/post/upd_myalbum';
      await fetch(API_URI, {
        method: 'POST',
        body: {
          id: albumId
        },
        headers: {
          'Content-Type': 'application/json',
        }
      });
    } catch(error) {
      console.log('error', error);
    }
  }
} 

export const deleteMyAlbum = (albumId) => {
  return async (dispatch) => {
    try {
      const API_URI = 'http://localhost:2093/api/post/del_myalbum';
      await fetch(API_URI, {
        method: 'POST',
        body: JSON.stringify({
          id: albumId
        }),
        headers: {
          'Content-Type': 'application/json',
        }
      });
      dispatch(delAlbum(albumId));
    } catch(error) {
      console.log('error', error);
    }
  }
}
