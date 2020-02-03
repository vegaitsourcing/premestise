import React, { Component } from 'react';

class PageNumber extends Component {
    render() {

        const currentPage = this.props.currentPage;
        const pageNumber = this.props.pageNumber;
        const filterByPage = this.props.filterByPageHandler;
        return (
            
                <li>
                    <a className={currentPage==pageNumber?'pagination__pages--active':''} href="!#" onClick={(e)=>{e.preventDefault(); filterByPage(pageNumber)}}>{pageNumber}</a>
                </li>
            
        );
    }
}

export default PageNumber;