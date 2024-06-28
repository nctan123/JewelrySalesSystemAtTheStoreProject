import axios from "axios"


const loginApi = (email, password) => {
    return axios.post("https://reqresproject.in/api/login", { email, password });
}
const fetchAllCustomer = (page) => {
    return axios.get(`https://jssatsproject.azurewebsites.net/api/Customer/GetAll?pageIndex=${page}`);
}
const fetchAllRing = (page) => {
    return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=1&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllEarring = (page) => {
     return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=2&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllBangles = (page) => {
     return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=3&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllNecklace = (page) => {
    return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=4&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllReGold = (page) => {
     return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=5&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllWhGold = (page) => {
     return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=6&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllDiamond = (page) => {
     return axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall01?categoryID=7&pageIndex=${page}&pageSize=12&ascending=true`);
}
const fetchAllDiamondTest = () => {
    // return axios.get("https://jssatsproject.azurewebsites.net/api/Diamond/GetByCode?code=DIA001");
}
const fetchAllPromotion = () => {
    // return axios.get("https://jssatsproject.azurewebsites.net/api/Promotion/GetAll");

}
const fetchAllInvoice = () => {
    return axios.get("https://reqres.in/api/users?page=1");
}
const fetchAllListOrder = () => {
    // return axios.get("https://jssatsproject.azurewebsites.net/api/SellOrder/getall");
}
const fetchAllProduct = () => {
    // return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchPaymentMethod = () =>{
    return axios.get("https://jssatsproject.azurewebsites.net/api/PaymentMethod/Getall");
}
export {fetchPaymentMethod,fetchAllProduct,fetchAllDiamondTest,fetchAllReGold,fetchAllWhGold,fetchAllRing,fetchAllEarring,fetchAllNecklace,fetchAllBangles,fetchAllPromotion,fetchAllInvoice,fetchAllCustomer,loginApi,fetchAllDiamond,fetchAllListOrder};

