import React, { Component } from "react";

class ChildAgeNumberFilter extends Component {
  render() {
    const selectedAge = this.props.selectedAge;
    const pageNumber = this.props.pageNumber;
    const filterByPage = this.props.filterByAgeHandler;

    return (
        <li>
            <a className={ selectedAge===pageNumber ? 'tab-year active':'tab-year'}
            href="!#" 
            onClick={e => {
              e.preventDefault();
              filterByPage(pageNumber);
          }}>{pageNumber}</a>
        </li>
    );
  }
}

export default ChildAgeNumberFilter;
