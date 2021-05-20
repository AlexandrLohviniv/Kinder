import React, {Component} from 'react';
import MyTable from '../../components/MyTable';
import SearchPanel from '../../components/SearchPanel';
import {Badge} from 'react-bootstrap';

import './BanPage.css';
export default class BanUsers extends Component {
    render() {
        return (
            <div className="mainContent">
                <Badge variant="secondary">BAN USERS</Badge>
                <SearchPanel/>
                <MyTable/>
            </div>
        )
    }
}