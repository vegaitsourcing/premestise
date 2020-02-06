import React, { Component } from "react";
import { connect } from "react-redux";

class NavigationItem extends Component {
  render() {
    const tabNumber = this.props.tabNumber;
    const currentTab = this.props.currentTab;
    const iconName =
      tabNumber === 1
        ? "font-ico-map-q-pin"
        : tabNumber === 2
        ? "font-ico-map-pin"
        : "font-ico-new-wish";
    const currentTabClass = currentTab === tabNumber ? "current" : "";
    return (
      <li
        onClick={() => this.props.changeTab(tabNumber)}
        className={"tabs__link " + currentTabClass}
        data-tab={"tab-" + tabNumber}
      >
        <span className={iconName}></span>
        {this.props.title}
      </li>
    );
  }
}

const mapDispatchToProps = dispatch => {
  return {
    changeTab: tabNumber => {
      dispatch({ type: "CHANGE_NAVIGATION_TAB", changeTo: tabNumber });
    }
  };
};

export default connect(null, mapDispatchToProps)(NavigationItem);
