import React, { useState, useCallback, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import styles from './index.module.css';
import authService from '../api-authorization/AuthorizeService';
import fetchEstates from "../../utils/fetchEstates";
import SingleEstate from '../single-estate';

const RealEstates = (props) => {
    console.log("props", props);
    const [estates, setEstates] = useState([]);
    
    const getEstates = useCallback(async () => {
        const user = await authService.getUser();
        const userId = user['sub'];
        const searchId = props.match.params.id;
        const estates = await fetchEstates(searchId);
        console.log("estates", estates);
        setEstates(estates);
    
    }, [props.match.params.id]);

    const renderEstates = () => {
        return estates.map((estate) => {
            return (
                <SingleEstate key={estate.id} {...estate} />
            )
        })
    }

    useEffect(() => {
        getEstates()
    
}, [props.match.params.id, getEstates])


    return (
        <table>
            <thead>
                <tr>
                    <th>district</th>
                    <th>buildingType</th>
                    <th>size</th>
                    <th>floor</th>
                    <th>totalFloors</th>
                    <th>year</th>
                    <th>type</th>
                    <th>price</th>
                    <th>pic</th>
                </tr>
            </thead>

            <tbody>
                {renderEstates()}
            </tbody>
        </table>
    )
}


export default RealEstates