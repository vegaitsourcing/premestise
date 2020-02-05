import axios from "axios";
import { webApiURL } from "../../HostAddresses/webApiHost";

export const GetLatestWish = () => {
  return dispatch => {
    axios.get(webApiURL + "/api/request/latest").then(res => {
      dispatch({ type: "LATEST_WISH", payload: res.data });
    });
  };
};

export const PrepareRequest = fromKindergardenId => {
  return dispatch => {
    dispatch({ type: "PREPARE_REQUEST_FORM", payload: fromKindergardenId });
    dispatch({ type: "SWITCH_TO_REQUEST_FORM" });
  };
};
