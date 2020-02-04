import {
    GET_COMMENTS_BEGIN,
    GET_COMMENTS_EMPTY,
    GET_COMMENTS_SUCCESFULL,
    CREATE_COMMENT_FAILURE,
    CREATE_COMMENT_BEGIN,
    CREATE_COMMENT_SUCCESFULL
} from "../actionTypes";
import CommentsAPI from "../api/commentsApi";

const getCommentsEmpty = () => ({
    type: GET_COMMENTS_EMPTY
});

const getCommentsBegin = () => ({
    type: GET_COMMENTS_BEGIN
});

const getCommentsSuccesfull = (comments) => ({
    type: GET_COMMENTS_SUCCESFULL,
    payload: comments
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
        dispatch(getCommentsBegin());
        CommentsAPI.byPost(postId, page, pageSize)
            .then(response => {
                let comments = response.data;
                if(comments.length == 0) dispatch(getCommentsEmpty());
                else dispatch(getCommentsSuccesfull(comments));
            }).catch(error => {
                console.log(error.status, error.data.message);
                dispatch(getCommentsEmpty());
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
                console.log(error.status, error.data.message);
                dispatch(createCommentFailure(parentCommentId));
            });
    }
}

export {
    loadComments,
    createComment
};
