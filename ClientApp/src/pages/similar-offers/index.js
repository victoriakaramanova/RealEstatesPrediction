import React, { useState, useCallback, useEffect, useMatch } from 'react';
import Search from '../../components/singleSearch';
import SingleEstate from '../../components/single-estate';
import RealEstates from '../../components/real-estates';
import fetchSearch from '../../utils/fetchSearch';
import fetchEstates from "../../utils/fetchEstates";
import styles from './index.module.css';


const SimilarOffersPage = (props) => {
    console.log('simPageprops', props)
    //const match = useMatch();
    console.log("params.id", props.match.params.id);
    const [search, setSearch] = useState(null);
    const [estates, setEstates] = useState([]);

    const getSearch = useCallback(async () => {
        const searchId = props.match.params.id//props.match.params.id;
        const search = await fetchSearch(searchId);
        console.log('search', search)
        setSearch(search);
    }, [props.match.params.id]);

    const getEstates = useCallback(async () => {
        const searchId = props.match.params.id;
        const estates = await fetchEstates(searchId);
        setEstates(estates);
    }, [props.match.params.id])

    //const renderSearch = () => {
    //    return <Search {...search} />
    //}

    const renderEstates = () => {
        return estates.map((estate) => {
            return (
                <SingleEstate key={estate.id} {...estate} />
            )
        })
    }

    useEffect(() => {
        getSearch()
    }, [props.match.params.id, getSearch])

    useEffect(() => {
        getEstates()

    }, [props.match.params.id, getEstates])

    return (
        <div>
            <table className="table table-light table-hover table-sm">
                <thead>
                    <tr>
                    <th>Квартал</th>
                    <th>Вид</th>
                    <th>Квадратура</th>
                    <th>Етаж</th>
                    <th>Общо етажи</th>
                    <th>Година</th>
                    <th>Тип строителство</th>
                    <th>Прогнозна цена</th>
                    <th>От дата</th>
                    </tr>
                
                    <Search className={styles.search}
                        {...search} />
                    
                </thead>
                <tbody>
                    <tr><td className={styles.announce} colSpan="10">РЕАЛНИ ПРЕДЛОЖЕНИЯ</td></tr>
                    {renderEstates()}
                </tbody>
            </table>
        </div>
        )
}

//<thead>
//    <tr>
//        <th>district</th>
//        <th>buildingType</th>
//        <th>size</th>
//        <th>floor</th>
//        <th>totalFloors</th>
//        <th>year</th>
//        <th>type</th>
//        <th>price</th>
//        <th>pic</th>
//    </tr>
//</thead>

//district = { search.district }
//buildingType = { search.buildingType }
//size = { search.size }
//floor = { search.floor }
//totalFloors = { search.totalFloors }
//year = { search.year }
//type = { search.type }
//score = { search.score }

export default SimilarOffersPage;
