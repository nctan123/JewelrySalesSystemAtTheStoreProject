import React, { useEffect } from 'react'
import { Outlet, useNavigate, useLocation } from 'react-router-dom' // include useLocation for getting current pathname
import { Header, SidebarLeft, SidebarRight,SidebarForBuy } from '../../components'
import style from '../../style/scroll.module.css'

const Public = () => {
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    const Authorization = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    if (!Authorization || role !== 'seller') {
      navigate('/login');
    }
  }, [navigate]);

  // Check if the current route is either /promotion or /return_ex
  const hideSidebarRight = location.pathname.includes('/promotion') || location.pathname.includes('/purchase') || location.pathname.includes('/searchInvoice') ;
  const activeSidebarForBuy = location.pathname.includes('/purchase/buyIn') 
  return (
<<<<<<< HEAD


    <div className='w-full flex h-[100vh] '>
      <div className='w-[240px] overflow-y-scroll flex-none bg-[#5D5FEF]'>
        <SidebarLeft></SidebarLeft>
      </div>


      <div className='flex-auto border'>
        <div className='h-[95%] flex justify-center'><Outlet /></div>
=======
    <div className='w-full flex h-[100vh]'>
      <div className='w-[240px] overflow-y-scroll flex-none bg-[#5D5FEF]'>
        <SidebarLeft />
      </div>

      <div className='flex-auto border'>
        <div className='h-[95%] flex justify-center w-full'><Outlet/></div>
>>>>>>> FE_Giang
      </div>

      {!hideSidebarRight && (
        <div className='w-[400px] flex-none bg-[#56565821]'>
          <SidebarRight />
        </div>
      )}
      {activeSidebarForBuy && (
        <div className='w-[400px] flex-none bg-[#56565821]'>
          <SidebarForBuy />
        </div>
      )}
      
    </div>
  )
}
<<<<<<< HEAD
// border-[#5D5FEF]
export default Public
=======

export default Public
>>>>>>> FE_Giang
