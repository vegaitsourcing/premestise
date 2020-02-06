import { ConfirmEmail } from "../../Actions/EmailActions/EmailActions";
import InfoPage from "../Info/InfoPage";

import React, { Component } from "react";
import queryString from "querystring";
import { connect } from "react-redux";

class EmailConfirm extends Component {
  componentDidMount() {
    const queryId = queryString.parse(window.location.search)["?id"];
    this.props.confirmEmail(queryId);
  }

  render() {
    const emailConfirmed = this.props.emailConfirmed;
    const successMessage = "Podacu su uspesno razmenjeni, srecan premestaj zeli Vam ekipa Premesti.se.";
    const waitingMessage = "Potvrda podudaranja u toku.";
    
    return (
      <div className="wrap">
        {
          <InfoPage
            description={
              emailConfirmed
                ? successMessage
                : waitingMessage
            }
          />
        }
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    emailConfirmed: state.emailConfirmation
  };
};

const mapDispatchToProps = dispatch => {
  return {
    confirmEmail: id => {
      dispatch(ConfirmEmail(id));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EmailConfirm);
