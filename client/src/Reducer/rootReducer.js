


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
    fromKindergardenId: null,//clean
    toKindergardenIds: [], //clean
    emailVerification: false,
    emailConfirmation: false,
    emailRecovery: false,
    emailRemoveRequest: false,
    prePopulatedId: null,
    matchedCount: 0,
    allWishes: [],
    allWishesForMap:[]

}

const calculateAge = birthDate => Math.floor((new Date() - new Date(birthDate).getTime()) / 3.15576e+10)

const rootReducer = (state = initState, action) => {

    if (action.type === 'CHANGE_NAVIGATION_TAB') {

        return {
            ...state,
            currentNavTab: action.changeTo
        }
    }

    if (action.type === 'GET_ALL_KINDERGARDENS') {

        return {
            ...state,
            kindergardens: action.payload
        }
    }

    if (action.type === 'SET_MATCHED_COUNT') {

        return {
            ...state,
            matchedCount: action.payload
        }
    }

    if (action.type === 'FOR_MAP_WISHES') {

        return {
            ...state,
            allWishesForMap: action.payload.data
        }
    }

    if (action.type === 'GO_TO_NEXT_PAGE') {


        let wishesWithChildAge = state.allWishes.filter((wish) =>
            wish.childAge === state.selectedChildAge)

        let filteredLength = wishesWithChildAge.length;

        wishesWithChildAge = wishesWithChildAge.filter((_, index) =>
            (index >= ((state.currentPage + 1) - 1) * state.perPageCount &&
                index < state.perPageCount * (state.currentPage + 1)));

        return {
            ...state,
            allPendingWishesFiltered: wishesWithChildAge,
            pagesCount: Math.ceil(filteredLength / state.perPageCount),
            currentPage: state.currentPage + 1,
            elementsAmount: filteredLength

        }
    }

    if (action.type === 'GO_TO_PREVIOUS_PAGE') {

        let wishesWithChildAge = state.allWishes.filter((wish) =>
            wish.childAge === state.selectedChildAge)

        let filteredLength = wishesWithChildAge.length;

        wishesWithChildAge = wishesWithChildAge.filter((_, index) =>
            (index >= ((state.currentPage - 1) - 1) * state.perPageCount &&
                index < state.perPageCount * (state.currentPage - 1)));

        return {
            ...state,
            allPendingWishesFiltered: wishesWithChildAge,
            pagesCount: Math.ceil(filteredLength / state.perPageCount),
            currentPage: state.currentPage - 1,
            elementsAmount: filteredLength

        }
    }

    if (action.type === 'PREPARE_REQUEST_FORM') {

        return {
            ...state,
            prePopulatedId: action.payload
        }
    }

    if (action.type === 'SWITCH_TO_LATEST_WISH_TAB') {

        return {
            ...state,
            currentNavTab: 2
        }
    }

    if (action.type === 'SWITCH_TO_REQUEST_FORM') {

        return {
            ...state,
            currentNavTab: 1
        }
    }

    if (action.type === 'LATEST_WISH') {

        return {
            ...state,
            latestWish: action.payload
        }
    }

    if (action.type === 'PAGE_SELECTION') {


        return {
            ...state,
            currentPage: action.payload
        }
    }

    if (action.type === 'GET_ALL_WISHES') {

        if (action.payload.filterByAge !== null && action.payload.filterByPage === null) {

            let wishesWithChildAge = state.allWishes.filter((wish) =>
                wish.childAge === action.payload.filterByAge)

            let filteredLength = wishesWithChildAge.length;

            wishesWithChildAge = wishesWithChildAge.filter((_, index) =>
                index >= 0 && index < state.perPageCount
            );

            return {
                ...state,
                allPendingWishesFiltered: wishesWithChildAge,
                selectedChildAge: action.payload.filterByAge,
                pagesCount: Math.ceil(filteredLength / state.perPageCount),
                currentPage: 1,
                elementsAmount: filteredLength

            }

        }
        else if (action.payload.filterByAge === null && action.payload.filterByPage !== null) {
            console.log(action.payload.filterByPage)

            let wishesWithChildAge = state.allWishes.filter((wish) =>
                wish.childAge === state.selectedChildAge)

            let filteredLength = wishesWithChildAge.length;

            wishesWithChildAge = wishesWithChildAge.filter((_, index) =>
                (index >= (action.payload.filterByPage - 1) * state.perPageCount &&
                    index < state.perPageCount * action.payload.filterByPage));

            return {
                ...state,
                allPendingWishesFiltered: wishesWithChildAge,
                pagesCount: Math.ceil(filteredLength / state.perPageCount),
                currentPage: action.payload.filterByPage,
                elementsAmount: filteredLength

            }
        }
        else //1 1 // init
        {
            let wishes = action.payload.data.map(wish => {
                wish.childAge = calculateAge(wish.childBirthDate);
                return wish;
            });

            let wishesWithChildAge = wishes.filter((wish) =>
                wish.childAge === state.selectedChildAge)
            let filteredLength = wishesWithChildAge.length;


            wishesWithChildAge = wishesWithChildAge.filter((_, index) =>
                index >= (state.currentPage - 1) * state.perPageCount &&
                index < state.perPageCount * state.currentPage);

            console.log(action.payload, 'REDUCER DATA')

            return {
                ...state,
                allWishes: wishes,
                pagesCount: Math.ceil(filteredLength / state.perPageCount),
                allPendingWishesFiltered: wishesWithChildAge,
                elementsAmount: filteredLength
            }
        }


    }

    if (action.type === 'FILTER_BY_CHILD_AGE_AND_PAGE') {

        return {
            ...state,
            selectedChildAge: action.payload
        }
    }

    if (action.type === 'VERIFY_EMAIL') {

        return {
            ...state,
            emailVerification: action.payload
        }
    }


    if (action.type === 'CONFIRM_EMAIL') {

        return {
            ...state,
            emailConfirmation: action.payload
        }
    }
    if (action.type === 'RECOVER_EMAIL') {

        return {
            ...state,
            emailRecovery: action.payload
        }
    }

    if (action.type === 'REMOVE_REQUEST_EMAIL') {

        return {
            ...state,
            emailRemoveRequest: action.payload
        }
    }

    if (action.type === 'DONT_VERIFY_EMAIL') {

        return {
            ...state,
            emailRemoveRequest: action.payload
        }
    }

    return state;
}

export default rootReducer