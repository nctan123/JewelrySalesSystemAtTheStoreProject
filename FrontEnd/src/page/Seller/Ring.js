import React, { useEffect, useState } from 'react'
import { fetchAllRing } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import logo from '../../assets/logo.png'
import ring from '../../assets/img/seller/ring.png'
import { createContext } from 'react'

  
const Ring = () => {
  const [listRing, setListRing] = useState([])
  const SelectedItemContext = createContext(null)
  useEffect(() => {
    getRing();
  }, [])

  const getRing = async () => {
    let res = await fetchAllRing();
    if (res && res.data && res.data.data) {
      setListRing(res.data.data)
    }
    console.log(setListRing)
  }

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  
  function formatName(name) {
    return name.replace(/\s*Ring$/, "");
  }

const [listScreenRing, setListScreenRing] = useState([]);

const handleItemClick = (item) => {
  const itemIndex = listScreenRing.findIndex((i) => i.id === item.id);
  if (itemIndex === -1) {
    // Nếu chưa có, thêm item vào mảng
    setListScreenRing([...listScreenRing, item]);
  } else {
    // Nếu đã có, không làm gì cả
    console.log("Item đã có trong mảng listScreenRing");
  }
 
};

  return (<>

    {/* <div className='flex px-3 w-full items-center'> */}
    <SelectedItemContext.Provider value={listScreenRing}>
    <div className='grid grid-cols-5 gap-1 w-full ml-3 mt-1'>
      {listRing && listRing.length > 0 &&
        listRing.filter(item => item.categoryId === 1 && item.stallsId === 2).map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => handleItemClick(item)}>
                <img className=' mt-0 w-[100%] h-[79%] rounded-xl object-cover bg-[#ffffff1f]' src={ring} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{formatName(item.name)}</div>
                <div className=' flex justify-center text-[0.8em] mt-[5px] font-normal'>ID: {item.id}</div>
                <div className=' flex justify-center text-[#d48c20]'>{formatPrice(item.gemCost)}đ</div>
            </div>
          )
        })}
         
    </div>

    </SelectedItemContext.Provider>
  </>
  )
}

export default Ring 

