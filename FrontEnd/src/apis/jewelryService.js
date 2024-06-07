import axios from "axios"

const loginApi = (email, password) => {
    return axios.post("https://reqres.in/api/login", { email, password });
}
const fetchAllRing = () => {
    return axios.get("https://jssats.azurewebsites.net/api/product/getAll");
}
const fetchAllPromotion = () => {
    return axios.get("https://jssats.azurewebsites.net/api/promotion/getAll");
}
const fetchAllInvoice = () => {
    return axios.get("https://reqres.in/api/users?page=1");
}
const fetchAllCustomer = () => {
    return axios.get("https://jssats.azurewebsites.net/api/customer/getAll");
}
export {fetchAllRing,fetchAllPromotion,fetchAllInvoice,fetchAllCustomer,loginApi};