import { WEB_API_URL } from "../../Config/config";
import { GET_ALL_KINDERGARDENS } from "../../Reducer/reducerActions";

import axios from "axios";

export const GetAllKindergardens = () => {
  return dispatch => {
    axios.get(WEB_API_URL + "/api/kindergarden").then(res => {
      dispatch({ type: GET_ALL_KINDERGARDENS, payload: res.data });
    });
  };
};

export const NewMoveRequest = newRequest => {
  return dispatch => {
    axios.post(WEB_API_URL + "/api/request", newRequest).then(res => {
      window.location.href = "/success";
    });
  };
};
