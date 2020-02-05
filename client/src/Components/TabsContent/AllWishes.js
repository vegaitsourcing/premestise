import React, { Component } from "react";
import { connect } from "react-redux";
import {
  GetAllWishes,
  PrepareRequest
} from "../../Actions/NavActions/AllWishesActions";
import WishPaging from "../Filters/WishPaging";
import ChildAgeFilter from "../Filters/ChildAgeFilter";
import WishView from "../../Components/WishView/WishView";

class AllWishes extends Component {
  componentDidMount() {
    this.props.getAllWishes();
  }

  render() {
    const allWishes = this.props.allWishesFiltered;
    const selectedAge = this.props.selectedAge;
    return (
      <div id="tab-3" className="tab-content current">
        <h2 className="tab-title">Sve zelje</h2>

        <ChildAgeFilter selectedAge={selectedAge} />
        <div className="tab-wizard">
          {allWishes != null
            ? allWishes.map(wish => {
                return <WishView wish={wish} />;
              })
            : ""}
        </div>
        <WishPaging />
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    allWishesFiltered: state.allPendingWishesFiltered,
    selectedAge: state.selectedChildAge,
    currentPage: state.currentPage
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getAllWishes: () => {
      dispatch(GetAllWishes());
    },
    prepareRequest: fromKindergardenId => {
      dispatch(PrepareRequest(fromKindergardenId));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(AllWishes);
