import axios from 'axios';
import appconfig from '../config';

const api = axios.create({
    baseURL: appconfig.baseURL,
    headers: {
        'Content-Type': 'application/json',
    },
});

api.interceptors.request.use((config) => {
    const token = appconfig.apiToken;
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;
