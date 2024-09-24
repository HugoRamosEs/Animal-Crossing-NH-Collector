import styles from './MainContent.module.scss';
import Header from './Header/Header';
import Filters from './Filters/Filters';
import DataTable from './DataTable/DataTable';
import Footer from './Footer/Footer';
import Logout from './Logout/Logout';

function MainContent() {
    return (
        <div className={`${styles.main_content} container`}>
            <Logout />
            <Header />
            <Filters />
            <DataTable />
            <Footer />
        </div>
    );
}

export default MainContent;
