import React, { Component } from "react";
import { Link } from "react-router-dom";
class SuccessSubmitMessage extends Component {
  render() {
    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>Hvala na kontaktu</h2>
              <p>
                Uskoro ćete dobiti verifikacioni mejl koji morate potvrditi kako
                biste pristupili sistemu.
              </p>
              <Link to="/">Vratite se na početnu stranicu</Link>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default SuccessSubmitMessage;
