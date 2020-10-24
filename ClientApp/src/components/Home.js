import React, { Component } from 'react';
import LoadData from './loadData/index';

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

  render () {
    return (
        <div>
            <LoadData />
      </div>
    );
  }
}
