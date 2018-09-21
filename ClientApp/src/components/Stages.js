import React, { Component } from 'react';
import { Button, Glyphicon, Form, FormControl, FormGroup, ControlLabel, Checkbox } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Link } from 'react-router-dom';
import { loginUserAndStart } from '../services/userService'
import './Home.css'

export class Stages extends Component {
    //displayName = Home.name

    constructor(props) {
        super(props);

        this.state = {
            questions: props.location.state.questions,
            stage: 1,
            answers: []
        }
    }

    nameChange(e) {
        this.setState({
            [e.target.name]: e.target.value,
            [e.target.name + "Valid"]: e.target.value && e.target.value.length > 1 ? 'success' : 'error'
        })
    }

    // async LoginAndStart(){
    //     debugger;

    //     let isSuccess = (e)=>e === 'success';

    //     if( isSuccess(this.state.emailValid) && isSuccess(this.state.firstNameValid) && isSuccess(this.state.lastNameValid)){
    //         debugger;
    //         let result = await loginUserAndStart(this.state.email, this.state.firstName, this.state.lastName);
    //         debugger;
    //     }
    // }

    handleChange(e, answer) {
        debugger;
    }


    render() {

        let cmdIntro = <span><span className='cmd-intro'>Pro.net</span>:<span className='cmd-slash'>/</span>$</span>;

        let renderAnswer = (answers) => {
            return answers.map((answer) => (
                <Checkbox onChange={(e) => this.handleChange(e, answer)}>{answer.description}</Checkbox>
            ))
        };

        let questionsRender = this.state.questions.filter((question) => question.stageNumber === this.state.stage).map((question) =>
            <div key={question.id}>
                <p> <pre className="code-header-syntax">
                    <code className="code-header-syntax">{question.description}</code>
                </pre></p>
                <FormGroup>
                    <pre>
                        <code class="language-markup">
                    {renderAnswer(question.answers)}
                    </code>
                    </pre>
                </FormGroup>
            </div>
        );

        return (
            <div className="home-wrapper">
                <h1>Pro.net_Quiz# &gt;_&lt; </h1>
                <p>{cmdIntro} start -stage {this.state.stage}</p>
                <br />
                {questionsRender}
                <br />
                <br />
                 <p> -- Be careful, you have only one "live", and have no more "saves". Make sure that you are done and then click <Button bsStyle="success">I am done with stage {this.state.stage}</Button> button. </p>
            </div>
        );
    }
}
