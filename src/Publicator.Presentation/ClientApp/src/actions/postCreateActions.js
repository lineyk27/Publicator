import PostsAPI from "../api/postsApi";

function createPost(name, content, communityId, tags){
    var postId = "";
    PostsAPI.createNew(name, content, communityId, tags)
        .then(response => {
            postId = response.data.postid;
        }).catch(error => {
            console.log(error.response.data);
        });
    return postId;
}

export default createPost;