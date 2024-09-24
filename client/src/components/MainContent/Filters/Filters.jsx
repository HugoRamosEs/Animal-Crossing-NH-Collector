import styles from './Filters.module.scss'
import TypeFilter from './TypeFilter/TypeFilter';
import AlphabeticFilter from './AlphabeticFilter/AlphabeticFilter';
import SearchFilter from './SearchFilter/SearchFilter';

function Filters() {
    return (
        <aside className={styles.filters}>
            <TypeFilter />
            <AlphabeticFilter />
            <SearchFilter />
        </aside>
    )
}

export default Filters
