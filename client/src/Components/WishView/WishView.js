import React, { Component } from "react";
import { connect } from "react-redux";
import { PrepareRequest } from "../../Actions/NavActions/LatestWishActions";
class WishView extends Component {
  render() {
    let wish = this.props.wish;
    return (
      <div className="wishItem">
        <h3 id="latestWishFrom">
          {" "}
          <span className="font-ico-circle">
            {wish != null ? wish.fromKindergarden.name : ""}
          </span>{" "}
          >>
          <br />
          {wish != null
            ? wish.toKindergardens.map((kinder, index) => (
                <React.Fragment>
                  <span className="name" id="latestWishTo">
                    <strong>{index + 1}.</strong> {kinder.name}{" "}
                  </span>
                  <br />
                </React.Fragment>
              ))
            : ""}
        </h3>
        <a
          href="javascript:void(0);"
          id="changeLatestWishButton"
          className="btn"
          onClick={() => this.props.prepareRequest(wish.fromKindergarden.id)}
        >
          Zameni se
        </a>
        <br />
      </div>
    );
  }
}
const mapDispatchToProps = dispatch => {
  return {
    prepareRequest: fromKindergardenId => {
      dispatch(PrepareRequest(fromKindergardenId));
    }
  };
};

export default connect(null, mapDispatchToProps)(WishView);
