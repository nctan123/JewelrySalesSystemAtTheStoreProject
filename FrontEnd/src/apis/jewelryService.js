import axios from "axios"

const fetchAllRing = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllEarring = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllNecklace = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllBangles = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllWhGold = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllReGold = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllDiamond = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/diamond/getall", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllPromotion = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/promotion/getAll", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}
const fetchAllInvoice = () => {
    return axios.get("https://reqres.in/api/users?page=1");
}
const fetchAllCustomer = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/customer/getAll", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}

export { fetchAllReGold, fetchAllWhGold, fetchAllRing, fetchAllEarring, fetchAllNecklace, fetchAllBangles, fetchAllPromotion, fetchAllInvoice, fetchAllCustomer, fetchAllDiamond };