// import React, { useEffect, useState } from 'react'
// // import { fetchAllaccount } from '../../../apis/jewelryService'
// import clsx from 'clsx'
// import { IoIosSearch } from "react-icons/io";
// import axios from "axios";

// const Staff = () => {
//     const [originalListaccount, setOriginalListaccount] = useState([]);
//     const [listaccount, setListaccount] = useState([]);
//     const [currentPage, setCurrentPage] = useState(1);
//     const [searchQuery, setSearchQuery] = useState('');
//     const accountsPerPage = 10;

//     useEffect(() => {
//         getaccount();
//     }, []);

//     const getaccount = async () => {
//         // let res = await fetchAllaccount();
//         // if (res && res.data && res.data.data) {
//         //     const accounts = res.data.data;
//         //     setOriginalListaccount(accounts);
//         //     setListaccount(accounts);
//         // }
//         try {
//             const token = localStorage.getItem('token');
//             if (!token) {
//                 throw new Error("No token found");
//             }
//             const res = await axios.get('https://jssatsproject.azurewebsites.net/api/account/getAll', {
//                 headers: {
//                     Authorization: `Bearer ${token}`
//                 }
//             });
//             console.log('... check staff', res);
//             if (res && res.data && res.data.data) {
//                 const staffs = res.data.data;
//                 setOriginalListaccount(staffs);
//                 setListaccount(staffs);
//             }
//         } catch (error) {
//             console.error('Error fetching staffs:', error);
//             if (error.response) {
//                 console.error('Error response:', error.response.data);
//             } else if (error.request) {
//                 console.error('Error request:', error.request);
//             } else {
//                 console.error('Error message:', error.message);
//             }
//         }
//     };

//     const handlePageChange = (page) => {
//         setCurrentPage(page);
//     };

//     const handleSearchChange = (e) => {
//         setSearchQuery(e.target.value);
//         setCurrentPage(1);
//     };

//     const handleSearch = () => {
//         if (searchQuery === '') {
//             // If search query is empty, reset to original list of accounts
//             setListaccount(originalListaccount);
//         } else {
//             const filteredaccounts = originalListaccount.filter((account) =>
//                 account.phone.toLowerCase().includes(searchQuery.toLowerCase())
//             );

//             // Update state with filtered accounts
//             setListaccount(filteredaccounts);
//         }
//     };

//     const indexOfLastaccount = currentPage * accountsPerPage;
//     const indexOfFirstaccount = indexOfLastaccount - accountsPerPage;
//     const currentaccounts = listaccount.slice(indexOfFirstaccount, indexOfLastaccount);
//     const totalPages = Math.ceil(listaccount.length / accountsPerPage);
//     const placeholders = Array.from({ length: accountsPerPage - currentaccounts.length });

//     return (
//         <div className="flex items-center justify-center min-h-screen">
//             <div>
//                 <h1 className="text-2xl font-bold text-center mb-4">Staff management list</h1>
//                 <div className="flex mb-4">
//                     <div className="relative">
//                         <input
//                             type="text"
//                             placeholder="Search by phone number"
//                             value={searchQuery}
//                             onChange={handleSearchChange}
//                             className="px-3 py-2 border border-gray-300 rounded-md w-[400px]"
//                         />
//                         <IoIosSearch className="absolute top-0 right-0 mr-3 mt-3 cursor-Staffer text-gray-500" onClick={handleSearch} />
//                     </div>
//                 </div>
//                 <div className="w-[1000px] overflow-hidden">
//                     <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
//                         <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white sticky top-0">
//                             <tr>
//                                 <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">ID</th>
//                                 <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Username</th>
//                                 <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Password</th>
//                                 <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Role Id</th>
//                                 <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">Action</th>

//                             </tr>
//                         </thead>
//                         <tbody>
//                             {currentaccounts.map((item, index) => (
//                                 <tr key={index} className="cursor-Staffer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
//                                     <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
//                                     <td className="text-sm font-normal text-[#637381]">{item.username}</td>
//                                     <td className="text-sm font-normal text-[#637381]">{item.password}</td>
//                                     <td className="text-sm font-normal text-[#637381]">
//                                         {
//                                             item.roleId === 2
//                                                 ? 'Admin'
//                                                 : item.roleId === 1
//                                                     ? 'Seller'
//                                                     : item.roleId === 4
//                                                         ? 'Manager'
//                                                         : item.roleId === 3
//                                                             ? 'Cashier'
//                                                             : item.roleId
//                                         }
//                                     </td>

//                                     <td className="text-sm font-normal text-[#637381]">
//                                         <button className="my-2 border border-white bg-[#4741b1d7] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none">
//                                             Edit
//                                         </button>
//                                     </td>
//                                 </tr>
//                             ))}
//                             {placeholders.map((_, index) => (
//                                 <tr key={`placeholder-${index}`} className="cursor-Staffer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)]">
//                                     <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">-</td>
//                                     <td className="text-sm font-normal text-[#637381]">-</td>
//                                     <td className="text-sm font-normal text-[#637381]">-</td>
//                                     <td className="text-sm font-normal text-[#637381]">-</td>
//                                     <td className="text-sm font-normal text-[#637381]">-</td>
//                                     <td className="text-sm font-normal text-[#637381]">-</td>
//                                 </tr>
//                             ))}
//                         </tbody>
//                     </table>
//                 </div>
//                 <div className="flex justify-center my-4">
//                     {Array.from({ length: totalPages }, (_, i) => (
//                         <button
//                             key={i}
//                             onClick={() => handlePageChange(i + 1)}
//                             className={clsx(
//                                 "mx-1 px-3 py-1 rounded",
//                                 { "bg-blue-500 text-white": currentPage === i + 1 },
//                                 { "bg-gray-200": currentPage !== i + 1 }
//                             )}
//                         >
//                             {i + 1}
//                         </button>
//                     ))}
//                 </div>
//             </div>
//         </div>
//     )
// }

// export default Staff
import React, { useEffect, useState } from 'react'
// import { fetchAllaccount } from '../../../apis/jewelryService'
import clsx from 'clsx'
import { IoIosSearch } from "react-icons/io";
import axios from "axios";

const Staff = () => {
    const [originalListaccount, setOriginalListaccount] = useState([]);
    const [listaccount, setListaccount] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchQuery, setSearchQuery] = useState('');
    const accountsPerPage = 10;

    useEffect(() => {
        getaccount();
    }, []);

    const getaccount = async () => {
        // let res = await fetchAllaccount();
        // if (res && res.data && res.data.data) {
        //     const accounts = res.data.data;
        //     setOriginalListaccount(accounts);
        //     setListaccount(accounts);
        // }
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get('https://jssatsproject.azurewebsites.net/api/staff/getAll', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            console.log('... check staff', res);
            if (res && res.data && res.data.data) {
                const staffs = res.data.data;
                setOriginalListaccount(staffs);
                setListaccount(staffs);
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

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
        setCurrentPage(1);
    };

    const handleSearch = () => {
        if (searchQuery === '') {
            // If search query is empty, reset to original list of accounts
            setListaccount(originalListaccount);
        } else {
            const filteredaccounts = originalListaccount.filter((account) =>
                account.phone.toLowerCase().includes(searchQuery.toLowerCase())
            );

            // Update state with filtered accounts
            setListaccount(filteredaccounts);
        }
    };

    const indexOfLastaccount = currentPage * accountsPerPage;
    const indexOfFirstaccount = indexOfLastaccount - accountsPerPage;
    const currentaccounts = listaccount.slice(indexOfFirstaccount, indexOfLastaccount);
    const totalPages = Math.ceil(listaccount.length / accountsPerPage);
    const placeholders = Array.from({ length: accountsPerPage - currentaccounts.length });

    return (
        <div className="flex items-center justify-center min-h-screen">
            <div>
                <h1 className="text-2xl font-bold text-center mb-4">Staff management list</h1>
                <div className="flex mb-4">
                    <div className="relative">
                        <input
                            type="text"
                            placeholder="Search by phone number"
                            value={searchQuery}
                            onChange={handleSearchChange}
                            className="px-3 py-2 border border-gray-300 rounded-md w-[400px]"
                        />
                        <IoIosSearch className="absolute top-0 right-0 mr-3 mt-3 cursor-Staffer text-gray-500" onClick={handleSearch} />
                    </div>
                </div>
                <div className="w-[1000px] overflow-hidden">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white sticky top-0">
                            <tr>
                                <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">ID</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Username</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Password</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Role Id</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">Action</th>

                            </tr>
                        </thead>
                        <tbody>
                            {currentaccounts.map((item, index) => (
                                <tr key={index} className="cursor-Staffer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                                    <td className="text-sm font-normal text-[#637381]">{item.username}</td>
                                    <td className="text-sm font-normal text-[#637381]">{item.password}</td>
                                    <td className="text-sm font-normal text-[#637381]">
                                        {
                                            item.roleId === 2
                                                ? 'Admin'
                                                : item.roleId === 1
                                                    ? 'Seller'
                                                    : item.roleId === 4
                                                        ? 'Manager'
                                                        : item.roleId === 3
                                                            ? 'Cashier'
                                                            : item.roleId
                                        }
                                    </td>

                                    <td className="text-sm font-normal text-[#637381]">
                                        <button className="my-2 border border-white bg-[#4741b1d7] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none">
                                            Edit
                                        </button>
                                    </td>
                                </tr>
                            ))}
                            {placeholders.map((_, index) => (
                                <tr key={`placeholder-${index}`} className="cursor-Staffer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)]">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">-</td>
                                    <td className="text-sm font-normal text-[#637381]">-</td>
                                    <td className="text-sm font-normal text-[#637381]">-</td>
                                    <td className="text-sm font-normal text-[#637381]">-</td>
                                    <td className="text-sm font-normal text-[#637381]">-</td>
                                    <td className="text-sm font-normal text-[#637381]">-</td>
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
        </div>
    )
}

export default Staff
