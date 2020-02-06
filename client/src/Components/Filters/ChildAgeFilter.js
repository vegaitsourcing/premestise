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
        <li>
          <a
            className={selectedAge == i ? "tab-year active" : "tab-year"}
            href="!#"
            onClick={event => {
              this.preventEventAndFilter(event, i);
            }}
          >
            {i}
          </a>
        </li>
      );
    }
    return <ul>{filterNumbers}</ul>;
  };

  render() {
    const currentPage = this.props.currentPage;
    const perPageCount = this.props.perPageCount;
    const elementsAmount = this.props.elementsAmount;

    return (
      <div className="tab-years">
        <p>Odaberi starosno doba:</p>
        <ul className="tab-years__list">
          {this.renderClickableFilterNumbers()}
        </ul>

        <span className="number-info">
          ({(currentPage - 1) * perPageCount + 1}-
          {currentPage * perPageCount < elementsAmount
            ? currentPage * perPageCount
            : elementsAmount}{" "}
          od ukupno {elementsAmount})
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
