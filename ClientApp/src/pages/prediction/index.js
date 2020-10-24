import React, { useState, useEffect, Component } from 'react';
import { useHistory } from 'react-router-dom';
import Input from '../../components/input';
import SubmitButton from '../../components/button';
import styles from '../prediction/index.module.css';
import authService from '../../components/api-authorization/AuthorizeService'
import Searches from '../../components/userSearches';
import IntegerInput from '../../components/validation';


const PredictionPage = () => {
    const [buildingType, setBuildingType] = useState('1-СТАЕН');
    const [district, setDistrict] = useState('Банишора');
    const [floor, setFloor] = useState(1);
    const [totalFloors, setTotalFloors] = useState(1);
    const [size, setSize] = useState(10);
    const [year, setYear] = useState(2000);
    const [type, setType] = useState('Тухла');
    const [score, setScore] = useState('');
    const [updatedSearches, setUpdatedSearches] = useState([]);

    const [buildingTypes] = useState([
        { label: '1-СТАЕН', value: '1-СТАЕН' },
        { label: '2-СТАЕН', value: '2-СТАЕН' },
        { label: '3-СТАЕН', value: '3-СТАЕН' },
        { label: '4-СТАЕН', value: '4-СТАЕН' },
        { label: 'МНОГОСТАЕН', value: 'МНОГОСТАЕН' },
        { label: 'МЕЗОНЕТ', value: 'МЕЗОНЕТ' },
        { label: 'АТЕЛИЕ, ТАВАН', value: 'АТЕЛИЕ, ТАВАН' },
    ]);

    const [types] = useState([
        { label: 'Тухла', value: 'Тухла' },
        { label: 'ЕПК', value: 'ЕПК' },
        { label: 'Панел', value: 'Панел' },
        { label: 'Гредоред', value: 'Гредоред' },
        { label: 'ПК', value: 'ПК' },
    ]);

    const [districts] = useState([
        { label: 'Банишора', value: 'Банишора' },
        { label: 'Белите брези', value: 'Белите брези' },
        { label: 'Бенковски', value: 'Бенковски' },
        { label: 'Борово', value: 'Борово' },
        { label: 'Ботунец', value: 'Ботунец' },
        { label: 'Ботунец 2', value: 'Ботунец 2' },
        { label: 'Бояна', value: 'Бояна' },
        { label: 'Бъкстон', value: 'Бъкстон' },
        { label: 'Витоша', value: 'Витоша' },
        { label: 'Военна рампа', value: 'Военна рампа' },
        { label: 'Враждебна', value: 'Враждебна' },
        { label: 'Връбница 1', value: 'Връбница 1' },
        { label: 'Връбница 2', value: 'Връбница 2' },
        { label: 'Гевгелийски', value: 'Гевгелийски' },
        { label: 'Гео Милев', value: 'Гео Милев' },
        { label: 'Горна баня', value: 'Горна баня' },
        { label: 'Горубляне', value: 'Горубляне' },
        { label: 'Гоце Делчев', value: 'Гоце Делчев' },
        { label: 'Градина', value: 'Градина' },
        { label: 'Дианабад', value: 'Дианабад' },
        { label: 'Димитър Миленков', value: 'Димитър Миленков' },
        { label: 'Докторски паметник', value: 'Докторски паметник' },
        { label: 'Драгалевци', value: 'Драгалевци' },
        { label: 'Дружба 1', value: 'Дружба 1' },
        { label: 'Дружба 2', value: 'Дружба 2' },
        { label: 'Дървеница', value: 'Дървеница' },
        { label: 'Експериментален', value: 'Експериментален' },
        { label: 'Западен парк', value: 'Западен парк' },
        { label: 'Захарна фабрика', value: 'Захарна фабрика' },
        { label: 'Зона Б-18', value: 'Зона Б-18' },
        { label: 'Зона Б-19', value: 'Зона Б-19' },
        { label: 'Зона Б-5', value: 'Зона Б-5' },
        { label: 'Зона Б-5-3', value: 'Зона Б-5-3' },
        { label: 'Иван Вазов', value: 'Иван Вазов' },
        { label: 'Изгрев', value: 'Изгрев' },
        { label: 'Изток', value: 'Изток' },
        { label: 'Илинден', value: 'Илинден' },
        { label: 'Илиянци', value: 'Илиянци' },
        { label: 'Карпузица', value: 'Карпузица' },
        { label: 'Княжево', value: 'Княжево' },
        { label: 'Красна поляна 1', value: 'Красна поляна 1' },
        { label: 'Красна поляна 2', value: 'Красна поляна 2' },
        { label: 'Красна поляна 3', value: 'Красна поляна 3' },
        { label: 'Красно село', value: 'Красно село' },
        { label: 'Кремиковци', value: 'Кремиковци' },
        { label: 'Кръстова вада', value: 'Кръстова вада' },
        { label: 'Лагера', value: 'Лагера' },
        { label: 'Левски', value: 'Левски' },
        { label: 'Левски В', value: 'Левски В' },
        { label: 'Левски Г', value: 'Левски Г' },
        { label: 'Лозенец', value: 'Лозенец' },
        { label: 'Люлин - център', value: 'Люлин - център' },
        { label: 'Люлин 1', value: 'Люлин 1' },
        { label: 'Люлин 10', value: 'Люлин 10' },
        { label: 'Люлин 2', value: 'Люлин 2' },
        { label: 'Люлин 3', value: 'Люлин 3' },
        { label: 'Люлин 4', value: 'Люлин 4' },
        { label: 'Люлин 5', value: 'Люлин 5' },
        { label: 'Люлин 6', value: 'Люлин 6' },
        { label: 'Люлин 7', value: 'Люлин 7' },
        { label: 'Люлин 8', value: 'Люлин 8' },
        { label: 'Люлин 9', value: 'Люлин 9' },
        { label: 'Малашевци', value: 'Малашевци' },
        { label: 'Малинова долина', value: 'Малинова долина' },
        { label: 'Манастирски ливади', value: 'Манастирски ливади' },
        { label: 'Медицинска академия', value: 'Медицинска академия' },
        { label: 'Младост 1', value: 'Младост 1' },
        { label: 'Младост 1А', value: 'Младост 1А' },
        { label: 'Младост 2', value: 'Младост 2' },
        { label: 'Младост 3', value: 'Младост 3' },
        { label: 'Младост 4', value: 'Младост 4' },
        { label: 'Модерно предградие', value: 'Модерно предградие' },
        { label: 'Мусагеница', value: 'Мусагеница' },
        { label: 'Надежда 1', value: 'Надежда 1' },
        { label: 'Надежда 2', value: 'Надежда 2' },
        { label: 'Надежда 3', value: 'Надежда 3' },
        { label: 'Надежда 4', value: 'Надежда 4' },
        { label: 'Обеля', value: 'Обеля' },
        { label: 'Обеля 1', value: 'Обеля 1' },
        { label: 'Обеля 2', value: 'Обеля 2' },
        { label: 'Оборище', value: 'Оборище' },
        { label: 'Овча купел', value: 'Овча купел' },
        { label: 'Овча купел 1', value: 'Овча купел 1' },
        { label: 'Овча купел 2', value: 'Овча купел 2' },
        { label: 'Орландовци', value: 'Орландовци' },
        { label: 'Павлово', value: 'Павлово' },
        { label: 'ПЗ Хладилника', value: 'ПЗ Хладилника' },
        { label: 'Подуяне', value: 'Подуяне' },
        { label: 'Полигона', value: 'Полигона' },
        { label: 'Разсадника', value: 'Разсадника' },
        { label: 'Редута', value: 'Редута' },
        { label: 'Република', value: 'Република' },
        { label: 'Света Троица', value: 'Света Троица' },
        { label: 'Свобода', value: 'Свобода' },
        { label: 'Сердика', value: 'Сердика' },
        { label: 'Симеоново', value: 'Симеоново' },
        { label: 'Славия', value: 'Славия' },
        { label: 'Слатина', value: 'Слатина' },
        { label: 'Стрелбище', value: 'Стрелбище' },
        { label: 'Студентски град', value: 'Студентски град' },
        { label: 'Сухата река', value: 'Сухата река' },
        { label: 'Толстой', value: 'Толстой' },
        { label: 'Триъгълника', value: 'Триъгълника' },
        { label: 'Филиповци', value: 'Филиповци' },
        { label: 'Фондови жилища', value: 'Фондови жилища' },
        { label: 'Хаджи Димитър', value: 'Хаджи Димитър' },
        { label: 'Хиподрума', value: 'Хиподрума' },
        { label: 'Хладилника', value: 'Хладилника' },
        { label: 'Център', value: 'Център' },
        { label: 'Челопечене', value: 'Челопечене' },
        { label: 'Яворов', value: 'Яворов' },
        { label: 'в.з.Американски колеж', value: 'в.з.Американски колеж' },
        { label: 'в.з.Врана - Лозен', value: 'в.з.Врана - Лозен' },
        { label: 'в.з.Киноцентъра 3 част', value: 'в.з.Киноцентъра 3 част' },
        { label: 'в.з.Малинова долина', value: 'в.з.Малинова долина' },
        { label: 'в.з.Малинова долина - Герена', value: 'в.з.Малинова долина - Герена' },
        { label: 'в.з.Симеоново - Драгалевци', value: 'в.з.Симеоново - Драгалевци' },
        { label: 'гр. Банкя', value: 'гр. Банкя' },
        { label: 'гр. Нови Искър', value: 'гр. Нови Искър' },
        { label: 'ж.гр.Южен парк', value: 'ж.гр.Южен парк' },
        { label: 'м-т Гърдова глава', value: 'м-т Гърдова глава' },
        { label: 'м-т Детски град', value: 'м-т Детски град' },
        { label: 'м-т Камбаните', value: 'м-т Камбаните' },
        { label: 'м-т Киноцентъра', value: 'м-т Киноцентъра' },
        { label: 'с. Бистрица', value: 'с. Бистрица' },
        { label: 'с. Герман', value: 'с. Герман' },
        { label: 'с. Долни Пасарел', value: 'с. Долни Пасарел' },
        { label: 'с. Иваняне', value: 'с. Иваняне' },
        { label: 'с. Лозен', value: 'с. Лозен' },
        { label: 'с. Панчарево', value: 'с. Панчарево' },
        { label: 'с. Световрачене', value: 'с. Световрачене' },
    ]);

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
                'Authorization': !token ? {} : `Bearer ${token}`,
            }
        });
        if (!response.ok) {
            console.log("you didnt get prediction form res")
            //history.push('/error');
        } else {
            const prediction = await response.json();
            setBuildingType([prediction].map(({ name }) => ({ label: name, value: name })));
            setType([prediction].map(({ name }) => ({ label: name, value: name })));
            setDistrict([prediction].map(({ name }) => ({ label: name, value: name })));
            //event.compPredictChangeHandler = () => event.compPredictChangeHandler();
            setScore(prediction.score.toFixed(2));
            setUpdatedSearches(...updatedSearches, 1)

            //console.log(updatedSearches)
        }


        setBuildingType('1-СТАЕН');
        setType('Тухла');
        setDistrict('Банишора');
        setFloor(1);
        setTotalFloors(1);
        setSize(10);
        setYear(2000);

        //useEffect(() => {
        //    handleSubmit()
        //},[]);

        //value = { district }
        //onChange = { e => setDistrict(e.target.value)}
        //label = 'district'
        //id = 'district'
    }
    return (
        <div>
            <form className={styles.container} onSubmit={handleSubmit}>
                <select onChange={e => setBuildingType(e.currentTarget.value)}>
                    {buildingTypes.map(({ label, value }) => (
                        <option key={value} value={value}>
                            {label}
                        </option>
                        ))}
                </select>
                <div>
                <select onChange={e => setDistrict(e.currentTarget.value)}>
                    {districts.map(({ label, value }) => (
                        <option key={value} value={value}>
                            {label}
                        </option>
                    ))}
                    </select>
                </div>
                <div>
                <IntegerInput
                    value={parseInt(floor)} min={1} max={25}
                    label='етаж'
                    id='floor'
                    onChange={(floor) => setFloor(parseInt(floor))}
                    />
                    <span>етаж</span>
                </div>
                <div>
                <IntegerInput
                    value={parseInt(totalFloors)} min={1} max={25}
                    label='общо етажи'
                    id='totalFloors'
                    onChange={(totalFloors) => setTotalFloors(parseInt(totalFloors))}
                    />
                    <span>общо етажи</span>

                </div>
                <div>
                <IntegerInput
                    value={parseInt(size)} min={10} max={1000}
                    label='квадратура'
                    id='size'
                    onChange={(size) => setSize(parseInt(size))}
                    />
                    <span>квадратура</span>

                </div>
                <div>
                <IntegerInput
                    value={parseInt(year)} min={1870} max={2050}
                    label="година на строителство"
                    id='year'
                    onChange={(year) => setYear(parseInt(year))}
                    />
                    <span>година на строителство</span>
                </div>
                <select onChange={e => setType(e.currentTarget.value)}>
                    {types.map(({ label, value }) => (
                        <option key={value} value={value}>
                            {label}
                        </option>
                    ))}
                </select>

                <SubmitButton title='Прогнозирай' />
            </form>

            <div>Predicted price: {score} EUR</div>

            <Searches updatedSearches={updatedSearches} />
        </div>
    )
}

export default PredictionPage;
