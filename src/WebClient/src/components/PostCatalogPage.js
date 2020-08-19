import React from "react"
import PostCatalog from "./PostCatalog";
import { CATALOG_PERIOD_MONTH, CATALOG_PERIOD_YEAR, CATALOG_PERIOD_DAY, CATALOG_PERIOD_WEEK } from "../constants";
import { 
    POST_CATALOG_TYPE_HOT,
    POST_CATALOG_TYPE_NEW,
    POST_CATALOG_TYPE_SUBSCRIPTION
} from "../actionTypes";
import { 
    unloadPostCatalog, 
    loadByCommunityPostCatalog,
    loadBySubscriptionPostCatalog,
    loadCatalogHot,
    loadCatalogNew
} from "../actions/postCatalogActions"
import { bindActionCreators } from "redux";
import { withRouter, Link } from "react-router-dom"
import { withTranslation } from "react-i18next"
import { Form } from "react-bootstrap"
import { connect } from "react-redux"
import { Button } from "react-bootstrap"
import _ from "lodash"

class PostCatalogPage extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            period: CATALOG_PERIOD_DAY,
            periods: [
                CATALOG_PERIOD_DAY,
                CATALOG_PERIOD_WEEK,
                CATALOG_PERIOD_MONTH,
                CATALOG_PERIOD_YEAR
            ]
        };
    }
    resolveCatalogType = () => {
        if(_.isEmpty(this.props.match.params)){
            this.handleHot();
        }else{
            //this.props.history.push(`/${this.props.match.params.catalog}` );
            if(this.props.match.params.catalog === "new"){
                this.handleNew();
                return POST_CATALOG_TYPE_NEW;
            }else if (this.props.match.params.catalog === "hot"){
                this.handleHot();
                return POST_CATALOG_TYPE_HOT;
            }else{
                this.handleBySubscription();
                return POST_CATALOG_TYPE_SUBSCRIPTION;
            }
        }
    }
    getCatalogType = () => {
        if(this.props.match.params.catalog === "new"){
            return POST_CATALOG_TYPE_NEW;
        }else if (this.props.match.params.catalog === "bySubscription"){
            return POST_CATALOG_TYPE_SUBSCRIPTION;
        }else{
            return POST_CATALOG_TYPE_HOT;
        }
    }
    componentWillMount(){
        this.resolveCatalogType();
    }
    handleHot = () => {
        this.props.loadCatalogHot(1, 10, this.state.period);
        this.props.history.push(`/hot`);
    }
    handleNew = () => {
        this.props.loadCatalogNew(1, 10);
        this.props.history.push(`/new`);
    }
    handleBySubscription = () => {
        this.props.loadCatalogBySubscription(1, 10);
        this.props.history.push(`/bySubscription`);
    }
    handlePeriodChanged = (event) => {
        event.persist();
        var period = this.state.periods[event.currentTarget.selectedIndex];
        this.setState({period: period}, () => {
            this.props.unloadPostCatalog();
            this.handleHot();
        });
    }
    handleLoadMore = () => {
        let { lastPage } = this.props;
        let catalogType = this.props.match.params.catalog;
        if(catalogType === "hot"){
            this.props.loadCatalogHot(lastPage+1, 10, this.state.period);
        }else if(catalogType === "new"){
            this.props.loadCatalogNew(lastPage+1, 10);
        }else if(catalogType === "bySubscription"){
            this.props.loadCatalogBySubscription(lastPage+1, 10);
        }
    }
    getFocusStyle = (type) => {
        let curr = this.getCatalogType();
        if(curr === type){
            return "primary";
        }
        return "secondary";
    }
    render(){
        const{periods} = this.state;
        const{end, isAuthorized} = this.props;
        console.log(end);
        return(
            <div>
                <div className="row mb-3">
                    <div className="col-md-1">
                        <Button
                            onClick={this.handleHot}
                            variant={this.getFocusStyle(POST_CATALOG_TYPE_HOT)}
                            >Hot</Button>
                    </div>
                    <div className="col-md-1">
                        <Button
                            onClick={this.handleNew}
                            variant={this.getFocusStyle(POST_CATALOG_TYPE_NEW)}
                            >New</Button>
                    </div>
                    {isAuthorized && 
                        <div className="col-md-2">
                            <Button
                                onClick={this.handleBySubscription}
                                variant={this.getFocusStyle(POST_CATALOG_TYPE_SUBSCRIPTION)}
                                >Subscription</Button>
                        </div>
                    }
                    {this.getCatalogType() === POST_CATALOG_TYPE_HOT && 
                        <div className="col-md-8 row justify-content-end">
                            <div className="col-md-4 row">
                                <span className="col-md-5">Period</span>
                                <Form className="col-md-7">
                                    <Form.Group controlId="exampleForm.SelectCustom" className="mb-0">
                                        <Form.Control as="select" onChange={this.handlePeriodChanged} >
                                        {(_.map(periods, (value, index) => <option key={index}>{value}</option> )
                                        )}
                                        </Form.Control>
                                    </Form.Group>
                                </Form>
                            </div>
                        </div>
                    }
                </div>
                <PostCatalog/>
                <Button 
                    block
                    size="lg"
                    onClick={this.handleLoadMore} 
                    disabled={end}>
                    {!end ? "Load more" : "All is loaded"}
                </Button>
            </div>
        );
    }
}

const mapStateToProps = state => ({
    posts: state.postCatalog.posts,
    lastPage: state.postCatalog.lastPage,
    catalogType: state.postCatalog.catalogType,
    end: state.postCatalog.end,
    isAuthorized: state.login.isAuthorized
})

const mapDispatchToProps = dispatch => ({
        unloadPostCatalog: bindActionCreators(unloadPostCatalog, dispatch),
        loadByCommunityPostCatalog: bindActionCreators(loadByCommunityPostCatalog, dispatch),
        loadCatalogBySubscription: bindActionCreators(loadBySubscriptionPostCatalog, dispatch),
        loadCatalogNew: bindActionCreators(loadCatalogNew, dispatch),
        loadCatalogHot: bindActionCreators(loadCatalogHot, dispatch)
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(PostCatalogPage)))