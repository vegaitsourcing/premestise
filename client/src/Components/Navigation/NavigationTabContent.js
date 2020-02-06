import AllWishes from "../TabsContent/AllWishes";
import LatestWish from "../TabsContent/LatestWish";
import MoveRequestForm from "../TabsContent/MoveRequestForm";

import React, { Component } from "react";

class NavigationTabContent extends Component {
  resolveTabContent = (currentTab) => {
    return currentTab === 2 ? 
      <LatestWish />
     : <AllWishes />
    
  }
  render() {
    const currentTab = this.props.currentTab;
    const toRenderComponent =
      currentTab === 1 ? (
        <MoveRequestForm />
      ) : this.resolveTabContent(currentTab);

    return <div className="wrap">{toRenderComponent}</div>;
  }
}

export default NavigationTabContent;
