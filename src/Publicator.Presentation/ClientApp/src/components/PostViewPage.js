import React, { useState } from "react"
import { createComment, loadComments, unloadComments } from "../actions/commentActions";
import { loadPostView, votePost, bookmarkPost } from "../actions/postViewActions";
import { Link } from "react-router-dom"
import { withRouter } from "react-router";
import { withTranslation } from "react-i18next";
import { connect } from "react-redux"
import { Spinner, Form, Button, ButtonGroup } from "react-bootstrap"
import { bindActionCreators } from "redux"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { 
    faBookmark as solidBookmark, 
    faLongArrowAltDown,
    faLongArrowAltUp
  } from "@fortawesome/free-solid-svg-icons";
import { faBookmark as regularBookmark } from "@fortawesome/free-regular-svg-icons";

import { 
    BLOCK_CODE, 
    BLOCK_DELIMITER, 
    BLOCK_HEADER, 
    BLOCK_IMAGE,
    BLOCK_LIST,
    BLOCK_PARAGRAPH,
    BLOCK_QUOTE,
    BLOCK_EMBED,
    T_CREATOR,
    T_COMMUNITY,
    T_COMMENTS,
    ROUTE_USER
 } from "../constants"
import _ from "lodash"

class PostViewPage extends React.Component{
    constructor(props){
        super(props);
        const{id} = this.props.match.params;
        this.props.loadPostView(id);
        this.props.loadComments(id);
        this.state = { validated: null };
    }
    handleComment = (parentCommentId, text) => {
        const {createComment} = this.props;
        let parentId = parentCommentId !== undefined ? parentCommentId : "";
        const{id} = this.props.match.params;
        createComment(id, text, parentId);
        this.props.loadComments(id);
    }
    handleBookmark = () => {
        console.log("click handled");
        const{id} = this.props.match.params;
        this.props.bookmarkPost(id);
    }
    handleDownVote = () => {
        const{postInfo} = this.props;
        let postId = postInfo.id;
        this.props.votePost(postId, false);
    }
    handleUpVote = () => {
        const{postInfo} = this.props;
        let postId = postInfo.id;
        this.props.votePost(postId, true);
    }
    get voteColor() {
        const {postInfo} = this.props;
        if(postInfo.currentVote !== null){
            if(postInfo.currentVote.up === true) return "text-success";
            else return "text-danger";
        }
        return "text-dark"
    }
    render(){
        const{ postInfo, t, comments } = this.props;
        const{ validated } = this.state;
        console.log("POST INFORMATION !!!!");
        console.log(postInfo);
        return(
            <React.Fragment>
                {postInfo === null && 
                    <div>
                        Loading post...
                    </div>
                }{postInfo !== null &&
                    <div>
                        <h2>{postInfo.name}</h2>
                        <span className="text-muted" >{t(T_CREATOR)}
                            <Link to={`/users/${postInfo.creatorUser.nickname}`}>{postInfo.creatorUser.nickname}</Link>
                        </span>
                        <br/>
                        <span className="text-muted">{t(T_COMMUNITY)}:&nbsp; 
                            <Link to={`/communities/$`}>A community name here.</Link>
                        </span>
                        <hr/>
                        <div>
                            {_.map(JSON.parse(postInfo.content).blocks, itemView)}
                        </div>
                        <hr/>
                        <ButtonGroup >
                            <Button variant="link" color="green" onClick={this.handleUpVote}>
                                <FontAwesomeIcon icon={faLongArrowAltUp} />
                            </Button>
                            <Button variant="link" disabled >
                                <span className={this.voteColor} >
                                    {"   "}{postInfo.currentRating}{"   "}
                                </span>
                            </Button>
                            <Button variant="link" onClick={this.handleDownVote}>
                                <FontAwesomeIcon color="red" icon={faLongArrowAltDown}/>
                            </Button>
                        </ButtonGroup>
                        {" "}
                        <Button 
                            variant={postInfo.currentBookmark.bookmarked === true ? "primary" : "outline-primary"} 
                            onClick={this.handleBookmark}>
                            <FontAwesomeIcon icon={postInfo.currentBookmark.bookmarked === true ? solidBookmark : regularBookmark}/>
                        </Button>
                        <hr/>
                        <h3>{t(T_COMMENTS)}</h3>
                        <div>
                            {comments.loading === true &&
                                <Spinner animation="border" role="status">
                                    <span className="sr-only">Loading...</span>
                                </Spinner>
                            }{comments.comments.length === 0 && 
                                <p className="text-center text-muted">Nothing found!</p>
                            }{comments.comments.length !== 0 && 
                                <CommentsView comments={comments.comments} handleSubmit={this.handleComment}/>
                            }
                        </div>
                        <CommentInput submitCallback={this.handleComment} />
                    </div>
                }
            </React.Fragment>
        );
    }
}

class CommentInput extends React.Component{
    constructor(props){
        super(props);
        this.state = {validated: false, callback: this.props.submitCallback};
    }
    handleSubmit = (event) => {
        const form = event.currentTarget;
        this.setState({validated: true});
        event.preventDefault();
        event.stopPropagation();
        if (form.checkValidity() === false) {
          return;
        }
        let commentText = form.elements["text"].value;
        form.elements["text"].value = '';
        this.state.callback(this.props.parentCommentId, commentText);
        this.setState({validated: true});
    }
    render(){
        const{ parentCommentId } = this.props;
        return(
            <Form onSubmit={this.handleSubmit} >
                <Form.Group>
                    {parentCommentId === undefined && 
                        <Form.Label className="h5" >Create new comment: </Form.Label>
                    }
                    <Form.Control 
                        as="textarea"
                        className="mb-3 mw-100" 
                        rows="3"
                        required
                        type="text"
                        name="text"
                        />
                    <Form.Control.Feedback type="invalid">
                        Comment cannot be empty!
                    </Form.Control.Feedback>
                    <Button variant="primary" type="submit"  >
                        {parentCommentId === undefined ? "Comment" : "Reply"}
                    </Button>
                </Form.Group>
            </Form>
        );
    }
}

class CommentBlock extends React.Component{
    constructor(props){
        super(props);
        this.state = {open: false};
    }
    handleClose = () => {
        this.setState({
            open: !this.state.open
        });
    }
    render(){
        const {comment, handleSubmit} = this.props;
        const{ open } = this.state;
        return(
            <React.Fragment>
                <Link to={`/users/${comment.creatorUser.nickname}`}>{comment.creatorUser.nickname}</Link>
                <p>{comment.content}</p>
                <div>
                    <span onClick={this.handleClose} className="text-muted" >Reply</span>
                </div>
                {open && 
                    <CommentInput submitCallback={handleSubmit} parentCommentId={comment.id} />
                }
                <div className="ml-5">
                    <CommentsView comments={comment.replies} handleSubmit={ handleSubmit }/>
                </div>
            </React.Fragment>
        )
    }
}
class CommentsView extends React.Component{
    render(){
    const {comments, handleSubmit} = this.props;
    return (
        <React.Fragment>
            {comments != undefined && 
                _.map(comments, (comment, index) => {
                    return(
                        <div key={index} className="pl-3">
                            <CommentBlock comment={comment} handleSubmit={handleSubmit}/>
                        </div>
                    );
                })
            }
        </React.Fragment>
        );
    }
}

function itemView(item, index){
    return(
        <React.Fragment key={index}>
            {item.type === BLOCK_PARAGRAPH && 
                <p>{item.data.text}</p>
            }{item.type === BLOCK_HEADER && 
                <h3>
                    {item.data.text}
                </h3>
            }{item.type === BLOCK_CODE && 
                <pre>
                    <code>
                        {item.data.code}
                    </code>
                </pre>
            }{item.type == BLOCK_DELIMITER && 
                <h3 className="text-center">
                    ***
                </h3>
            }{item.type === BLOCK_IMAGE && 
                <React.Fragment>
                    <img src={item.data.file.url} className="mw-100"/>
                    <p className="text-center text-muted">{item.data.caption}</p>
                </React.Fragment>
            }{item.type === BLOCK_LIST &&
                <React.Fragment>
                {item.data.style === "ordered" && 
                    <ol>
                        {_.map(item.data.items, (value,index) => {
                            return(
                                <li key={index} >{value}</li>
                            );
                        })}
                    </ol>
                }{item.data.style === "unordered" && 
                <ul>
                    {_.map(item.data.items, (value,index) => {
                        return(
                            <li key={index} >{value}</li>
                        );
                    })}
                </ul>
                }
                </React.Fragment>
            }{item.type === BLOCK_QUOTE && 
                <blockquote className="blockquote">
                    <p className="mb-0">{item.data.text}</p>
                    <footer className="blockquote-footer">{item.data.caption}</footer>
                </blockquote>
            }{item.type === BLOCK_EMBED && 
                <div className="row justify-content-center">
                    <iframe className="col-xs-12 col-md-7 d-flex" height="350px" src={item.data.embed} />
                </div>
            }
        </React.Fragment>
    );
}

const mapStateToProps = state => ({
    postInfo: state.postView.postInfo,
    comments: state.comment
});

const mapDispatchToProps = dispatch => ({
    createComment: (postId, text, parentComentId) => {
        return dispatch(createComment(postId, text, parentComentId));
    },
    loadComments: (postId) => {
        return dispatch(loadComments(postId));
    },
    unloadComments: bindActionCreators(unloadComments, dispatch),
    loadPostView: bindActionCreators(loadPostView, dispatch),
    bookmarkPost: bindActionCreators(bookmarkPost, dispatch),
    votePost: bindActionCreators(votePost, dispatch),
});

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(PostViewPage)))
