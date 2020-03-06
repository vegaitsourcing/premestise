import Header from "../Header/Header";
import PromoBanner from "../PromoBanner/PromoBanner";
import Navigation from "../Navigation/Navigation";
import ContactForm from "../ContactForm/ContactForm";
import Footer from "../Footer/Footer";
//import MapWishView from "../Map/MapWishView";
import QuestionInfo from "../QuestionsInfo/QuestionsInfo";
import MatchedInfo from "../MatchedInfo/MatchedInfo";

import React, { Component } from "react";

class HomePage extends Component {
  render() {
    return (
      <React.Fragment>
        <Header />
        <PromoBanner />
        <Navigation />
      
        <MatchedInfo />
        <QuestionInfo />
        <ContactForm />
        <Footer />
      </React.Fragment>
    );
  }
}

export default HomePage;
