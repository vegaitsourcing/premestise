import axios from "axios";
import { webApiURL } from "../../HostAddresses/webApiHost";

export const GetMatchedCount = form => {
  return dispatch => {
    axios.get(webApiURL + "/api/request/matched/count").then(res => {
      dispatch({ type: "SET_MATCHED_COUNT", payload: res.data });
    });
  };
};
