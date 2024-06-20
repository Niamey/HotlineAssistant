import constants from '../common/constants';

// export const setUser = (data: string) => sessionStorage.setItem('user', data);
export const getToken = () => localStorage.getItem(constants.auth.tokenKey);
export const setToken = (jwt: string) => localStorage.setItem(constants.auth.tokenKey, jwt);
export const removeToken = () => localStorage.removeItem(constants.auth.tokenKey);
