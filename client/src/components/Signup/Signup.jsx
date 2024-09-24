import styles from './Signup.module.scss';
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';

function Signup() {
    const [errors, setErrors] = useState([]);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrors([]);

        const username = e.target.username.value;
        const email = e.target.email.value;
        const password = e.target.password.value;
        const repeatedPassword = e.target.repeatedPassword.value;

        try {
            const response = await fetch('https://localhost:7180/api/auth/signup', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, email, password, repeatedPassword }),
                credentials: 'include'
            });

            const result = await response.json();

            if (!response.ok) {
                if (typeof result.errors === 'object') {
                    const errorMessages = Object.values(result.errors).flat();
                    setErrors(errorMessages);
                } else if (result.message) {
                    setErrors([result.message]);
                } else {
                    setErrors(['Ocurrió un error desconocido.']);
                }
            } else {
                navigate('/login');
            }
        } catch (error) {
            setErrors(['Ocurrió un error al conectar con el servidor.']);
            console.error('Error connecting to the server:', error);
        }
    };

    return (
        <div className={styles.signup}>
            <header>
                <h1>Animal Crossing New Horizons - Collector</h1>
            </header>
            <main>
                <form onSubmit={handleSubmit}>
                    <h2>Registrarse</h2>
                    <fieldset>
                        <label htmlFor="email">Nombre de usuario</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            placeholder='Escribe tu nombre de usuario...'
                        />

                        <label htmlFor="email">Correo Electrónico</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            placeholder='Escribe tu correo electrónico...'
                        />

                        <label htmlFor="password">Contraseña</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            placeholder="Escribe tu contraseña..."
                        />

                        <label htmlFor="rpassword">Repetir Contraseña</label>
                        <input
                            type="password"
                            id="repeatedPassword"
                            name="repeatedPassword"
                            placeholder="Repite la contraseña..."
                        />

                        <button type="submit">Registrarse</button>

                        {errors.length > 0 && (
                            <div className={styles.errorContainer}>
                                {errors.map((error, index) => (
                                    <p key={index} className={styles.error}>{error}</p>
                                ))}
                            </div>
                        )}
                    </fieldset>
                    <p>¿Ya tienes cuenta? <Link to='/login'>Inicia sesión</Link></p>
                </form>
            </main>
            <footer>
                <h2><a href=''>Hugo Ramos</a> &copy; 2024</h2>
            </footer>
        </div>
    );
}

export default Signup;
