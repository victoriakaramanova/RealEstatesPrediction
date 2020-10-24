import React, { Component } from 'react';
import authService from '../api-authorization/AuthorizeService'
import styles from './index.module.css';

class GatherData extends Component {
    static displayName = GatherData.name;

    constructor(props) {
        super(props);
        this.state = { realEstates: [], loading: true };
    }

    componentDidMount() {
        this.populateRealEstates();
    }

    static renderRealEstatesTable(realEstates) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>district</th>
                        <th>buildingType</th>
                        <th>type</th>
                        <th>price</th>
                        <th>pic</th>
                    </tr>
                </thead>
                <tbody>
                    {realEstates.map(realEstate =>
                        <tr key={realEstate.id}>
                            <td>{realEstate.district}</td>
                            <td>{realEstate.buildingType}</td>
                            <td>{realEstate.type}</td>
                            <td>{realEstate.price}</td>
                            <td> <a href={realEstate.url} >
                                <img src={realEstate.pic} alt="pic" className={styles['offer-url']} />
                            </a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : GatherData.renderRealEstatesTable(this.state.realEstates);

        return (
            <div>
                <h1 id="tabelLabel" >Latest offers</h1>
                {contents}
            </div>
        );
    }

    async populateRealEstates() {
        const token = await authService.getAccessToken();
        //console.log(token);
        const response = await fetch('gatherdata', {
            mode: 'cors',
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        //console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ realEstates: data, loading: false });
    }
}

export default GatherData;
