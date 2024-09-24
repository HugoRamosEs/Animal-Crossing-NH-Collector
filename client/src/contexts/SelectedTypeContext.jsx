import PropTypes from 'prop-types';
import { createContext, useState } from 'react';

export const SelectedTypeContext = createContext(null);

export const SelectedTypeProvider = ({ children }) => {
    const [selectedType, setSelectedType] = useState([]);

    return (
        <SelectedTypeContext.Provider value={{ selectedType, setSelectedType }}>
            {children}
        </SelectedTypeContext.Provider>
    );
};

SelectedTypeProvider.propTypes = {
    children: PropTypes.node.isRequired,
};
