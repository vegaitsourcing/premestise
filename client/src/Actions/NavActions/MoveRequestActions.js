import axios from "axios";
import { webApiURL } from "../../HostAddresses/webApiHost";

export const GetAllKindergardens = () => {
  return dispatch => {
    axios.get(webApiURL + "/api/kindergarden").then(res => {
      dispatch({ type: "GET_ALL_KINDERGARDENS", payload: res.data });
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
