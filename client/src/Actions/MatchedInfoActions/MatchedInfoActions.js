import { WEB_API_URL } from "../../Config/config";
import { SET_MATCHED_COUNT } from "../../Reducer/reducerActions";

import axios from "axios";

export const GetMatchedCount = form => {
  return dispatch => {
    axios.get(WEB_API_URL + "/api/request/matched/count").then(res => {
      dispatch({ type: SET_MATCHED_COUNT, payload: res.data });
    });
  };
};
