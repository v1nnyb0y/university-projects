import React from 'react';
import { connect } from 'react-redux';
import _ from 'lodash';
import { getParamValues } from '../utils/functions';
import {
  initiateUsersData
} from '../actions/result';
import Loader from './Loader';

class RedirectPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: false,
    };
  }

  componentDidMount() {
    const { setExpiryTime, history, location } = this.props;

    try {
      if (_.isEmpty(location.hash)) {
        return history.push('/dashboard');
      }
      const access_token = getParamValues(location.hash);
      const expiryTime = new Date().getTime() + access_token.expires_in * 1000;
      localStorage.setItem('params', JSON.stringify(access_token));
      localStorage.setItem('expiry_time', expiryTime);
      setExpiryTime(expiryTime);
      this.setState({
        isLoading: true,
      })
      this.props.dispatch(initiateUsersData(access_token['access_token'])).then(() => {
        this.setState({
          isLoading: false,
        });
        window.location = '/music#dashboard';
      });
    } catch (error) {
      history.push('/home');
    }
  }

  render () {
    const { isLoading } = this.state;
    return (
      <Loader show={isLoading}>Loading...</Loader>
    );
  }
}

const mapStateToProps = (state) => {
  return state;
}

export default connect(mapStateToProps)(RedirectPage);