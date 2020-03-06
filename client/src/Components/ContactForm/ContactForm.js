import { SendForm } from "../../Actions/ContactFormActions/ContactFormActions";

import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

class ContactForm extends Component {
  state = {
    emailErrorMessage: null,
    messageErrorMessage: null,
    email: "",
    message: "",
    emailErrorExists: true,
    messageErrorExists: true,
    privacyContactFormCheckbox: false,
    privacyContactFormErrorMessage: null,
  };

  handleSubmitData = event => {
    event.preventDefault();
    let form = {
      email: this.state.email,
      message: this.state.message
    };

    if (this.state.privacyContactFormCheckbox === false) {
      this.setState({
        privacyContactFormErrorMessage: "Obavezno prihvatiti politiku privatnosti!",
        validationErrorExists: true
      });
    }


    if (form.email === "") {
      this.setState({ emailErrorMessage: "Obavezno uneti e-mail adresu!" });
    }
    if (form.message === "") {
      this.setState({ messageErrorMessage: "Obavezno uneti text poruke!" });
    }

    if (!this.checkIfHasErrors()) {
      this.props.sendForm(form);
    }
  };

  checkIfHasErrors = () => {
    return this.state.emailErrorExists || this.state.messageErrorExists
      ? true
      : false;
  };

  emailFormatValidation = emailAddress => {
    let mailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!mailRegex.test(emailAddress)) {
      this.setState({
        email: emailAddress,
        emailErrorMessage: "Proveriti format e-mail adrese!"
      });
      this.setState({ emailErrorExists: true });
    } else {
      this.setState({
        emailErrorMessage: null,
        email: emailAddress,
        emailErrorExists: false
      });
    }
  };

  handlePrivacyContactFormChange = event => {
    if (event.target.checked === true) {
      this.setState({
        privacyContactFormCheckbox: true,
        privacyContactFormErrorMessage: null
      });
      
    } else {
     
      this.setState({
        privacyContactFormCheckbox: false,
         privacyContactFormErrorMessage: "Obavezno prihvatiti politiku privatnosti!" });
    }
  };

  handleEmailChange = event => {
    event.preventDefault();

    if (event.target.value !== "") {
      this.emailFormatValidation(event.target.value);
    } else {
      this.setState({
        email: "",
        emailErrorMessage: "Proveriti format e-mail adrese!",
        emailErrorExists: true
      });
    }
  };

  handleMessageChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({
        message: event.target.value,
        messageErrorMessage: null,
        messageErrorExists: false
      });
    } else {
      this.setState({
        messageErrorMessage: "Obavezno uneti text poruke!",
        message: "",
        messageErrorExists: true
      });
    }
  };

  render() {

    const { email, message } = this.state;
    const messageSocial = "Ili nas pronađite na Facebook-u:";
    const formHeader = "Pišite nam";
    const formDescription = "Unapred smo zahvalni za sve dobre i loše stvari na sajtu koje nam javite.";
    const sendButtonText = "Pošalji";

    return (
      <section class="contact-us" data-section-name="form">
        <div class="contact-us__wrap wrap">
      <h2 class="contact-us__heading">{formHeader}</h2>
          <form class="contact-us__form">
            <div className="contact-us__email noMarginBottom">
              <input
                class="contact-us__input"
                id="email"
                type="email"
                placeholder="E-mail"
                onChange={this.handleEmailChange}
                value={email}
              />
              <span class="contact-us__text">
                {formDescription}
              </span>
            </div>
            <span className="errorMessageContactForm">{this.state.emailErrorMessage}</span>
            <textarea
              
              className="contact-us__textarea noMarginBottom"
              id="message"
              name="message"
              placeholder="Text"
              onChange={this.handleMessageChange}
              value={message}
            ></textarea>
            <span className="errorMessageContactForm">{this.state.messageErrorMessage}</span>
            <span class="contact-us__text--hidden">
            {formDescription}
            </span>
            <div class="contact-us__send">
            <div  >
               <label for="privacy" className="contact-us__text noMarginLeft">
               <input type="checkbox" name="privacy" className="checkBoxMiddleAlign"
                 checked={this.state.privacyContactFormCheckbox}
                  onClick={this.handlePrivacyContactFormChange}/>
                 &nbsp; Prihvatam politiku privatnosti</label>
                <div>
                <span className="errorMessageContactForm" >
                  {this.state.privacyContactFormErrorMessage}
                </span>
                </div>
              </div>
 
              <Link to="/contact" onClick={this.handleSubmitData}>
    <button class="contact-us__btn btn js-submit">{sendButtonText}</button>
              </Link>
            </div>
          </form>
        </div>
      </section>
    );
  }
}

const mapDispatchToProps = dispatch => {
  return {
    sendForm: form => {
      dispatch(SendForm(form));
    }
  };
};

export default connect(null, mapDispatchToProps)(ContactForm);
