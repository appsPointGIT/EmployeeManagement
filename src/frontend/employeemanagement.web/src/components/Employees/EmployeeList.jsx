import { useEffect, useState } from 'react';
import { useToast } from '../../Contexts/ToastContext';
import { Button } from 'reactstrap';
import { FaCheckCircle, FaRegCircle, FaPlusCircle, FaEdit, FaRegTrashAlt } from 'react-icons/fa';
import { getEmployees, deleteEmployee } from '../../services/employeeService';
import DataTable from '../Common/DataTable';
import EmployeeForm from './EmployeeForm';

const EmployeeList = () => {
    const [employees, setEmployees] = useState([]);
    const [modal, setModal] = useState(false);
    const [formData, setFormData] = useState(initialFormState());
    const [isEditing, setIsEditing] = useState(false);
    const { setToast } = useToast();

    function initialFormState() {
        return {
            id: '',
            title: '',
            firstName: '',
            middleName: '',
            lastName: '',
            nicNumber: '',
            epfNumber: '',
            etfNumber: '',
            dateOfBirth: '',
            gender: '',
            departmentId: '',
            basicSalary: 0,
            activeStatus: false,
        };
    }

    useEffect(() => {
        loadEmployees();
    }, []);

    const loadEmployees = async () => {
        try {
            const result = await getEmployees();
            const sanitizedEmployees = result.data.map(employee => ({
                id: employee.id,
                title: employee.title,
                firstName: employee.firstName,
                middleName: employee.middleName,
                lastName: employee.lastName,
                nicNumber: employee.nicNumber,
                epfNumber: employee.epfNumber,
                etfNumber: employee.etfNumber,
                dateOfBirth: employee.dateOfBirth.split('T')[0],
                gender: employee.gender,
                departmentId: employee.department ? employee.department.id : '',
                departmentName: employee.department ? employee.department.departmentName : 'N/A',
                basicSalary: employee.salary.basicSalary,
                activeStatus: employee.activeStatus === true,
            }));
            setEmployees(sanitizedEmployees);
        } catch (error) {
            console.error('Error loading employees:', error);
        }
    };

    const toggleModal = () => {
        setModal(!modal);
        if (!modal) {
            resetForm();
        }
    };

    const resetForm = () => {
        setFormData(initialFormState());
        setIsEditing(false);
    };

    const handleCreate = () => {
        toggleModal();
    };

    const handleEdit = (id) => {
        const employee = employees.find(emp => emp.id === id);
        if (employee) {
            toggleModal();
            setIsEditing(true);
            setFormData({
                id: employee.id,
                title: employee.title,
                firstName: employee.firstName,
                middleName: employee.middleName,
                lastName: employee.lastName,
                nicNumber: employee.nicNumber,
                epfNumber: employee.epfNumber,
                etfNumber: employee.etfNumber,
                dateOfBirth: employee.dateOfBirth,
                gender: employee.gender,
                departmentId: employee.departmentId,
                basicSalary: employee.basicSalary,
                activeStatus: employee.activeStatus,
            });
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this employee?')) {
            try {
                await deleteEmployee(id);
                setToast({ show: true, message: 'Employee deleted successfully', type: 'success' });
                loadEmployees();
            } catch (error) {
                setToast({ show: true, message: 'Error deleting employee', type: 'danger' });
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
            headerName: 'Actions',
            field: 'actions',
            width: 100,
            sortable: false,
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
        },
        { headerName: 'ID', field: 'id', flex: 1 },
        { headerName: 'Title', field: 'title', flex: 1 },
        { headerName: 'First Name', field: 'firstName', flex: 0 },
        { headerName: 'Middle Name', field: 'middleName', flex: 1 },
        { headerName: 'Last Name', field: 'lastName', flex: 0 },
        { headerName: 'NIC', field: 'nicNumber', flex: 0 },
        { headerName: 'EPF No', field: 'epfNumber', flex: 0 },
        { headerName: 'ETF No', field: 'etfNumber', flex: 0 },
        { headerName: 'Date of Birth', field: 'dateOfBirth', flex: 0 },
        { headerName: 'Gender', field: 'gender', flex: 1 },
        { headerName: 'Department', field: 'departmentName', flex: 0 },
        { headerName: 'Basic Salary', field: 'basicSalary', flex: 0 },
        {
            headerName: 'Active Status',
            field: 'activeStatus',
            flex: 0,
            renderCell: ({ value }) => (
                value ? <FaCheckCircle color="green" /> : <FaRegCircle color="grey" />
            ),
        },
    ];

    return (
        <div>
            <h2>Employees</h2>
            <hr />
            <div style={{ display: 'flex', justifyContent: 'flex-start' }}>
                <Button color="success" onClick={handleCreate} className="mb-3">
                    <span><FaPlusCircle /> Add New Employee</span>
                </Button>
            </div>
            <DataTable columns={columns} data={employees} />

            <EmployeeForm
                modal={modal}
                toggleModal={toggleModal}
                formData={formData}
                handleChange={handleChange}
                isEditing={isEditing}
                loadEmployees={loadEmployees}
            />

        </div>
    );
};

export default EmployeeList;
