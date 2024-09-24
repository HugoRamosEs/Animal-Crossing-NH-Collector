import styles from './DataTable.module.scss';
import React, { useContext, useEffect, useState } from 'react';
import { SelectedTypeContext } from '../../../contexts/SelectedTypeContext';
import { SelectedLetterContext } from '../../../contexts/SelectedLetterContext';
import { SearchContext } from '../../../contexts/SearchContext';

const typeLabels = {
    fish: 'Peces',
    bug: 'Insectos',
    fossil: 'Fósiles',
    work_of_art: 'Obras de arte',
    underwater_creature: 'Criaturas submarinas',
    tree: 'Árboles'
};

const DataTable = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [expandedRow, setExpandedRow] = useState(null);
    const { selectedType } = useContext(SelectedTypeContext);
    const { selectedLetter } = useContext(SelectedLetterContext);
    const { searchTerm } = useContext(SearchContext);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch("/data/data.json");
                if (!response.ok) throw new Error('Network response was not ok');
                const result = await response.json();

                const updatedData = result.map(item => ({
                    ...item,
                    image: item.image || "img/no_asset.png",
                    parts: item.parts ? item.parts.map(part => ({
                        ...part,
                        part_image: part.part_image || "img/no_asset.png",
                        obtained: part.obtained || false
                    })) : []
                }));

                setData(updatedData);
            } catch (error) {
                setError(error);
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, []);

    const filteredData = data.filter(item =>
        (selectedType.length === 0 || selectedType.includes(item.type)) &&
        (selectedLetter.length === 0 || selectedLetter.some(letter => item.name.startsWith(letter))) &&
        (item.name.toLowerCase().includes(searchTerm.toLowerCase()))
    );

    const toggleRow = (index) => {
        setExpandedRow(expandedRow === index ? null : index);
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error loading data: {error.message}</p>;

    let expandableRowCount = 0;
    let nonExpandableRowCount = 0;

    return (
        <div className={styles.data_table}>
            <table>
                <thead>
                    <tr>
                        <th>Obtenido</th>
                        <th>Nombre</th>
                        <th>Tipo</th>
                        <th>Imagen</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredData.map((item, index) => {
                        const isExpandable = item.parts.length > 0;
                        const isOdd = isExpandable ? expandableRowCount % 2 === 0 : nonExpandableRowCount % 2 === 0;
                        const rowClass = isExpandable 
                            ? (isOdd ? styles.expandableRowEven : styles.expandableRowOdd) 
                            : (isOdd ? styles.nonExpandableRowEven : styles.nonExpandableRowOdd);

                        if (isExpandable) {
                            expandableRowCount++;
                        } else {
                            nonExpandableRowCount++;
                        }

                        return (
                            <React.Fragment key={index}>
                                <tr 
                                    onClick={() => isExpandable && toggleRow(index)} 
                                    className={rowClass}
                                >
                                    <td>
                                        <input type="checkbox" />
                                    </td>
                                    <td>{item.name}</td>
                                    <td>{typeLabels[item.type] || item.type}</td>
                                    <td>
                                        <img src={item.image} alt={item.name} />
                                    </td>
                                </tr>
                                {expandedRow === index && item.parts && item.parts.length > 0 && (
                                    <tr className={styles.subTableRow}>
                                        <td colSpan="4">
                                            <table className={styles.sub_table}>
                                                <thead>
                                                    <tr>
                                                        <th>Obtenido</th>
                                                        <th>Parte</th>
                                                        <th>Imagen</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {item.parts.map((part, partIndex) => (
                                                        <tr key={partIndex}>
                                                            <td>
                                                                <input 
                                                                    type="checkbox" 
                                                                />
                                                            </td>
                                                            <td>{part.part_name}</td>
                                                            <td>
                                                                <img src={part.part_image} alt={part.part_name} />
                                                            </td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                )}
                            </React.Fragment>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
};

export default DataTable;
