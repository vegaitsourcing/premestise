import InfoPage from "../Info/InfoPage";
import { RemoveRequestEmail } from "../../Actions/EmailActions/EmailActions";

import React, { Component } from "react";
import queryString from "querystring";
import { connect } from "react-redux";

class EmailRemoveRequest extends Component {
  componentDidMount() {
    const queryId = queryString.parse(window.location.search)["?id"];
    this.props.removeRequestEmail(queryId);
  }

  render() {
    const emailRemoved = this.props.removeRequestConfirmed;
    const successMessage = "Uspesno uklonjen zahtev iz sistema usled neispravnih podataka, pokusajte ponovo... ";
    const waitingMessage = "Uklanjanje u toku...";

    return (
      <div>
        {
          <InfoPage
            description={
              emailRemoved
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
    removeRequestConfirmed: state.emailRemoveRequest
  };
};

const mapDispatchToProps = dispatch => {
  return {
    removeRequestEmail: id => {
      dispatch(RemoveRequestEmail(id));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EmailRemoveRequest);
