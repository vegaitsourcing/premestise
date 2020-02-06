import React, { Component } from "react";
import { Link } from "react-router-dom";

class ErrorInfo extends Component {
  render() {
    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>Došlo je do greške</h2>
              <p>Molim Vas pokušajte ponovo, hvala na razumevanju.</p>
              <Link to="/">Vratite se na početnu stranicu</Link>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default ErrorInfo;
