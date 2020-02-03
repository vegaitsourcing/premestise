import axios from 'axios';
import {webapi} from '../../webApiHost/webapi';


export const ConfirmEmail = (id) => {

    return dispatch => {
        axios.get(webapi+'/api/email/confirm?id='+id)
        .then((res)=>{
            dispatch({type:"CONFIRM_EMAIL", payload: true })
        })
    }  
}

export const VerifyEmail = (id) => {

    return dispatch => {
        axios.get(webapi+'/api/email/verify?id='+id)
        .then((res)=>{
            dispatch({type:"VERIFY_EMAIL", payload: true })
            
        })
    }  
}

export const RecoverEmail = (id) => {

    return dispatch => {
        axios.get(webapi+'/api/email/recover?id='+id)
        .then((res)=>{
            dispatch({type:"RECOVER_EMAIL", payload: true })
        })
    }  
}

export const RemoveRequestEmail = (id) => {

    return dispatch => {
        axios.get(webapi+'/api/request/delete?id='+id)
        .then((res)=>{
            dispatch({type:"REMOVE_REQUEST_EMAIL", payload: true })
        })
    }  
}