import React, { Component } from 'react';
import {connect} from 'react-redux';
import queryString from 'querystring';
import {VerifyEmail} from '../../Actions/EmailActions/EmailActions';
import InfoPage from '../Info/InfoPage';
import { Link } from 'react-router-dom'

class EmailVerification extends Component {


    componentDidMount(){
        const queryId = queryString.parse(window.location.search)['?id'];
        this.props.verifyEmail(queryId);
    }

    render() {
        const emailVerified = this.props.emailVerified;
        return (
            <div className="wrap">
                {
                <InfoPage description={emailVerified?"Vas zahtev je usao u sistem, obavesticemo Vas cim se dogodi poklapanje.": "Verifikacija u toku" }/>               
                }
            </div>
        );
    }
}


const mapStateToProps = (state) => {
    return {
        emailVerified: state.emailVerification,

    }
}

const mapDispatchToProps = (dispatch) => {

    return {
        verifyEmail: (id) => {  dispatch(VerifyEmail(id)) }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(EmailVerification);