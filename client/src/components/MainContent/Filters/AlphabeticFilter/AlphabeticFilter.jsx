import styles from './AlphabeticFilter.module.scss';
import { useContext, useEffect } from 'react';
import { SelectedLetterContext } from '../../../../contexts/SelectedLetterContext';

function AlphabeticFilter() {
    const alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'.split('');

    const { selectedLetter, setSelectedLetter } = useContext(SelectedLetterContext);

    const handleChangeLetter = (event) => {
        const value = event.target.value;
        if (event.target.checked) {
            setSelectedLetter([...selectedLetter, value]);
        } else {
            setSelectedLetter(selectedLetter.filter((letter) => letter !== value));
        }
    };

    useEffect(() => {
        console.log(selectedLetter);
    }, [selectedLetter]);

    return (
        <div className={styles.alphabetic_filter}>
            <h2>Letra</h2>
            <div className={styles.letters}>
                {alphabet.map((letter) => (
                    <div key={letter}>
                        <input 
                            type="checkbox" 
                            id={letter} 
                            name={letter} 
                            value={letter} 
                            onChange={handleChangeLetter} 
                        />
                        <label htmlFor={letter}>{letter}</label>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default AlphabeticFilter;
