import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { ToastProvider } from './Contexts/ToastContext';
import MsgToast from './Contexts/MsgToast';
import Header from './components/Common/Header';
import Home from './components/Home/Home';
import DepartmentList from './components/Departments/DepartmentList';
import EmployeeList from './components/Employees/EmployeeList';
import './App.css'

function App() {
    return (
        <Router>
            <div className="app">
                <Header />
                <ToastProvider>
                    <div className="main-view">
                        <MsgToast />

                        <Routes>
                            <Route path="/" element={<Home />} />
                            <Route path="/home" element={<Home />} />
                            <Route path="/departments" element={<DepartmentList />} />
                            <Route path="/employees" element={<EmployeeList />} />
                        </Routes>
                    </div>
                </ToastProvider>
            </div>
        </Router>
    );
}

export default App;
