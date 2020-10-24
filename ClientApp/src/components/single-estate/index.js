import React from 'react';
import styles from './index.module.css';
import { Link } from 'react-router-dom';

const SingleEstate = ({ district, buildingType, size, floor, totalFloors, year, type, price, url, pic, id }) => {
    return (
        <tr className={styles.container}>
            <td>{district}</td>
            <td>{buildingType}</td>
            <td>{size}</td>
            <td>{floor}</td>
            <td>{totalFloors}</td>
            <td>{year}</td>
            <td>{type}</td>
            <td>{price}</td>
            <td>
                <a href={url}>
                    <img className={styles['image']} src={pic} alt="pic" />
                </a>
            </td>
        </tr>
    )
}

export default SingleEstate;

