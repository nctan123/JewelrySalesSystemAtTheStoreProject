import axios from "axios";

const fetchAllRing = () => {
    return axios.get("https://reqres.in/api/users?page=1");
}

const loginApi = (email, password) => {
    return axios.post("https://reqres.in/api/login", { email, password });
}

export { fetchAllRing, loginApi };


