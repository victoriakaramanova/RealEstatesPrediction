import React, { useState, useEffect } from 'react';
import authService from '../api-authorization/AuthorizeService';
import Input from '../input';
import styles from './index.module.css';
import SubmitButton from '../button';
//import Searches from '../userSearches';
import { Container } from 'reactstrap';

const Predict = () => {
    const [buildingType, setBuildingType] = useState('');
    const [district, setDistrict] = useState('');
    const [floor, setFloor] = useState('');
    const [totalFloors, setTotalFloors] = useState('');
    const [size, setSize] = useState('');
    const [year, setYear] = useState('');
    const [type, setType] = useState('');
    const [score, setScore] = useState('');
    //const [updatedSearches, setUpdatedSearches] = useState([]);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const user = await authService.getUser();
        const userId = user['sub'];
        const token = await authService.getAccessToken();
        const response = await fetch('api/Prediction', {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify({
                buildingType,
                district,
                floor,
                totalFloors,
                size,
                year,
                type,
                userId
            }),
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'Authorization': !token ? {} :`Bearer ${token}`,
            }
        });
        if (!response.ok) {
            console.log("you didnt get prediction form res")
            //history.push('/error');
        } else {
            const prediction = await response.json();
            event.compPredictChangeHandler = () => event.compPredictChangeHandler();
            setScore(prediction.score.toFixed(2));

            //event.compPredictChangeHandler = event.compPredictChangeHandler();

            //setUpdatedSearches(...updatedSearches, 1)
            //console.log(updatedSearches)
        }

        setBuildingType('');
        setType('');
        setDistrict('');
        setFloor('');
        setTotalFloors('');
        setSize('');
        setYear('');

        //useEffect(() => {
        //    handleSubmit()
        //},[]);
    }

    return (
      <div>
        <form className={styles.container} onSubmit={handleSubmit}>
            <Input
                value={buildingType}
                onChange={e => setBuildingType(e.target.value)}
                label='buildingType'
                id='buildingType'
            />
            <Input
                value={district}
                onChange={e => setDistrict(e.target.value)}
                label='district'
                id='district'
            />
            <Input
                value={floor}
                onChange={e => setFloor(e.target.value)}
                label='floor'
                id='floor'
            />
            <Input
                value={totalFloors}
                onChange={e => setTotalFloors(e.target.value)}
                label='totalFloors'
                id='totalFloors'
            />
            <Input
                value={size}
                onChange={e => setSize(e.target.value)}
                label='size'
                id='size'
            />
            <Input
                value={year}
                onChange={e => setYear(e.target.value)}
                label='year'
                id='year'
            />
            <Input
                value={type}
                onChange={e => setType(e.target.value)}
                label='type'
                id='type'
                />
                <SubmitButton>'Прогнозна цена' </SubmitButton>
        </form>

            <div>Predicted price: {score} EUR</div>

        </div>     
        )
}

export default Predict;
