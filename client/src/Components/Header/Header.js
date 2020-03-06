import React, { Component } from "react";

class Header extends Component {
  render() {
    const commonQuestions = "Česta pitanja";
    const writeUs = "Pišite nam";
    const whereToMove = "Gde zelis da se premestis?";
    const latestWish = "Najnovija želja";
    const allWishes = "Sve želje";
    const aboutUsText = "O nama";
    const writeUsViaFacebook = "Pisite nam putem Facebook-a";
    const findUsOnFacebook = "Posetite nas na Facebook-u ";

    return (
      <header className="header">
        <div className="wrap">
          <div className="header__container">
            <a href="/" className="header__home">
              <img
                src="assets/images/logo.png"
                alt=""
                className="header__logo"
              />
              <img
                src="assets/images/logo-mobile.png"
                alt=""
                className="header__logo header__logo--mobile"
              />
            </a>
            <nav className="nav">
              <ul className="nav__list">
                <li className="nav__item">
                  <a
                    data-section-name-link="questions"
                    className="nav__link js-scroll-to"
                  >
                    {commonQuestions}
                  </a>
                </li>
                <li className="nav__item">
                  <a
                    data-section-name-link="form"
                    className="nav__link js-scroll-to"
                  >
                    {writeUs}
                  </a>
                </li>
              </ul>
              <div className="nav__mobile">
                <ul>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="tabs"
                      href="javascript:;"
                    >
  <span className="font-ico-map-q-pin"></span> {whereToMove}&nbsp;
                    </a>
                  </li>


                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="questions"
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> {commonQuestions}&nbsp;
                    </a>
                  </li>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="form"
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> {writeUs}&nbsp;
                    </a>
                  </li>

                </ul>

              </div>
            </nav>
            <div className="mobile-icon js-menu-btn">
              <span className="mobile-icon__bar"></span>
              <span className="mobile-icon__bar"></span>
              <span className="mobile-icon__bar"></span>
            </div>
          </div>
        </div>
      </header>
    );
  }
}

export default Header;
