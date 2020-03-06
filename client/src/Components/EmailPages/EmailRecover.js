import { RecoverEmail } from "../../Actions/EmailActions/EmailActions";
import InfoPage from "../Info/InfoPage";

import React, { Component } from "react";
import queryString from "querystring";
import { connect } from "react-redux";

class EmailRecover extends Component {
  componentDidMount() {
    const queryId = queryString.parse(window.location.search)["?id"];
    //const queryId = window.location.hash.split('?id=')[1]; 
    this.props.recoverEmail(queryId);
  }

  render() {
    const emailRecovered = this.props.emailRecovered;
    const successMessage = "Uspešno ste vraćeni u sistem posto dogovor nije postignut, srećno u daljem traganju...";
    const waitingMessage = "Vraćanje u sistem, pričekajte momenat...";

    return (
      <div>
        {
          <InfoPage
            description={
              emailRecovered
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
    emailRecovered: state.emailRecovery
  };
};

const mapDispatchToProps = dispatch => {
  return {
    recoverEmail: id => {
      dispatch(RecoverEmail(id));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EmailRecover);
