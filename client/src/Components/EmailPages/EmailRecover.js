import { RecoverEmail } from "../../Actions/EmailActions/EmailActions";
import InfoPage from "../Info/InfoPage";

import React, { Component } from "react";
import queryString from "querystring";
import { connect } from "react-redux";

class EmailRecover extends Component {
  componentDidMount() {
    const queryId = queryString.parse(window.location.search)["?id"];
    this.props.recoverEmail(queryId);
  }

  render() {
    const emailRecovered = this.props.emailRecovered;

    return (
      <div>
        {
          <InfoPage
            description={
              emailRecovered
                ? "Uspesno ste vraceni u sistem posto dogovor nije postignut, srecno u daljem traganju..."
                : "Vracanje u sistem, pricekajte momenat..."
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
