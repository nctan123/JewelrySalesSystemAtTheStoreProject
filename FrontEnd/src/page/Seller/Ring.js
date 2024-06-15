import React, { useEffect, useState } from 'react'
import { fetchAllRing } from '../../apis/jewelryService'
import clsx from 'clsx'
import style from "../../style/cardForList.module.css"
import ring from '../../assets/img/seller/ring.png'
import { useDispatch } from 'react-redux'
import { addProduct} from '../../store/slice/cardSilec'
import Barcode from 'react-barcode';

  
const Ring = () => {
  const dispatch = useDispatch()
  const [listRing, setListRing] = useState([]);

  useEffect(() => {
    getRing();
  }, []);

  const getRing = async () => {
    let res = await fetchAllRing();
    if (res && res.data && res.data.data) {
      setListRing(res.data.data)
    }
  };

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  
  function formatName(name) {
    return name.replace(/\s*Ring$/, "");
  }

  return (<>

    {/* <div className='flex px-3 w-full items-center'> */}
 
    <div className='grid grid-cols-5 gap-1 w-full ml-3 mt-1'>
      {listRing && listRing.length > 0 &&
        listRing.filter(item => item.categoryId === 1).map((item, index) => {
          return (
            <div key={`ring-${index}`} className={clsx(style.card)} onClick={() => dispatch(addProduct(item))} >
                <img className=' mt-0 w-[100%] h-[70%] rounded-xl object-cover bg-[#ffffff1f]' src={ring} />
                <div className=' flex justify-center text-[0.7em] mt-[5px] font-normal'>{formatName(item.name)}</div>
                <div className='absolute bottom-8 w-full flex justify-center text-[0.8em] mt-[5px] font-normal'>Code: {item.code}</div>
                <div className='absolute bottom-2 w-full flex justify-center text-[#d48c20]'>{formatPrice(item.productValue)}đ</div>
            </div>
          )
        })}
         
    </div>

  
  </>
  )
}

export default Ring 



