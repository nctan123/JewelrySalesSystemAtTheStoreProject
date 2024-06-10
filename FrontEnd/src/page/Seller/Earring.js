import React, { useEffect, useState } from 'react'
import { fetchAllEarring } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import earring from '../../assets/img/seller/earring.png'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, deleteProduct } from '../../store/slice/cardSilec'

const Earring = () => {
  const [listEarring, setlistEarring] = useState([]);
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
  return (<>
          <div className='grid grid-cols-5 gap-1 w-full ml-3 mt-1'>
      {listEarring && listEarring.length > 0 &&
        listEarring.filter(item => item.categoryId === 2).map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[79%] rounded-xl object-cover bg-[#ffffff1f]' src={earring} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{formatName(item.name)}</div>
                <div className=' flex justify-center text-[0.8em] mt-[5px] font-normal'>ID: {item.id}</div>
                <div className=' flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
    </div>
  </>)
}

export default Earring