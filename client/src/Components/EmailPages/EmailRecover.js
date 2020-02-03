import React, { Component } from 'react';
import queryString from 'querystring';
import {RecoverEmail} from '../../Actions/EmailActions/EmailActions';
import {connect} from 'react-redux';
import { Link } from 'react-router-dom'
import InfoPage from '../Info/InfoPage';

class EmailRecover extends Component {

    componentDidMount(){
        const queryId = queryString.parse(window.location.search)['?id'];
        this.props.recoverEmail(queryId);
    }

    render() {
        const emailRecovered = this.props.emailRecovered;
        return (
            <div>
                
                    {
                    <InfoPage description={emailRecovered?"Uspesno ste vraceni u sistem posto dogovor nije postignut, srecno u daljem traganju...": "Vracanje u sistem, pricekajte momenat..." }/>               
                    }
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        emailRecovered: state.emailRecovery,
    }
}

const mapDispatchToProps = (dispatch) => {

    return {
        recoverEmail: (id) => {  dispatch(RecoverEmail(id)) }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(EmailRecover);

