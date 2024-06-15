import React, { useEffect, useState } from 'react'
import { fetchAllRing } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import ring from '../../assets/img/seller/ring.png'
import { useDispatch } from 'react-redux'
import { addProduct } from '../../store/slice/cardSilec'



const Ring = () => {
  const dispatch = useDispatch()
  const [listRing, setListRing] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  

  useEffect(() => {
    getRing();
  }, []);

  const getRing = async () => {
    let res = await fetchAllRing();
    console.log(res)
    if (res && res.data && res.data.data) {
      setListRing(res.data.data);
       
    }
  };

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }

  function formatName(name) {
    return name.replace(/\s*Ring$/, "");
  }

  const filteredRing = listRing.filter((ring) =>
  (ring.id.toString().includes(searchTerm) ||
    ring.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
    ring.code.toLowerCase().includes(searchTerm.toLowerCase()))
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
          {filteredRing && filteredRing.length > 0 &&
            filteredRing.filter(item => item.categoryId === 1 && item.status === "active").map((item, index) => {
              return (
                  <div key={`ring-${index}`} class="relative flex flex-col justify-center items-center w-[200px] px-[20px] pb-8 h-[280px] bg-[#fff] shadow-xl rounded-lg mb-2">
                  <div className=' bg-[#fff] rounded-md shadow-md'>
                  <img class="mt-0 w-28 h-28  rounded-lg hover:-translate-y-30 duration-700 hover:scale-125" src={ring} />
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
                      <button class="px-2 bg-blue-600 p-1 rounded-md text-white font-semibold shadow-xl shadow-blue-500/50 hover:ring-2 ring-blue-400 hover:scale-75 duration-500">Details</button>
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

export default Ring



