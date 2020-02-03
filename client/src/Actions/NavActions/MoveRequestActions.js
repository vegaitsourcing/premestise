import axios from 'axios';
import {webapi} from '../../webApiHost/webapi';

export const GetAllKindergardens = () => {
    
    return dispatch => {
        axios.get(webapi+'/api/kindergarden')
        .then((res)=>{
             dispatch({type:"GET_ALL_KINDERGARDENS", payload: res.data })
        })
    }  
}

export const NewMoveRequest = (newRequest) => {
 
    
    return dispatch => {
        axios.post(webapi+'/api/request', newRequest)
        .then((res)=>{
            window.location.href = "/success";
            
        })
    }  
}
