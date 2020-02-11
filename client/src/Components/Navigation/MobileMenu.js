import React, { Component } from 'react';

class MobileMenu extends Component {
    render() {
        return (
            <div class="nav__mobile">
            <ul>
                <li>
                    <a class="nav__mobile-link js-scroll-to-mobile" data-section-mobile="tabs" href="javascript:;">
                        <span class="font-ico-map-q-pin"></span> Gde zelis da se premestis? </a>
                </li>
                <li>
                    <a class="nav__mobile-link js-scroll-to-mobile" data-section-mobile="tabs" href="javascript:;">
                        <span class="font-ico-map-q-pin"></span> Najnovija zelja </a>
                </li>
                <li>
                    <a class="nav__mobile-link js-scroll-to-mobile" data-section-mobile="tabs" href="javascript:;">
                        <span class="font-ico-map-q-pin"></span> Sve zelje </a>
                </li>
                <li>
                    <a class="nav__mobile-link js-scroll-to-mobile" data-section-mobile="questions" href="javascript:;">
                        <span class="font-ico-map-q-pin"></span> Cesta pitanja </a>
                </li>
                <li>
                    <a class="nav__mobile-link js-scroll-to-mobile" data-section-mobile="form" href="javascript:;">
                        <span class="font-ico-map-q-pin"></span> Pisite nam </a>
                </li>
                <li>
                    <a class="nav__mobile-link js-scroll-to-mobile" data-section-mobile="" href="javascript:;">
                        <span class="font-ico-map-q-pin"></span> O nama </a>
                </li>
            </ul>
            <ul class="form-social-links">
                <li>
                    <a class="form-link" href="javascript:;">
                        <span class="font-ico-messenger"></span> Pisite nam putem Facebook-a</a>
                </li>
                <li>
                    <a class="form-link" href="javascript:;">
                        <span class="font-ico-facebook"></span> Posetite nas na Facebook-u</a>
                </li>
            </ul>
        </div>
        );
    }
}

export default MobileMenu;