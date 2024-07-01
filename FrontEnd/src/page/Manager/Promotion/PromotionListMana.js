

import React, { useEffect, useState } from 'react';
import { IoIosSearch } from "react-icons/io";
import { format } from 'date-fns';
import axios from "axios";
import clsx from 'clsx';
import { toast } from 'react-toastify';
import { CiViewList } from "react-icons/ci";

const PromotionListMana = () => {
    const [originalListPromotion, setOriginalListPromotion] = useState([]);
    const [listPromotion, setListPromotion] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedPromotion, setSelectedPromotion] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const promotionsPerPage = 10;

    useEffect(() => {
        getPromotion();
    }, []);

    const getPromotion = async () => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get('https://jssatsproject.azurewebsites.net/api/promotion/getAll', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            console.log('... check promotion', res);
            if (res && res.data && res.data.data) {
                const promotions = res.data.data;
                setOriginalListPromotion(promotions);
                setListPromotion(promotions);
            }
        } catch (error) {
            console.error('Error fetching promotions:', error);
            if (error.response) {
                console.error('Error response:', error.response.data);
            } else if (error.request) {
                console.error('Error request:', error.request);
            } else {
                console.error('Error message:', error.message);
            }
        }
    };

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
        setCurrentPage(1);
    };

    const handleSearch = () => {
        let filteredPromotions = originalListPromotion;

        if (searchQuery) {
            filteredPromotions = filteredPromotions.filter((promotion) =>
                promotion.name.toLowerCase().includes(searchQuery.toLowerCase())
            );
        }

        setListPromotion(filteredPromotions);
        setSearchQuery('')
    };

    const handleDetailClick = (promotion) => {
        setSelectedPromotion(promotion);
    };




    const indexOfLastPromotion = currentPage * promotionsPerPage;
    const indexOfFirstPromotion = indexOfLastPromotion - promotionsPerPage;
    const currentPromotions = listPromotion.slice(indexOfFirstPromotion, indexOfLastPromotion);

    const totalPages = Math.ceil(listPromotion.length / promotionsPerPage);
    const placeholders = Array.from({ length: promotionsPerPage - currentPromotions.length });

    return (
        <div className="flex items-center justify-center min-h-screen bg-white mx-5 pt-5 mb-5 rounded">
            <div>
                <h1 className="text-3xl font-bold text-center text-blue-800 mb-4 underline">Promotion list</h1>

                <div className="flex mb-4">
                    <div className="relative">
                        <input
                            type="text"
                            placeholder="Search by name"
                            value={searchQuery}
                            onChange={handleSearchChange}
                            className="px-3 py-2 border border-gray-300 rounded-md w-[400px]"
                        />
                        <IoIosSearch className="absolute top-0 right-0 mr-3 mt-3 cursor-pointer text-gray-500" onClick={handleSearch} />
                    </div>
                </div>
                <div className="w-[1200px] overflow-hidden ">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-sky-300 text-base font-semibold text-white sticky top-0">
                            <tr className="whitespace-nowrap text-xl font-bold text-[#212B36] ">
                                <th className="py-3 pl-3 rounded-l-lg">ID</th>
                                <th >Name</th>
                                <th className=" text-center">Discount Rate</th>
                                <th >From</th>
                                <th >To</th>
                                <th >Status</th>
                                <th className=" rounded-r-lg ">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {currentPromotions.map((item, index) => (
                                <tr key={index} className="cursor-pointer font-normal text-[#637381] bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] text-base hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3  py-4 text-black">{item.id}</td>
                                    <td >{item.name}</td>
                                    <td className=" text-center">{item.discountRate} </td>
                                    <td >{format(new Date(item.startDate), 'dd/MM/yyyy')}</td>
                                    <td >{format(new Date(item.endDate), 'dd/MM/yyyy')}</td>
                                    <td>
                                        {item.status === 'active'
                                            ? (<span className="text-green-500">Active</span>)
                                            : <span className="text-red-500">Inactive</span>}
                                    </td>
                                    <td className="text-3xl text-[#000099] pl-2"><CiViewList onClick={() => handleDetailClick(item)} /></td>

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
            </div>
            {selectedPromotion && !isModalOpen && (
                <div className="fixed inset-0 z-30 flex items-center justify-center z-10 bg-gray-800 bg-opacity-50">
                    <div className="bg-white rounded-lg p-8 max-w-md w-full">
                        <h2 className="text-xl font-bold text-blue-600 text-center mb-4">{selectedPromotion.name}</h2>
                        <p className="text-base text-gray-700 mb-2"> <strong>ID:</strong> {selectedPromotion.id}</p>
                        <p className="text-base text-gray-700 mb-2"><strong>Discount Rate: </strong>{selectedPromotion.discountRate}</p>
                        <p className="text-base text-gray-700 mb-2"><strong>Description: </strong>{selectedPromotion.description}</p>
                        <p className="text-base text-gray-700 mb-2"><strong>Start Date: </strong>{format(new Date(selectedPromotion.startDate), 'dd/MM/yyyy')}</p>
                        <p className="text-base text-gray-700 mb-2"><strong>End Date:</strong> {format(new Date(selectedPromotion.endDate), 'dd/MM/yyyy')}</p>
                        <div >
                            <p className="text-base text-gray-700 mb-2"><strong>Categories:</strong></p>
                            <ul className="list-disc list-inside">
                                {selectedPromotion.categories.map((category, index) => (
                                    <li key={index} className="text-sm">{category.name}</li>
                                ))}
                            </ul>
                        </div>
                        <p className="text-base text-gray-700 mb-2"><strong>Status: </strong>
                            {selectedPromotion.status === 'active'
                                ? (<span className="text-green-500 font-bold">Active</span>)
                                : <span className="text-red-500">Inactive</span>}
                        </p>
                        <div className='flex justify-end mt-6'>

                            <button
                                className="px-6 py-3 bg-blue-500 text-white rounded" onClick={() => setSelectedPromotion(null)}
                            >
                                Close
                            </button>
                        </div>
                    </div>
                </div>
            )}

        </div>
    );
};

export default PromotionListMana;
