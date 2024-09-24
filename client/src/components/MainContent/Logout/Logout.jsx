import styles from './Logout.module.scss';
import { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../../../contexts/AuthContext';

function Logout() {
    const [username, setUsername] = useState('');
    const [errors, setErrors] = useState([]);
    const navigate = useNavigate();
    const { setIsAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        const userInfo = localStorage.getItem('userInfo');

        if (userInfo) {
            try {
                const parsedData = JSON.parse(userInfo);
                if (parsedData && parsedData.userName) {
                    setUsername(parsedData.userName);
                }
            } catch (error) {
                console.error('Error parsing userInfo from localStorage:', error);
            }
        }
    }, []);

    const handleLogout = async (e) => {
        e.preventDefault();
        setErrors([]);

        try {
            const response = await fetch('https://localhost:7180/api/auth/logout', {
                method: 'POST',
                credentials: 'include'
            });

            const result = await response.json();

            if (!response.ok) {
                setErrors([result.message || 'Ocurrió un error desconocido.']);
            } else {
                localStorage.removeItem('userInfo');
                setIsAuthenticated(false);
                navigate('/login');
            }
        } catch (error) {
            setErrors(['Ocurrió un error al conectar con el servidor.']);
            console.error('Error connecting to the server:', error);
        } 
    };

    return (
        <div className={styles.logout}>
            <img src="logo.png" alt="Animal Crossing Logo" />
            <div className={styles.data}>
                <p>{username}</p>
                <button className="button" onClick={handleLogout}>Cerrar sesión</button>
            </div>
            {errors.length > 0 && (
                <div className={styles.errors}>
                    {errors.map((error, index) => (
                        <p key={index} className={styles.error}>{error}</p>
                    ))}
                </div>
            )}
        </div>
    );
}

export default Logout;
