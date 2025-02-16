import axios from "axios";
import { config } from "../constants/AppConstants";

const token = localStorage.getItem("token");
const BASE_URL = config.url.API_URL;

axios.defaults.baseURL = BASE_URL;
axios.defaults.headers.common = { Authorization: `bearer ${token}` };

export default axios;
