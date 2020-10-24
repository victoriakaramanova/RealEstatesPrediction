import React from 'react';
import {
    Link, useHistory
} from 'react-router-dom';
import styles from './index.module.css';
import LinkComponent from '../link';
import fetchEstates from '../../utils/fetchEstates';

const Search = ({ district, buildingType, size, floor, totalFloors, year, type, score, predictedAt, id }) => {
        const history = useHistory();
        const fDate = (inputFormat) => {
            const pad = (s) => { return (s < 10) ? '0' + s : s; }
            const d = new Date(inputFormat)
            return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('.')
    }

    return (
        <tr>
            <td>{district}</td>
            <td>{buildingType}</td>
            <td>{size}</td>
            <td>{floor}</td>
            <td>{totalFloors}</td>
            <td>{year}</td>
            <td>{type}</td>
            <td>{score}</td>
            <td>{fDate(predictedAt)}</td>
            <td><Link to={`/near/post/${id}`}>
                Реални оферти 
                </Link>
            </td>
        </tr>
        )
}

export default Search;
