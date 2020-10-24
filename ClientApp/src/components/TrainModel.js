import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

class TrainModel extends Component {
    static displayName = TrainModel.name;

    constructor(props) {
        super(props);
        this.state = { status: '', loading: true };
    }

    componentDidMount() {
        this.result();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : TrainModel.result;

        return (
            <div>
                <h1>Тренирането на модела завърши със статус {this.state.status}</h1>
                {contents}
            </div>
        );
    }

    async result() {
        const token = await authService.getAccessToken();
        //console.log(token);
        const response = await fetch('train', {
            mode: 'cors',
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        //console.log(response.status);
        const data = await response.status;//.json();
        console.log(data);
        this.setState({ status: data, loading: false });
    }
}

export default TrainModel;
