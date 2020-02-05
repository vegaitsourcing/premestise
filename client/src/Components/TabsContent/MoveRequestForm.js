import React, { Component } from "react";
import { connect } from "react-redux";
import {
  GetAllKindergardens,
  NewMoveRequest
} from "../../Actions/NavActions/MoveRequestActions";

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
      //id??
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
        parentNameErrorMessage: "Obavezno uneti ime roditelja!"
      });
      this.setState({ validationErrorExists: true });
    }

    if (form.Email === "") {
      this.setState({ emailErrorMessage: "Proveriti format e-mail adrese!" });
      this.setState({ validationErrorExists: true });
    }

    if (form.PhoneNumber === "") {
      this.setState({ phoneErrorMessage: "Obavezno uneti kontakt telefon!" });
      this.setState({ validationErrorExists: true });
    }

    if (form.ChildName === "") {
      this.setState({ childNameErrorMessage: "Obavezno uneti ime deteta!" });
      this.setState({ validationErrorExists: true });
    }
    if (form.ChildBirthDate === null) {
      this.setState({
        childBirthErrorMessage: "Obavezno uneti datum rođenja deteta!"
      });
      this.setState({ validationErrorExists: true });
    }
    if (form.FromKindergardenId === null) {
      this.setState({
        fromKindergardenErrorMessage: "Obavezno mesto relokacije!"
      });
      this.setState({ validationErrorExists: true });
    }

    if (form.ToKindergardenIds.length === 0) {
      this.setState({
        toKindergardenErrorMessage:
          "Obavezno odabrati bar jednu želju za premestaj!"
      });
      this.setState({ validationErrorExists: true });
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
    ) {
      return false;
    } else {
      return true;
    }
  };

  handleParentNameChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({ ParentNameSurname: event.target.value });
      this.setState({ parentNameErrorMessage: null });
    } else {
      this.setState({
        parentNameErrorMessage: "Obavezno uneti ime roditelja!"
      });
      this.setState({ ParentNameSurname: "" });
    }
  };

  handleChildNameChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({ ChildName: event.target.value });
      this.setState({ childNameErrorMessage: null });
    } else {
      this.setState({ childNameErrorMessage: "Obavezno uneti ime deteta!" });
      this.setState({ ChildName: "" });
    }
  };

  handleEmailChange = event => {
    event.preventDefault();
    //return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
    if (event.target.value !== "") {
      if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(event.target.value)) {
        this.setState({ Email: event.target.value });
        this.setState({ emailErrorMessage: "Proveriti format e-mail adrese!" });
      } else {
        this.setState({ emailErrorMessage: null });
        this.setState({ Email: event.target.value });
      }
    } else {
      this.setState({ Email: "" });
      this.setState({ emailErrorMessage: "Proveriti format e-mail adrese!" });
    }
  };

  handlePhoneNumberChange = event => {
    event.preventDefault();
    if (event.target.value !== "") {
      this.setState({ PhoneNumber: event.target.value });
      this.setState({ phoneErrorMessage: null });
    } else {
      this.setState({ PhoneNumber: "" });
      this.setState({ phoneErrorMessage: "Obavezno uneti kontakt telefon!" });
    }
  };

  handleChildBirthDateChange = event => {
    event.preventDefault();
    console.log(event.target.value);
    if (event.target.value !== null || event.target.value !== undefined) {
      this.setState({ ChildBirthDate: event.target.value });
      this.setState({ childBirthErrorMessage: null });
    } else {
      this.setState({ ChildBirthDate: null });
      this.setState({
        childBirthErrorMessage: "Obavezno uneti datum rođenja deteta!"
      });
    }
  };

  handleMoveFromLocationIdChange = event => {
    event.preventDefault();
    console.log(event.target.value);
    if (event.target.value !== null) {
      this.setState({ MoveFromLocationId: event.target.value });
      this.setState({ fromKindergardenErrorMessage: null });
    } else {
      this.setState({ MoveFromLocationId: null });
      this.setState({
        fromKindergardenErrorMessage: "Obavezno mesto relokacije!"
      });
    }
  };

  handleMoveToLocationId1Change = event => {
    event.preventDefault();
    //check format before setting state or smth
    if (event.target.value !== null) {
      this.setState({ MoveToLocationId1: event.target.value });
      this.setState({ toKindergardenErrorMessage: null });
    } else {
      this.setState({ MoveToLocationId1: null });
    }
  };

  handleMoveToLocationId2Change = event => {
    event.preventDefault();
    console.log(event.target.value);
    if (event.target.value !== null) {
      this.setState({ MoveToLocationId2: event.target.value });
      this.setState({ toKindergardenErrorMessage: null });
    } else {
      this.setState({ MoveToLocationId2: null });
    }
  };

  handleMoveToLocationId3Change = event => {
    event.preventDefault();
    if (event.target.value !== null) {
      this.setState({ MoveToLocationId3: event.target.value });
      this.setState({ toKindergardenErrorMessage: null });
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
    const errorMsgStyle = {
      color: "red"
    };

    const blockSpan = {
      display: "block"
    };

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

    return (
      <div id="tab-1" className="tab-content current">
        <h2 className="tab-title">Gde zelis da se premestis?</h2>
        <form>
          <div className="form-wrap">
            <div className="form-left">
              <input
                type="text"
                id="newRequestParentName"
                className="input parent-input"
                placeholder="Ime i prezime roditelja *"
                onChange={this.handleParentNameChange}
                value={ParentNameSurname}
              />
              <span style={errorMsgStyle}>
                {this.state.parentNameErrorMessage}
              </span>
              <input
                type="text"
                id="newRequestChildName"
                className="input child-input"
                placeholder="Ime deteta *"
                onChange={this.handleChildNameChange}
                value={ChildName}
              />
              <span style={errorMsgStyle}>
                {this.state.childNameErrorMessage}
              </span>
              <input
                type="email"
                id="newRequestEmail"
                className="input input__email"
                placeholder="Email *"
                onChange={this.handleEmailChange}
                value={Email}
              />
              <span style={{ ...errorMsgStyle, ...blockSpan }}>
                {this.state.emailErrorMessage}
              </span>
              <div className="form-left__double">
                <div className="inputSpanGlobalWrapper">
                  <div className="inputSpanWrapperPhone">
                    <input
                      type="text"
                      id="newRequestPhoneNumber"
                      className="input input__phone"
                      placeholder="Broj telefona *"
                      onChange={this.handlePhoneNumberChange}
                      value={PhoneNumber}
                    />

                    <span style={errorMsgStyle}>
                      {this.state.phoneErrorMessage}
                    </span>
                  </div>

                  <div className="inputSpanWrapperDate">
                    <input
                      type="date"
                      id="newRequestChildBirthDate"
                      className="input input__date"
                      placeholder="Datum rodjenja deteta"
                      onChange={this.handleChildBirthDateChange}
                      value={ChildBirthDate}
                    />

                    <span style={errorMsgStyle}>
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
                    Izaberi trenutnu lokaciju
                  </option>
                  {this.props.allKindergardens.map(kinder => (
                    <option value={kinder.id}>{kinder.name}</option>
                  ))}
                </select>
                <span style={errorMsgStyle}>
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
                    1. Lokacija na koju zelis da se premestis?
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
                    2. Lokacija na koju zelis da se premestis?
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
                    3. Lokacija na koju zelis da se premestis?
                  </option>
                  {this.props.allKindergardens.map(kinder => (
                    <option value={kinder.id}>{kinder.name}</option>
                  ))}
                </select>
                <span style={errorMsgStyle}>
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
                    <span className="font-ico-messenger"></span> Pisite nam{" "}
                  </a>
                </li>
                <li>
                  <a className="form-link" href="javascript:void(0);">
                    <span className="font-ico-facebook"></span> Posetite nas{" "}
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
                <span className="font-ico-envelope"></span> Obavesti me{" "}
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
