import { GetMatchedCount } from "../../Actions/MatchedInfoActions/MatchedInfoActions";

import React, { Component } from "react";
import { connect } from "react-redux";

class MatchedInfo extends Component {
  componentDidMount() {
    this.props.getMatchedCount();
  }

  render() {
    const matchedCount = "ostvarenih premeštaja do sada.";
    return (
      <div class="success-banner">
        <div class="success-banner__wrap wrap">
          <div class="success-banner__container">
            <span class="succeess-banner__number">
              {this.props.matchedCount}
            </span>
            <span class="font-ico-circle"></span>
          </div>
          <p class="succeess-banner__paragraf">
            {matchedCount}
          </p>
        </div>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    matchedCount: state.matchedCount
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getMatchedCount: () => {
      dispatch(GetMatchedCount());
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(MatchedInfo);
