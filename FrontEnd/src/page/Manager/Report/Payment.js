import React, { useEffect, useState, useRef } from 'react'
import clsx from 'clsx'
import { IoIosSearch } from "react-icons/io";
import axios from "axios";
import Modal from 'react-modal';
import { CiViewList } from "react-icons/ci";
const Payment = () => {
    const scrollRef = useRef(null);

    const [listPayment, setListPayment] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedOrder, setSelectedOrder] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [totalPages, setTotalPages] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const paymentPerPageOptions = [10, 15, 20, 25, 30, 35, 40, 45, 50];
    const [searchQuery1, setSearchQuery1] = useState(''); // when click icon => search, if not click => not search
    const [ascending, setAscending] = useState(true);

    useEffect(() => {
        if (scrollRef.current) {
            scrollRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [currentPage]);

    useEffect(() => {
        if (searchQuery) {

            handleSearch();

        } else {

            getPayment();

        }
    }, [pageSize, currentPage, searchQuery, ascending]);

    const handleDetailClick = async (id) => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/sellorder/getbyid?id=${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            // console.log('check detail click', res)
            if (res && res.data) {
                const details = res.data.data[0];
                // console.log('check detail click', res.data.data[0].paymentDetails)
                setSelectedOrder(details);
                setIsModalOpen(true); // Open modal when staff details are fetched
            }
        } catch (error) {
            console.error('Error fetching staff details:', error);
        }
    };

    const getPayment = async () => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/Payment/getall?pageIndex=${currentPage}&pageSize=${pageSize}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (res && res.data && res.data.data) {
                const allPayments = res.data.data;
                setListPayment(allPayments);
                setTotalPages(res.data.totalPages);
            }
        } catch (error) {
            console.error('Error fetching staffs:', error);
            if (error.response) {
                console.error('Error response:', error.response.data);
            } else if (error.request) {
                console.error('Error request:', error.request);
            } else {
                console.error('Error message:', error.message);
            }
        }
    };

    const formatCurrency = (value) => {
        return new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
            minimumFractionDigits: 0
        }).format(value);
    };

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    const handleSearchChange = (e) => {
        setSearchQuery1(e.target.value);
    };
    const handleSetQuery = async () => {
        setSearchQuery(searchQuery1)
        setCurrentPage(1);
    }

    const handleSearch = async () => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error('No token found');
            }
            const res = await axios.get(
                `https://jssatsproject.azurewebsites.net/api/sellorder/search?statusList=completed&customerPhone=${searchQuery}&ascending=${ascending}&pageIndex=${currentPage}&pageSize=${pageSize}`,
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            if (res && res.data && res.data.data) {
                const searched = res.data.data;
                setListPayment(searched);
                setTotalPages(res.data.totalPages);
                console.log('>>> check search', res)
            }
            else {
                setListPayment([]);
                setTotalPages(0);
            }
        } catch (error) {
            console.error('Error fetching search results:', error);
            if (error.response) {
                console.error('Error response:', error.response.data);
            } else if (error.request) {
                console.error('Error request:', error.request);
            } else {
                console.error('Error message:', error.message);
            }
        }
        // setSearchQuery1('');
    };

    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedOrder(null)

    };
    const handleSort = () => {
        setAscending(!ascending); // Toggle ascending state
        setCurrentPage(1); // Reset to first page when sorting changes
    };
    const formatDateTime = (isoString) => {
        const date = new Date(isoString);

        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        const seconds = String(date.getSeconds()).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
        const year = date.getFullYear();

        return `${hours}:${minutes}:${seconds} ${day}/${month}/${year}`;
    };
    const placeholders = Array.from({ length: pageSize - listPayment.length });

    return (
        <div className="flex items-center justify-center min-h-screen bg-white mx-5 pt-5 mb-5 rounded">
            <div>
                <h1 ref={scrollRef} className="text-3xl font-bold text-center text-blue-800 mb-4">List of payment</h1>
                <div className="flex justify-between mb-4">
                    <div className="flex items-center ml-2">
                        <label className="block mb-1 mr-2">Page Size:</label>
                        <select
                            value={pageSize}
                            onChange={(e) => {
                                setPageSize(parseInt(e.target.value));
                                setCurrentPage(1); // Reset to first page when page size changes
                            }}
                            className="px-3 py-2 border border-gray-300 rounded-md"
                        >
                            {paymentPerPageOptions.map((size) => (
                                <option key={size} value={size}>{size}</option>
                            ))}
                        </select>
                    </div>
                    {/* <div className="relative w-[400px]">
                        <input
                            type="text"
                            placeholder="Search by product code"
                            value={searchQuery1}
                            onChange={handleSearchChange}
                            className="px-3 py-2 border border-gray-300 rounded-md w-full"
                        />
                        <IoIosSearch className="absolute top-0 right-0 mr-3 mt-3 cursor-pointer text-gray-500" onClick={handleSetQuery} />
                    </div> */}
                </div>
                <div className="w-[1200px] overflow-hidden ">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-sky-300 text-base font-semibold text-white sticky top-0">
                            <tr className="whitespace-nowrap text-xl font-bold text-[#212B36] ">
                                <th className=" rounded-l-lg"></th>
                                <th className="py-3 pl-3">Order Code</th>
                                <th >Customer</th>
                                <th >Phone</th>
                                <th >Time</th>
                                <th >Total</th>
                                <th >Method</th>
                                <th >Status</th>
                                <th className=" rounded-r-lg ">Action</th>
                            </tr>
                        </thead>

                        <tbody >
                            {listPayment.map((item, index) => (
                                <tr key={index} className="cursor-pointer font-normal text-[#637381] bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] text-base hover:shadow-2xl">
                                    <td className="rounded-l-lg pr-3 pl-5 py-4 text-black">{index + (currentPage - 1) * pageSize + 1}</td>
                                    <td >{item.orderCode}</td>

                                    <td >{item.customerName}</td>
                                    <td >{item.customerPhone}</td>
                                    <td >{formatDateTime(item.createDate)}</td>
                                    <td className=''>{formatCurrency(item.amount)}</td>
                                    <td >{item.paymentMethodName || 'null'}</td>
                                    <td >
                                        {item.status === 'completed'
                                            ? (<span className="text-green-500 ">completed</span>)
                                            : item.status === 'pending' ? (
                                                <span className="text-red-500">pending</span>)
                                                : <span >{item.status}</span>
                                        }
                                    </td>

                                    <td className="text-3xl text-[#000099] pl-2"><CiViewList onClick={() => handleDetailClick(item.orderId)} /></td>
                                </tr>
                            ))}
                            {placeholders.map((_, index) => (
                                <tr key={`placeholder-${index}`} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)]">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div className="flex justify-center my-4">
                    {Array.from({ length: totalPages }, (_, i) => (
                        <button
                            key={i}
                            onClick={() => handlePageChange(i + 1)}
                            className={clsx(
                                "mx-1 px-3 py-1 rounded",
                                { "bg-blue-500 text-white": currentPage === i + 1 },
                                { "bg-gray-200": currentPage !== i + 1 }
                            )}
                        >
                            {i + 1}
                        </button>
                    ))}
                </div>
                <Modal
                    isOpen={isModalOpen}
                    onRequestClose={closeModal}
                    contentLabel="Product Details"
                    className="bg-white p-6 rounded-lg shadow-lg w-full max-w-[1100px] mx-auto"
                    overlayClassName="fixed inset-0 z-30 bg-black bg-opacity-50 flex justify-center items-center"
                >
                    {selectedOrder && (
                        <div className="fixed inset-0 flex items-center justify-center z-10 bg-gray-800 bg-opacity-50">
                            <div className="bg-white rounded-lg p-8 w-full max-w-[1100px]">
                                <p className="mb-4">
                                    <strong>Code:</strong> {selectedOrder.code}
                                </p>
                                <p className="mb-4">
                                    <strong>Customer:</strong> {selectedOrder.customerName}
                                </p>
                                <p className="mb-4">
                                    <strong>Phone:</strong> {selectedOrder.customerPhoneNumber}
                                </p>
                                <p className="mb-4">
                                    <strong>Total Value:</strong> {formatCurrency(selectedOrder.finalAmount)}
                                </p>
                                <p className="mb-4">
                                    <strong>Time:</strong> {formatDateTime(selectedOrder.createDate)}
                                </p>
                                <p className="mb-4">
                                    <strong>List product sold:</strong>
                                </p>
                                <div className="overflow-x-auto">
                                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left ">
                                        <thead className="w-full rounded-lg bg-sky-300 text-base font-semibold text-white sticky top-0">
                                            <tr className="whitespace-nowrap text-xl font-bold text-center text-[#212B36]">
                                                <th className="py-3 pl-3 rounded-l-lg  ">Code</th>
                                                <th className="">Name</th>
                                                <th className="">Quantity</th>
                                                <th className="">Promotion Rate</th>
                                                <th className="">Price</th>
                                                <th className="rounded-r-lg ">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {selectedOrder.sellOrderDetails && selectedOrder.sellOrderDetails.map((item, index) => (
                                                <tr key={index} className="cursor-pointer font-normal text-[#637381] bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] text-base hover:shadow-2xl">
                                                    <td className="rounded-l-lg pl-3 py-4 border-x border-gray-300">{item.productCode}</td>
                                                    <td className="border-x border-gray-300 pl-2">{item.productName}</td>
                                                    <td className="border-x border-gray-300 text-center">{item.quantity}</td>
                                                    <td className="border-x border-gray-300 text-center">{item.promotionRate}</td>
                                                    <td className="border-x border-gray-300 text-right pr-2 pl-6">{formatCurrency(item.unitPrice)}</td>
                                                    <td className="rounded-r-lg border-x border-gray-300 pl-2">{item.status}</td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </table>

                                </div>
                                <button
                                    onClick={closeModal}
                                    className="px-4 py-2 bg-gray-500 text-white rounded-md mr-2"
                                    style={{ width: '5rem' }}
                                >
                                    Close
                                </button>
                            </div>
                        </div>
                    )}
                </Modal>





            </div>
        </div>
    )
}

export default Payment