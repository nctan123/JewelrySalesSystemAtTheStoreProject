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
  
  return (
    <>
        <div className='grid grid-cols-5 gap-1 w-full ml-3 mt-1'>
      {listWhGold && listWhGold.length > 0 &&
        listWhGold.filter(item => item.categoryId === 7).map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[79%] rounded-xl object-cover bg-[#ffffff1f]' src={WhGold} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{item.name}</div>
                <div className=' flex justify-center text-[0.8em] mt-[5px] font-normal'>ID: {item.id}</div>
                <div className=' flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
    </div>
    </>
  )
}

export default WholesaleGold