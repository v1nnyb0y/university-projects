import { Provider } from 'react-redux';
import store from './store/store';
import AppRouter from './router/AppRouter';
import React from 'react';

const Root = () => {
    return (
        <Provider store={store}>
            <AppRouter />
        </Provider>
    )
}

export default Root