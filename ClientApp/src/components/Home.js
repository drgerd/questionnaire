import React, { Component } from 'react';
import { Button, Glyphicon } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Link } from 'react-router-dom';
import './Home.css'

export class Home extends Component {
  displayName = Home.name

  render() {

    let cmdIntro = <span><span className='cmd-intro'>Pro.net</span>:<span className='cmd-slash'>/</span>$</span>;
    return (

      <div className="home-wrapper">
        <h1>Pro.net_Quiz# :-| </h1>
        <p>{cmdIntro} help</p>
        <p> -- Welcome to Pro.Net Quiz</p>
        <p> -- More info you can learn in "help -a us"</p>
        <p> -- Use your skills and try to "win_prize"</p>
        <br />
        <p>{cmdIntro}  help -a us</p>
        <p> -- About us:</p>
        <p> -- For more information about Pro.Net Community please visit <a href="https://www.facebook.com/Pro-Net-community-323866207998632">Facebook <Glyphicon glyph="thumbs-up" /> Pro.Net Community </a> </p>
        <p> -- Video from previous meetups you can find  on <a href="https://www.youtube.com/channel/UCHNQfwr64nlpm7pmsrePfSA"> Pro.Net Community <Glyphicon glyph="film" /> YouTube channel Facebook </a> </p>
        <br />
        <p>{cmdIntro} win_prize</p>
        <p> -- <Link to='/loginAndStart'><Button bsStyle="success">Start Quiz</Button> </Link>  and try to win one of three prezents</p>
        {/* <p>Pro.net:~$  <Button  bsStyle="success">Start Quiz</Button>  and try to win one of three prezents</p> */}

        {/* <ul>
          <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
          <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
          <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        <p>To help you get started, we've also set up:</p>
        <ul>
          <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
          <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
          <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
        </ul>
        <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p> */}
      </div>
    );
  }
}
