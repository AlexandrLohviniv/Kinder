import React, {Component} from 'react';
import MyTable from '../../components/MyTable';

import './BanPage.css';
export default class BanUsers extends Component {
    render() {
        return (
            <div className="mainContent">
                <MyTable/>
            </div>
        )
    }
}