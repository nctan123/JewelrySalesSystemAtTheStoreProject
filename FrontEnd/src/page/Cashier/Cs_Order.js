import React,{ useState } from 'react'
import {NavLink,Outlet } from 'react-router-dom'
import { menuOrder } from '../../ultis/menuForOrder'

const notActive =
  // ' px-[25px] font-[300] font-sans italic flex items-center text-white text-[14px] border border-black';
  ' justify-center  w-[90%] ml-[10px] rounded-md font-thin font-serif italic flex items-center text-white text-[14px] bg-[#3f6d67e9]';

const activeStyle =
  ' justify-center  w-[90%] ml-[10px] rounded-md font-thin font-serif italic flex items-center text-white text-[14px] bg-[#3f6d676e] ';

const Cs_Order = () => {
  const [isOrderSubmenuOpen, setIsOrderSubmenuOpen] = useState(false);

  const handleOrderSubmenuToggle = () => {
    setIsOrderSubmenuOpen(!isOrderSubmenuOpen);
  };
  return (
    <div className=''>
      <div className='flex'>
      {menuOrder.map((item) => (
        <NavLink
        to={item.path}
        className={({ isActive }) =>`flex items-center p-0 text-white transition-colors ${isActive ? activeStyle: notActive}`}
        >
        <span className='w-[100px] flex justify-center py-2 rounded-md'>{item.text}</span>
        </NavLink>
      ))}
      </div>
      <div className=''><Outlet /></div>
    </div>

  )
}

export default Cs_Order