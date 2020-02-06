import React, { Component } from "react";
import { Link } from "react-router-dom";

class ErrorInfo extends Component {
  render() {
    const headerMessage = "Došlo je do greške";
    const mainMessage = "Molim Vas pokušajte ponovo, hvala na razumevanju.";
    const goHomeMessage = "Vratite se na početnu stranicu";
    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>{ headerMessage }</h2>
              <p>{ mainMessage }</p>
              <Link to="/">{ goHomeMessage }</Link>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default ErrorInfo;
