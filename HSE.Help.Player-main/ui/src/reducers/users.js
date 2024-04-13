import { ADD_USERS_DATA } from '../utils/constants';

const usersReducer = (state = {}, action) => {
    const { userId } = action;
    switch (action.type) {
      case ADD_USERS_DATA:
        return {
            ...state,
            userId
        };
      default:
        return state;
    }
  };
  
  export default usersReducer;
