import { WEB_API_URL } from "../../Config/config";
import {
  LATEST_WISH,
  PREPARE_REQUEST_FORM,
  SWITCH_TO_REQUEST_FORM
} from "../../Reducer/reducerActions";

import axios from "axios";

export const GetLatestWish = () => {
  return dispatch => {
    axios.get(WEB_API_URL + "/api/request/latest").then(res => {
      dispatch({ type: LATEST_WISH, payload: res.data });
    });
  };
};

export const PrepareRequest = fromKindergardenId => {
  alert(fromKindergardenId)
  return dispatch => {
    dispatch({ type: PREPARE_REQUEST_FORM, payload: fromKindergardenId });
    dispatch({ type: SWITCH_TO_REQUEST_FORM });
  };
};
