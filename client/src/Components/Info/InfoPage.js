import React, { Component } from "react";
import { Link } from "react-router-dom";
class InfoPage extends Component {
  render() {
    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>Hvala na kontaktu</h2>
              <p>{this.props.description}</p>
              <Link to="/">Vratite se na početnu stranicu</Link>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default InfoPage;
