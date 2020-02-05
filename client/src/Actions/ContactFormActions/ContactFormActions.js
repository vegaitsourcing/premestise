import axios from "axios";
import { webApiURL } from "../../HostAddresses/webApiHost";

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
