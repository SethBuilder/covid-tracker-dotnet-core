import React, { Component } from 'react';
import ReactCountryFlag from "react-country-flag"
import CountrySelect from "./CountrySelect";
export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = {country: {label: "Jordan", code: "jo"}, forecasts: [], loading: true };
  }

  handleClick(country) {
    if(country) {
      this.setState({country: country});
      this.populateData(country);
    }

  }
  
  componentDidMount() {
    this.populateData(this.state.country);
  }

  renderForecastsTable(forecasts) {
    return (
      <div>
        <CountrySelect onChange={(country) => this.handleClick(country)} />
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Update Date</th>
              <th>Confirmed</th>
              <th>Recovered</th>
              <th>Deaths</th>
            </tr>
          </thead>
          <tbody>
            {forecasts.map(forecast =>
              <tr key={forecast.date}>
                <td>{forecast.date}</td>
                <td>{forecast.confirmed}</td>
                <td>{forecast.recovered}</td>
                <td>{forecast.deaths}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >            
          <ReactCountryFlag
                countryCode={this.state.country.code}
                svg
                style={{
                    width: '2em',
                    height: '2em',
                }}
                title={this.state.country.label}
            />
            &nbsp;
            {this.state.country.label}</h1>
        <p>Latest incoming reports (updated hourly)</p>
        {contents}
      </div>
    );
  }

  async populateData(country) {

    const response = await fetch("StatReport"+"/"+country.label, {
      headers: {
        'Content-Type': 'application/json'
      },
    });
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}

export default FetchData;
