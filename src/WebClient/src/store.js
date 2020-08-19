import rootReducer from "./reducers/rootReducer";
import thunk from "redux-thunk";
import { createStore, applyMiddleware } from "redux";

const store = createStore(
    rootReducer,
    applyMiddleware(thunk)
);

export default store;