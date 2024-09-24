import styles from './TypeFilter.module.scss';
import { useContext, useEffect } from 'react';
import { SelectedTypeContext } from '../../../../contexts/SelectedTypeContext';

function TypeFilter() {
    const typeNames = ['fish', 'bug', 'fossil', 'work_of_art', 'underwater_creature', 'tree'];
    const typeLabels = {
        fish: 'Peces',
        bug: 'Insectos',
        fossil: 'Fósiles',
        work_of_art: 'Obras de arte',
        underwater_creature: 'Criaturas submarinas',
        tree: 'Árboles'
    };
    const { selectedType, setSelectedType } = useContext(SelectedTypeContext);

    const handleChangeType = (event) => {
        const value = event.target.value;
        if (event.target.checked) {
            setSelectedType([...selectedType, value]);
        } else {
            setSelectedType(selectedType.filter((type) => type !== value));
        }
    };

    useEffect(() => {
        console.log(selectedType);
    }, [selectedType]);

    return (
        <div className={styles.type_filter}>
            <h2>Tipos</h2>
            {typeNames.map((type) => (
                <div key={type}>
                    <input type="checkbox" id={type} name={type} value={type} onChange={handleChangeType} />
                    <label htmlFor={type}>{typeLabels[type]}</label>
                </div>
            ))}
        </div>
    );
}

export default TypeFilter;
