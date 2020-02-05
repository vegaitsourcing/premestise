import React, { Component } from "react";
import { connect } from "react-redux";
import { GetMatchedCount } from "../../Actions/MatchedInfoActions/MatchedInfoActions";
class MatchedInfo extends Component {
  componentDidMount() {
    this.props.getMatchedCount();
  }

  render() {
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
            ostvarenih premeštaja do sada.
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
