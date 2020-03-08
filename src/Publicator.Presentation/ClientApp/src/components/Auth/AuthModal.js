import React from 'react';
import { Modal } from "semantic-ui-react";
import AuthWindow from "./AuthWindow";

export default function showAuthModal(props){
    const{page, show} = props;
    console.log(page);
    return(
        <Modal open={show} closeOnDimmerClick={true} centered={false} >
            <AuthWindow page={page}/>
        </Modal>
    );
}