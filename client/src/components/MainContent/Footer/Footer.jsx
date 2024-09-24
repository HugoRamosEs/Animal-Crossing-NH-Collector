import styles from './Footer.module.scss'

function Footer() {
    return (
        <footer className={styles.footer}>
            <div className={styles.references_and_atributions}>
                <img src='logo_footer.png' alt='Furniture DIY Recipe Icon'/>
                <div>
                    <a href='https://creativecommons.org/licenses/by-nc-nd/4.0/deed.es' target='_blank'><img src='cc_by-nc-nd.png' alt='Attribution-NonCommercial-NoDerivatives 4.0 International' /></a>
                    <a href='https://nookipedia.com/wiki/Animal_Crossing:_New_Horizons/Gallery' target='_blank' className={styles.btn}>Nookpedia</a>
                </div>
            </div>
            <div className={styles.developed_by}>
                <h2><a href=''>Hugo Ramos</a> &copy; 2024</h2>
            </div>
        </footer>
    )
}

export default Footer
