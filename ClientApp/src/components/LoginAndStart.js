import React, { Component } from 'react';
import { Button, Glyphicon, Form, FormControl, FormGroup, ControlLabel } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Link, Redirect } from 'react-router-dom';
import { loginUserAndStart } from '../services/userService'
import './Home.css'

export class LoginAndStart extends Component {
    //displayName = Home.name

    constructor(props) {
        super(props);
        this.state = {
            email: "",
            firstName: "",
            lastName: "",
            emailValid: null,
            firstNameValid: null,
            lastNameValid: null,
             navigateToStart: false
        }

        // this.state = {
        //     email: "ss@ss.ss",
        //     firstName: "dsd",
        //     lastName: "asdd",
        //     emailValid: 'success',
        //     firstNameValid: 'success',
        //     lastNameValid: 'success',
        //     navigateToStart: false
        // }
    }

    emailChange(e) {
        const length = this.state.email.length;
        let re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        let isEmailValid = re.test(this.state.email);

        this.setState({
            email: e.target.value,
            emailValid: length > 5 && isEmailValid ? 'success' : 'error'
        })
    }

    nameChange(e) {
        this.setState({
            [e.target.name]: e.target.value,
            [e.target.name + "Valid"]: e.target.value && e.target.value.length > 1 ? 'success' : 'error'
        })
    }

    async LoginAndStart() {
        let isSuccess = (e) => e === 'success';

        if (isSuccess(this.state.emailValid) && isSuccess(this.state.firstNameValid) && isSuccess(this.state.lastNameValid)) {
            //todo: add try catch
            let result = await loginUserAndStart(this.state.email, this.state.firstName, this.state.lastName);
            this.setState({
                navigateToStart: true,
                navigateProps: result
            });
        }
    }

    render() {

        let cmdIntro = <span><span className='cmd-intro'>Pro.net</span>:<span className='cmd-slash'>/</span>$</span>;

        if (this.state.navigateToStart) {
            return <Redirect to={{ pathname: "/stages", state: { questions: this.state.navigateProps, user: {email:this.state.email} } }} />
        }

        return (
            <div className="home-wrapper">
                <h1>Pro.net_Quiz# :-/ </h1>
                <p>{cmdIntro} login</p>
                <p> -- Please enter your Email, First Name and Last Name </p>

                <form>
                    <FormGroup controlId="Email" validationState={this.state.emailValid}>
                        <FormControl type="text" name="email" value={this.state.email} placeholder="Enter your Email" onChange={(e) => this.emailChange(e)} />
                    </FormGroup>

                    <FormGroup controlId="FirstName" validationState={this.state.firstNameValid}>
                        <FormControl type="text" name="firstName" value={this.state.firstName} placeholder="Enter your first name" onChange={(e) => this.nameChange(e)} />
                    </FormGroup>

                    <FormGroup controlId="LastName" validationState={this.state.lastNameValid}>
                        <FormControl type="text" name="lastName" value={this.state.lastName} placeholder="Enter your last name" onChange={(e) => this.nameChange(e)} />
                    </FormGroup>
                    <p> -- Click <Button bsStyle="success" onClick={() => this.LoginAndStart()} >Login</Button> and challange yourself in quiz to get prize! </p>
                    <p> -- Chicken? then go <Link to='/'> back to home </Link> and loose your prize </p>
                </form>

            </div>
        );
    }
}
