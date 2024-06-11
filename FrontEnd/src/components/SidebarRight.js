import React, { useState, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
// import  addProduct, deleteProduct  from '../../../'
import { deleteProduct } from '../store/slice/cardSilec'


const SidebarRight = () => {
  const dispatch = useDispatch()
  const [currentTime, setCurrentTime] = useState(new Date());

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentTime(new Date());
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  const CartProduct = useSelector(state => state.cart.CartArr);
  const CusPoint = useSelector(state => state.cart.CusPoint);

  const [total, setTotal] = useState(0);

  useEffect(() => {
    const calculateTotal = () => {
      let totalValue = 0;
      CartProduct.forEach((product) => {
        totalValue += product.productValue;
      });
      setTotal(totalValue);
    };
    calculateTotal();
  }, [CartProduct]);
  const tax = total * 5 / 100
  const totalInvoice = tax + total
  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }

  return (<>


    <div className='flex justify-center '>
      <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[33em] bg-[#f3f1ed] mt-[20px]'>
        <div className='flex justify-between'>
          <select className="ml-[15px] relative text-black bg-transparent outline-none border border-white text-sm font-semibold rounded-md block w-[50%] p-1">
            <option>Sell</option>
            <option>Buy</option>
            <option>Return</option>
            <option>Exchange</option>
          </select>
          <div className='flex justify-end px-[15px] text-black font-thin'>{currentTime.toLocaleString()}</div>
        </div>
        <div className='flex justify-start px-[15px] text-black'>
          <p className='font-light'>Address:</p>
          <span className='w-full flex justify-center font-serif'>Jewelry Store</span>
        </div>
        <div className='flex  px-[15px] text-black'>
          <p className='w-[260px] font-light '>ID Customer:</p>
          {CusPoint && CusPoint[0] && (
            <span className='w-full flex font-serif'>{CusPoint[0].id}</span>
          )}
        </div>
        <div className='grid grid-cols-3 border border-x-0 border-t-0 mx-[10px] border-b-black pb-[2px]'>
          <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
          <div className='col-start-3 ml-6 flex justify-start'>Price</div>
        </div>
        <div id='screenSeller' className='grid-cols-3 h-[45%] overflow-y-auto'>
          {CartProduct && CartProduct.map((item, index) => {
            return (
              <div key={`ring-${index}`} className='grid grid-cols-3 cursor-pointer' onClick={() => dispatch(deleteProduct(item))}  >
                <div className='col-start-1 col-span-2 flex px-[10px] py-2 text-sm' >{item.name}</div>
                <div className='col-start-3 flex justify-center text-[#d48c20] px-[10px] py-2'>{formatPrice(item.code)}</div>
              </div>
            )
          })}
        </div>
        <div className='border mx-[15px] border-x-0 border-b-0 border-t-black grid grid-cols-2 py-2'>
          <div className='font-bold'>PAYMENT</div>
          <input className="w-42 h-full border-none rounded-md outline-none text-sm bg-[#ffff] text-red font-semibold  pl-2" type="text" name="point" id="inputPoint" placeholder="Note" />
        </div>
        <div className='px-[15px] grid grid-cols-2 grid-rows-3 pb-2 '>
          <div className='row-start-1 font-thin'>Subtotal:</div>
          <div className='col-start-2 flex justify-end'>{formatPrice(total.toFixed())}</div>
          <div className='row-start-2 font-thin'>Tax:</div>
          <div className='col-start-2 flex justify-end'>{formatPrice(tax)}</div>
          <div className='row-start-3 font-thin'>Point:</div>
          <div className='col-start-2 flex justify-end text-red-500'>          <input className="w-42 h-full border-none rounded-md outline-none text-sm bg-[#ffff] text-red font-semibold  pl-2" type="number" name="point" id="inputPoint" placeholder="Use Point" />
          </div>
        </div>
        <div className='bg-[#87A89E] h-[50px] grid grid-cols-2 '>
          <div className='mx-[15px] flex items-center font-bold text-lg'>{formatPrice(totalInvoice)}<span>.đ</span></div>
          <div className='col-start-2 flex justify-end items-center mr-[15px]'>
            <a type='submit' href='/' className=" m-0 border border-[#ffffff] bg-[#3f6d67] text-white px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Invoice</a>
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