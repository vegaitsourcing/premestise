import { webApiURL } from "../../HostAddresses/webApiHost";

import axios from "axios";

export const SendForm = form => {
  return dispatch => {
    axios
      .post(webApiURL + "/api/contact", form)
      .then(res => {
        window.location.href = "/contact";
      })
      .catch(error => (window.location.href = "/error"));
  };
};
