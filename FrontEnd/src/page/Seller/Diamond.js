import React, { useEffect, useState } from 'react'
import { fetchAllDiamond } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import diamond from '../../assets/img/seller/diamond.webp'
import {useDispatch } from 'react-redux'
import { addProduct} from '../../store/slice/cardSilec'

const Diamond = () => {
  const dispatch = useDispatch()
  const [listDiamond, setListDiamond] = useState([]);

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
  
  return (
    <>
       <div className='grid grid-cols-5 gap-1 w-full ml-3 mt-1'>
      {listDiamond && listDiamond.length > 0 &&
        listDiamond.filter(item => item.categoryId === 7 && item.status === "active").map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[79%] rounded-xl object-cover bg-[#ffffff1f]' src={diamond} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{item.name}</div>
                <div className=' flex justify-center text-[0.8em] mt-[5px] font-normal'>ID: {item.code}</div>
                <div className=' flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
    </div>
    </>
  )
}

export default Diamond