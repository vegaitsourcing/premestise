import React, { Component } from "react";
import queryString from "querystring";
import { ConfirmEmail } from "../../Actions/EmailActions/EmailActions";
import { connect } from "react-redux";
import InfoPage from "../Info/InfoPage";
class EmailConfirm extends Component {
  componentDidMount() {
    const queryId = queryString.parse(window.location.search)["?id"];
    this.props.confirmEmail(queryId);
  }

  render() {
    const emailConfirmed = this.props.emailConfirmed;
    return (
      <div className="wrap">
        {
          <InfoPage
            description={
              emailConfirmed
                ? "Podacu su uspesno razmenjeni, srecan premestaj zeli Vam ekipa Premesti.se."
                : "Potvrda podudaranja u toku."
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
