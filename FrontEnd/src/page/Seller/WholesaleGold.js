import React, { useEffect, useState }  from 'react'
import {fetchAllWhGold} from  '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import WhGold from '../../assets/img/seller/WhGold.jpg'
import { createContext } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, deleteProduct } from '../../store/slice/cardSilec'

const WholesaleGold = () => {
  const dispatch = useDispatch()
  const [listWhGold, setListWhGold] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    getWhGold();
  }, []);

  const getWhGold = async () => {
    let res = await fetchAllWhGold();
    if (res && res.data && res.data.data) {
      setListWhGold(res.data.data)
    }
  };

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  const filteredWhGold = listWhGold.filter((whGold) =>
  (whGold.id.toString().includes(searchTerm) ||
  whGold.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
  whGold.code.toLowerCase().includes(searchTerm.toLowerCase()))
  );

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  return (
    <>
          <div className='h-[70px] px-[30px] mt-5 mb-2 w-full'>
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
        <div className='grid grid-cols-5 mt-1 w-[800px]'>
      {filteredWhGold && filteredWhGold.length > 0 &&
        filteredWhGold.filter(item => item.categoryId === 7).map((item, index) => {
          return (
            <div key={`WhGold-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[79%] rounded-xl object-cover bg-[#ffffff1f]' src={WhGold} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{item.name}</div>
                <div className=' flex justify-center text-[0.8em] mt-[5px] font-normal'>Code: {item.code}</div>
                <div className=' flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
         </div>
      </div>
    </div>
    </>
  )
}

export default WholesaleGold