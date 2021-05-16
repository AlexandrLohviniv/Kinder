import React, {Component} from 'react';
import {Table, Container} from 'react-bootstrap';
import { connect } from 'react-redux';
import WithKinderService from '../hoc';
import MyTableItem from '../MyTableItem';
import {usersLoaded} from '../../actions';

import '../PageLink/PageLink.css';

class MyTable extends Component {

    componentDidMount() {
        const {KinderService} = this.props;
        KinderService.getAllUsers()
        .then(res => this.props.usersLoaded(res));
    }

    render() {
        const {userList}=this.props;

        return(
            <>
            <Container>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>id</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Nickname</th>
                                <th>Email</th>
                                <th>Role</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                userList.map(user => {
                                    return <MyTableItem key={user.id} user={user}/>
                                })
                            }
                        </tbody>
                    </Table>
                </Container>
            </>
        )
    }
};

const mapStateToProps = state => {
    return {
        userList: state.users
    }
}

const mapDispatchToProps = {
    usersLoaded
};

export default WithKinderService()(connect(mapStateToProps, mapDispatchToProps)(MyTable));