import { 
    SET_MY_ALBUM,
    ADD_MY_ALBUM, 
    DEL_MY_ALBUM
} from '../utils/constants';

const myalbumsReducer = (state = {}, action) => {
    if (!state.items) {
        state.items = [];
    }
    switch(action.type) {
        case SET_MY_ALBUM:
            return {
                ...state,
                items: [...state.items, action.album]
            };
        case ADD_MY_ALBUM:
            return {
                ...state,
                items: [...state.items, ...action.albums]
            }
        case DEL_MY_ALBUM:
            var deletedId = -1;
            for(var i in state.items) {
                var el = state.items[i]
                if (el.id === action.albumId) {
                    deletedId = i;
                    break;
                }
            }
            if (deletedId > -1) {
                state.items.splice(deletedId, 1);
                return {
                    ...state,
                    items: [...state.items],
                }
            }
            return state;
        default: 
            return state;
    }
}

export default myalbumsReducer;

