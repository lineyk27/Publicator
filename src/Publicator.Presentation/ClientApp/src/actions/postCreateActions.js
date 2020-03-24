import PostsAPI from "../api/postsApi";

function createPost(name, content, communityId, tags){
    PostsAPI.createNew(name, content, communityId, tags)
        .then(response => {
            return response.data.id;
        }).catch(error => {
            console.log(error);
            error = true;
        });
}

export default createPost;