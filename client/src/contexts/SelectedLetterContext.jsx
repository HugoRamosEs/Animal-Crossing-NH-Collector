import PropTypes from 'prop-types';
import { createContext, useState } from 'react';

export const SelectedLetterContext = createContext(null);

export const SelectedLetterProvider = ({ children }) => {
    const [selectedLetter, setSelectedLetter] = useState([]);

    return (
        <SelectedLetterContext.Provider value={{ selectedLetter, setSelectedLetter }}>
            {children}
        </SelectedLetterContext.Provider>
    );
};

SelectedLetterProvider.propTypes = {
    children: PropTypes.node.isRequired,
};
