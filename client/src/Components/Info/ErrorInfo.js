import {ReloadPreviousPage} from '../../Actions/ContactFormActions/ContactFormActions';

import React, { Component } from "react";

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
              <a onClick={ReloadPreviousPage}  href="#">{ goHomeMessage }</a>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default ErrorInfo;
