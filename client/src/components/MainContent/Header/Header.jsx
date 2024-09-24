import styles from './Header.module.scss'

function Header() {
    return (
        <>
            <header className={`${styles.header} 'container'`}>
                <h1>Animal Crossing New Horizons - Collector</h1>
            </header>
        </>
    )
}

export default Header
