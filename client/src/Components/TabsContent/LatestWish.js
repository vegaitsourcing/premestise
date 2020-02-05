import React, { Component } from "react";
import { connect } from "react-redux";
import {
  GetLatestWish,
  PrepareRequest
} from "../../Actions/NavActions/LatestWishActions";
import WishView from "../../Components/WishView/WishView";
class LatestWish extends Component {
  componentDidMount() {
    this.props.getLatestWish();
  }

  render() {
    return this.props.latestWish !== null ? (
      <div id="tab-2" className="tab-content current">
        <h2 className="tab-title">Najnovija zelja</h2>
        <div className="tab-wizard">
          <div className="tab-wizard__item">
            {<WishView wish={this.props.latestWish} />}
          </div>
        </div>
      </div>
    ) : (
      <React.Fragment />
    );
  }
}

const mapStateToProps = state => {
  return {
    latestWish: state.latestWish
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getLatestWish: () => {
      dispatch(GetLatestWish());
    },
    prepareRequest: fromKindergardenId => {
      dispatch(PrepareRequest(fromKindergardenId));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(LatestWish);
