import { webApiURL } from "../../HostAddresses/webApiHost";
import { GET_ALL_KINDERGARDENS } from "../../Reducer/reducerActions";

import axios from "axios";

export const GetAllKindergardens = () => {
  return dispatch => {
    axios.get(webApiURL + "/api/kindergarden").then(res => {
      dispatch({ type: GET_ALL_KINDERGARDENS, payload: res.data });
    });
  };
};

export const NewMoveRequest = newRequest => {
  return dispatch => {
    axios.post(webApiURL + "/api/request", newRequest).then(res => {
      window.location.href = "/success";
    });
  };
};
