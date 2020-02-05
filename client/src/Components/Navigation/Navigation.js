import React, { Component } from "react";
import NavigationItem from "./NavigationItem";
import NavigationTabContent from "../Navigation/NavigationTabContent";
import { connect } from "react-redux";

class Navigation extends Component {
  //mapState currentNavigationItem

  render() {
    const currentTab = this.props.currentTab;

    return (
      <section data-section-name="tabs">
        <div class="tabs-wrap">
          <div class="tabs__buttons">
            <ul class="tabs">
              <NavigationItem
                title="Gde zelis da se premestis?"
                tabNum={1}
                currentTab={currentTab}
              />
              <NavigationItem
                title="Najnovija zelja"
                tabNum={2}
                currentTab={currentTab}
              />
              <NavigationItem
                title="Sve zelje"
                tabNum={3}
                currentTab={currentTab}
              />
            </ul>
          </div>
          <NavigationTabContent currentTab={currentTab} />
        </div>
      </section>
    );
  }
}

const mapStateToProps = state => {
  return {
    currentTab: state.currentNavTab
  };
};

export default connect(mapStateToProps)(Navigation);
