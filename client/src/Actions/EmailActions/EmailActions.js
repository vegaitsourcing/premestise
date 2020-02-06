import { webApiURL } from "../../HostAddresses/webApiHost";
import {
  CONFIRM_EMAIL,
  VERIFY_EMAIL,
  RECOVER_EMAIL,
  REMOVE_REQUEST_EMAIL
} from "../../Reducer/reducerActions";

import axios from "axios";

export const ConfirmEmail = id => {
  return dispatch => {
    axios.get(webApiURL + "/api/email/confirm?id=" + id).then(res => {
      dispatch({ type: CONFIRM_EMAIL, payload: true });
    });
  };
};

export const VerifyEmail = id => {
  return dispatch => {
    axios.get(webApiURL + "/api/email/verify?id=" + id).then(res => {
      dispatch({ type: VERIFY_EMAIL, payload: true });
    });
  };
};

export const RecoverEmail = id => {
  return dispatch => {
    axios.get(webApiURL + "/api/email/recover?id=" + id).then(res => {
      dispatch({ type: RECOVER_EMAIL, payload: true });
    });
  };
};

export const RemoveRequestEmail = id => {
  return dispatch => {
    axios.get(webApiURL + "/api/request/delete?id=" + id).then(res => {
      dispatch({ type: REMOVE_REQUEST_EMAIL, payload: true });
    });
  };
};
