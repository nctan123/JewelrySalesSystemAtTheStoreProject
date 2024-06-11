import React, { useEffect, useState } from 'react'
import { fetchAllReGold } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import ReGold from '../../assets/img/seller/ReGold.png'
import { createContext } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, deleteProduct } from '../../store/slice/cardSilec'

const RetailGold = () => {
  const dispatch = useDispatch()
  const [listReGold, setListReGold] = useState([]);

  useEffect(() => {
    getReGold();
  }, []);

  const getReGold = async () => {
    let res = await fetchAllReGold();
    if (res && res.data && res.data.data) {
      setListReGold(res.data.data)
    }
  };

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  
 

  return (
    <>
     <div className='grid grid-cols-5 w-full h-[20%] ml-3 mt-1'>
      {listReGold && listReGold.length > 0 &&
        listReGold.filter(item => item.categoryId === 5).map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[70%] rounded-xl object-cover bg-[#ffffff1f]' src={ReGold} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{item.name}</div>
                <div className='absolute bottom-6 left-[40%] flex justify-center text-[0.8em] mt-[5px] font-normal'>ID: {item.id}</div>
                <div className='absolute bottom-1 left-[45%] flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
    </div>
    </>
  )
}

export default RetailGold