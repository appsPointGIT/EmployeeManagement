import {
    Navbar,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { FaSitemap, FaUsers } from 'react-icons/fa';

function Header() {

    return (
        <div>
            <Navbar color='light' fixed='top' container={true} >
                <NavbarBrand href="/">Employee Management App</NavbarBrand>
                <Nav className="" >
                    <NavItem>
                        <NavLink tag={Link} to="/departments"><span><FaSitemap /> Manage Departments</span></NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} to="/employees"><span><FaUsers /> Manage Employees</span></NavLink>
                    </NavItem>
                </Nav>
            </Navbar>
        </div>
    );
}

export default Header;