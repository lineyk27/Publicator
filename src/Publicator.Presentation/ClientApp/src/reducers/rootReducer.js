import {combineReducers} from "redux";
import commentReducer from "./commentReducer";
import communityReducer from "./communityReducer";
import loginReducer from "./loginReducer";
import postCatalogReducer from "./postCatalogReducer";
import postViewReducer from "./postViewReducer";
import userViewReducer from "./userViewReducer";

const rootReducer = combineReducers({
    comment: commentReducer,
    community: communityReducer,
    login: loginReducer,
    postCatalog: postCatalogReducer,
    postView: postViewReducer,
    userView: userViewReducer
});

export default rootReducer;