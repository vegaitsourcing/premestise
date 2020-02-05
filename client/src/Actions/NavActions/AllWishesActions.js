import axios from "axios";
import { webApiURL } from "../../HostAddresses/webApiHost";

export const GetAllWishes = (filterByAge = null, filterByPage = null) => {
  return dispatch => {
    axios.get(webApiURL + "/api/request/allwishes").then(res => {
      dispatch({
        type: "GET_ALL_WISHES",
        payload: { data: res.data, filterByAge, filterByPage }
      });
    });
  };
};

export const FilterByChildAge = age => {
  return dispatch => {
    dispatch({ type: "FILTER_BY_CHILD_AGE", payload: age });
  };
};

export const PageNumberSelector = pageNumber => {
  return dispatch => {
    dispatch({ type: "PAGE_SELECTION", payload: pageNumber });
  };
};

export const PrepareRequest = fromKindergardenId => {
  return dispatch => {
    dispatch({ type: "PREPARE_REQUEST_FORM", payload: fromKindergardenId });
    dispatch({ type: "SWITCH_TO_REQUEST_FORM" });
  };
};

export const GoToNextPage = () => {
  return dispatch => {
    dispatch({ type: "GO_TO_NEXT_PAGE" });
  };
};

export const GoToPreviousPage = () => {
  return dispatch => {
    dispatch({ type: "GO_TO_PREVIOUS_PAGE" });
  };
};

export const GetAllWishesForMap = () => {
  return dispatch => {
    axios.get(webApiURL + "/api/request/allwishes").then(res => {
      dispatch({ type: "FOR_MAP_WISHES", payload: { data: res.data } });
    });
  };
};
