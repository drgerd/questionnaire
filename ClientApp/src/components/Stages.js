import React, { Component } from 'react';
import { Button, Glyphicon, Form, FormControl, FormGroup, ControlLabel, Checkbox } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Link, Redirect } from 'react-router-dom';
import { sendAnswers } from '../services/qActionService'
import './Home.css'

export class Stages extends Component {
    //displayName = Home.name

    constructor(props) {
        super(props);

        this.state = {
            questions: props.location.state.questions,
            user: props.location.state.user,
            stage: 0,
            answers: [],
            isShowStageResult: false,
            stagesResult: null,
            navigateHome: false
        }
    }

    handleChange(e, answer) {
        if (!e.target.checked) {
            let resIndex = this.state.answers.findIndex(x => x.questionId === answer.questionId && x.id === answer.id)
            if (resIndex !== -1) {
                this.state.answers.splice(resIndex, 1);
            }
        } else {
            this.state.answers.push(answer);
        }

        this.setState({
            answers: this.state.answers
        });
    }

    async onSubmit() {
        let result = await sendAnswers(this.state.user, this.state.answers, this.state.stage + 1);
        this.state.stagesResult = result;
        this.state.isShowStageResult = true;
        this.state.answers = [];
        this.state.stage++;
        this.setState({ ...this.state });
    }

    onContinue() {
        this.setState({
            isShowStageResult: false
        })
    }

    onQuit() {
        this.setState({
            navigateHome: true
        })
    }

    cmdIntro = <span><span className='cmd-intro'>Pro.net</span>:<span className='cmd-slash'>/</span>$</span>;

    stageResultNavigationRender(){
        let result = [];

        let isCanContinue = this.state.stage < 3 && this.state.stagesResult.isWin;
        let isNeedQuit = this.state.stage <= 1 && !this.state.stagesResult.isWin;
        let isNeedQuitAndPresent = this.state.stage > 1 && this.state.stage <= 3 && !this.state.stagesResult.isWin;
        let isAwesomeWinner = this.state.stage >= 3 && this.state.stagesResult.isWin;

        if (isAwesomeWinner) {
            result.push(
                <React.Fragment key='q1'>
                    <h2> !!! Winner of the Super Prize !!!! </h2>
                    <h2> ! Congratulations ! </h2>
                </React.Fragment>
            );
        }

        if (isNeedQuitAndPresent) {
            result.push(
                <React.Fragment key='q2'>
                    <h2> You are the Winner of the Prize for stage {isCanContinue ? this.state.stage : this.state.stage - 1} </h2>
                    <h2> ! Congratulations ! </h2>
                </React.Fragment>
            );
        }

        if (isNeedQuit) {
            result.push(
                <React.Fragment key='q3'>
                    <h2> Sorry, at least you tried ! </h2>
                </React.Fragment>
            );
        }

        ///////////////////////////////////////

        result.push(
            <React.Fragment key='q4'>
                <p> -- Show this message to Pro.Net community team! --- </p>
            </React.Fragment>
        );

        ///////////////////////////////////////////

        if (isNeedQuitAndPresent) {
            result.push(
                <React.Fragment key='q5'>
                    <p> -- You Won stage {this.state.stage - 1}. Thank you and <Button bsStyle="success" onClick={() => this.onQuit()}>Quit</Button>.</p>
                </React.Fragment>
            );
        }

        if (isCanContinue) {
            result.push(
                <React.Fragment key='q6'>
                    <p> -- You Won stage {this.state.stage} <Button bsStyle="success" onClick={() => this.onContinue()}>Continue</Button> to try yourself in stage {this.state.stage + 1} to get better prize.</p>
                </React.Fragment>
            );
        }

        if (isAwesomeWinner) {
            result.push(
                <React.Fragment key='q7'>
                    <p> -- You Won All stages ! Thank you and <Button bsStyle="success" onClick={() => this.onQuit()}>Quit</Button>.</p>
                </React.Fragment>
            );
        }

        if (isNeedQuit) {
            result.push(
                <React.Fragment key='q8'>
                    <p> -- You failed {this.state.stage} stage, at least you tried. Thank you and <Button bsStyle="danger" onClick={() => this.onQuit()}>Quit</Button> </p>
                </React.Fragment>
            );
        }
        
        return result;
    }

    drawStageResultItems() {
        debugger;
        console.log(this);
        let result = [];
        for (let i = 1; i <= 3; i++) {
            let stageResult = {
                total: this.state.stagesResult["stage" + i + "Total"],
                done: this.state.stagesResult["stage" + i + "DoneCount"],
            }

            result.push(
                <li key={i + "_drawStageResultItems"}>
                    <p> -- Answered/Total: {stageResult.done} / {stageResult.total} ---
                    {this.state.stage >= i
                            ? stageResult.total === stageResult.done
                                ? <span className='res-won'> Won! </span>
                                : <span className='res-fail'> Failed. </span>
                            : <span className='res-notStarted'> Pending... </span>
                        }</p>
                </li>
            )
        }

        return result;
    }

    stageResultRender() {
        return (
            <div>
                <h1>Pro.net_Quiz# {this.state.stagesResult.isWin ? ':D' : ':['} </h1>
                <p>{this.cmdIntro} getStatus -stage {this.state.stage}</p>
                <p> -- Stage {this.state.stage} Result:</p>
                <ul>
                    {this.drawStageResultItems()}
                </ul>

                {this.stageResultNavigationRender()}
            </div>
        )
    }

    render() {

        let renderAnswer = (answers) => {
            return answers.map((answer) => (
                <Checkbox key={answer.id + '_' + answers.questionId} onChange={(e) => this.handleChange(e, answer)}>{answer.description}</Checkbox>
            ))
        };

        let questionItemRender = this.state.questions.filter((question) => question.stageNumber === this.state.stage + 1).map((question) =>
            <div key={question.id}>
                <pre className="code-header-syntax">
                    <code className="code-header-syntax">{question.description}</code>
                </pre>
                <FormGroup>
                    <pre className="code-answer-syntax">
                        <code className="code-answer-syntax">
                            {renderAnswer(question.answers)}
                        </code>
                    </pre>
                </FormGroup>
            </div>
        );

        let questionsRender = () => {
            return (
                <div>
                    <h1>Pro.net_Quiz# &gt;_&lt; </h1>
                    <p>{this.cmdIntro} start -stage {this.state.stage + 1}</p>
                    <br />
                    {questionItemRender}
                    <br />
                    <br />
                    <p> -- Be careful, you have only one "live", and have no more "saves". Make sure that you are done and then click <Button bsStyle="success" onClick={() => this.onSubmit()}>I am done with stage {this.state.stage + 1}</Button> button. </p>
                </div>
            )
        }
        if (this.state.navigateHome) {
            return <Redirect to={{ pathname: "/", }} />
        }

        return (
            <div className="home-wrapper">
                {this.state.isShowStageResult ? this.stageResultRender() : questionsRender()}
            </div>
        );
    }
}
