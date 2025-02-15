export const httpParams = (obj) => {
    Object.keys(obj).forEach((k) => (!obj[k] && obj[k] !== undefined) && delete obj[k]);
  
    for(var i=0,keys = Object.keys(obj),len=keys.length;i<len;i++){ 
      if(typeof obj[keys[i]] === 'undefined'){
        delete obj[keys[i]];
      }
    }
  
    return obj;
};