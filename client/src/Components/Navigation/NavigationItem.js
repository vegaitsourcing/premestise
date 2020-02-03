import React, { Component } from 'react';
//import {changeTab} from '../../Actions/NavActions/NavigationActions';
import {connect} from 'react-redux';

class NavigationItem extends Component {
    render() {

        const tabNum = this.props.tabNum;
        const currentTab = this.props.currentTab;
        const iconName = tabNum === 1 ? 'font-ico-map-q-pin' : (tabNum === 2 ? 'font-ico-map-pin' : 'font-ico-new-wish');
        const currentTabClass = (currentTab === tabNum) ? 'current' : '';
        return (
            <li  onClick={() => this.props.changeTab(tabNum)} className={"tabs__link "+currentTabClass} data-tab={"tab-"+tabNum}><span className={iconName}></span>{this.props.title}</li>
        ); 
    }
}

const mapDispatchToProps = (dispatch) => {

    return {
        changeTab: (tabNum) => { dispatch({type:"CHANGE_NAVIGATION_TAB", changeTo: tabNum }) },
    }
}


export default connect(null, mapDispatchToProps)(NavigationItem);