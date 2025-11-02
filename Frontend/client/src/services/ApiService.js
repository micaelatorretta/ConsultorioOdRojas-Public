import axios from 'axios';
/**
 * Servicio genérico para realizar peticiones HTTP.
 */
class ApiService {
  constructor(baseURL) {
    this.api = axios.create({
      baseURL,
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  async get(url, config = {}) {
    const response = await this.api.get(url, config);
    return response.data;
  }

  async post(url, data, config = {}) {
    const response = await this.api.post(url, data, config);
    return response.data;
  }

  async put(url, data, config = {}) {
    const response = await this.api.put(url, data, config);
    return response.data;
  }

  async delete(url, config = {}) {
    const response = await this.api.delete(url, config);
    return response.data;
  }
}

/* ver si http o https*/
export default new ApiService('https://localhost:7287');
