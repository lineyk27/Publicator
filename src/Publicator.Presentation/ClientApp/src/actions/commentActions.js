import {
    GET_COMMENTS_LOAD,
    GET_COMMENTS_UNLOAD,
    GET_COMMENTS_END,
    CREATE_COMMENT_FAILURE,
    CREATE_COMMENT_BEGIN,
    CREATE_COMMENT_SUCCESFULL
} from "../actionTypes";
import CommentsAPI from "../api/commentsApi";

const getCommentsUnload = () => ({
    type: GET_COMMENTS_UNLOAD
});

const getCommentsEnd = () => ({
    type: GET_COMMENTS_END
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

function loadComments(postId, page, pageSize){
    return dispatch => {
        // loading ui
        CommentsAPI.byPost(postId, page, pageSize)
            .then(response => {
                let comments = response.data;
                if(comments.length == 0) dispatch(getCommentsEnd());
                else dispatch(getCommentsLoad(comments));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
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
                dispatch(createCommentSuccesfull(replyCommentId, comment));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
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
