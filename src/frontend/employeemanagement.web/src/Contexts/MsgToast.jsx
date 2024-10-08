import { useEffect } from 'react';
import { useToast } from './ToastContext';
import { Toast, ToastBody, ToastHeader } from 'reactstrap';

export default function MsgToast() {
    const { toast, setToast } = useToast();
    useEffect(() => {
        if (toast.show) {
            const timer = setTimeout(() => {
                setToast(prev => ({ ...prev, show: false }));
            }, 3000);

            return () => clearTimeout(timer);
        }
    }, [toast.show, setToast]);

    return (
        <>
            {toast.show && (
                <div className="rounded toast-container">
                    <Toast isOpen={toast.show} style={{ backgroundColor: toast.type === 'success' ? '#d4edda' : '#f8d7da' }} fade={false} >
                        <ToastHeader toggle={() => setToast({ show: false })}>
                            {toast.type === 'success' ? 'Success' : 'Error'}
                        </ToastHeader>
                        <ToastBody>{toast.message}</ToastBody>
                    </Toast>
                </div>
            )}
        </>
    );
}
