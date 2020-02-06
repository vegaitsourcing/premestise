import React, { Component } from "react";

class PromoBanner extends Component {
  render() {
    const bannerHeadTitle = "Lakša komunikacija roditelja radi pronalaženja zamene mesta u vrtićima";
    const bannerDescription = "Kad premeštamo dete iz jednog vrtića u drugi i kada nema slobodnih \
    mesta, potrebno je da nađemo odgovarajuću zamenu, sa jednim ili \
    više drugara. Upišite Vaše želje, a mi ćemo Vam poslati e-mail \
    kada se desi poklapanje";

    return (
      <section
        class="banner"
        data-section-name="banner"
      >
        <div className="wrap">
          <div className="banner__holder">
            <h1 className="banner__heading">
            {bannerHeadTitle}
            </h1>
            <p className="banner__paragraf">
            {bannerDescription}
            </p>
          </div>
        </div>
      </section>
    );
  }
}

export default PromoBanner;
