import React from 'react';
import { Button } from 'react-bootstrap';

const Header = (props) => {
  const { isValidSession } = props;

  const handleLogout = (events) => {
    localStorage.clear();
    window.location = "/";
  }
  
  if (typeof (isValidSession) === 'function' && isValidSession()) {
    return (
      <div className={'row'}>
        <h1 className="col-md-10 main-heading">Spotify Music Search</h1>
        <div className={'col-md-2 main-heading'}>
          <Button variant="info" onClick={handleLogout}>
            Logout
          </Button>
        </div>
      </div>
    )
  } else {
    return (
      <h1 className={'main-heading'}>Spotify Music Search</h1>
    )
  }
};

export default Header;
