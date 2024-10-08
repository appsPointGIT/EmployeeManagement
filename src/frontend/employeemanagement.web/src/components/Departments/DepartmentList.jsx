import { useEffect, useState } from 'react';
import { useToast } from '../../contexts/ToastContext';
import { Button } from 'reactstrap';
import { FaPlusCircle, FaEdit, FaRegTrashAlt } from 'react-icons/fa';
import { getDepartments, deleteDepartment } from '../../services/departmentService';
import DataTable from '../Common/DataTable';
import DepartmentForm from './DepartmentForm';

const DepartmentList = () => {
    const [departments, setDepartments] = useState([]);
    const [modal, setModal] = useState(false);
    const [formData, setFormData] = useState({ id: '', departmentName: '' });
    const [isEditing, setIsEditing] = useState(false);
    const { setToast } = useToast();

    useEffect(() => {
        loadDepartments();
    }, []);

    const loadDepartments = async () => {
        try {
            const result = await getDepartments();
            setDepartments(result.data);
        } catch (error) {
            console.error('Error loading departments:', error);
        }
    };

    const toggleModal = () => {
        setModal(!modal);
        if (!modal) {
            setFormData({ id: '', departmentName: '' });
            setIsEditing(false);
        }
    };

    const handleCreate = () => {
        toggleModal();
    };

    const handleEdit = (id) => {
        const department = departments.find(dep => dep.id === id);
        if (department) {
            toggleModal();
            setIsEditing(true);
            setFormData({ id: department.id, departmentName: department.departmentName });
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this department?')) {
            try {
                await deleteDepartment(id);
                setToast({ show: true, message: 'Department deleted successfully', type: 'success' });
                loadDepartments();
            } catch (error) {
                setToast({ show: true, message: 'Error deleting department', type: 'danger' });
                console.error('Error:', error);
            }
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevData => ({ ...prevData, [name]: value }));
    };

    const columns = [
        {
            sortable: false,
            width: 100,
            headerName: 'Actions',
            field: 'actions',
            renderCell: ({ row }) => (
                <div style={{ display: 'flex', justifyContent: 'space-between', width: '100%' }}>
                    <Button
                        variant="outlined"
                        color="primary"
                        size="small"
                        style={{ marginTop: '2px' }}
                        onClick={() => handleEdit(row.id)}
                    >
                        <FaEdit />
                    </Button>
                    <Button
                        variant="outlined"
                        color="warning"
                        size="small"
                        style={{ marginLeft: '2px', marginTop: '2px' }}
                        onClick={() => handleDelete(row.id)}
                    >
                        <FaRegTrashAlt />
                    </Button>
                </div>
            ),
            flex: 0,
        },
        { headerName: 'ID', field: 'id', flex: 0 },
        { headerName: 'Department Name', field: 'departmentName', flex: 1 },
    ];

    return (
        <div>
            <br />
            <h2 >Departments</h2>
            <hr />
            <div style={{ display: 'flex', justifyContent: 'flex-start' }}>
                <Button
                    variant="contained"
                    color="success"
                    onClick={handleCreate}
                    className="mb-3"
                >
                    <span> <FaPlusCircle /> Add New Department</span>
                </Button>
            </div>
            <DataTable columns={columns} data={departments} />

            <DepartmentForm
                modal={modal}
                toggleModal={toggleModal}
                formData={formData}
                handleChange={handleChange}
                isEditing={isEditing}
                loadDepartments={loadDepartments}
            />

        </div>
    );
};

export default DepartmentList;
