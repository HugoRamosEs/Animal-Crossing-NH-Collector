import './App.scss';
import AppRoutes from './components/AppRoutes/AppRoutes';
import { BrowserRouter as Router } from 'react-router-dom';
import { SearchProvider } from './contexts/SearchContext';
import { SelectedLetterProvider } from './contexts/SelectedLetterContext';
import { SelectedTypeProvider } from './contexts/SelectedTypeContext';
import { AuthProvider } from './contexts/AuthContext';

function App() {
    return (
        <Router>
            <AuthProvider>
                <SearchProvider>
                    <SelectedLetterProvider>
                        <SelectedTypeProvider>
                            <div className='content'>
                                <AppRoutes />
                            </div>
                        </SelectedTypeProvider>
                    </SelectedLetterProvider>
                </SearchProvider>
            </AuthProvider>
        </Router>
    );
}

export default App;
