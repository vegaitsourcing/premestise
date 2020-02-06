import React, { Component } from "react";

class Footer extends Component {
  render() {
    return (
      <footer class="footer">
        <div class="wrap footer__wrap">
          <a href="/" class="footer__logo-link">
            <img
              src="assets/images/logo-footer.png"
              alt=""
              class="footer__logo-img"
            />
            <img
              src="assets/images/logo-footer-2.png"
              alt=""
              class="footer__logo--hidden"
            />
          </a>
          <span class="footer__rights">
            Projekat podržavaju&nbsp;
            <a href="https://www.dsi.rs/en/" class="footer__link">
              Inicijativa Digitalna Srbija
            </a>
            ,&nbsp;
            <a href="https://www.vegaitsourcing.rs/" class="footer__link">
              VegaIT
            </a>
            ,&nbsp;
            <a
              href="https://www.weareelder.com/"
              class="footer__link footer__link--right"
            >
              
              Elder Creative Agency&nbsp;
            </a>
            | &copy;&nbsp;
            <span class="footer__border--left">
              Premesti se 2020. All rights reserved.&nbsp;
            </span>
            |&nbsp;
            <a href="javascript:;" class="footer__link">
              Privacy policy
            </a>
          </span>
        </div>
      </footer>
    );
  }
}

export default Footer;
