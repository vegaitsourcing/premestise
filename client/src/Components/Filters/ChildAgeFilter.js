import { GetAllWishes } from "../../Actions/NavActions/AllWishesActions";

import React, { Component } from "react";
import { connect } from "react-redux";

class ChildAgeFilter extends Component {
  filterByAge = ageValue => {
    this.props.getAllWishes(ageValue);
  };
  preventEventAndFilter = (event, age) => {
    event.preventDefault();
    this.filterByAge(age);
  };

  renderClickableFilterNumbers = () => {
    const maximumAgeFilter = 6;
    const selectedAge = this.props.selectedAge;
    let filterNumbers = [];
    for (let i = 1; i <= maximumAgeFilter; i++) {
      filterNumbers.push(

      );
    }
    return <ul>{filterNumbers}</ul>;
  };

  lowerPageRange = () => {
    return (this.props.currentPage - 1) * this.props.perPageCount + 1;
  }

  upperPageRange = () => {
    return this.props.currentPage * this.props.perPageCount < this.props.elementsAmount
      ? this.props.currentPage * this.props.perPageCount
      : this.props.elementsAmount
  }

  render() {
    const elementsAmount = this.props.elementsAmount;

    return (
      <div className="tab-years">
        <p>Odaberi starosno doba:</p>
        <ul className="tab-years__list">
          {this.renderClickableFilterNumbers()}
        </ul>

        <span className="number-info">
          (
          {this.lowerPageRange()}-
          {this.upperPageRange()}&nbsp;
          od ukupno {elementsAmount}
          )
        </span>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    perPageCount: state.perPageCount,
    elementsAmount: state.elementsAmount,
    currentPage: state.currentPage
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getAllWishes: filterByAge => {
      dispatch(GetAllWishes(filterByAge));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(ChildAgeFilter);
