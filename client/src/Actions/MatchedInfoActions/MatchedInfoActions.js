import { webApiURL } from "../../HostAddresses/webApiHost";
import { SET_MATCHED_COUNT } from "../../Reducer/reducerActions";

import axios from "axios";

export const GetMatchedCount = form => {
  return dispatch => {
    axios.get(webApiURL + "/api/request/matched/count").then(res => {
      dispatch({ type: SET_MATCHED_COUNT, payload: res.data });
    });
  };
};
