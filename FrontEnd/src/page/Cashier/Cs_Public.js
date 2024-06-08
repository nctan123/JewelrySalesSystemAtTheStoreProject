import React from 'react'
import Cs_SidebarLeft from './Cs_SidebarLeft'
import { Outlet } from 'react-router-dom'
import { Header} from '../../components'

const Cs_Public = () => {
  return (
    <div className='w-full flex h-[100vh] '>
      <div className='w-[240px] overflow-y-scroll flex-none bg-[#5D5FEF] '>
        <Cs_SidebarLeft/>
      </div>

      <div className='flex-auto border bg-[#857e7e25] '>
      <div className='h-[70px] px-[30px] flex items-center mb-2'><Header/></div>
      <div className='h-[90%] flex justify-center overflow-y-auto'><Outlet/></div>
      </div>
    
    </div>
  )
}

export default Cs_Public