import React from "react"
import Loading from "./Loading";
import { bindActionCreators } from "redux";
import { withRouter, Link } from "react-router-dom"
import { withTranslation } from "react-i18next"
import { connect } from "react-redux"
import { Card, Figure } from "react-bootstrap"
import _ from "lodash"
import { loadCommunitiesCatalog, unloadCommunitiesCatalog } from "../actions/communityActions"

class CommunitiesCatalogPage extends React.Component{
    componentDidMount(){
        this.props.loadCommunitiesCatalog();
    }
    componentWillUnmount(){
        //this.props.unloadCommunitiesCatalog();
    }
    render(){
        const{communities} = this.props;
        return(
            <div>
                {(communities === null || communities.length === 0) &&
                    <Loading/>
                }
                {(communities !== null && communities.length !== 0) &&  
                    _.map(communities, (community, index) => {
                        return (
                            <div key={index}>
                                {communityCard(community)}
                            </div>
                        )
                    })
                }
            </div>
        );
    }
}

function communityCard(community){
    return(
        <Card className="mb-3">
            <Card.Body>
                <div className="row">
                    <div className="col-2">
                        <Figure className="m-0">
                            <Figure.Image 
                                width={150}
                                height={150}
                                roundedCircle 
                                src={community.imageUrl} />
                        </Figure>
                    </div>
                    <div className="col-5">
                        <Card.Title>
                            <Link to={`/communities/${community.id}`}>
                                {community.name}
                            </Link>
                        </Card.Title>
                        <Card.Text>
                            {community.description}
                        </Card.Text>
                    </div>
                </div>
            </Card.Body>
        </Card>
    );
}

const mapStateToProps = state => ({
    communities: state.community.communities,
})

const mapDispatchToProps = dispatch => ({
    loadCommunitiesCatalog: bindActionCreators(loadCommunitiesCatalog, dispatch),
    unloadCommunitiesCatalog: bindActionCreators(unloadCommunitiesCatalog, dispatch),
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CommunitiesCatalogPage)))