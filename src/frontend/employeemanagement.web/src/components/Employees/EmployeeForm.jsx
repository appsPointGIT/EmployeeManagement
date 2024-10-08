import React, { useEffect, useState } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, FormGroup, Label, Input, Row, Col } from 'reactstrap';
import { useToast } from '../../Contexts/ToastContext';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { createEmployee, updateEmployee } from '../../services/employeeService';
import { getDepartments } from '../../services/departmentService';

export default function EmployeeForm(props) {
    const [departments, setDepartments] = useState([]);
    const { setToast } = useToast();

    useEffect(() => {
        if (props.modal) {
            loadDepartments();
        } else {
            formik.resetForm();
        }
    }, [props.modal]);

    const loadDepartments = async () => {
        try {
            const result = await getDepartments();
            setDepartments(result.data);
        } catch (error) {
            console.error('Error loading departments:', error);
        }
    };

    const validationSchema = Yup.object({
        title: Yup.string().max(10, 'Title cannot exceed 10 characters').required('Title is required'),
        firstName: Yup.string().max(100, 'First name cannot exceed 100 characters').required('First name is required'),
        middleName: Yup.string().max(100, 'Middle name cannot exceed 100 characters'),
        lastName: Yup.string().max(100, 'Last name cannot exceed 100 characters').required('Last name is required'),
        nicNumber: Yup.string().max(15, 'NIC number cannot exceed 15 characters').required('NIC number is required'),
        epfNumber: Yup.string().max(10, 'EPF number cannot exceed 10 characters'),
        etfNumber: Yup.string().max(10, 'ETF number cannot exceed 10 characters'),
        dateOfBirth: Yup.date().required('Date of Birth is required'),
        gender: Yup.string().max(10, 'Gender cannot exceed 10 characters').required('Gender is required'),
        departmentId: Yup.number().required('Department is required'),
        basicSalary: Yup.number().required('Salary is required'),
        activeStatus: Yup.boolean()
    });

    const formik = useFormik({
        initialValues: {
            title: props.formData.title || '',
            firstName: props.formData.firstName || '',
            middleName: props.formData.middleName || '',
            lastName: props.formData.lastName || '',
            nicNumber: props.formData.nicNumber || '',
            epfNumber: props.formData.epfNumber || '',
            etfNumber: props.formData.etfNumber || '',
            dateOfBirth: props.formData.dateOfBirth || '',
            gender: props.formData.gender || '',
            departmentId: props.formData.departmentId || '',
            basicSalary: props.formData.basicSalary || 0,
            activeStatus: props.formData.activeStatus || false
        },
        validationSchema,
        enableReinitialize: true,
        onSubmit: async (values, { resetForm }) => {
            try {
                const payload = {
                    title: values.title,
                    firstName: values.firstName,
                    middleName: values.middleName,
                    lastName: values.lastName,
                    nicNumber: values.nicNumber,
                    epfNumber: values.epfNumber,
                    etfNumber: values.etfNumber,
                    dateOfBirth: values.dateOfBirth,
                    gender: values.gender,
                    activeStatus: values.activeStatus,
                    department: {
                        id: values.departmentId
                    },
                    salary: {
                        basicSalary: values.basicSalary
                    }
                };

                if (props.isEditing) {
                    await updateEmployee(props.formData.id, payload);
                    setToast({ show: true, message: 'Employee updated successfully', type: 'success' });
                } else {
                    await createEmployee(payload);
                    setToast({ show: true, message: 'Employee created successfully', type: 'success' });
                }

                props.loadEmployees();
                props.toggleModal();
                resetForm();
            } catch (error) {
                setToast({ show: true, message: `Error: ${error.message}`, type: 'danger' });
                console.error('Error:', error);
            }
        }
    });

    return (
        <Modal isOpen={props.modal} toggle={props.toggleModal}>
            <ModalHeader toggle={props.toggleModal}>
                {props.isEditing ? 'Edit Employee' : 'Add New Employee'}
            </ModalHeader>
            <form onSubmit={formik.handleSubmit}>
                <ModalBody>
                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="title">Title</Label>
                                <Input
                                    type="select"
                                    name="title"
                                    id="title"
                                    value={formik.values.title}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                >
                                    <option value="">Select Title</option>
                                    <option value="Mr">Mr</option>
                                    <option value="Ms">Ms</option>
                                    <option value="Mrs">Mrs</option>
                                    <option value="Dr">Dr</option>
                                </Input>
                                {formik.touched.title && formik.errors.title ? (
                                    <p className="text-danger">{formik.errors.title}</p>
                                ) : null}
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup>
                                <Label for="firstName">First Name</Label>
                                <Input
                                    type="text"
                                    name="firstName"
                                    id="firstName"
                                    value={formik.values.firstName}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.firstName && formik.errors.firstName ? (
                                    <p className="text-danger">{formik.errors.firstName}</p>
                                ) : null}
                            </FormGroup>
                        </Col>
                    </Row>

                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="middleName">Middle Name</Label>
                                <Input
                                    type="text"
                                    name="middleName"
                                    id="middleName"
                                    value={formik.values.middleName}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.middleName && formik.errors.middleName ? (
                                    <p className="text-danger">{formik.errors.middleName}</p>
                                ) : null}
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup>
                                <Label for="lastName">Last Name</Label>
                                <Input
                                    type="text"
                                    name="lastName"
                                    id="lastName"
                                    value={formik.values.lastName}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.lastName && formik.errors.lastName ? (
                                    <p className="text-danger">{formik.errors.lastName}</p>
                                ) : null}
                            </FormGroup>
                        </Col>
                    </Row>

                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="nicNumber">NIC Number</Label>
                                <Input
                                    type="text"
                                    name="nicNumber"
                                    id="nicNumber"
                                    value={formik.values.nicNumber}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.nicNumber && formik.errors.nicNumber ? (
                                    <p className="text-danger">{formik.errors.nicNumber}</p>
                                ) : null}
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup>
                                <Label for="dateOfBirth">Date of Birth</Label>
                                <Input
                                    type="date"
                                    name="dateOfBirth"
                                    id="dateOfBirth"
                                    value={formik.values.dateOfBirth}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.dateOfBirth && formik.errors.dateOfBirth ? (
                                    <p className="text-danger">{formik.errors.dateOfBirth}</p>
                                ) : null}
                            </FormGroup>
                        </Col>
                    </Row>

                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="epfNumber">EPF Number</Label>
                                <Input
                                    type="text"
                                    name="epfNumber"
                                    id="epfNumber"
                                    value={formik.values.epfNumber}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.epfNumber && formik.errors.epfNumber ? (
                                    <p className="text-danger">{formik.errors.epfNumber}</p>
                                ) : null}
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup>
                                <Label for="etfNumber">ETF Number</Label>
                                <Input
                                    type="text"
                                    name="etfNumber"
                                    id="etfNumber"
                                    value={formik.values.etfNumber}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.etfNumber && formik.errors.etfNumber ? (
                                    <p className="text-danger">{formik.errors.etfNumber}</p>
                                ) : null}
                            </FormGroup>
                        </Col>
                    </Row>

                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="gender">Gender</Label>
                                <Input
                                    type="select"
                                    name="gender"
                                    id="gender"
                                    value={formik.values.gender}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                >
                                    <option value="">Select Gender</option>
                                    <option value="Male">Male</option>
                                    <option value="Female">Female</option>
                                </Input>
                                {formik.touched.gender && formik.errors.gender ? (
                                    <p className="text-danger">{formik.errors.gender}</p>
                                ) : null}
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup>
                                <Label for="departmentId">Department</Label>
                                <Input
                                    type="select"
                                    name="departmentId"
                                    id="departmentId"
                                    value={formik.values.departmentId}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                >
                                    <option value="">Select Department</option>
                                    {departments.map(dep => (
                                        <option key={dep.id} value={dep.id}>
                                            {dep.departmentName}
                                        </option>
                                    ))}
                                </Input>
                                {formik.touched.departmentId && formik.errors.departmentId ? (
                                    <p className="text-danger">{formik.errors.departmentId}</p>
                                ) : null}
                            </FormGroup>
                        </Col>
                    </Row>

                    <Row>
                        <Col md={6}>
                            <FormGroup>
                                <Label for="basicSalary">Basic Salary</Label>
                                <Input
                                    type="number"
                                    name="basicSalary"
                                    id="basicSalary"
                                    min="0"
                                    step="0.01"
                                    value={formik.values.basicSalary}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.basicSalary && formik.errors.basicSalary ? (
                                    <p className="text-danger">{formik.errors.basicSalary}</p>
                                ) : null}
                            </FormGroup>
                        </Col>

                        <Col md={6} className="d-flex justify-content-center align-items-center">
                            <FormGroup check >
                                <Label check for="activeStatus">
                                    <Input
                                        type="checkbox"
                                        name="activeStatus"
                                        id="activeStatus"
                                        checked={formik.values.activeStatus}
                                        onChange={formik.handleChange}
                                    />
                                    {' '} Active Status
                                </Label>
                            </FormGroup>
                        </Col>
                    </Row>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" type="submit">
                        {props.isEditing ? 'Update' : 'Save'}
                    </Button>
                    <Button color="secondary" onClick={props.toggleModal}>Cancel</Button>
                </ModalFooter>
            </form>
        </Modal>
    );

}
