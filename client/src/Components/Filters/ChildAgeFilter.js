import React, { Component } from 'react';
import {GetAllWishes} from '../../Actions/NavActions/AllWishesActions';
import {connect} from 'react-redux';

class ChildAgeFilter extends Component {
    render() {
    
        const selectedAge = this.props.selectedAge;
        const currentPage = this.props.currentPage;
        const perPageCount = this.props.perPageCount;
        const elementsAmount = this.props.elementsAmount;

        
        const filterByAge = (ageValue) => {
            this.props.getAllWishes(ageValue)
        }



        return (
            <div className="tab-years">
            <p>Odaberi starosno doba:</p>
            <ul className="tab-years__list">
                <li>
                    <a className={selectedAge==1?'tab-year active':'tab-year'} href="!#" onClick={(e)=>{e.preventDefault(); filterByAge(1)}} >1</a>
                </li>
                <li>
                    <a className={selectedAge==2?'tab-year active':'tab-year'} href="javascript:void(0);" onClick={(e)=>{e.preventDefault(); filterByAge(2)}}>2</a>
                </li>
                <li>
                    <a className={selectedAge==3?'tab-year active':'tab-year'} href="javascript:void(0);" onClick={(e)=>{e.preventDefault(); filterByAge(3)}}>3</a>
                </li>
                <li>
                    <a className={selectedAge==4?'tab-year active':'tab-year'} href="javascript:void(0);" onClick={(e)=>{e.preventDefault(); filterByAge(4)}}>4</a>
                </li>
                <li>
                    <a className={selectedAge==5?'tab-year active':'tab-year'} href="javascript:void(0);" onClick={(e)=>{e.preventDefault(); filterByAge(5)}}>5</a>
                </li>
                <li>
                    <a className={selectedAge==6?'tab-year active':'tab-year'} href="javascript:void(0);" onClick={(e)=>{e.preventDefault(); filterByAge(6)}}>6</a>
                </li>
                <li>
                    <a className={selectedAge==7?'tab-year active':'tab-year'} href="javascript:void(0);" onClick={(e)=>{e.preventDefault(); filterByAge(7)}}>7</a>
                </li>
            </ul>
         
    <span className="number-info">
        (
            {(currentPage-1)*perPageCount+1}-{currentPage*perPageCount<elementsAmount?currentPage*perPageCount:elementsAmount} od ukupno {elementsAmount}
            
        )
        
        </span>
        </div>
        );
    }
}

const mapStateToProps = (state) => {

    return {
        perPageCount: state.perPageCount,
        elementsAmount: state.elementsAmount,
        currentPage: state.currentPage
    }
}

const mapDispatchToProps = (dispatch) => {

    return {
        getAllWishes: (filterByAge) => {  dispatch(GetAllWishes(filterByAge)) }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ChildAgeFilter);