import React, { Component } from 'react';
import {connect} from 'react-redux';
import {SendForm} from '../../Actions/ContactFormActions/ContactFormActions';
import { Link } from 'react-router-dom'
class ContactForm extends Component {

    state = {
        emailErrorMessage: null,
        messageErrorMessage: null,
        email: '',
        message: '',
        emailErrorExists:true,
        messageErrorExists:true
        
    }

    handleSubmitData = (event) => {
        event.preventDefault();
        let form = {
            email: this.state.email,
            message: this.state.message
        };

        if(form.email === "")
        {
            this.setState({emailErrorMessage: "Obavezno uneti e-mail adresu!"})
        }
        if(form.message === "")
        {
            this.setState({messageErrorMessage: "Obavezno uneti text poruke!"})
        }
        
        if(!this.checkIfHasErrors()  ){
            this.props.sendForm(form);
        }
    }

    checkIfHasErrors = () => {

        return (this.state.emailErrorExists || this.state.messageErrorExists) ? true: false;  
    }

    handleEmailChange = (event) => {
        event.preventDefault();
        let mailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if(event.target.value !== "")
        {
            if(!mailRegex.test(event.target.value))
            {
                this.setState({email: event.target.value})
                this.setState({emailErrorMessage: "Proveriti format e-mail adrese!"})
                this.setState({emailErrorExists: true})
            }
            else
            {
                this.setState({emailErrorMessage: null})
                this.setState({email: event.target.value})
                this.setState({emailErrorExists: false})
            }
        }else {
            this.setState({email: ""})
            this.setState({emailErrorMessage: "Proveriti format e-mail adrese!"})
            this.setState({emailErrorExists: true})
        }
    }

    handleMessageChange = (event) => {
        event.preventDefault();
        if(event.target.value !== "")
        {
            this.setState({message: event.target.value})
            this.setState({messageErrorMessage: null})
            this.setState({messageErrorExists: false})
        } else {
            this.setState({messageErrorMessage: "Obavezno uneti text poruke!"})
            this.setState({message: ""})
            this.setState({messageErrorExists: true})
        }

    }


    render() {

        const errorMsgStyle = {
            color: 'yellow',
            display: 'block',
            marginBottom: 32

        };

        const noMarginBottom = {
            marginBottom: 0
        }
        const { email, message } = this.state;

        return (
            <section class="contact-us" data-section-name="form">
			<div class="contact-us__wrap wrap">
				<h2 class="contact-us__heading">Pišite nam</h2>
				<form class="contact-us__form">
					<div class="contact-us__email" style={noMarginBottom}>
						<input class="contact-us__input" id="email" type="email" placeholder="E-mail" onChange={this.handleEmailChange} value={email}/>
						<span class="contact-us__text">Unapred smo zahvalni za sve dobre i loše stvari na sajtu koje nam javite.</span>
					</div>
                    <span style={errorMsgStyle}>{this.state.emailErrorMessage}</span>
					<textarea  style={noMarginBottom} class="contact-us__textarea" id="message" name="message" placeholder="Text" onChange={this.handleMessageChange} value={message}></textarea>
					<span style={errorMsgStyle}>{this.state.messageErrorMessage}</span>
                    <span class="contact-us__text--hidden">Unapred smo zahvalni za sve dobre i loše stvari na sajtu koje nam javite.</span>
					<div class="contact-us__send">
						<a href="javascript:;" class="contact-us__facebook"> Ili nas pronađite na Facebook-u: <span class="font-ico-facebook"></span>
						</a>
                        <Link to="/contact" onClick={this.handleSubmitData}>
						<button class="contact-us__btn btn js-submit" >Pošalji</button>
                        </Link>
					</div>
				</form>
			</div>
		</section>
        );
    }
}


const mapDispatchToProps = (dispatch) => {

    return {
        sendForm: (form) => {  dispatch(SendForm(form)) },
    }
}

export default connect(null, mapDispatchToProps)(ContactForm);