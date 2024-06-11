
import React, { useEffect, useState } from 'react'
import { fetchAllRing } from '../../../apis/jewelryService'
import clsx from 'clsx'
import { IoIosSearch } from "react-icons/io";
import logo from '../../../assets/img/gnxmxmy001727-nhan-vang-18k-dinh-da-cz-pnj-1.png'

const ProductAdmin = () => {
    const [originalListProduct, setOriginalListProduct] = useState([]);
    const [listProduct, setListProduct] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchQuery, setSearchQuery] = useState('');
    const ProductsPerPage = 10;

    useEffect(() => {
        getProduct();
    }, []);

    const getProduct = async () => {
        let res = await fetchAllRing();
        if (res && res.data && res.data.data) {
            const Products = res.data.data;
            setOriginalListProduct(Products);
            setListProduct(Products);
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
            // If search query is empty, reset to original list of Products
            setListProduct(originalListProduct);
        } else {
            const filteredProducts = originalListProduct.filter((Product) =>
                Product.code.toLowerCase().includes(searchQuery.toLowerCase())
            );

            // Update state with filtered Products
            setListProduct(filteredProducts);
        }
    };

    const indexOfLastProduct = currentPage * ProductsPerPage;
    const indexOfFirstProduct = indexOfLastProduct - ProductsPerPage;
    const currentProducts = listProduct.slice(indexOfFirstProduct, indexOfLastProduct);

    const totalPages = Math.ceil(listProduct.length / ProductsPerPage);
    const placeholders = Array.from({ length: ProductsPerPage - currentProducts.length });

    return (
        <div className="flex items-center justify-center min-h-screen">
            <div>
                <h1 className="text-2xl font-bold text-center mb-4">Product management list</h1>
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
                <div className="w-[1000px] overflow-hidden">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white sticky top-0">
                            <tr>
                                <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Product ID</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Code</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Name</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Img</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Price</th>
                                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {currentProducts.map((item, index) => (
                                <tr key={index} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                                    <td className="text-sm font-normal text-[#637381]">{item.code}</td>
                                    <td className="text-sm font-normal text-[#637381]">{item.name}</td>
                                    <td className="text-sm font-normal text-[#637381]"> <img src={logo} className="w-12 h-12" /> </td>
                                    <td className="text-sm font-normal text-[#637381]">{item.productValue}</td>
                                    <td className="text-sm font-normal text-[#637381]">
                                        <button className="my-2 border border-white bg-[#4741b1d7] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none">
                                            Edit
                                        </button>
                                    </td>
                                </tr>
                            ))}
                            {placeholders.map((_, index) => (
                                <tr key={`placeholder-${index}`} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)]">
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

export default ProductAdmin

