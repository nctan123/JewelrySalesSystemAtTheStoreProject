import React, { useEffect, useState } from 'react';
import { fetchAllPromotion } from '../../../apis/jewelryService';
import clsx from 'clsx';
import { IoIosSearch } from "react-icons/io";
import { format } from 'date-fns'; // Import format function from date-fns

const PromotionList = () => {
  const [originalListPromotion, setOriginalListPromotion] = useState([]);
  const [listPromotion, setListPromotion] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const promotionsPerPage = 10;

  useEffect(() => {
    getPromotion();
  }, []);

  const getPromotion = async () => {
    let res = await fetchAllPromotion();
    console.log('... check promotion', res);
    if (res && res.data && res.data.data) {
      const promotions = res.data.data;
      setOriginalListPromotion(promotions);
      setListPromotion(promotions);
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

    //   if (startDate && endDate) {
    //     const start = new Date(startDate);
    //     const end = new Date(endDate);
    //     filteredPromotions = filteredPromotions.filter((promotion) => {
    //       const promotionStartDate = new Date(promotion.startDate);
    //       const promotionEndDate = new Date(promotion.endDate);
    //       return promotionStartDate <= start && promotionEndDate >= end;
    //     });
    // }

    setListPromotion(filteredPromotions);
  };

  const indexOfLastPromotion = currentPage * promotionsPerPage;
  const indexOfFirstPromotion = indexOfLastPromotion - promotionsPerPage;
  const currentPromotions = listPromotion.slice(indexOfFirstPromotion, indexOfLastPromotion);

  const totalPages = Math.ceil(listPromotion.length / promotionsPerPage);
  const placeholders = Array.from({ length: promotionsPerPage - currentPromotions.length });

  return (
    <div className="flex items-center justify-center min-h-screen">
      <div>
        <h1 className="text-2xl font-bold text-center mb-4">Promotion List</h1>
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
          {/* <div className="ml-4">
            <input
              type="date"
              value={startDate}
              onChange={(e) => setStartDate(e.target.value)}
              className="px-3 py-2 border border-gray-300 rounded-md"
            />
            <input
              type="date"
              value={endDate}
              onChange={(e) => setEndDate(e.target.value)}
              className="px-3 py-2 border border-gray-300 rounded-md ml-2"
            />
            <button onClick={handleSearch} className="ml-2 px-3 py-2 border border-gray-300 rounded-md bg-blue-500 text-white">
              Filter
            </button>
          </div> */}
        </div>
        <div className="w-[1000px] overflow-hidden">
          <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
            <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white sticky top-0">
              <tr>
                <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Promotion ID</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Full Name</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">Discount Rate</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Description</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">From</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">To</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa]">Status</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36] bg-[#f6f8fa] text-center">Action</th>
              </tr>
            </thead>
            <tbody>
              {currentPromotions.map((item, index) => (
                <tr key={index} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                  <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                  <td className="text-sm font-normal text-[#637381]">{item.name}</td>
                  <td className="text-sm font-normal text-[#637381] text-center">{item.discountRate} %</td>
                  <td className="text-sm font-normal text-[#637381]">{item.description}</td>
                  <td className="text-sm font-normal text-[#637381]">{format(new Date(item.startDate), 'dd/MM/yyyy')}</td>
                  <td className="text-sm font-normal text-[#637381]">{format(new Date(item.endDate), 'dd/MM/yyyy')}</td>
                  <td className="text-sm font-normal text-[#637381]">{item.status}</td>
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
  );
}

export default PromotionList;
