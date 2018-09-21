import React, { Component } from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
// import { NavMenu } from './NavMenu';
import './Layout.css';

export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
      <Grid fluid>
        <Row>
          <Col sm={12}>
            <div className="header">
              <div className="logo"></div>
            </div>
          </Col>
        </Row>
        <Row>
          <Col sm={12}>
            <div className="splitter"></div>
          </Col>
        </Row>
        <Row>
          {/* <Col sm={3}>
            <NavMenu />
          </Col> */}
          <Col sm={9}>
            {this.props.children}
          </Col>
        </Row>

        <Row>
          <Col sm={12}></Col>
        </Row>
      </Grid>
    );
  }
}
