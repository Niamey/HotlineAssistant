import Axios, { AxiosError } from 'axios';
import { SessionStorage } from 'quasar';
import constants from '../common/constants';


const baseApi = Axios.create({
  withCredentials: true,
  timeout: 120000,
  responseType: 'json',
  headers: {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    Accept: 'application/json, text/plain, */*'
  }
});

export function setJWT (jwtToken: string) {
  baseApi.defaults.headers.common.Authorization = `Bearer ${jwtToken}`;
}

export function clearJWT () {
  delete baseApi.defaults.headers.common.Authorization;
}

baseApi.interceptors.request.use((config) => {
  // Do something before request is sent
  return config;
}, (error) => {
  console.log('Error', error.message);
});

// Add a response interceptor
baseApi.interceptors.response.use((response) => {
  // Do something with response data
  checkAppVersion(response);
  return response;
}, (error: AxiosError) => {
  return Promise.reject(error);
});

function checkAppVersion (response: any) {
  if (response.headers[constants.versionId] !== SessionStorage.getItem(constants.versionId)) {
    SessionStorage.set(constants.isNeedUpdate, true);
  }
};

export default baseApi;
