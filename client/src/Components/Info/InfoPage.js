import {ReloadPreviousPage} from '../../Actions/ContactFormActions/ContactFormActions';

import React, { Component } from "react";

class InfoPage extends Component {
  render() {
    const headerMessage = "Hvala na kontaktu";
    const goHomeMessage = "Vratite se na početnu stranicu";
    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>{ headerMessage }</h2>
              <p>{ this.props.description }</p>
              <a onClick={ReloadPreviousPage}  href="#">{ goHomeMessage }</a>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default InfoPage;
