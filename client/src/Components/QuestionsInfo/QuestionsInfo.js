import QuestionAccordion from './QuestionAccordion';
import React, { Component } from "react";
class QuestionsInfo extends Component {
  render() {
    const questionSectionTitle = "Česta pitanja";
    return (
      <section  className="accordions" data-section-name="questions">
        <div className="wrap">
          <h2 className="accordions__title">{questionSectionTitle}</h2>
          <div className="accordions__wrap">
            <QuestionAccordion
             title="Kome je namenjen ovaj sajt?" 
             description="Premesti.se je namenjen roditeljima koji tragaju za premeštajem svoje dece iz jednog vrtića u drugi." 
             />
            <QuestionAccordion
             title="Kako radi ovaj sajt?" 
             description="Popunjavanjem forme pristupa se sistemu i proverava se da li se Vaša želja za premeštajem poklapa sa ostalim željama u sistemu. Ukoliko postoji poklapanje, na mejl ćete dobiti kontakt podatke drugog roditelja i potencijalno izvršiti zamenu. Ukoliko ne postoji trenutno poklapanje, čeka se da se poklapanje zahteva dogodi." 
             />
            <QuestionAccordion
             title="Da li ovaj sajt ima veze sa zahtevom koji se podnosi ustanovi, npr. Radosno Detinjstvo?" 
             description="Nema, ovo je nezavisan projekat." 
             />
            <QuestionAccordion
             title="Koja je svrha ovog sajta ?" 
             description="Da se lakše stupi u kontakt sa roditeljima koji žele da izvrše premeštaj dece iz jednog vrtića u drugi." 
             />
            <QuestionAccordion
             title="Koliko je sigurna moja privatnost ?" 
             description="Podatke koje ste uneli u sistem neće biti javno dostupni i služe samo za realizovanje ideje ove aplikacije." 
             />
            <QuestionAccordion
             title="Ko je napravio ovaj sajt ?" 
             description="Ovaj projekat je rezultat Code for a Cause akcije kompanije Vega IT sa ciljem rešavanja svakodnevnih problema u našem okruženju." 
             />
          </div>
        </div>
    </section>
    );
  }
}

export default QuestionsInfo;
