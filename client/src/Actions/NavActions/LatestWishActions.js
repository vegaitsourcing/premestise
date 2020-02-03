import axios from 'axios';
import {webapi} from '../../webApiHost/webapi';
export const GetLatestWish = () => {

    return dispatch => {
        axios.get(webapi+'/api/request/latest')
        .then((res)=>{
            dispatch({type:"LATEST_WISH", payload: res.data })
            
        })
    }  
}


export const PrepareRequest = (fromKindergardenId) => {

    console.log(fromKindergardenId,'PrepareRequest')
    return dispatch => {
        dispatch({type:"PREPARE_REQUEST_FORM", payload: fromKindergardenId })
        dispatch({type:"SWITCH_TO_REQUEST_FORM" })
    }  
}


