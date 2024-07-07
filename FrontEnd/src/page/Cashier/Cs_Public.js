import React, { useEffect } from 'react'
import Cs_SidebarLeft from './Cs_SidebarLeft'
import { Outlet, useNavigate } from 'react-router-dom'
import { Header } from '../../components'

const Cs_Public = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const Authorization = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    if (!Authorization || role !== 'cashier') {
      navigate('/login');
    }
  }, [navigate]);
  return (
    <div className='w-full flex h-[100vh] '>
      <div className='w-[240px] overflow-y-scroll flex-none bg-[#5D5FEF] '>
        <Cs_SidebarLeft />
      </div>

      <div className='flex-auto border bg-[#b9a6a663] '>
        <div className='h-[90%] flex justify-start'><Outlet /></div>
      </div>

    </div>
  )
}

export default Cs_Public