import { 
    POST_CREATE_BEGIN,
    POST_CREATE_SUCCESFULL
} from "../actionTypes";

const initialState = {
    created: false,
    createdId: null
};

function postCreateReducer(state=initialState, action){
    switch(action.types){
        case POST_CREATE_BEGIN:
            return{
                ...state,
                loading: true
            };
        case POST_CREATE_SUCCESFULL:
            return {
                ...state,
                created: true,
                createdId: action.postId
            };
        default:
            return state;
    }
}

export { postCreateReducer }