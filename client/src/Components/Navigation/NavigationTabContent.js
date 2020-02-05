import React, { Component } from "react";
import AllWishes from "../TabsContent/AllWishes";
import LatestWish from "../TabsContent/LatestWish";
import MoveRequestForm from "../TabsContent/MoveRequestForm";

class NavigationTabContent extends Component {
  render() {
    const currentTab = this.props.currentTab;
    const toRenderComponent =
      currentTab === 1 ? (
        <MoveRequestForm />
      ) : currentTab === 2 ? (
        <LatestWish />
      ) : (
        <AllWishes />
      );

    return <div className="wrap">{toRenderComponent}</div>;
  }
}

export default NavigationTabContent;
