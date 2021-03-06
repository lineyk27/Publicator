import {
    GET_COMMENTS_END,
    GET_COMMENTS_LOAD,
    GET_COMMENTS_UNLOAD,
    CREATE_COMMENT_BEGIN,
    CREATE_COMMENT_FAILURE,
    CREATE_COMMENT_SUCCESFULL
} from "../actionTypes";

/*
    comments structure:
    [
        {
            id: guid,
            content: json,
            creationTime: datetime,
            creatorUser: {
                id: guid,
                username: string,
                joinDate: datetime,
                imageUrl: string,
                role: {
                    name: string
                }
                // something else
            },
            replies: [
                {
                    id: guid,
                    ...
                }
            ]
        }
    ]

*/

const initialState = {
    loading: false,
    comments: [],
    replyCommentId: null,
    replyCommentLoading: false
}

function commentReducer(state=initialState, action){
    switch(action.type){
        case GET_COMMENTS_LOAD:
            return {
                comments: action.comments
            };
        case GET_COMMENTS_END:
            return {
                ...state,
                end: true
            };
        case GET_COMMENTS_UNLOAD:
            return initialState;
        case CREATE_COMMENT_BEGIN:
            return {
                ...state,
                replyCommentId: action.replyCommentId,
                replyCommentLoading: true
            };
        case CREATE_COMMENT_SUCCESFULL:
            let comments = state.comments;
            let comment = searchCommentById(action.replyCommentId, comments);
            if(comment !== null) {
                comment.replies.push(action.payload);
                return {
                    ...state,
                    replyCommentLoading: false,
                    replyCommentId: null,
                    comments: state.comments
                };
            }
            else{
                console.log("push to all comments");
                console.log(action.payload);
                return {
                    ...state,
                    replyCommentLoading: false,
                    replyCommentId: null,
                    comments: [action.payload, ...(state.comments)]
                };
            }
        case CREATE_COMMENT_FAILURE:
            return {
                ...state,
                replyCommentId: action.replyCommentId,
                replyCommentLoading: false,
                result: CREATE_COMMENT_FAILURE
            };
        default:
            return state;
    }
}

function searchCommentById(commentId, commentTree){
    if( Array.isArray(commentTree) && commentTree.length !== 0){
       for(let i = 0; i < commentTree.length; i++){
            if(commentTree[i].id === commentId){
                return {comment: commentTree[i]};
            }
            return searchCommentById(commentId, commentTree[i].replies);
        }
    }
    return null;
}

export default commentReducer;