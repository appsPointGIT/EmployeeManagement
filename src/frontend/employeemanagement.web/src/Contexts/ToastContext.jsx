import { React, createContext, useContext, useState } from 'react';

const ToastContext = createContext();

export const ToastProvider = ({ children }) => {
    const [toast, setToast] = useState({ show: false, message: '', type: '' });

    return (
        <ToastContext.Provider value={{ toast, setToast }}>
            {children}
        </ToastContext.Provider>
    );
};

export const useToast = () => {
    return useContext(ToastContext);
};
