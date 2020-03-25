import React from "react"
import { withTranslation } from "react-i18next"
import { Container, Badge, Toast } from "react-bootstrap"
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { withRouter } from "react-router";
import EditorJS from "@editorjs/editorjs"
import { Button ,Form, InputGroup } from "react-bootstrap"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faTimesCircle, faExclamationCircle } from "@fortawesome/free-solid-svg-icons"
import _ from "lodash"
import createPost from "../actions/postCreateActions"
import CommunitiesAPI from "../api/communitiesApi"

import { 
    T_POSTNAME,
    T_COMMUNITY,
    T_TAGS,
    T_ADDTAG,
    T_NEWPOST,
    T_CREATEPOST, 
    T_ERROR, 
    T_FILLALLFIELDS
} from "../constants"

class NewPostPage extends React.Component{
    constructor(props){
        super(props);
        this.editor = new EditorJS({
            holderId:"editor"
        });

        this.state = {
            name: "",
            content: {blocks: []},
            tags: [],
            communityId: null,
            currentTag: "",
            communities: null,
            showNotification: false
        }
    }
    componentDidMount() {
        CommunitiesAPI.all()
            .then(response => {
                let comms = response.data;
                this.setState({ communities: comms });
            }).catch(error => {
                console.log(error)
            });
    }
    componentWillUnmount(){
        this.editor.destroy();
    }
    handleAddTag = () => {
        if(this.state.currentTag != ""){
            this.setState(prevState => {
                return{
                    tags: [...prevState.tags, prevState.currentTag],
                    currentTag: ""
                }
            })
        }
    }
    handleChange = (event) => {
        this.setState({[event.currentTarget.name]: event.currentTarget.value});
    }
    handleSubmit = () => {
        this.editor.save().then(data => {
            this.setState({content: data});
        }).catch(reason => {
            console.log(reason);
        });

        if (this.state.name !== "" 
            && this.state.content.blocks.length != 0 
            && this.state.communityId !== null
            && this.state.tags.length != 0) {
                const{name, communityId, tags, content} = this.state;
                this.props.createPost(name, JSON.stringify(content), communityId, tags);
                if (this.props.postInfo != null) {

                }
            }
            else{
                this.showNotification(true);
            }
    }
    handleRemoveTag = (event) => {
        const tagName = event.currentTarget.getAttribute("value");
        this.setState(prevState => {
            const arr = prevState.tags;
            _.pull(arr, tagName);
            return { tags: arr}
        });

    }
    handleCommunityChange = (event) => {
        var community = this.state.communities[event.currentTarget.selectedIndex];
        this.setState({communityId: community.id});
    }
    getPostInfo = () => {
        console.log(this.props);
        console.log(this.props.postInfo);
    }
    showNotification = (val) => {
        this.setState({showNotification: val});
    }
    render(){
        const{t} = this.props;
        return(
            <Container>
                <Toast
                    style={{position: "absolute", backgroundColor: "white"}}
                    onClose={() => this.showNotification(false)} show={this.state.showNotification} delay={5000} autohide>
                    <Toast.Header>
                        <FontAwesomeIcon icon={faExclamationCircle} className="mr-auto" color="red"/>
                        <span className="mr-auto" >{t(T_ERROR)}</span>
                    </Toast.Header>
                    <Toast.Body>
                        {t(T_FILLALLFIELDS)}
                    </Toast.Body>
                </Toast>
                <h3>Create new post.</h3>
                <Form
                    noValidate
                    >
                    <Form.Group>
                        <Form.Label>{t(T_POSTNAME)}</Form.Label>
                        <Form.Control
                            type="text"
                            required
                            name="name"
                            value={this.state.name}
                            onChange={this.handleChange}
                            />
                    </Form.Group>
                    <div id="editor"/>
                    <Form.Group>
                        <h5>{t(T_COMMUNITY)}</h5>
                        <Form.Control as="select" custom onChange={this.handleCommunityChange} >
                            {this.state.communities !== null &&
                                (_.map(this.state.communities, (value, index) => {
                                    return (
                                        <option key={index}>{value.name}</option>
                                        )
                                    })
                                )
                            }
                            {this.state.communities == null &&
                                <option disabled >Loading...</option>
                            }
                        </Form.Control>
                    </Form.Group>
                    <Form.Group>
                        <h5>{t(T_TAGS)}</h5>
                        <div>
                            {this.state.tags.map((tag, index) => {
                                return(
                                    <React.Fragment key={index} >
                                        <Badge
                                            style={{fontSize: "13px"}}
                                            variant="secondary"
                                            value={tag}
                                            onClick={this.handleRemoveTag}
                                            >{tag}{" "}
                                            <FontAwesomeIcon icon={faTimesCircle} size="14px"/>
                                        </Badge>{" "}
                                    </React.Fragment>
                                    )
                                })
                            }
                        </div>
                        <InputGroup className="mb-3">
                            <Form.Control
                                aria-describedby="basic-addon2"
                                name="currentTag"
                                value={this.state.currentTag}
                                onChange={this.handleChange}
                                disabled={this.state.tags.length > 6}
                                />
                            <InputGroup.Append>
                                <Button 
                                    variant="outline-secondary" 
                                    onClick={this.handleAddTag}>{t(T_ADDTAG)}</Button>
                            </InputGroup.Append>
                        </InputGroup>
                    </Form.Group>
                    <Button onClick={this.handleSubmit}>{t(T_CREATEPOST)}</Button>
                </Form>
            </Container>
        );
    }
}

const mapStateToProps = state => ({
    postInfo: state.postView.postInfo
})

const mapDispatchToProps = dispatch => ({
    createPost: bindActionCreators(createPost, dispatch)
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(NewPostPage)))