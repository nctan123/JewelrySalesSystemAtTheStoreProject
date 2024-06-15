import React, { useEffect, useState } from 'react'
import { fetchAllEarring } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import earring from '../../assets/img/seller/earring.png'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, deleteProduct } from '../../store/slice/cardSilec'

const Earring = () => {
  const [listEarring, setlistEarring] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
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
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
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
          {filteredEarring && filteredEarring.length > 0 &&
            filteredEarring.filter(item => item.categoryId === 2 && item.status === "active").map((item, index) => {
              return (
                <div key={`earring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                  <img className=' mt-0 w-[100%] h-[70%] rounded-xl object-cover bg-[#ffffff1f]' src={earring} />
                  <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{formatName(item.name)}</div>
                  <div className='absolute bottom-8 w-full flex justify-center text-[0.8em] mt-[5px] font-normal'>Code: {item.code}</div>
                  <div className='absolute bottom-2 w-full flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
                </div>
              )
            })}

        </div>
      </div>
    </div>
  </>)
}

export default Earring