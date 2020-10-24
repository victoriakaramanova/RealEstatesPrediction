import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from '../src/pages/home';
import LoadData from './components/loadData/index';
import GatherData from './components/gatherData/index';
import TrainModel from './components/TrainModel';
import PredictionPage from '../src/pages/prediction';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import RealEstates from './components/real-estates';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import ErrorCmp from './pages/error';

import './custom.css'
import SimilarOffersPage from './pages/similar-offers';

export default class App extends Component {
  static displayName = App.name;
        //<AuthorizeRoute path='/near/post/:id' component={RealEstates} />

        //<Route component={ErrorCmp} />

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/load-data' component={LoadData} />
        <AuthorizeRoute path='/gather-data' component={GatherData} />
        <AuthorizeRoute path='/train' component={TrainModel} />
        <AuthorizeRoute path='/prediction' component={PredictionPage} />
        <AuthorizeRoute path='/near/post/:id' component={SimilarOffersPage} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        

      </Layout>
    );
  }
}
