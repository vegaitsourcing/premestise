import axios from 'axios';
import {webapi} from '../../webApiHost/webapi';

export const GetMatchedCount = (form) => {
 
    return dispatch => {
        axios.get(webapi+'/api/request/matched/count')
        .then((res)=>{
           
            dispatch({type:"SET_MATCHED_COUNT", payload: res.data})
           
        })
    }  
}
