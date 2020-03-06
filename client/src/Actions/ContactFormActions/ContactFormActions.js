import { WEB_API_URL } from "../../Config/config";

import axios from "axios";

export const SendForm = form => {
  return dispatch => {
    axios
      .post(WEB_API_URL + "/api/contact", form)
      .then(res => {
        window.location.href = "/contact";
      })
      .catch(error => (window.location.href = "/error"));
  };
};


export const ReloadPreviousPage = (e) => {
  e.preventDefault()
  window.location.replace(document.referrer)
};
