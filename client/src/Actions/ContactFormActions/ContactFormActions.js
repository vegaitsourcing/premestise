import axios from 'axios';
import {webapi} from '../../webApiHost/webapi';

export const SendForm = (form) => {
 
    return dispatch => {
        axios.post(webapi+'/api/contact', form)
        .then((res)=>{
            window.location.href = "/contact";
        }).catch(error => window.location.href = "/error")
    }  
}
