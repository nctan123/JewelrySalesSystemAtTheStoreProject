import React from 'react'
import { Outlet } from 'react-router-dom' // giúp cho chạy đc các router con trong router cha
import { Header, SidebarLeft, SidebarRight } from '../../components'

const Public = () => {
  return (

    <div className='w-full flex h-[100vh] '>
      <div className='w-[240px]  flex-none bg-[#5D5FEF] '>
        <SidebarLeft></SidebarLeft>
      </div>

      <div className='flex-auto border overflow-y-auto'>
        <div className='h-[70px] px-[30px] flex items-center'><Header /></div>
        <div className='h-full '><Outlet /></div>
      </div>

      <div className='w-[400px] flex-none bg-[#7476f3]'>
        <SidebarRight></SidebarRight>
      </div>
    </div>


  )
}

export default Public