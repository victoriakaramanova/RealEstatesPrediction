import React, { Component } from 'react';
import LoadData from '../../components/loadData';
import styles from './index.module.css';

export class Home extends Component {
    static displayName = Home.name;

    componentDidMount() {
        this.makeAPICall();
    }
    async makeAPICall() {
        const response = await fetch('api/Offers', {
            mode: 'cors'
        });
        const data = await response.json();
        console.log(data);
    }

    render() {
        return (
            <div className="container">
                <h1 id="tabelLabel">Научете цената на търсеното от Вас жилище</h1>
                <LoadData />
            </div>
        );
    }
}

