import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSpinner, faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { MdFace } from "react-icons/md";
import { MdFace4 } from "react-icons/md";
import { FaMoneyBillWave } from "react-icons/fa"; // cash
import vnPayLogo from '../../../assets/vnpay.jpg'
const DetailPage = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const searchParams = new URLSearchParams(location.search);
    const phone = searchParams.get('phone');
    const id = searchParams.get('id');

    const [customerData, setCustomerData] = useState(null);
    const [sellOrderData, setSellOrderData] = useState(null);

    useEffect(() => {
        const fetchCustomerData = async () => {
            try {
                const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/customer/getbyphone?phonenumber=${phone}`);
                if (res && res.data && res.data.data) {
                    setCustomerData(res.data.data[0]);
                }
                console.log('>>> check customer', res)
            } catch (error) {
                console.error('Error fetching customer details:', error);
            }
        };

        const fetchSellOrderData = async () => {
            try {
                const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/sellorder/getbyid?id=${id}`);
                if (res && res.data && res.data.data) {
                    setSellOrderData(res.data.data[0]);
                }
            } catch (error) {
                console.error('Error fetching sell order details:', error);
            }
        };

        if (phone && id) {
            fetchCustomerData();
            fetchSellOrderData();
        }
    }, [phone, id]);

    if (!customerData || !sellOrderData) {
        return (
            <div className="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50 backdrop-blur-sm">
                <FontAwesomeIcon
                    icon={faSpinner}
                    className="fa-spin fa-2x text-white"
                />
            </div>
        );
    }

    const formatCurrency = (value) => {
        return new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
            minimumFractionDigits: 0
        }).format(value);
    };

    const formatDateTime = (isoString) => {
        const date = new Date(isoString);

        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        const seconds = String(date.getSeconds()).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();

        return `${hours}:${minutes}:${seconds} ${day}/${month}/${year}`;
    };

    const handleBack = () => {
        navigate(-1); // Quay lại trang trước đó
    };

    return (
        <div className='min-h-screen bg-white mx-5 pt-5 rounded relative'>

            <div className="grid grid-cols-2 gap-4">
                {/* Sell Order Details */}
                <div className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                    <h2 className="text-lg font-bold mb-2">Sell Order</h2>
                    <p><strong>Id:</strong> {sellOrderData.id}</p>
                    <p><strong>Code:</strong> {sellOrderData.code}</p>
                    <p><strong>Staff:</strong> {sellOrderData.staffName}</p>
                    <p><strong>Time:</strong> {formatDateTime(sellOrderData.createDate)}</p>
                    <p><strong>Description:</strong> {sellOrderData.description}</p>
                    <p><strong>Total Amount:</strong> {formatCurrency(sellOrderData.finalAmount)}</p>
                    <p><strong>Status:</strong>
                        {sellOrderData.status === 'completed' ? (
                            <span className="text-green-500 bg-green-100 font-bold p-1 px-2 mx-2 rounded-xl">COMPLETED</span>
                        ) : sellOrderData.status === 'cancelled' ? (
                            <span className="text-red-500 bg-red-100 font-bold p-1 px-2 mx-2 rounded-xl">CANCELLED</span>
                        ) : sellOrderData.status === 'processing' ? (
                            <span className="text-yellow-600 bg-yellow-100 font-bold p-1 px-2 mx-2 rounded-xl">PROCESSING</span>
                        ) : sellOrderData.status === 'draft' ? (
                            <span className="text-black bg-gray-100 font-bold p-1 px-7 mx-2 rounded-xl">DRAFT</span>
                        ) : (
                            <span className="relative group text-blue-500 bg-blue-100 font-bold p-1 px-2 mx-2 rounded-xl">
                                WAITING...
                            </span>
                        )}
                    </p>
                    <p className="mb-4 flex items-center">
                        <strong className="mr-2">Payment Method:</strong>{sellOrderData.paymentMethod}
                        {sellOrderData.paymentMethod === 'VnPay' ? (
                            <img src={vnPayLogo} alt="VNPay Logo" className="w-5 h-auto mx-2" />
                        ) : sellOrderData.paymentMethod === 'Cash' ? (
                            <FaMoneyBillWave className="text-green-500 text-2xl mx-2" />
                        ) : (
                            'null'
                        )}
                    </p>
                </div>
                {/* Customer Details */}
                <div className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                    <h2 className="text-lg font-bold mb-2">Customer Details</h2>
                    <p><strong>Name:</strong> {customerData.firstname} {customerData.lastname}</p>
                    <p><strong>Phone:</strong> {customerData.phone}</p>
                    <p><strong>Email:</strong> {customerData.email}</p>
                    <p><strong>Gender:</strong>
                        {customerData.gender === 'Male'
                            ? <span className='text-blue-500 font-bold mx-2'>Male</span>
                            : <span className='text-pink-500 font-bold mx-2'>Female</span>
                        }
                    </p>
                    <p><strong>Address:</strong> {customerData.address}</p>
                </div>
            </div>
            <div className="w-[1200px] overflow-hidden ">
                <table className="font-inter w-full table-auto text-left">
                    <thead className="w-full rounded-lg bg-blue-900 text-base font-semibold text-white  sticky top-0">
                        <tr className="whitespace-nowrap text-xl  font-bold">
                            <th className="rounded-l-lg"></th>
                            <th className='py-2'>Product Code</th>
                            <th>Name</th>
                            <th>Unit Price</th>
                            <th>Promotion Rate</th>
                            <th className="rounded-r-lg">Quantity</th>
                        </tr>
                    </thead>
                    <tbody>
                        {sellOrderData.sellOrderDetails && sellOrderData.sellOrderDetails.map((item, index) => (
                            <tr key={index} className="cursor-pointer font-normal text-black bg-white shadow-md rounded font-bold text-base hover:shadow-2xl">
                                <td className="rounded-l-lg pr-3 pl-5 py-4 text-black ">{index + 1}</td>
                                <td>{item.productCode}</td>
                                <td>{item.productName}</td>
                                <td>{formatCurrency(item.unitPrice)}</td>
                                <td>{item.promotionRate || 'null'}</td>
                                <td className="rounded-r-lg">{item.quantity}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <button
                onClick={handleBack}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                <FontAwesomeIcon icon={faArrowLeft} className="mr-2" />
                Back
            </button>

        </div>
    );
};

export default DetailPage;
