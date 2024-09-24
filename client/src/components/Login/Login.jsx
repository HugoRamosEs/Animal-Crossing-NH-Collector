import styles from './Login.module.scss';
import { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { AuthContext } from '../../contexts/AuthContext';

function Login() {
    const [errors, setErrors] = useState([]);
    const navigate = useNavigate();
    const { setIsAuthenticated } = useContext(AuthContext);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrors([]);

        const email = e.target.email.value;
        const password = e.target.password.value;

        try {
            const response = await fetch('https://localhost:7180/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
                credentials: 'include'
            });

            const result = await response.json();
            console.log(result);

            if (!response.ok) {
                if (typeof result.errors === 'object') {
                    const errorMessages = Object.values(result.errors).flat();
                    setErrors(errorMessages);
                } else if (result.ErrorCode) {
                    setErrors([result.message]);
                } else {
                    setErrors(['Ocurrió un error desconocido.']);
                }
            } else {
                if (result.data) {
                    localStorage.setItem('userInfo', JSON.stringify(result.data));
                    setIsAuthenticated(true);
                    navigate('/dashboard');
                }
            }
        } catch (error) {
            setErrors(['Ocurrió un error al conectar con el servidor.']);
            console.error('Error connecting to the server:', error);
        }
    };

    return (
        <div className={styles.login}>
            <header>
                <h1>Animal Crossing New Horizons - Collector</h1>
            </header>
            <main>
                <form onSubmit={handleSubmit}>
                    <h2>Iniciar Sesión</h2>
                    <fieldset>
                        <label htmlFor="email">Correo Electrónico</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            placeholder="Escribe tu correo electrónico..."
                        />

                        <label htmlFor="password">Contraseña</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            placeholder="Escribe tu contraseña..."
                        />

                        <button type="submit">Iniciar sesión</button>

                        {errors.length > 0 && (
                            <div className={styles.errorContainer}>
                                {errors.map((error, index) => (
                                    <p key={index} className={styles.error}>{error}</p>
                                ))}
                            </div>
                        )}
                    </fieldset>
                    <p>¿No tienes cuenta? <Link to='/signup'>Regístrate</Link></p>
                </form>
            </main>
            <footer>
                <h2><a href=''>Hugo Ramos</a> &copy; 2024</h2>
            </footer>
        </div>
    );
}

export default Login;
