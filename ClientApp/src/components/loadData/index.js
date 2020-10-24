import React, { Component } from 'react';
import authService from '../api-authorization/AuthorizeService'
import Slider from 'infinite-react-carousel';
import ReactDOM from 'react-dom';
//import Carousel, { autoplayPlugin } from '@brainhubeu/react-carousel';
//import '@brainhubeu/react-carousel/lib/style.css';
//import ReactInfiniteImageLinkCorousal from 'react-infinite-image-link-carousel'
import styles from './index.module.css';

//require('react-carousel/lib/carousel.css');

class LoadData extends Component {
    static displayName = LoadData.name;

  constructor(props) {
    super(props);
    this.state = { offers: [], loading: true };
  }

  componentDidMount() {
    this.populateOffers();
  }

    static renderOffersTable(offers) {
        return (
            <Slider
                autoplay
                arrows={false}
                centerPadding={0}
                dots
                adaptiveHeight
                centerMode={true} >
            {offers.map(offer =>
        <a href={offer.url} key={offer.id}>
            <img src={offer.pic} alt={offer.type} />
        </a>

    )}
</Slider> 
               
           
                    
            
    )
    
  }

  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : LoadData.renderOffersTable(this.state.offers);

    return (
      <div>
        
        {contents}
      </div>
    );
  }

  async populateOffers() {
      const token = await authService.getAccessToken();
      const response = await fetch('api/Offers', {
          mode: 'cors',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
      });
      const data = await response.json();
      console.log(data);
      console.log("populateOffers",data);
      this.setState({ offers: data, loading: false });
  }
}

export default LoadData;

