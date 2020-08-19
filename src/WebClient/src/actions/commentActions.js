import {
    GET_COMMENTS_LOAD,
    GET_COMMENTS_UNLOAD,
    CREATE_COMMENT_FAILURE,
    CREATE_COMMENT_BEGIN,
    CREATE_COMMENT_SUCCESFULL
} from "../actionTypes";
import CommentsAPI from "../api/commentsApi";

const getCommentsUnload = () => ({
    type: GET_COMMENTS_UNLOAD
});

const getCommentsLoad = (comments) => ({
    type: GET_COMMENTS_LOAD,
    comments: comments
});

const createCommentBegin = (replyCommentId) => ({
    type: CREATE_COMMENT_BEGIN,
    replyCommentId: replyCommentId
});

const createCommentFailure = (replyCommentId) => ({
    type: CREATE_COMMENT_FAILURE,
    replyCommentId: replyCommentId
});

const createCommentSuccesfull = (replyCommentId, comment) => ({
    type: CREATE_COMMENT_SUCCESFULL,
    payload: comment,
    replyCommentId: replyCommentId
});

function loadComments(postId){
    return dispatch => {
        // loading ui
        CommentsAPI.byPost(postId)
            .then(response => {
                let comments = response.data;
                dispatch(getCommentsLoad(comments));
            }).catch(error => {
                console.log(error);
                dispatch(getCommentsUnload());
            });
    }
}

function createComment(postId, text, parentCommentId){
    return dispatch => {
        dispatch(createCommentBegin(parentCommentId));
        return CommentsAPI.create(postId, text, parentCommentId)
            .then(response => {
                let comment = response.data;
                dispatch(createCommentSuccesfull(parentCommentId, comment));
            }).catch(error => {
                console.log(error);
                dispatch(createCommentFailure(parentCommentId));
            });
    }
}

function unloadComments(){
    return dispatch => {
        dispatch(getCommentsUnload());
    }
}

export {
    loadComments,
    createComment,
    unloadComments
};
