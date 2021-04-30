import React, {Component} from 'react';
import {Nav, Navbar, Button} from 'react-bootstrap';
import PageLink from '../PageLink';

import '../PageLink/PageLink.css';

export default class Navibar extends Component {

    render(){
        return(
                <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
                    <Navbar.Brand>KINDER</Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav"/>
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Link><PageLink link="/" title="Home"/></Nav.Link>
                            <Nav.Link><PageLink link='/ban_users' title="Ban"></PageLink></Nav.Link>
                            <Nav.Link><PageLink link='/make_admin' title="Make Admin"></PageLink></Nav.Link>
                        </Nav>
                        <Nav>
                            <Button variant="primary" className="mr-2">Log In</Button>
                        </Nav>
                    </Navbar.Collapse>
                </Navbar>
            )
        }
}