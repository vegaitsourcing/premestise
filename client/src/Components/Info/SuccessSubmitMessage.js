import React, { Component } from "react";
import { Link } from "react-router-dom";

class SuccessSubmitMessage extends Component {
  render() {
    const headerMessage = "Hvala na kontaktu";
    const mainMessage = "Uskoro ćete dobiti verifikacioni mejl koji morate potvrditi kako biste pristupili sistemu.";
    const goHomeMessage = "Vratite se na početnu stranicu";

    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>{ headerMessage }</h2>
              <p> { mainMessage }</p>
              <Link to="/">{ goHomeMessage }</Link>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default SuccessSubmitMessage;
