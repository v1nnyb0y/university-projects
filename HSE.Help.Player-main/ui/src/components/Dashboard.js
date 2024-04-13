import React from 'react';
import { Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import DashboardAdmin from './DashboardAdmin';
import DashboardUser from './DashboardUser';
import Header from './Header';

const Dashboard = (props) => {
  const { isValidSession, isAdminSession } = props;
  const { userId } = props;

  if (!isValidSession()) {
    return (
      <Redirect
          to={{
            pathname: '/home',
            state: {
              session_expired: true
            }
          }}
        />
    )
  }

  if (isAdminSession(userId)) {
    return (
      <React.Fragment>
        <Header isValidSession={isValidSession} />
        <DashboardAdmin {...props}/>
      </React.Fragment>
    )
  } else {
    return (
      <React.Fragment>
        <Header isValidSession={isValidSession}/>
        <DashboardUser {...props}/>
      </React.Fragment>
    )
  }
}

const mapStateToProps = (state) => {
  return state.userId;
};

export default connect(mapStateToProps)(Dashboard);
