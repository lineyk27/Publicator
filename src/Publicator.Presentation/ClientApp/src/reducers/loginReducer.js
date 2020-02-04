import { 
    LOGIN_BEGIN,
    LOGIN_FAILURE, 
    LOGIN_SUCCESFULL 
} from "../actionTypes";

const initialState = {isAuthorized: false, userInfo: null, loading: false, error: false};

function loginReducer (state = initialState, action){
    switch(action.type){
        case LOGIN_BEGIN:
            return{
                ...state,
                loading: true
            }
        case LOGIN_FAILURE:
            return {
                ...state,
                loading: false,
                error: true
            }
        case LOGIN_SUCCESFULL:
            return {
                ...state,
                loading: false,
                errorType: null,
                userInfo: action.payload
            }
        default:
            return state;
    }
}

export default loginReducer;