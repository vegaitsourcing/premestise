import {
  GetAllKindergardens,
  NewMoveRequest
} from "../../Actions/NavActions/MoveRequestActions";

import React, { Component } from "react";
import { connect } from "react-redux";


class MoveRequestForm extends Component {
  state = {
    parentNameErrorMessage: null,
    childNameErrorMessage: null,
    emailErrorMessage: null,
    phoneErrorMessage: null,
    childBirthErrorMessage: null,
    fromKindergardenErrorMessage: null,
    toKindergardenErrorMessage: null,
    validationErrorExists: true,
    ParentNameSurname: "",
    ChildName: "",
    Email: "",
    PhoneNumber: "",
    ChildBirthDate: null,
    MoveFromLocationId: null,
    MoveToLocationId1: null,
    MoveToLocationId2: null,
    MoveToLocationId3: null
  };

  handleSubmitData = event => {
    event.preventDefault();

    let toKindergardenIds = [];
    if (this.state.MoveToLocationId1)
      toKindergardenIds.push(this.state.MoveToLocationId1);
    if (this.state.MoveToLocationId2)
      toKindergardenIds.push(this.state.MoveToLocationId2);
    if (this.state.MoveToLocationId3)
      toKindergardenIds.push(this.state.MoveToLocationId3);

    let form = {
      ParentName: this.state.ParentNameSurname,
      Email: this.state.Email,
      PhoneNumber: this.state.PhoneNumber,
      ChildName: this.state.ChildName,
      ChildBirthDate: this.state.ChildBirthDate,
      FromKindergardenId: this.state.MoveFromLocationId,
      ToKindergardenIds: toKindergardenIds
    };

    if (form.ParentName === "") {
      this.setState({
        parentNameErrorMessage: "Obavezno uneti ime roditelja!",
        validationErrorExists: true
      });
    }

    if (form.Email === "") {
      this.setState({
        emailErrorMessage: "Proveriti format e-mail adrese!",
        validationErrorExists: true
      });
    }

    if (form.PhoneNumber === "") {
      this.setState({
        phoneErrorMessage: "Obavezno uneti kontakt telefon!",
        validationErrorExists: true
      });
    }

    if (form.ChildName === "") {
      this.setState({
        childNameErrorMessage: "Obavezno uneti ime deteta!",
        validationErrorExists: true
      });
    }
    if (form.ChildBirthDate === null) {
      this.setState({
        childBirthErrorMessage: "Obavezno uneti datum rođenja deteta!",
        validationErrorExists: true
      });
    }
    if (form.FromKindergardenId === null) {
      this.setState({
        fromKindergardenErrorMessage: "Obavezno mesto relokacije!",
        validationErrorExists: true
      });
    }

    if (form.ToKindergardenIds.length === 0) {
      this.setState({
        toKindergardenErrorMessage:
          "Obavezno odabrati bar jednu želju za premestaj!",
        validationErrorExists: true
      });
    }

    if (this.checkIfHasErrors()) {
      this.props.newMoveRequest(form);
    }
  };

  checkIfHasErrors = () => {
    if (
      !(
        this.state.childNameErrorMessage ||
        this.state.parentNameErrorMessage ||
        this.state.emailErrorMessage ||
        this.state.phoneErrorMessage ||
        this.state.ChildBirthDate ||
        this.state.fromKindergardenErrorMessage ||
        this.state.toKindergardenErrorMessage
      )
    ) return false;

    return true;

  };

  handleParentNameChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({
        ParentNameSurname: event.target.value,
        parentNameErrorMessage: null
      });
    } else {
      this.setState({
        parentNameErrorMessage: "Obavezno uneti ime roditelja!",
        ParentNameSurname: ""
      });
    }
  };

  handleChildNameChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({
        ChildName: event.target.value,
        childNameErrorMessage: null
      });
    } else {
      this.setState({
        childNameErrorMessage: "Obavezno uneti ime deteta!",
        ChildName: ""
      });
    }
  };

  emailFormatValidation = (emailAddress) => {
    const mailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!mailRegex.test(emailAddress)) {
      this.setState({
        Email: emailAddress,
        emailErrorMessage: "Proveriti format e-mail adrese!"
      });
      this.setState({});
    } else {
      this.setState({
        emailErrorMessage: null,
        Email: emailAddress
      });

    }
  }

  handleEmailChange = event => {
    event.preventDefault();

    if (event.target.value !== "") {
      this.emailFormatValidation(event.target.value)
    } else {
      this.setState({
        Email: "",
        emailErrorMessage: "Proveriti format e-mail adrese!"
      });
    }
  };

  handlePhoneNumberChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({
        PhoneNumber: event.target.value,
        phoneErrorMessage: null
      });
    } else {
      this.setState({
        PhoneNumber: "",
        phoneErrorMessage: "Obavezno uneti kontakt telefon!"
      });
    }
  };

  handleChildBirthDateChange = event => {
    event.preventDefault();
    console.log(event.target.value);
    if (event.target.value !== null || event.target.value !== undefined) {
      this.setState({
        ChildBirthDate: event.target.value,
        childBirthErrorMessage: null
      });
    } else {
      this.setState({
        ChildBirthDate: null,
        childBirthErrorMessage: "Obavezno uneti datum rođenja deteta!"
      });
    }
  };

  handleMoveFromLocationIdChange = event => {
    event.preventDefault();
    console.log(event.target.value);
    if (event.target.value !== null) {
      this.setState({
        MoveFromLocationId: event.target.value,
        fromKindergardenErrorMessage: null
      });

    } else {
      this.setState({
        MoveFromLocationId: null,
        fromKindergardenErrorMessage: "Obavezno mesto relokacije!"
      });
    }
  };

  handleMoveToLocationId1Change = event => {
    event.preventDefault();
    //check format before setting state or smth
    if (event.target.value !== null) {
      this.setState({
        MoveToLocationId1: event.target.value,
        toKindergardenErrorMessage: null
      });

    } else {
      this.setState({ MoveToLocationId1: null });
    }
  };

  handleMoveToLocationId2Change = event => {
    event.preventDefault();

    if (event.target.value !== null) {
      this.setState({
        MoveToLocationId2: event.target.value,
        toKindergardenErrorMessage: null
      });
    } else {
      this.setState({ MoveToLocationId2: null });
    }
  };

  handleMoveToLocationId3Change = event => {
    event.preventDefault();

    if (event.target.value !== null) {
      this.setState({
        MoveToLocationId3: event.target.value,
        toKindergardenErrorMessage: null
      });
    } else {
      this.setState({ MoveToLocationId3: null });
    }
  };

  componentDidMount() {
    this.props.getAllKindergardens();
    if (this.props.prePopulatedId != null) {
      this.setState({ toKindergardenErrorMessage: null });
      this.state.MoveToLocationId1 = this.props.prePopulatedId;
    }
  }

  render() {
    const {
      ParentNameSurname,
      ChildName,
      Email,
      PhoneNumber,
      ChildBirthDate,
      MoveFromLocationId,
      MoveToLocationId1,
      MoveToLocationId2,
      MoveToLocationId3
    } = this.state;
    const fromKindergardenId = this.props.fromKindergardenId;

    const whereToMove = "Gde zelis da se premestis?"
    const parentNamePlaceholderText = "Ime i prezime roditelja *";
    const childNamePlaceHolderText = "Ime deteta *";
    const emailPlaceholderText = "Email *";
    const phonePlaceholderText = "Broj telefona *";
    const birthdatePlaceholderText = "Datum rođenja deteta";
    const locationToFirstWishText = "1. Lokacija na koju želiš da se premestiš?";
    const locationToSecondWishText = "2. Lokacija na koju želiš da se premestiš?";
    const locationToThirdWishText = "3. Lokacija na koju želiš da se premestiš?";
    const chooseCurrentLocationText = "Izaberi trenutnu lokaciju";
    const messengerLinkText = "Pišite name";
    const facebookLinkText = "Posetite nas";
    const submitFormButtonText = "Obavesti me"

    return (
      <div id="tab-1" className="tab-content current">
      <h2 className="tab-title">{whereToMove}</h2>
        <form>
          <div className="form-wrap">
            <div className="form-left">
              <input
                type="text"
                id="newRequestParentName"
                className="input parent-input"
                placeholder={parentNamePlaceholderText}
                onChange={this.handleParentNameChange}
                value={ParentNameSurname}
              />
              <span className="errorMessageMainForm" >
                {this.state.parentNameErrorMessage}
              </span>
              <input
                type="text"
                id="newRequestChildName"
                className="input child-input"
                placeholder={childNamePlaceHolderText}
                onChange={this.handleChildNameChange}
                value={ChildName}
              />
              <span className="errorMessageMainForm" >
                {this.state.childNameErrorMessage}
              </span>
              <input
                type="email"
                id="newRequestEmail"
                className="input input__email"
                placeholder={emailPlaceholderText}
                onChange={this.handleEmailChange}
                value={Email}
              />
              <span className="errorMessageMainForm blockSpan">
                {this.state.emailErrorMessage}
              </span>
              <div className="form-left__double">
                <div className="inputSpanGlobalWrapper">
                  <div className="inputSpanWrapperPhone">
                    <input
                      type="text"
                      id="newRequestPhoneNumber"
                      className="input input__phone"
                      placeholder={phonePlaceholderText}
                      onChange={this.handlePhoneNumberChange}
                      value={PhoneNumber}
                    />

                    <span className="errorMessageMainForm" >
                      {this.state.phoneErrorMessage}
                    </span>
                  </div>

                  <div className="inputSpanWrapperDate">
                    <input
                      type="date"
                      id="newRequestChildBirthDate"
                      className="input input__date"
                      placeholder={birthdatePlaceholderText}
                      onChange={this.handleChildBirthDateChange}
                      value={ChildBirthDate}
                    />

                    <span className="errorMessageMainForm" >
                      {this.state.childBirthErrorMessage}
                    </span>
                  </div>
                </div>
              </div>

              <br />
            </div>

            <div className="form-right">
              <div className="select-wrap">
                <select
                  id="newRequestFromKindergardenSelect"
                  onChange={this.handleMoveFromLocationIdChange}
                  value={MoveFromLocationId}
                >
                  <option value="" selected disabled hidden>
                    {chooseCurrentLocationText}
                  </option>
                  {this.props.allKindergardens.map(kinder => (
                    <option value={kinder.id}>{kinder.name}</option>
                  ))}
                </select>
                <span className="errorMessageMainForm" >
                  {this.state.fromKindergardenErrorMessage}
                </span>
              </div>
              <div className="select-wrap">
                <select
                  id="newRequestToKindergarden1Select"
                  onChange={this.handleMoveToLocationId1Change}
                  value={
                    this.props.prePopulatedId !== null
                      ? this.props.prePopulatedId
                      : MoveToLocationId1
                  }
                >
                  <option value="" selected disabled hidden>
                    {locationToFirstWishText}
                  </option>
                  {this.props.allKindergardens.map(kinder => (
                    <option value={kinder.id}>{kinder.name}</option>
                  ))}
                </select>
              </div>
              <div className="select-wrap">
                <select
                  id="newRequestToKindergarden2Select"
                  onChange={this.handleMoveToLocationId2Change}
                  value={MoveToLocationId2}
                >
                  <option value="" selected disabled hidden>
                    {locationToSecondWishText}
                  </option>
                  {this.props.allKindergardens.map(kinder => (
                    <option value={kinder.id}>{kinder.name}</option>
                  ))}
                </select>
              </div>
              <div className="select-wrap">
                <select
                  id="newRequestToKindergarden3Select"
                  onChange={this.handleMoveToLocationId3Change}
                  value={MoveToLocationId3}
                >
                  <option value="" selected disabled hidden>
                    {locationToThirdWishText}
                  </option>
                  {this.props.allKindergardens.map(kinder => (
                    <option value={kinder.id}>{kinder.name}</option>
                  ))}
                </select>
                <span className="errorMessageMainForm" >
                  {this.state.toKindergardenErrorMessage}
                </span>
              </div>
            </div>
          </div>

          <div className="form-buttons">
            <div className="form-buttons__left">
              <ul className="form-social-links">
                <li>
                  <a className="form-link" href="javascript:void(0);">
                    <span className="font-ico-messenger"></span> {messengerLinkText}
                  </a>
                </li>
                <li>
                  <a className="form-link" href="javascript:void(0);">
                    <span className="font-ico-facebook"></span> {facebookLinkText}
                  </a>
                </li>
              </ul>
            </div>

            <div className="form-buttons__right">
              <button
                className="btn"
                type="submit"
                id="submit-new-request"
                onClick={this.handleSubmitData}
              >
                <span className="font-ico-envelope"></span> {submitFormButtonText}
              </button>
            </div>
          </div>
        </form>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    currentTab: state.currentNavTab,
    allKindergardens: state.kindergardens,
    fromKindergardenId: state.fromKindergardenId,
    prePopulatedId: state.prePopulatedId
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getAllKindergardens: () => {
      dispatch(GetAllKindergardens());
    },
    newMoveRequest: formRequest => {
      dispatch(NewMoveRequest(formRequest));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(MoveRequestForm);
