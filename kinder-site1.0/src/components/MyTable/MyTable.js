import React, {Component} from 'react';
import {Table, Container} from 'react-bootstrap';
import { connect } from 'react-redux';
import WithKinderService from '../hoc';
import MyTableItem from '../MyTableItem';
import {usersLoaded, usersRequested} from '../../actions';
import {Spinner} from 'react-bootstrap';

import '../PageLink/PageLink.css';
import { Button, Pagination } from 'react-bootstrap';

class MyTable extends Component {

    

    componentDidMount() {
            
        this.props.usersRequested();

        const {KinderService} = this.props;
        KinderService.getAllUsers()
        .then(res => this.props.usersLoaded(res));
    }

    render() {
        const {userList, loading, isBannedPage}=this.props;


        if(loading) {
            return (
                <>
                    <Container>
                        <Spinner animation="border" variant="info" />
                    </Container>
                </>
            );
        }

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
                                    return <MyTableItem key={user.id} user={user} isBannedPage={isBannedPage}/>
                                })
                            }
                        </tbody>
                    </Table>
                    <Button variant="primary" size="lg">
                        Принять изменения
                    </Button>
                    <br></br><br></br>
                    <Pagination>
                        <Pagination.First />
                        <Pagination.Prev />   
                        <Pagination.Item key={1} active={true}>
                            {1}
                        </Pagination.Item>
                        <Pagination.Next />
                        <Pagination.Last />
                    </Pagination>
                </Container>
                <br></br>
            </>
        )
    }
};

const mapStateToProps = state => {
    return {
        userList: state.users,
        loading: state.loading
    }
}

const mapDispatchToProps = {
    usersLoaded,
    usersRequested
};

export default WithKinderService()(connect(mapStateToProps, mapDispatchToProps)(MyTable));