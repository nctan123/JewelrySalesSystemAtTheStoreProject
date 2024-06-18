import axios from "axios"

<<<<<<< HEAD
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
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
const fetchAllDiamondTest = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/Diamond/GetByCode?code=DIA001");
}
const fetchAllPromotion = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/Promotion/GetAll");
=======
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
>>>>>>> develop
}
const fetchAllInvoice = () => {
    return axios.get("https://reqres.in/api/users?page=1");
}
const fetchAllCustomer = () => {
<<<<<<< HEAD
    return axios.get("https://jssatsproject.azurewebsites.net/api/customer/getAll");
}
const fetchAllListOrder = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/SellOrder/getall");
}
const fetchAllProduct = () => {
    return axios.get("https://jssatsproject.azurewebsites.net/api/product/getall");
}
export {fetchAllProduct,fetchAllDiamondTest,fetchAllReGold,fetchAllWhGold,fetchAllRing,fetchAllEarring,fetchAllNecklace,fetchAllBangles,fetchAllPromotion,fetchAllInvoice,fetchAllCustomer,loginApi,fetchAllDiamond,fetchAllListOrder};
=======
    return axios.get("https://jssatsproject.azurewebsites.net/api/customer/getAll", {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
        }
    });
}

export { fetchAllReGold, fetchAllWhGold, fetchAllRing, fetchAllEarring, fetchAllNecklace, fetchAllBangles, fetchAllPromotion, fetchAllInvoice, fetchAllCustomer, fetchAllDiamond };
>>>>>>> develop
