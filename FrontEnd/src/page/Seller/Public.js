import React from 'react'
import { Outlet } from 'react-router-dom' // giúp cho chạy đc các router con trong router cha
import { Header, SidebarLeft, SidebarRight } from '../../components'
// import Home from './Home'
 import style from '../../style/scroll.module.css'

const Public = () => {
  return (
    
    <div className='w-full flex h-[100vh] '> 
        <div className='w-[240px] overflow-y-scroll flex-none bg-[#5D5FEF] '>
            <SidebarLeft></SidebarLeft>
        </div>

        <div className='flex-auto border '>
          <div className='h-[70px] px-[30px] flex items-center mb-2'><Header/></div>
          <div className='h-[90%] flex justify-center overflow-y-auto'><Outlet/></div>
        </div>

        <div className='w-[400px] flex-none bg-[#56565821]'>
            <SidebarRight></SidebarRight>
        </div>
    </div>


  )
}

export default Public