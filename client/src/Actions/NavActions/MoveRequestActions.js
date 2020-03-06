import { WEB_API_URL } from "../../Config/config";
import { GET_ALL_KINDERGARDENS, GET_CITIES, GET_KINDERS_BY_CITY } from "../../Reducer/reducerActions";

import axios from "axios";

export const GetAllKindergardens = () => {
  return dispatch => {
    axios.get(WEB_API_URL + "/api/kindergarden").then(res => {
      dispatch({ type: GET_ALL_KINDERGARDENS, payload: res.data });
    });
  };
};

export const NewMoveRequest = newRequest => {
  let a = 1;
  return dispatch => {
    axios.post(WEB_API_URL + "/api/request", newRequest).then(res => {
      window.location.href = "/success";
    });
  };
};

export const GetCities = () => {
  return dispatch => {
    axios.get(WEB_API_URL + "/api/kindergarden/cities").then(res => {
      dispatch({ type: GET_CITIES, payload: res.data });
    });
  };
};

export const GetKindergardensByCity = (city) => {
  return dispatch => {
    axios.get(WEB_API_URL + "/api/kindergarden/bycity?city="+city).then(res => {
      dispatch({ type: GET_KINDERS_BY_CITY, payload: res.data });
    });
  };
};