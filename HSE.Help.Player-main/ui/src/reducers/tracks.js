import { SET_TRACK, ADD_TRACK } from '../utils/constants';

const trackReducer = (state = {}, action) => {
  const { tracks } = action;
  switch (action.type) {
    case SET_TRACK:
      return tracks;
    case ADD_TRACK:
      return {
        ...state,
        next: tracks.next,
        items: [...state.items, ...tracks.items]
      };
    default:
      return state;
  }
};

export default trackReducer;
