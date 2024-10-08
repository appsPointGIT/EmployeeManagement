import api from './api';

export const getDepartments = async () => {
    return await api.get('/department');
};

export const getDepartmentById = async (id) => {
    return await api.get(`/department/${id}`);
};

export const createDepartment = async (department) => {
    return await api.post('/department', department);
};

export const updateDepartment = async (id, department) => {
    return await api.put(`/department/${id}`, department);
};

export const deleteDepartment = async (id) => {
    return await api.delete(`/department/${id}`);
};
