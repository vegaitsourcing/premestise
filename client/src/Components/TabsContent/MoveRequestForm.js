import {
  GetAllKindergardens,
  NewMoveRequest,
  GetCities,
  GetKindergardensByCity
} from "../../Actions/NavActions/MoveRequestActions";

import React, { Component } from "react";
import { connect } from "react-redux";


class MoveRequestForm extends Component {
  state = {
    privacyMainFormCheckbox: false,
    privacyMainFormErrorMessage: null,
    


    groupErrorMessage: null,
    cityErrorMessage: null,
    parentNameErrorMessage: null,
    emailErrorMessage: null,
    phoneErrorMessage: null,
    fromKindergardenErrorMessage: null,
    toKindergardenErrorMessage: null,
    validationErrorExists: true,
    ParentNameSurname: "",
    Email: "",
    PhoneNumber: "",
    ChildBirthDate: null,
    City: null,
    Group: null,
    MoveFromLocationId: null,
    MoveToLocationId1: null,
    MoveToLocationId2: null,
    MoveToLocationId3: null,
    ageGroups:[],

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
      City: this.state.City,
      Group: this.state.Group,
      FromKindergardenId: this.state.MoveFromLocationId,
      ToKindergardenIds: toKindergardenIds,
      ChildBirthDate: new Date(),
      ChildName: 'None'
    };

    if (this.state.privacyMainFormCheckbox === false) {
      this.setState({
        privacyMainFormErrorMessage: "Obavezno prihvatiti politiku privatnosti!",
        validationErrorExists: true
      });
    }

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


    if (form.City === null) {
      this.setState({
        cityErrorMessage: "Obavezno odabrati grad relokacije!",
        validationErrorExists: true
      });
    }

    if (form.Group === null) {
      this.setState({
        groupErrorMessage: "Obavezno odabrati grupu!",
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
          "Obavezno odabrati želju za premestaj!",
        validationErrorExists: true
      });
    }

    if (this.checkIfNotHasErrors()) {
      this.props.newMoveRequest(form);
    }
  };

  checkIfNotHasErrors = () => {
    if (
        !this.state.groupErrorMessage &&
        !this.state.cityErrorMessage &&
        !this.state.parentNameErrorMessage &&
        !this.state.emailErrorMessage &&
        !this.state.phoneErrorMessage &&
        !this.state.fromKindergardenErrorMessage &&
        !this.state.toKindergardenErrorMessage &&
        !this.state.privacyMainFormErrorMessage
    ) return true;

    return false;

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

  handlePrivacyMainFormChange = event => {
    if (event.target.checked === true) {
      this.setState({
        privacyMainFormCheckbox: true,
        privacyMainFormErrorMessage: null
      });
      
    } else {
     
      this.setState({
        privacyMainFormCheckbox: false,
         privacyMainFormErrorMessage: "Obavezno prihvatiti politiku privatnosti!" });
    }
  };



  handleCityChange = event => {
    event.preventDefault();

    if (event.target.value !== null) {
      this.setState({
        City: event.target.value,
        cityErrorMessage: null
      });
      this.props.getKindergardensByCity(event.target.value)
    } else {
      this.setState({ City: null });
    }
  };

  handleGroupChange = event => {
    event.preventDefault();

    if (event.target.value !== null) {
      this.setState({
        Group: event.target.value,
        groupErrorMessage: null
      });
    } else {
      this.setState({ Group: null });
    }
  };


  handleMoveFromLocationIdChange = event => {
    event.preventDefault();
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
    this.generateAgeGroups()
    this.props.getCities();
    if (this.props.prePopulatedId != null) {
      this.setState({ toKindergardenErrorMessage: null });
      this.state.MoveToLocationId1 = this.props.prePopulatedId;
    }
  }

  generateAgeGroups() {
    let currentYear = new Date().getFullYear()
    let groups = [
      {
        id: 1,
        text: 'Rođeni 01.03.'+(currentYear-1)+'-28.02.'+(currentYear)+' Bebe grupa'
      },
      {
        id: 2,
        text: 'Rođeni 01.03.'+(currentYear-2)+'-28.02.'+(currentYear-1)+' Mlađa jaslena grupa'
      },
      {
        id: 3,
        text: 'Rođeni 01.03.'+(currentYear-3)+'-28.02.'+(currentYear-2)+' Starija jaslena grupa'
      },
      {
        id: 4,
        text: 'Rođeni 01.03.'+(currentYear-4)+'-28.02.'+(currentYear-3)+' Mlađa grupa'
      },
      {
        id: 5,
        text: 'Rođeni 01.03.'+(currentYear-5)+'-28.02.'+(currentYear-4)+' Srednja grupa'
      },
      {
        id: 6,
        text: 'Rođeni 01.03.'+(currentYear-6)+'-28.02.'+(currentYear-5)+' Starija grupa'
      },
      {
        id: 7,
        text: 'Rođeni 01.03.'+(currentYear-7)+'-28.02.'+(currentYear-6)+' Predškolska grupa'
      }
    ]
    this.setState({
      ageGroups: groups
    })

  }

  render() {
    const {
      ParentNameSurname,
      Email,
      PhoneNumber,
      ChildBirthDate,
      City,
      Group,
      MoveFromLocationId,
      MoveToLocationId1,
      MoveToLocationId2,
      MoveToLocationId3
    } = this.state;
    const fromKindergardenId = this.props.fromKindergardenId;

    const whereToMove = "Gde želis da se premestiš?"
    const parentNamePlaceholderText = "Ime i prezime roditelja *";
    const emailPlaceholderText = "Email *";
    const phonePlaceholderText = "Broj telefona *";
    const birthdatePlaceholderText = "Datum rođenja deteta";
    const cityPlaceholderText = "Grad *";
    const groupPlaceholderText = "Odabrati grupu *";
    const locationToFirstWishText = "Lokacija na koju želiš da se premestiš?";
    const locationToSecondWishText = "2. Lokacija na koju želiš da se premestiš?";
    const locationToThirdWishText = "3. Lokacija na koju želiš da se premestiš?";
    const chooseCurrentLocationText = "Izaberi trenutnu lokaciju";
    const messengerLinkText = "Pišite nam";
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


                </div>
              </div>


                <select
                  id="city"
                  onChange={this.handleCityChange}
                  value={City}
                >
                  <option value="" selected disabled hidden>
                    {cityPlaceholderText}
                  </option>
                  {this.props.cities.map(city => (
                    <option value={city}>{city}</option>
                  ))}
                </select>
                <span className="errorMessageMainForm" >
                  {this.state.cityErrorMessage}
                </span>
             

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
                <span className="errorMessageMainForm" >
                  {this.state.toKindergardenErrorMessage}
                </span>
              </div>
              <select
                  id="group"
                  onChange={this.handleGroupChange}
                  value={Group}
                >
                  <option value="" selected disabled hidden>
                    {groupPlaceholderText}
                  </option>
                  {this.state.ageGroups.map(group => (
                    <option value={group.id}>{group.text}</option>
                  ))}
                </select>
                <span className="errorMessageMainForm" >
                  {this.state.groupErrorMessage}
                </span>
                <div className="mainPrivacyPolicyCheckBox">

                  <div className="inline_block">
                  <label for="privacy"  >
                    <input type="checkbox" name="privacy" className="checkBoxMiddleAlign"
                 checked={this.state.privacyMainFormCheckbox}
                  onClick={this.handlePrivacyMainFormChange}/>&nbsp;&nbsp;&nbsp;Prihvatam politiku privatnosti</label>
                  </div>
                  <div>
                <span className="errorMessageMainForm " >
                  {this.state.privacyMainFormErrorMessage}
                </span>
                </div>
                </div>
            </div>
          </div>

          <div className="form-buttons">
            <div className="form-buttons__left">
              <ul className="form-social-links">

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
    prePopulatedId: state.prePopulatedId,
    cities: state.cities
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getKindergardensByCity: (city) => {
      dispatch(GetKindergardensByCity(city))
    },
    getAllKindergardens: () => {
      dispatch(GetAllKindergardens());
    },
    getCities: () => {
      dispatch(GetCities());
    },
    newMoveRequest: formRequest => {
      dispatch(NewMoveRequest(formRequest));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(MoveRequestForm);
