import React from "react";
import _ from "lodash";
import {
    BLOCK_CODE,
    BLOCK_DELIMITER,
    BLOCK_EMBED,
    BLOCK_HEADER,
    BLOCK_IMAGE,
    BLOCK_LIST,
    BLOCK_PARAGRAPH,
    BLOCK_QUOTE,
} from "../constants"
function ItemView(item){
    return(
        <React.Fragment >
            {item.type === BLOCK_PARAGRAPH && 
                <span>{item.data.text}</span>
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
            }{item.type === BLOCK_DELIMITER && 
                <h3 className="text-center">
                    ***
                </h3>
            }{item.type === BLOCK_IMAGE && 
                <div>
                    <img src={item.data.file.url} alt={item.data.file.url} className="mw-100"/>
                    <p className="text-center text-muted">{item.data.caption}</p>
                </div>
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
                    <iframe className="col-xs-12 col-md-7 d-flex" height="350px" src={item.data.embed} title={item.data.embed} />
                </div>
            }
        </React.Fragment>
    );
}

export default ItemView;