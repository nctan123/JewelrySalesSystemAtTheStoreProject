import React from 'react'
import logo from '../assets/logo.png'
import { sidebarMenu } from '../ultis/menu'
import { NavLink } from 'react-router-dom'
import { hover } from '@testing-library/user-event/dist/hover'
import { useState } from 'react'

const notActive = 'py-4 px-[25px] font-[300] font-sans italic flex gap-3 items-center text-white text-[14px]'
const activeStyle = 'py-4 justify-center  w-[90%] ml-[10px] rounded-2xl font-thin font-serif italic flex gap-3 items-center text-white text-[14px] bg-[#90A0F4]'

const notActiveJew = 'py-2  font-[300] font-sans italic flex gap-3 items-center text-white text-[14px]'
const activeStyleJew = 'py-1 gap-4 w-[94%] rounded-xl font-thin font-serif italic flex  text-white text-[14px] bg-[#33333330] mt-[10px]'

const Sildebar = () => {
  const [isJewelrySubmenuOpen, setIsJewelrySubmenuOpen] = useState(false);
const handleJewelrySubmenuToggle = () => {
  setIsJewelrySubmenuOpen(!isJewelrySubmenuOpen);
};
const handleMenuItemClick = (item) => {
  if (item.text !== 'Jewelry') {
    setIsJewelrySubmenuOpen(false);
  }
  // Xử lý sự kiện nhấp vào menu item ở đây
};

  return (
    <div className='container_sidebarleft flex flex-col'>
      <div className='w-full mb-6 h-[70px] py-[50px] flex flex-col gap-2 font-serif text-white text-[25px] justify-center items-center'>
        <img className='mt-[20px] w-[60px] object-contain' src={logo} alt='logo' />
        <span >Jewelry Store</span>
      </div>
      <div className='flex flex-col'>
      {sidebarMenu.map(item => (
      <div key={item.path}>
        <NavLink
          to={item.path}
          end={item.end}
          className={({ isActive }) =>`flex items-center p-4 text-white transition-colors ${isActive ?activeStyle : notActive}`}
          onClick={item.text === 'Jewelry' ? handleJewelrySubmenuToggle : () => handleMenuItemClick(item)}>
          {item.icons}
          <span className="ml-4">{item.text}</span>
          
        </NavLink>
        {item.text === 'Jewelry' && isJewelrySubmenuOpen && (
          <div className="pl-8">
            {item.subMenu.map(subItem => (
              <NavLink
                to={subItem.path}
                key={subItem.path}
                className={({ isActive }) =>`flex items-center p-4 text-white transition-colors ${isActive ? activeStyleJew : notActiveJew}`}>
                {subItem.icons}
                <span className="ml-4">{subItem.text}</span>
              </NavLink>
            ))}
          </div>
        )}
      </div>
    ))}
      </div>
    </div>
  )
}

export default Sildebar