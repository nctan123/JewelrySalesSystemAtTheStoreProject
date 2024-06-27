import React, { useEffect, useState } from 'react';
import { IoIosSearch } from "react-icons/io";
import { format } from 'date-fns'; // Import format function from date-fns
import axios from "axios";
import clsx from 'clsx';
import { CiViewList } from "react-icons/ci";

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
    const getNamefromDescription = (value) => {
        if (!value) return '';  // Check if value is undefined or null
        const newlinePosition = value.indexOf('\n');
        return newlinePosition !== -1 ? value.substring(0, newlinePosition) : value;
    }

    const getDescription = (value) => {
        if (!value) return '';  // Check if value is undefined or null
        const newlinePosition = value.indexOf('\n');
        return newlinePosition !== -1 ? value.substring(newlinePosition + 1) : '';
    }

    const formatEffectiveDate = (date) => {
        if (!date) return '';  // Check if date is undefined or null
        try {
            return format(new Date(date), 'dd/MM/yyyy');
        } catch (error) {
            console.error('Invalid date format:', date);
            return 'Invalid Date';
        }
    }

    const indexOfLastPolicy = currentPage * policysPerPage;
    const indexOfFirstPolicy = indexOfLastPolicy - policysPerPage;
    const currentPolicys = listPolicy.slice(indexOfFirstPolicy, indexOfLastPolicy);

    const totalPages = Math.ceil(listPolicy.length / policysPerPage);
    const placeholders = Array.from({ length: policysPerPage - currentPolicys.length });

    return (
        <div className="flex items-center justify-center min-h-screen bg-white mx-5 pt-5 mb-5 rounded">
            <div>
                <h1 className="text-3xl font-bold text-center text-blue-800 mb-4 underline">Return Buy Back Policy List</h1>
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
                <div className="w-[1200px] overflow-hidden">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-sky-300 text-base font-semibold text-white sticky top-0">
                            <tr className="whitespace-nowrap text-xl font-bold text-[#212B36] ">
                                <th className="py-3 pl-3 rounded-l-lg">Policy ID</th>
                                <th >Name</th>
                                <th >Effective Date</th>
                                <th className=" text-center">Status</th>
                                <th className=" rounded-r-lg ">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {currentPolicys.map((item, index) => (
                                <tr key={index} className="cursor-pointer font-normal text-[#637381] bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] text-base hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3  py-4 text-black">{item.id}</td>
                                    <td >{getNamefromDescription(item.description)}</td>
                                    <td >{formatEffectiveDate(item.effectiveDate)}</td>
                                    <td className="text-center">
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
                <div className="fixed inset-0 z-30 flex items-center justify-center z-10 bg-gray-800 bg-opacity-50">
                    <div className="bg-white rounded-lg p-8 max-w-md w-full">
                        <h2 className="text-2xl font-bold text-blue-600 text-center mb-4">{getNamefromDescription(selectedPolicy.description)}</h2>

                        <p className="text-sm text-gray-700 mb-2 text-xl"><strong>ID:</strong> {selectedPolicy.id}</p>
                        <p className="text-sm text-gray-700 mb-2 text-xl"><strong>Description: </strong>{getDescription(selectedPolicy.description)}</p>

                        <p className="text-sm text-gray-700 mb-2 text-xl"><strong>Effective Date:</strong> {formatEffectiveDate(selectedPolicy.effectiveDate)}</p>
                        <p className="text-sm text-gray-700 mb-2 text-xl"><strong>Status:</strong> {selectedPolicy.status === 'active'
                            ? (<span className="text-green-500">Active</span>)
                            : <span className="text-red-500">Inactive</span>}</p>

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

