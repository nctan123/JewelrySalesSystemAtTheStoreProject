import React, { useState, useEffect } from 'react'
// import dayjs from 'dayjs';
// import { SelectedItemContext } from '../page/Seller/Ring'
// import { useContext } from 'react';

const SidebarRight = () => {
  const [currentTime, setCurrentTime] = useState(new Date());

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentTime(new Date());
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  // const listScreenRing = useContext(SelectedItemContext);
  // console.log(listScreenRing)
 
  return (<>


    <div className='flex justify-center '>
      <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[33em] bg-[#f3f1ed] mt-[20px]'>
        <div className='flex justify-end px-[15px] text-black font-thin'>{currentTime.toLocaleString()}</div>
        <div className='flex justify-start px-[15px] text-black'>
          <p className='font-light'>Address:</p>
          <span className='w-full flex justify-center font-serif'>Jewelry Store</span>
        </div>
        <div className='flex  px-[15px] text-black'>
          <p className='w-[260px] font-light '>ID Customer:</p>
          <span className='w-full flex font-serif'>01111</span>
        </div>
        <div className='grid grid-cols-3 border mx-[10px] border-b-black pb-[2px]'>
          <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
          <div className='col-start-3  flex justify-start'>Price</div>
        </div>
        <div id='screenSeller' className=' h-[45%]'>
        {/* {listScreenRing && listScreenRing.map((item, index) => {
         return (
          <div key={`ring-${index}`}>
              <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{item.name}</div>
              <div className=' flex justify-center text-[#d48c20]'>{item.gemCost}</div>
          </div>
        )
})} */}
        </div>
        <div className='border mx-[15px] border-t-black grid grid-cols-2 py-2'>
          <div className='font-bold'>PAYMENT</div>
          <div className='flex justify-end'><button className="m-0 p-0 text-xs border w-fit px-2 border-[#ffffff] bg-[#3f6d67] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Use Point</button></div>
        </div>
        <div className='px-[15px] grid grid-cols-2 grid-rows-3 pb-2 '>
          <div className='row-start-1 font-thin'>Subtotal:</div>
          <div className='col-start-2 flex justify-end'>9.000.000</div>
          <div className='row-start-2 font-thin'>Tax:</div>
          <div className='col-start-2 flex justify-end'>1.000.000</div>
          <div className='row-start-3 font-thin'>Point:</div>
          <div className='col-start-2 flex justify-end text-red-500'>1.000.000</div>
        </div>
        <div className='bg-[#87A89E] h-[50px] grid grid-cols-2 '>
          <div className='mx-[15px] flex items-center font-bold text-lg'>9.000.000 <span>.đ</span></div>
          <div className='col-start-2 flex justify-end items-center mr-[15px]'>
            <button className=" m-0 border border-[#ffffff] bg-[#3f6d67] text-white px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Invoice</button>
          </div>
        </div>
      </div>
    </div>
    <div className='flex'>
      <button className="border border-[#ffffff] bg-[#3f6d67e3] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Temporary</button>
      <button className="border border-[#ffffff] bg-[#3f6d67e3] text-white  rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Points</button>
      <button className="border border-[#ffffff] bg-[#3f6d67e3] text-white  rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">List Temporary</button>
    </div>





  </>
  )
}

export default SidebarRight