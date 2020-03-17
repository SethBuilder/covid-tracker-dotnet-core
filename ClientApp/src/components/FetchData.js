import React, { Component } from 'react';
import ReactCountryFlag from "react-country-flag"
import CountrySelect from "./CountrySelect";
import FooterPage from "./FooterPage";
import publicIp from 'public-ip';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = {
      forecasts: {
        country:
        {
          code: "",
          label: ""
        }
      }, loading: true
    };

  }

  handleClick(country) {
    if (country) {
      this.setState({ country: country });
      this.populateData(country);
    }

  }

  componentDidMount() {
    this.populateData(this.state.forecasts.country);
  }

  renderForecastsTable(forecasts) {
    return (
      <div>
        <CountrySelect onChange={(country) => this.handleClick(country)} />
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Last Checked</th>
              <th>Confirmed</th>
              <th>Recovered</th>
              <th>Deaths</th>
            </tr>
          </thead>
          <tbody>
            {/* {forecasts.map(forecast => */}
            <tr key={forecasts.lastUpdate}>
              <td>{forecasts.lastUpdate}</td>
              <td>{forecasts.confirmed}</td>
              <td>{forecasts.recovered}</td>
              <td>{forecasts.deaths}</td>
            </tr>
            {/* )} */}
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
            countryCode={this.state.forecasts.country.code}
            svg
            style={{
              width: '2em',
              height: '2em',
            }}
            title={this.state.forecasts.country.label}
          />
            &nbsp;
            {this.state.forecasts.country.label}</h1>
        <p>Latest incoming data (updated hourly)</p>
        {contents}
        <FooterPage />
      </div>
    );
  }

  async populateData(country = { code: "", label: "" }) {
    let response = {};
    response = await fetch("StatReport?" + "selectedCountry=" + country.label+"&selectedCountryCode="+country.code + "&" + "ip=" + await publicIp.v4(), {
      headers: {
        'Content-Type': 'application/json'
      },
    });
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}

export default FetchData;
