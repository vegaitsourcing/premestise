import {PUBLIC_DOMAIN_URL} from "../../Config/config"
import React, { Component } from "react";

class Footer extends Component {
  render() {
    return (
      <footer className="footer">
        <div className="wrap footer__wrap">
          <a href="/" class="footer__logo-link">
            <img
              src="assets/images/logo-footer.png"
              alt=""
              className="footer__logo-img"
            />
            <img
              src="assets/images/logo-footer-2.png"
              alt=""
              className="footer__logo--hidden"
            />
          </a>
          <span className="footer__rights">
            Projekat podržavaju&nbsp;
            <a href="https://www.dsi.rs/en/" target="_blank" className="footer__link">
              Inicijativa Digitalna Srbija
            </a>
            ,&nbsp;
            <a href="https://www.vegaitsourcing.rs/" target="_blank" className="footer__link">
              VegaIT
            </a>
            ,&nbsp;
            <a
              href="https://www.weareelder.com/"
              className="footer__link footer__link--right"
              target="_blank"
            >
              
              Elder Creative Agency&nbsp;
            </a>
            | &copy;&nbsp;
            <span className="footer__border--left">
              Premesti se 2020. All rights reserved.&nbsp;
            </span>
            |&nbsp;
            <a href={PUBLIC_DOMAIN_URL+'/privacy'} className="footer__link">
              Privacy policy
            </a>
          </span>
        </div>
      </footer>
    );
  }
}

export default Footer;
