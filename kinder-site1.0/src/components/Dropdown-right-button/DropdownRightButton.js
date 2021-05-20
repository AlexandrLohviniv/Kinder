import React, { Component } from 'react';
import {DropdownButton, Dropdown} from 'react-bootstrap';

export default class DropdownRightButton extends Component {


    render() {

        const {title, direction} = this.props; 

        return(
            <div className="mb-2">
                <DropdownButton
                    // as={ButtonGroup}
                    key={direction}
                    id={`dropdown-button-drop-${direction}`}
                    drop={direction}
                    variant="light"
                    title={title}
                >
                    <Dropdown.Item eventKey="1">Admin</Dropdown.Item>
                    <Dropdown.Item eventKey="2">simpleUser</Dropdown.Item>
                    <Dropdown.Item eventKey="3">premiumUser</Dropdown.Item>
                    <Dropdown.Item eventKey="4">superLikeUser</Dropdown.Item>
                    <Dropdown.Item eventKey="5">premiumSuperLikeUser</Dropdown.Item>
                    
                </DropdownButton>
                
  </div>
        )
    }
}