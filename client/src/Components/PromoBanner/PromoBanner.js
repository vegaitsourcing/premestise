import React, { Component } from "react";

class PromoBanner extends Component {
  render() {
    return (
      <section
        class="banner"
        data-section-name="banner"
        style={{
          backgroundImage: "url(assets/images/baby-boy-child-159533.jpg)"
        }}
      >
        <div className="wrap">
          <div className="banner__holder">
            <h1 className="banner__heading">
              Lakša komunikacija roditelja radi pronalaženja zamene mesta u
              vrtićima
            </h1>
            <p className="banner__paragraf">
              Kad premeštamo dete iz jednog vrtića u drugi i kada nema slobodnih
              mesta, potrebno je da nađemo odgovarajuću zamenu, sa jednim ili
              više drugara. Upišite Vaše želje, a mi ćemo Vam poslati e-mail
              kada se desi poklapanje
            </p>
          </div>
        </div>
      </section>
    );
  }
}

export default PromoBanner;
