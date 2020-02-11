import {
  GetLatestWish,
  PrepareRequest
} from "../../Actions/NavActions/LatestWishActions";
import WishView from "../../Components/WishView/WishView";

import React, { Component } from "react";
import { connect } from "react-redux";

class LatestWish extends Component {
  componentDidMount() {
    this.props.getLatestWish();
  }

  render() {
    const latestWish = "Najnovija želja";
    const noWishes = "Trenutno ne postoji ni jedna želja u sistemu...";
    return this.props.latestWish !== null ? (
      <div id="tab-2" className="tab-content current">
        <h2 className="tab-title">{latestWish}</h2>
        <div className="tab-wizard">
          <div className="tab-wizard__item">
            <WishView wish={this.props.latestWish} />
          </div>
        </div>
      </div>
    ) : (
        <div>
          <p>{noWishes}</p>
        </div>
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