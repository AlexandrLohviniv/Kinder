import React, { Component } from 'react';
import {Container, InputGroup, FormControl} from 'react-bootstrap';
import LoadingButton from '../LoadingButton';



export default class SearchPanel extends Component {

    

    render() {
        return (
            <div>
                <br></br> {/* Временный отступ. TODO: добавить отступ в css */}
                <Container>

                    {/* Поиск по id
                    <InputGroup size="default" className="mb-3">
                        <InputGroup.Prepend>
                            <InputGroup.Text id="inputGroup-sizing-default">Id</InputGroup.Text>
                        </InputGroup.Prepend>
                        <FormControl aria-label="id" aria-describedby="inputGroup-sizing-default" />
                    </InputGroup> */}
                
                    {/* Поиск по имени и фамилии */}
                    <InputGroup className="mb-3">
                        <InputGroup.Prepend>
                            <InputGroup.Text>First and last name</InputGroup.Text>
                        </InputGroup.Prepend>
                        <FormControl />
                        <FormControl />
                    </InputGroup>

                    {/* Поиск по никнейму */}
                    <InputGroup size="default" className="mb-3">
                        <InputGroup.Prepend>
                            <InputGroup.Text id="inputGroup-sizing-default">Nickname</InputGroup.Text>
                        </InputGroup.Prepend>
                        <FormControl aria-label="Nickname" aria-describedby="inputGroup-sizing-default" />
                    </InputGroup>

                    {/* Поиск по email */}
                    <InputGroup size="default" className="mb-3">
                        <InputGroup.Prepend>
                            <InputGroup.Text id="inputGroup-sizing-default">Email</InputGroup.Text>
                        </InputGroup.Prepend>
                        <FormControl aria-label="email" aria-describedby="inputGroup-sizing-default" />
                    </InputGroup>
                    <LoadingButton text="Найти"/>
                    
                </Container>
                <br></br>
            </div>
        )
    }
}