import React, { useEffect, useState } from 'react'
import { fetchAllNecklace } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import logo from '../../assets/logo.png'
import necklace from '../../assets/img/seller/necklace.png'
import { createContext } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, deleteProduct } from '../../store/slice/cardSilec'

const Necklace = () => {
  const dispatch = useDispatch()
  const [listNecklace, setListNecklace] = useState([]);

  useEffect(() => {
    getNecklace();
  }, []);

  const getNecklace = async () => {
    let res = await fetchAllNecklace();
    if (res && res.data && res.data.data) {
      setListNecklace(res.data.data)
    }
  };

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  
  function formatName(name) {
    return name.replace(/\s*Necklace$/, "");
  }
  return (<>
      <div className='grid grid-cols-5 gap-1 w-full ml-3 mt-1'>
      {listNecklace && listNecklace.length > 0 &&
        listNecklace.filter(item => item.categoryId === 4).map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[79%] rounded-xl object-cover bg-[#ffffff1f]' src={necklace} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{formatName(item.name)}</div>
                <div className=' flex justify-center text-[0.8em] mt-[5px] font-normal'>ID: {item.id}</div>
                <div className=' flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
    </div>
  </>)
}

export default Necklace