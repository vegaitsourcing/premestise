import React, { Component } from "react";
import queryString from "querystring";
import { RemoveRequestEmail } from "../../Actions/EmailActions/EmailActions";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import InfoPage from "../Info/InfoPage";

class EmailRemoveRequest extends Component {
  componentDidMount() {
    const queryId = queryString.parse(window.location.search)["?id"];
    this.props.removeRequestEmail(queryId);
  }

  render() {
    const emailRemoved = this.props.removeRequestConfirmed;
    return (
      <div>
        {
          <InfoPage
            description={
              emailRemoved
                ? "Uspesno uklonjen zahtev iz sistema usled neispravnih podataka, pokusajte ponovo... "
                : "Uklanjanje u toku..."
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
