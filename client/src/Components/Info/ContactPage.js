import React, { Component } from "react";
import { Link } from "react-router-dom";
class ContactPage extends Component {
  render() {
    return (
      <React.Fragment>
        <div class="info">
          <div class="info-wrap">
            <img src="assets/images/logo.png" alt="" />
            <div class="info-wrap__content">
              <h2>Hvala na kontaktu</h2>
              <p>Uspešno ste poslali poruku, uskoro ćemo Vam odgovoriti.</p>
              <Link to="/">Vratite se na početnu stranicu</Link>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default ContactPage;
