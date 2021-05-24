import React, {Component} from 'react';
import MyTable from '../../components/MyTable';
import SearchPanel from '../../components/SearchPanel';
import {Badge} from 'react-bootstrap';

import './MakeAdminPage.css';
export default class MakeAdmin extends Component {
    render() {
        return (
            <div className="mainContent">
                <Badge variant="secondary">MAKE ADMIN PAGE</Badge>
                <SearchPanel/>
                <MyTable isBannedPage="false"/>
            </div>
        )
    }
}