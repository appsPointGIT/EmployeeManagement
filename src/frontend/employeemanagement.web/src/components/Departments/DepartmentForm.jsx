import { useToast } from '../../Contexts/ToastContext';
import { Modal, ModalHeader, ModalBody, ModalFooter, FormGroup, Label, Input, Button } from 'reactstrap';
import { createDepartment, updateDepartment } from '../../services/departmentService';
import { Formik } from 'formik';
import * as Yup from 'yup';

const DepartmentForm = (props) => {
    const { setToast } = useToast();

    const validationSchema = Yup.object().shape({
        departmentName: Yup.string().max(100, 'Department name cannot exceed 100 characters').required('Department name is required'),
    });

    const handleSubmit = async (values) => {
        try {
            if (props.isEditing) {
                await updateDepartment(props.formData.id, { departmentName: values.departmentName });
                setToast({ show: true, message: 'Department updated successfully', type: 'success' });
            } else {
                await createDepartment({ departmentName: values.departmentName });
                setToast({ show: true, message: 'Department created successfully', type: 'success' });
            }
            props.loadDepartments();
            props.toggleModal();
        } catch (error) {
            setToast({ show: true, message: `Error: ${error.message}`, type: 'danger' });
            console.error('Error:', error);
        }
    };

    return (
        <Modal isOpen={props.modal} toggle={props.toggleModal}>
            <ModalHeader toggle={props.toggleModal}>
                {props.isEditing ? 'Edit Department' : 'Add New Department'}
            </ModalHeader>
            <Formik
                initialValues={{
                    departmentName: props.formData.departmentName || '',
                }}
                validationSchema={validationSchema}
                enableReinitialize={true}
                onSubmit={handleSubmit}
            >
                {({ handleChange, handleBlur, handleSubmit, values, errors, touched }) => (
                    <form onSubmit={handleSubmit}>
                        <ModalBody>
                            <FormGroup>
                                <Label for="departmentName">Department Name</Label>
                                <Input
                                    type="text"
                                    name="departmentName"
                                    id="departmentName"
                                    value={values.departmentName}
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />
                                {touched.departmentName && errors.departmentName && (
                                    <p className="text-danger">{errors.departmentName}</p>
                                )}
                            </FormGroup>
                        </ModalBody>
                        <ModalFooter>
                            <Button color="primary" type="submit">
                                {props.isEditing ? 'Update' : 'Save'}
                            </Button>
                            <Button color="secondary" onClick={props.toggleModal}>Cancel</Button>
                        </ModalFooter>
                    </form>
                )}
            </Formik>
        </Modal >
    );
};

export default DepartmentForm;
