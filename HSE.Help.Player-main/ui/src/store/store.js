import { createStore, combineReducers, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import albumsReducer from '../reducers/albums';
import artistsReducer from '../reducers/artists';
import playlistReducer from '../reducers/playlist';
import usersReducer from '../reducers/users';
import myalbumsReducer from '../reducers/myalbum';
import trackReducer from '../reducers/tracks';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
const store = createStore(
  combineReducers({
    albums: albumsReducer,
    artists: artistsReducer,
    playlist: playlistReducer,
    tracks: trackReducer,
    userId: usersReducer,
    myalbums: myalbumsReducer
  }),
  composeEnhancers(applyMiddleware(thunk))
);

export default store;
