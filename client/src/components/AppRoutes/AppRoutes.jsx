import MainContent from '../MainContent/MainContent';
import Login from '../Login/Login';
import Signup from '../Signup/Signup';
import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthContext } from '../../contexts/AuthContext';

function AppRoutes() {
    const { isAuthenticated } = useContext(AuthContext);

    return (
        <Routes>
            <Route path='/dashboard' element={ isAuthenticated ? <MainContent /> : <Navigate to="/login" /> }/>
            <Route path='/login' element={ !isAuthenticated ? <Login /> : <Navigate to="/dashboard" /> }/>
            <Route path='/signup' element={ !isAuthenticated ? <Signup /> : <Navigate to="/dashboard" /> }/>
            <Route path='*' element={ <Navigate to={ isAuthenticated ? "/dashboard" : "/login"} /> }/>
        </Routes>
    );
}

export default AppRoutes;
