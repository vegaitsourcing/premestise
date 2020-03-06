import * as reducerActions from "./reducerActions";

const initState = {
  currentNavTab: 1,
  kindergardens: [],
  latestWish: null,
  allPendingWishesFiltered: [],
  currentlyPendingCount: null, //
  currentPage: 1,
  perPageCount: 3,
  selectedChildAge: 1,
  pagesCount: 0,
  elementsAmount: 0,
  fromKindergardenId: null, //clean
  toKindergardenIds: [], //clean
  emailVerification: false,
  emailConfirmation: false,
  emailRecovery: false,
  emailRemoveRequest: false,
  prePopulatedId: null,
  matchedCount: 0,
  allWishes: [],
  allWishesForMap: [],
  cities: []
};

const calculateAge = birthDate =>
  Math.floor((new Date() - new Date(birthDate).getTime()) / 3.15576e10);

const createInitialWishesChunk = (allWishes, state) => {
  let wishesFilteredByAge,
    filteredLength,
    wishesWithMappedDateToAge,
    initialWishesChunk;
  //Wishes view set by initial filter: page 1 and age of 1
  wishesWithMappedDateToAge = allWishes.map(wish => {
    wish.childAge = calculateAge(wish.childBirthDate);
    return wish;
  });

  wishesFilteredByAge = wishesWithMappedDateToAge.filter(
    wish => wish.childAge === state.selectedChildAge
  );

  filteredLength = wishesFilteredByAge.length;

  //Initialy filtered by page 1 and age 1
  initialWishesChunk = wishesFilteredByAge.filter(
    (_, index) =>
      index >= (state.currentPage - 1) * state.perPageCount &&
      index < state.perPageCount * state.currentPage
  );

  return { filteredLength, initialWishesChunk, wishesWithMappedDateToAge };
};
const wishesChunkFilteredByPageNumber = (pageNumber, state) => {
  let wishesWithMappedDateToAge, filteredLength, filteredWishesByPageNumber;

  wishesWithMappedDateToAge = state.allWishes.filter(
    wish => wish.childAge === state.selectedChildAge
  );

  filteredLength = wishesWithMappedDateToAge.length;

  filteredWishesByPageNumber = wishesWithMappedDateToAge.filter(
    (_, index) =>
      index >= (pageNumber - 1) * state.perPageCount &&
      index < state.perPageCount * pageNumber
  );

  return { filteredLength, filteredWishesByPageNumber };
};

const wishesChunkFilteredByAge = (age, state) => {
  let filteredLength, filteredWishesByAge;

  filteredWishesByAge = state.allWishes.filter(wish => wish.childAge === age);

  filteredLength = filteredWishesByAge.length;

  filteredWishesByAge = filteredWishesByAge.filter(
    (_, index) => index >= 0 && index < state.perPageCount
  );

  return { filteredLength, filteredWishesByAge };
};

const rootReducer = (state = initState, action) => {
  if (action.type === reducerActions.CHANGE_NAVIGATION_TAB) {
    return {
      ...state,
      currentNavTab: action.changeTo
    };
  }

  if (action.type === reducerActions.GET_ALL_KINDERGARDENS) {
    return {
      ...state,
      kindergardens: action.payload
    };
  }

  if (action.type === reducerActions.SET_MATCHED_COUNT) {
    return {
      ...state,
      matchedCount: action.payload
    };
  }

  if (action.type === reducerActions.FOR_MAP_WISHES) {
    return {
      ...state,
      allWishesForMap: action.payload.data
    };
  }

  if (action.type === reducerActions.GO_TO_NEXT_PAGE) {
    let wishesWithChildAge = state.allWishes.filter(
      wish => wish.childAge === state.selectedChildAge
    );

    let filteredLength = wishesWithChildAge.length;

    wishesWithChildAge = wishesWithChildAge.filter(
      (_, index) =>
        index >= (state.currentPage + 1 - 1) * state.perPageCount &&
        index < state.perPageCount * (state.currentPage + 1)
    );

    return {
      ...state,
      allPendingWishesFiltered: wishesWithChildAge,
      pagesCount: Math.ceil(filteredLength / state.perPageCount),
      currentPage: state.currentPage + 1,
      elementsAmount: filteredLength
    };
  }

  if (action.type === reducerActions.GO_TO_PREVIOUS_PAGE) {
    let wishesWithChildAge = state.allWishes.filter(
      wish => wish.childAge === state.selectedChildAge
    );

    let filteredLength = wishesWithChildAge.length;

    wishesWithChildAge = wishesWithChildAge.filter(
      (_, index) =>
        index >= (state.currentPage - 1 - 1) * state.perPageCount &&
        index < state.perPageCount * (state.currentPage - 1)
    );

    return {
      ...state,
      allPendingWishesFiltered: wishesWithChildAge,
      pagesCount: Math.ceil(filteredLength / state.perPageCount),
      currentPage: state.currentPage - 1,
      elementsAmount: filteredLength
    };
  }

  if (action.type === reducerActions.PREPARE_REQUEST_FORM) {
    return {
      ...state,
      prePopulatedId: action.payload
    };
  }

  if (action.type === reducerActions.SWITCH_TO_LATEST_WISH_TAB) {
    return {
      ...state,
      currentNavTab: 2
    };
  }

  if (action.type === reducerActions.GET_CITIES) {
    return {
      ...state,
      cities: action.payload
    };
  }

  if (action.type === reducerActions.GET_KINDERS_BY_CITY) {
    return {
      ...state,
      kindergardens: action.payload
    };
  }

  if (action.type === reducerActions.SWITCH_TO_REQUEST_FORM) {
    return {
      ...state,
      currentNavTab: 1
    };
  }

  if (action.type === reducerActions.LATEST_WISH) {
    return {
      ...state,
      latestWish: action.payload
    };
  }

  if (action.type === reducerActions.PAGE_SELECTION) {
    return {
      ...state,
      currentPage: action.payload
    };
  }

  if (action.type === reducerActions.GET_ALL_WISHES) {
    //filter by age number
    if (
      action.payload.filterByAge !== null &&
      action.payload.filterByPage === null
    ) {
      const { filteredLength, filteredWishesByAge } = wishesChunkFilteredByAge(
        action.payload.filterByAge,
        state
      );

      return {
        ...state,
        allPendingWishesFiltered: filteredWishesByAge,
        selectedChildAge: action.payload.filterByAge,
        pagesCount: Math.ceil(filteredLength / state.perPageCount),
        currentPage: 1,
        elementsAmount: filteredLength
      };
    }

    //filter by page number
    if (
      action.payload.filterByAge === null &&
      action.payload.filterByPage !== null
    ) {
      const {
        filteredLength,
        filteredWishesByPageNumber
      } = wishesChunkFilteredByPageNumber(action.payload.filterByPage, state);

      return {
        ...state,
        allPendingWishesFiltered: filteredWishesByPageNumber,
        pagesCount: Math.ceil(filteredLength / state.perPageCount),
        currentPage: action.payload.filterByPage,
        elementsAmount: filteredLength
      };
    }
    //initial view: first page and first age
    const {
      filteredLength,
      wishesWithMappedDateToAge,
      initialWishesChunk
    } = createInitialWishesChunk(action.payload.data, state);

    return {
      ...state,
      allWishes: wishesWithMappedDateToAge,
      pagesCount: Math.ceil(filteredLength / state.perPageCount),
      allPendingWishesFiltered: initialWishesChunk,
      elementsAmount: filteredLength
    };
  }

  if (action.type === reducerActions.FILTER_BY_CHILD_AGE_AND_PAGE) {
    return {
      ...state,
      selectedChildAge: action.payload
    };
  }

  if (action.type === reducerActions.VERIFY_EMAIL) {
    return {
      ...state,
      emailVerification: action.payload
    };
  }

  if (action.type === reducerActions.CONFIRM_EMAIL) {
    return {
      ...state,
      emailConfirmation: action.payload
    };
  }
  if (action.type === reducerActions.RECOVER_EMAIL) {
    return {
      ...state,
      emailRecovery: action.payload
    };
  }

  if (action.type === reducerActions.REMOVE_REQUEST_EMAIL) {
    return {
      ...state,
      emailRemoveRequest: action.payload
    };
  }

  if (action.type === reducerActions.DONT_VERIFY_EMAIL) {
    return {
      ...state,
      emailRemoveRequest: action.payload
    };
  }

  return state;
};

export default rootReducer;
