import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import './Home.css';
import { FaSitemap, FaUsers } from 'react-icons/fa';

function Home() {
  return (
    <div className="home-container">
      <div className="content-wrapper">
        <h2 className="main-heading">Welcome to the Employee Management System</h2>

        <div className="buttons-container">
          <Link to="/departments">
            <Button color="primary" size="lg" className="action-button">
              <span><FaSitemap /> Manage Departments</span>
            </Button>
          </Link>

          <Link to="/employees">
            <Button color="success" size="lg" className="action-button">
              <span><FaUsers /> Manage Employees</span>
            </Button>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default Home;
