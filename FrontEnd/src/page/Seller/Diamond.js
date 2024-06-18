import React, { useEffect, useState } from 'react'
import { fetchAllDiamond } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import diamond from '../../assets/img/seller/diamond.webp'
import {useDispatch } from 'react-redux'
import { addProduct} from '../../store/slice/cardSilec'
import Popup from 'reactjs-popup';
const Diamond = () => {
  const dispatch = useDispatch()
  const [listDiamond, setListDiamond] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    getDiamond();
  }, []);

  const getDiamond = async () => {
    let res = await fetchAllDiamond();
    if (res && res.data && res.data.data) {
      setListDiamond(res.data.data)
    }
  };

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
   const filteredDiamond = listDiamond.filter((diamond) =>
  (diamond.id.toString().includes(searchTerm) ||
   diamond.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
   diamond.code.toLowerCase().includes(searchTerm.toLowerCase()))
  );

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };
  return (
    <>
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
          {filteredDiamond && filteredDiamond.length > 0 &&
            filteredDiamond.filter(item => item.categoryId === 7 && item.status === "active").map((item, index) => {
              return (
                <div key={`diamond-${index}`} class="relative flex flex-col justify-center items-center w-[200px] px-[20px] pb-8 h-[280px] bg-[#fff] shadow-xl rounded-lg mb-2">
                <div className=' bg-[#fff] rounded-md shadow-md'>
                  <img class="mt-0 w-28 h-28  rounded-lg hover:-translate-y-30 duration-700 hover:scale-125" src={diamond} />
                </div>
                <div class="max-w-sm h-auto">

                  <div class="absolute top-[20px] left-4 w-[180px] flex justify-center items-center sm:justify-between">
                    <h2 class="text-black text-sm font-normal tracking-widest">{item.name}</h2>
                  </div>
                  <div className='absolute bottom-[50px] right-0 w-full'>
                    <p class="text-sm text-[#de993f] flex justify-center">Code: {item.code}</p>
                    <div class=" flex gap-3 items-center justify-center">
                      <p class="text-[#cc4040] font-bold text-sm">{formatPrice(item.productValue - (item.productValue * item.discountRate))}đ</p>
                      <p class="text-[#121212] font-semibold text-sm line-through">{formatPrice(item.productValue)}đ</p>
                    </div>
                  </div>
                  <div class="absolute bottom-[-10px] right-0 w-full flex justify-around items-center">
                  <Popup trigger={<button onClick='' class="px-3 bg-[#3b9c7f] p-1 rounded-md text-white font-semibold shadow-md shadow-[#87A89E] hover:ring-2 ring-blue-400 hover:scale-75 duration-500">Details</button>} position="right center">
                  {close => (
                        <div className='fixed top-0 bottom-0 left-0 right-0 bg-[#6f85ab61] overflow-y-auto'>
                          <div className='bg-[#fff] my-[70px] mx-auto rounded-md w-[40%] shadow-[#b6b0b0] shadow-md'>
                            <div className="flex items-center justify-between p-2 md:p-5 border-b rounded-t dark:border-gray-600">
                              <h3 className="text-md font-semibold text-gray-900">
                                {item.name}
                              </h3>
                              <a className='cursor-pointer text-black text-[24px] py-0' onClick={close}>&times;</a>
                            </div>


                            <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
                              <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
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
                                      {item.materialName}
                                    </td>
                                  </tr>
                                  <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                                    <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                                      Material Weight
                                    </td>
                                    <td class="px-6 py-2">
                                      {item.materialWeight}
                                    </td>
                                  </tr>
                                  <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                                    <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                                      DiamondCode
                                    </td>
                                    <td class="px-6 py-2 flex items-center gap-4">
                                      {item.diamondCode}
                                
                                        
                                    </td>
                                  </tr>

                                  <tr class="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700">
                                    <td scope="row" class="px-6 py-2 font-medium whitespace-nowrap dark:text-white">
                                      Diamond Name
                                    </td>
                                    <td class="px-6 py-2">
                                      {item.diamondName}
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      )}
                    </Popup>

                    <button onClick={() => dispatch(addProduct(item))} class="px-2 border-2 border-white p-1 rounded-md text-white font-semibold shadow-lg shadow-white hover:scale-75 duration-500">Add to Cart</button>
                  </div>
                </div>
              </div>
              )
            })}

        </div>
      </div>
    </div>
    </>
  )
}

export default Diamond