import React from 'react'
import logo from '../../assets/logo.png'
import { sidebarMenuAdmin } from '../../ultis/MenuOfAdmin/MenuAdmin'
import { NavLink } from 'react-router-dom'
import { useState } from 'react'
import './SildebarLeftMenu.css'

//const notActive = 'py-4 px-[25px] font-[300] font-sans italic flex gap-3 items-center text-white text-[14px]'
// const activeStyle = 'py-4 justify-center  w-[90%] ml-[10px] rounded-2xl font-thin font-serif italic flex gap-3 items-center text-white text-[14px] bg-[#90A0F4]'
const Sildebar = () => {
    const [isReportOpen, setIsReportOpen] = useState(false);

    const handleReportOpenToggle = () => {
        setIsReportOpen(!isReportOpen);
    }
    // className='mt-[20px] w-[60px] object-contain'
    return (
        <div className='container_sidebarleft flex flex-col'>
            <div class="w-full mb-6 h-[70px] py-[50px] flex flex-col gap-2 font-serif text-white text-[25px] justify-center items-center">
                <img className='mt-[20px] w-[60px] object-contain' src={logo} />
                <span>Jewelry Store</span>
            </div>
            <div className='flex flex-col'>
                {/* <NavLink to={'/'} className='py-8 px-[25px] font-bold rounded-md'>
          Home
        </NavLink> */}
                {sidebarMenuAdmin.map(item => (
                    <div key={item.path} className='silderleft'>

                        <NavLink
                            to={item.path}
                            end={item.end}
                            activeClassName='active'
                            onClick={item.text === 'report' ? handleReportOpenToggle : undefined}
                        >
                            {item.iconAdmin}
                            <span className='ml-4'>{item.text}</span>
                            {isReportOpen === false ? item.iconAdmin2 : item.iconAdmin3}
                        </NavLink>

                        {item.text === 'report' && isReportOpen && item.subMenu.length > 0 && (
                            <div className='dropdown'>

                                {item.subMenu.map((subItem) => (
                                    <NavLink
                                        to={subItem.path}
                                        key={subItem.path}
                                        activeClassName='active-1'
                                    >
                                        {subItem.iconAdmin}
                                        <span className='ml-4'> {subItem.text}</span>
                                    </NavLink>
                                ))}


                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div >
    )
}
export default Sildebar