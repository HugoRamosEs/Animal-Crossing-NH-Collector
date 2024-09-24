import styles from './SearchFilter.module.scss';
import { useContext } from 'react';
import { SearchContext } from '../../../../contexts/SearchContext';

function SearchFilter() {
    const { searchTerm, setSearchTerm } = useContext(SearchContext);

    const handleChange = (e) => {
        setSearchTerm(e.target.value);
    };

    return (
        <div className={styles.search_filter}>
            <h2>Buscar</h2>
            <input
                type="text"
                value={searchTerm}
                onChange={handleChange}
                placeholder="Buscar..."
            />
        </div>
       
    );
}

export default SearchFilter;
