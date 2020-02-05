import React, { Component } from "react";

class Header extends Component {
  render() {
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
                    href="javascript:;"
                    data-section-name-link="questions"
                    className="nav__link js-scroll-to"
                  >
                    Česta pitanja{" "}
                  </a>
                </li>
                <li className="nav__item">
                  <a
                    href="javascript:;"
                    data-section-name-link="form"
                    className="nav__link js-scroll-to"
                  >
                    Pišite nam{" "}
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
                      <span className="font-ico-map-q-pin"></span> Gde zelis da
                      se premestis?{" "}
                    </a>
                  </li>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="tabs"
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> Najnovija
                      zelja{" "}
                    </a>
                  </li>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="tabs"
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> Sve zelje{" "}
                    </a>
                  </li>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="questions"
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> Cesta pitanja{" "}
                    </a>
                  </li>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile="form"
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> Pisite nam{" "}
                    </a>
                  </li>
                  <li>
                    <a
                      className="nav__mobile-link js-scroll-to-mobile"
                      data-section-mobile=""
                      href="javascript:;"
                    >
                      <span className="font-ico-map-q-pin"></span> O nama{" "}
                    </a>
                  </li>
                </ul>
                <ul className="form-social-links">
                  <li>
                    <a className="form-link" href="javascript:;">
                      <span className="font-ico-messenger"></span> Pisite nam
                      putem Facebook-a
                    </a>
                  </li>
                  <li>
                    <a className="form-link" href="javascript:;">
                      <span className="font-ico-facebook"></span> Posetite nas
                      na Facebook-u
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
