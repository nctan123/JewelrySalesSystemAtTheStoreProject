import axios from "axios"

const loginApi = (email, password) => {
    return axios.post("https://reqresproject.in/api/login", { email, password });
}
const fetchAllRing = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllEarring = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllNecklace = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllBangles = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllWhGold = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllReGold = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllDiamond = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/diamond/getall");
}
const fetchAllPromotion = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/promotion/getAll");
}
const fetchAllInvoice = () => {
    return axios.get("https://reqres.in/api/users?page=1");
}
const fetchAllCustomer = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/customer/getAll");
}
export {fetchAllReGold,fetchAllWhGold,fetchAllRing,fetchAllEarring,fetchAllNecklace,fetchAllBangles,fetchAllPromotion,fetchAllInvoice,fetchAllCustomer,loginApi,fetchAllDiamond};