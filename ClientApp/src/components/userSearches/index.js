import React, { useState, useCallback, useEffect } from 'react';
import styles from './index.module.css';
import authService from '../api-authorization/AuthorizeService';
import fetchSearches from "../../utils/fetchSearches";
import Search from '../singleSearch';
import SingleEstate from '../single-estate';


const Searches = (props) => {
    const [searches, setSearches] = useState([]);

    const getSearches = useCallback(async () => {
        const user = await authService.getUser();
        const userId = user['sub'];
        const searches = await fetchSearches(userId);
        setSearches(searches)
    }, [props.updatedSearches]);

    const renderSearches = () => {
        return searches.map((search) => {
            return (
                <Search className={styles.colour} key={search.id} {...search} />
            )
        })
    }

    useEffect(() => {
        getSearches()
    }, [props.updatedSearches, getSearches])

    return (
        <table className='table table-striped table-bordered table-hover' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Квартал</th>
                    <th>Вид строителство</th>
                    <th>Квадратура</th>
                    <th>Етаж</th>
                    <th>Общо етажи</th>
                    <th>Година</th>
                    <th>Тип строителство</th>
                    <th>Прогнозна цена</th>
                    <th>Дата на търсене</th>
                    <th>Препратки</th>
                </tr>
            </thead>
            <tbody>
            {renderSearches()}

            </tbody>
        </table>
        
        )
}


export default Searches
