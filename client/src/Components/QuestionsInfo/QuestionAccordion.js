import React, { Component } from 'react';

class QuestionAccordion extends Component {
    render() {
        return (
            <div className="accordion">
            <button className="accordion__btn js-open-accordion">
                <span className="font-ico-chevron-down"></span> {this.props.title} </button>
            <div className="accordion__content">
                <p>{this.props.description}</p>
            </div>
        </div>
        );
    }
}

export default QuestionAccordion;