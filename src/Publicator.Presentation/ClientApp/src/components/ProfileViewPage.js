import React from "react"
import { connect } from "react-redux";
import { withRouter } from "react-router";

class ProfileViewPage extends React.Component{
    render(){
        return(
            <p>Loading...</p>
        );
    }
}

const mapStateToProps = state => ({
    userInfo: state.userView.userInfo,
    loading: state.userView.loading
})

export default withRouter(withTranslation()(connect(mapStateToProps, null)(LogIn)))
