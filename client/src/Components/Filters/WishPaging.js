import React, { Component } from 'react';
import {connect} from 'react-redux';
import {GetAllWishes, PageNumberSelector, GoToNextPage, GoToPreviousPage} from '../../Actions/NavActions/AllWishesActions';
import PageNumber from './PageNumber';


class WishPaging extends Component {

     filterByPage = (pageNumber) => {
        this.props.getAllWishes(pageNumber)
        this.props.selectPageNumber(pageNumber)
    }

    createPaging = () => {
       
        let pageNumbers = [];
        for(let i=1; i<=this.props.pagesCount; i++) {
            pageNumbers.push( <PageNumber currentPage={this.props.currentPage} pageNumber={i} filterByPageHandler={this.filterByPage} />)
        }
        return (<ul>{pageNumbers}</ul>)
        }

        goToNextPage = () => {
            this.props.goNext()
        }

        goToPreviousPage = () => {
            this.props.goPrevious()
        }

    render() {
     

        return (
            <div className="pagination">
            <div className="pagination__pages">
                {
                    this.props.currentPage>1? (<div className="pagination-directions__left">
                    <a href="javascript:void(0);" onClick={() => this.goToPreviousPage()}>
                        <span className="font-ico-chevron-down"></span>
                    </a>
                </div>):''
                }
                
                
                {
                    this.createPaging()
                }

                {
                    this.props.currentPage!==this.props.pagesCount? (
                        <div className="pagination-directions__right">
                        <a href="javascript:void(0);" onClick={() => this.goToNextPage()}>
                            <span className="font-ico-chevron-down"></span>
                        </a>
                    </div>
                    ):''
                }
                
               
            </div>
        </div>
        );
    }
}

const mapStateToProps = (state) => {

    return {
        currentPage: state.currentPage,
        pagesCount: state.pagesCount
    }
}


const mapDispatchToProps = (dispatch) => {

    return {
        getAllWishes: (filterByPage) => {  dispatch(GetAllWishes(null, filterByPage)) },
        selectPageNumber: (pageNumber)=> {dispatch(PageNumberSelector(pageNumber))},
        goNext: () => {dispatch(GoToNextPage())},
        goPrevious: () => {dispatch(GoToPreviousPage())}
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(WishPaging);