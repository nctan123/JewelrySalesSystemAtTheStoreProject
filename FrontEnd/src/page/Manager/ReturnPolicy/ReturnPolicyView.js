import React, { useEffect, useState } from 'react';
import { IoIosSearch } from "react-icons/io";
import { format } from 'date-fns'; // Import format function from date-fns
import axios from "axios";
import clsx from 'clsx';

const ReturnPolicyView = () => {
    const [originalListPolicy, setOriginalListPolicy] = useState([]);
    const [listPolicy, setListPolicy] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedPolicy, setSelectedPolicy] = useState(null); // State to hold selected policy
    const policysPerPage = 10;

    useEffect(() => {
        getPolicy();
    }, []);

    const getPolicy = async () => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get('https://jssatsproject.azurewebsites.net/api/ReturnBuyBackPolicy/GetAll', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            console.log('... check policy', res);
            if (res && res.data && res.data.data) {
                const policys = res.data.data;
                setOriginalListPolicy(policys);
                setListPolicy(policys);
            }
        } catch (error) {
            console.error('Error fetching policys:', error);
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
        let filteredPolicys = originalListPolicy;

        if (searchQuery) {
            filteredPolicys = filteredPolicys.filter((policy) =>
                policy.name.toLowerCase().includes(searchQuery.toLowerCase())
            );
        }

        setListPolicy(filteredPolicys);
        setSearchQuery('')
    };

    const handleDetailClick = (policy) => {
        setSelectedPolicy(policy); // Set selected policy for detail view
    };

    const indexOfLastPolicy = currentPage * policysPerPage;
    const indexOfFirstPolicy = indexOfLastPolicy - policysPerPage;
    const currentPolicys = listPolicy.slice(indexOfFirstPolicy, indexOfLastPolicy);

    const totalPages = Math.ceil(listPolicy.length / policysPerPage);
    const placeholders = Array.from({ length: policysPerPage - currentPolicys.length });

    return (
        <div className="flex items-center justify-center min-h-screen">
            <div>
                <h1 className="text-2xl font-bold text-center mb-4">Policy List</h1>
                {/* <div className="flex mb-4">
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
                </div> */}
                <div className="w-[1000px] overflow-hidden">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white sticky top-0">
                            <tr>
                                <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Policy ID</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Effective Date</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">status</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {currentPolicys.map((item, index) => (
                                {
                                    "id": 1,
                                    "description": "Policy 1 Description",
                                    "effectiveDate": "2024-01-01T00:00:00",
                                    "status": "inactive"
                                },
                                <tr key={index} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                                    <td className="text-sm font-normal text-[#637381]">{format(new Date(item.effectiveDate), 'dd/MM/yyyy')}</td>
                                    <td className="text-sm font-normal text-[#637381] text-center">
                                        {item.status === 'active'
                                            ? (<span className="text-green-500 ">Active</span>)
                                            : <span className="text-red-500">Inactive</span>}
                                    </td>

                                    <td className="text-sm font-normal text-[#637381]">
                                        <button
                                            className="my-2 border border-white bg-[#4741b1d7] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none"
                                            onClick={() => handleDetailClick(item)}
                                        >
                                            Detail
                                        </button>
                                    </td>
                                </tr>
                            ))}
                            {placeholders.map((_, index) => (
                                <tr key={`placeholder-${index}`} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)]">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381] py-4">-</td>
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

            {/* Modal for Policy Detail */}
            {selectedPolicy && (
                <div className="fixed inset-0 flex items-center justify-center z-10 bg-gray-800 bg-opacity-50">
                    <div className="bg-white rounded-lg p-8 max-w-md w-full">
                        <p className="text-sm text-gray-700 mb-2">ID: {selectedPolicy.id}</p>
                        <p className="text-sm text-gray-700 mb-2">Description: {selectedPolicy.description}</p>
                        <p className="text-sm text-gray-700 mb-2">Effective Date: {format(new Date(selectedPolicy.effectiveDate), 'dd/MM/yyyy')}</p>
                        <p className="text-sm text-gray-700 mb-2">Status : {selectedPolicy.status === 'active'
                            ? (<span className="text-green-500 ">Active</span>)
                            : <span className="text-red-500">Inactive</span>}

                        </p>
                        <button
                            className="mt-4 bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded-md" onClick={() => setSelectedPolicy(null)}
                        >
                            Close
                        </button>
                    </div>
                </div>
            )}
        </div>

    );
};

export default ReturnPolicyView;

