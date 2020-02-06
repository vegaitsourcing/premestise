import QuestionAccordion from './QuestionAccordion';
import React, { Component } from "react";
class QuestionsInfo extends Component {
  render() {
    const questionSectionTitle = "Česta pitanja";
    return (
      <section className="accordions" data-section-name="questions">
        <div className="wrap">
          <h2 className="accordions__title">{questionSectionTitle}</h2>
          <div className="accordions__wrap">
            <QuestionAccordion
             title="Kome je namenjan ovaj sajt?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
            <QuestionAccordion
             title="Kako radi ovaj sajt?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
            <QuestionAccordion
             title="Da li ovaj sajt ima veze sa zahtevom koji se podnosti ustanovi, npr. Radosno Detinjstvo?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
            <QuestionAccordion
             title="Kome je namenjan ovaj sajt?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
            <QuestionAccordion
             title="Koja je svrha ovog sajta ?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
            <QuestionAccordion
             title="Koliko je sigurna moja privatnost ?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
            <QuestionAccordion
             title="Ko je napravio ovaj sajt ?" 
             description="Premesti.se je namenjan roditeljima koji tragaju za premestaj svoje dece iz jednig vrtica u dugi." 
             />
          </div>
        </div>
    </section>
    );
  }
}

export default QuestionsInfo;
