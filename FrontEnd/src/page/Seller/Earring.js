import React, { useEffect, useState } from 'react'
import { fetchAllEarring } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import earring from '../../assets/img/seller/earring.png'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, deleteProduct } from '../../store/slice/cardSilec'
import axios from 'axios'
import Modal from 'react-modal';

const Earring = () => {
  const [listEarring, setlistEarring] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedJewelry, setselectedJewelry] = useState(null);
  const [selectedDiamond, setselectedDiamond] = useState(null);
  const closeModal = () => {
    setIsModalOpen(false);
    setselectedJewelry(null);
  };
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
        setselectedJewelry(details);
        setIsModalOpen(true); // Open modal when staff details are fetched


        const resDiamond = await axios.get(`https://jssatsproject.azurewebsites.net/api/diamond/getbycode?code=${details.diamondCode}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
        setselectedDiamond(resDiamond.data.data[0]);

      }
    } catch (error) {
      console.error('Error fetching staff details:', error);
    }
  };
  const formatCurrency = (value) => {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
      minimumFractionDigits: 0
    }).format(value);
  };
  function capitalizeFirstLetter(string) {
    return string.split(' ').map(word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');
  }



  const getEarring = async () => {
    let res = await fetchAllEarring();
    if (res && res.data && res.data.data) {
      setlistEarring(res.data.data)
    }
  };
  useEffect(() => {
    getEarring();
  }, []);
  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }
  function formatName(name) {
    return name.replace(/\s*Earrings$/, "");
  }
  const dispatch = useDispatch()

  const filteredEarring = listEarring.filter((Earring) =>
    (Earring.id.toString().includes(searchTerm) ||
  Earring.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
  Earring.code.toLowerCase().includes(searchTerm.toLowerCase()))
    );
  
    const handleSearch = (event) => {
      setSearchTerm(event.target.value);
    };
  return (<>
       <div className='h-[70px] pl-[30px] mt-5 mb-2 w-full'>
      <form className="max-w-md mx-auto">
        <div className="relative">
          <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
            <svg
              className="w-4 h-4 text-gray-500 dark:text-gray-400"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 20 20"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
              />
            </svg>
          </div>
          <input
            type="search"
            id="default-search"
            className="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500"
            placeholder="Search Item, ID in here..."
            required
            value={searchTerm}
            onChange={handleSearch}
          />
        </div>
      </form>
      <div className='h-[88vh] overflow-y-auto mt-3 flex justify-center'>
        <div className='grid grid-cols-4 mt-1 w-[850px]'>
          {filteredEarring && filteredEarring.length > 0 &&
            filteredEarring.filter(item => item.categoryId === 2 && item.status === "active").map((item, index) => {
              return (
                <div key={`earring-${index}`} class="relative flex flex-col justify-center items-center w-[200px] px-[20px] pb-8 h-[280px] bg-[#fff] shadow-xl rounded-lg mb-2">
                  <div className=' bg-[#fff] rounded-md shadow-md'>
                    <img class="mt-0 w-28 h-28  rounded-lg hover:-translate-y-30 duration-700 hover:scale-125" src={earring} />
                    </div>
                <div class="max-w-sm h-auto">

                <div class="absolute top-[10px] w-full left-0 p-1 sm:justify-between">
                    <h2 class="text-black text-sm font-normal tracking-widest text-center">{item.name}</h2>
                  </div>
                  <div className='absolute bottom-[50px] right-0 w-full'>
                    <p class="text-sm text-[#de993f] flex justify-center">Code: {item.code}</p>
                    <div class=" flex gap-3 items-center justify-center">
                      <p class="text-[#cc4040] font-bold text-sm">{formatCurrency(item.productValue - (item.productValue * item.discountRate))}</p>
                      <p class="text-[#121212] font-semibold text-sm line-through">{formatCurrency(item.productValue)}</p>
                    </div>
                  </div>
                  <div class="absolute bottom-[-10px] right-0 w-full flex justify-around items-center">
                    <button onClick={() => handleDetailClick(item.code)} class="px-3 bg-[#3b9c7f] p-1 rounded-md text-white font-semibold shadow-md shadow-[#87A89E] hover:ring-2 ring-blue-400 hover:scale-75 duration-500">Details</button>
                    <button onClick={() => dispatch(addProduct(item))} class="px-2 border-2 border-white p-1 rounded-md text-white font-semibold shadow-lg shadow-white hover:scale-75 duration-500">Add to Cart</button>
                    </div>
                  </div>
                </div>
              )
            })}
        </div>
        <Modal
          isOpen={isModalOpen}
          onRequestClose={closeModal}
          contentLabel="Staff Details"
          className="bg-white rounded-md shadow-lg max-w-md mx-auto"
          overlayClassName="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center"
        >
          {selectedJewelry && (
           
              <div className="">
                <div className="flex items-center py-2 mb-2 justify-between border-b rounded-t">
                  <h3 className="text-md ml-6 font-semibold text-gray-900">
                    {selectedJewelry.name}
                  </h3>
                  <a className='cursor-pointer text-black text-[24px] py-0' onClick={closeModal} >&times;</a>
                </div>
                <div class="relative overflow-x-auto shadow-md ">
                  <table class="w-full text-sm text-left rtl:text-right text-gray-500">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700">
                      <tr className='hidden'>
                        <th scope="col" class="px-6 py-2">
                          Information
                        </th>
                        <th scope="col" class="px-6 py-2">
                          Details
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                        <td scope="row" class=" px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                          Material Name
                        </td>
                        <td class="px-6 py-2">
                          {selectedJewelry.materialName}
                        </td>
                      </tr>
                      <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                        <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                          Material Weight
                        </td>
                        <td class="px-6 py-2">
                          {selectedJewelry.materialWeight}
                        </td>
                      </tr>
                      <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                        <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                          DiamondCode
                        </td>
                        <td class="px-6 py-2 flex items-center gap-4">
                          {selectedJewelry.diamondCode}
                        </td>
                      </tr>

                      <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                        <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                          Diamond Name
                        </td>
                        <td class="px-6 py-2">
                          {selectedJewelry.diamondName}
                        </td>
                      </tr>
                      {selectedDiamond && (<>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Shape:
                          </td>
                          <td class="px-6 py-2">
                            {selectedDiamond.shapeName}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Origin:
                          </td>
                          <td class="px-6 py-2">
                            {capitalizeFirstLetter(selectedDiamond.originName)}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Fluorescence:
                          </td>
                          <td class="px-6 py-2">
                            {capitalizeFirstLetter(selectedDiamond.fluorescenceName)}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Color:
                          </td>
                          <td class="px-6 py-2">
                            {capitalizeFirstLetter(selectedDiamond.colorName)}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Symmetry:
                          </td>
                          <td class="px-6 py-2">
                            {capitalizeFirstLetter(selectedDiamond.symmetryName)}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Polish:
                          </td>
                          <td class="px-6 py-2">
                            {capitalizeFirstLetter(selectedDiamond.polishName)}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Cut:
                          </td>
                          <td class="px-6 py-2">
                            {capitalizeFirstLetter(selectedDiamond.cutName)}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Clarity:
                          </td>
                          <td class="px-6 py-2">
                            {selectedDiamond.clarityName}
                          </td>
                        </tr>
                        <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                          <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                            Carat:
                          </td>
                          <td class="px-6 py-2">
                            {selectedDiamond.caratWeight}
                          </td>
                        </tr>
                      </>)
                      }
                    </tbody>
                  </table>
                </div>
              </div>
          
          )}
        </Modal>
      </div>
    </div>
  </>)
}

export default Earring