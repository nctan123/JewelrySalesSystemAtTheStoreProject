import React, { useEffect, useState } from 'react'
import clsx from 'clsx'
import { IoIosSearch } from "react-icons/io";
import logo from '../../../assets/img/seller/ReGold.png'
import axios from "axios";
import Modal from 'react-modal';
import { CiViewList } from "react-icons/ci";

const RetailGoldManager = () => {
    const [originalListProduct, setOriginalListProduct] = useState([]);
    const [listProduct, setListProduct] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedRetailGold, setSelectedRetailGold] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const ProductsPerPage = 10;

    useEffect(() => {
        getProduct();
    }, []);

    const handleDetailClick = async (id) => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/product/getbycode?code=${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (res && res.data && res.data.data) {
                const details = res.data.data[0];
                setSelectedRetailGold(details);
                console.log('>>> check ressss', res)
                setIsModalOpen(true); // Open modal when staff details are fetched
            }

        } catch (error) {
            console.error('Error fetching staff details:', error);
        }
    };

    const getProduct = async () => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                throw new Error("No token found");
            }
            const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/product/getall`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            console.log('... check staff', res);
            if (res && res.data && res.data.data) {
                const allProducts = res.data.data;
                const diamondProducts = allProducts.filter(product => product.category === 'Retail gold');
                setListProduct(diamondProducts);
                setOriginalListProduct(diamondProducts);
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
        setSearchQuery(e.target.value);
        setCurrentPage(1);
    };

    const handleSearch = () => {
        let filteredProduct = originalListProduct;

        if (searchQuery) {
            filteredProduct = filteredProduct.filter((product) =>
                product.code.toLowerCase().includes(searchQuery.toLowerCase())
            );
        }

        setListProduct(filteredProduct);
        setSearchQuery('')
    };

    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedRetailGold(null);
    };

    const indexOfLastProduct = currentPage * ProductsPerPage;
    const indexOfFirstProduct = indexOfLastProduct - ProductsPerPage;
    const currentProducts = listProduct.slice(indexOfFirstProduct, indexOfLastProduct);

    const totalPages = Math.ceil(listProduct.length / ProductsPerPage);
    const placeholders = Array.from({ length: ProductsPerPage - currentProducts.length });

    return (
        <div className="flex items-center justify-center min-h-screen bg-white mx-5 pt-5 mb-5 rounded">
            <div>
                <h1 className="text-3xl font-bold text-center text-blue-800 mb-4 underline">List of retail gold</h1>
                <div className="flex mb-4">
                    <div className="relative">
                        <input
                            type="text"
                            placeholder="Search by code"
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
                                <th >Category</th>
                                <th>Code</th>
                                <th >Name</th>
                                <th className="pl-7"> Img</th>
                                <th>Price</th>
                                <th>Stall</th>
                                <th>Status</th>
                                <th className=" rounded-r-lg ">Action</th>
                            </tr>
                        </thead>

                        <tbody >
                            {currentProducts.map((item, index) => (
                                <tr key={index} className="cursor-pointer font-normal text-[#637381] bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] text-base hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3  py-4 text-black">{item.id}</td>
                                    <td >{item.category}</td>
                                    <td> {item.code} </td>
                                    <td>{item.name}</td>
                                    <td > <img src={logo} className="w-20 h-20" /> </td>
                                    <td >{formatCurrency(item.productValue)}</td>
                                    <td >
                                        {item.stalls && item.stalls.name ? item.stalls.name : 'Null'}
                                    </td>
                                    <td> {item.status} </td>
                                    <td className="text-3xl text-[#000099] pl-4"><CiViewList onClick={() => handleDetailClick(item.code)} /></td>


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
                    contentLabel="Staff Details"
                    className="bg-white p-6 rounded-lg shadow-lg max-w-md mx-auto"
                    overlayClassName="fixed inset-0 z-30 bg-black bg-opacity-50 flex justify-center items-center"
                >

                    {selectedRetailGold && (
                        <div className="fixed inset-0 flex items-center justify-center z-10 bg-gray-800 bg-opacity-50">
                            <div className="bg-white rounded-lg p-8 max-w-md w-full">
                                <h2 className="text-xl text-center text-blue-600 font-bold mb-4">{selectedRetailGold.name}</h2>

                                {/* <p><strong>ID:</strong> {selectedDiamond.id}</p> */}
                                <p className="text-sm text-gray-700 mb-2"><strong>ID:</strong> {selectedRetailGold.id}</p>
                                <p className="text-sm text-gray-700 mb-2"><strong>Code:</strong> {selectedRetailGold.code}</p>
                                <p className="text-sm text-gray-700 mb-2"><strong>Category:</strong>{selectedRetailGold.category}</p>
                                <p className="text-sm text-gray-700 mb-2"><strong>Material:</strong> {selectedRetailGold.materialName}</p>
                                <p className="text-sm text-gray-700 mb-2"><strong>Material Weight:</strong> {selectedRetailGold.materialWeight}</p>

                                <p className="text-sm text-gray-700 mb-2"><strong>Price Rate: </strong>{selectedRetailGold.priceRate}</p>
                                <h1 ><strong>Price:</strong> {formatCurrency(selectedRetailGold.productValue)}</h1>
                                <div className='flex'>
                                    <button onClick={closeModal} className="mt-4 px-4 py-2 bg-blue-500 text-white rounded" style={{ width: '5rem' }}>Close</button>
                                </div>
                            </div>
                        </div>
                    )}
                </Modal>
            </div>
        </div>
    )
}

export default RetailGoldManager