// import { Button } from 'bootstrap';
import React, {Component} from 'react';
import {Nav, Navbar, Button} from 'react-bootstrap';


import './header.css';

export default class Header extends Component {

    render(){
        return(
                <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
                    <Navbar.Brand>KINDER</Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav"/>
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Link>Ban</Nav.Link>
                            <Nav.Link>Make admin</Nav.Link>
                            <Nav.Link>SUKA BLYAT SHTO TAM BILO</Nav.Link>
                        </Nav>
                        <Nav>
                            <Button variant="primary" className="mr-2">Log In</Button>
                        </Nav>
                    </Navbar.Collapse>
                </Navbar>
            )
        }
}