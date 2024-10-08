import api from './api';

export const getEmployees = async () => {
    return await api.get('/employee');
};

export const getEmployeeById = async (id) => {
    return await api.get(`/employee/${id}`);
};

export const createEmployee = async (employee) => {
    return await api.post('/employee', employee);
};

export const updateEmployee = async (id, employee) => {
    return await api.put(`/employee/${id}`, employee);
};

export const deleteEmployee = async (id) => {
    return await api.delete(`/employee/${id}`);
};
