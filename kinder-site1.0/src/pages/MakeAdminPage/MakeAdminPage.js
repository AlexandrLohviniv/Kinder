import React, {Component} from 'react';
import MyTable from '../../components/MyTable';
import SearchPanel from '../../components/SearchPanel';

import './MakeAdminPage.css';
export default class MakeAdmin extends Component {
    render() {
        return (
            <div className="mainContent">
                <SearchPanel/>
                <MyTable/>
            </div>
        )
    }
}