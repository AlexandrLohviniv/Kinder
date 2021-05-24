import React, {Component} from 'react';
import {Link} from 'react-router-dom';

export default class PageLink extends Component {
    render(){
        const {link, title} = this.props;
        return(
            <Link to={link} className="nav_link">{title}</Link>
        )
    }
}