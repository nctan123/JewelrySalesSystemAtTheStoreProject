import React from 'react'
import Sildebar from '../../components/Admin/SildebarLeftMenu'
import { RiAccountCircleLine } from "react-icons/ri";
<<<<<<< HEAD
import { Outlet } from 'react-router-dom'
import { Header } from '../../components';
=======
import { Outlet, useNavigate } from 'react-router-dom';

>>>>>>> FE_Tai
export default function Admin() {
    return (
        <>
<<<<<<< HEAD

            <div className='w-full flex h-[100vh] '>

                <div className='w-[240px] h-[100vh] flex-none bg-[#5D5FEF] '>
=======
            <div className='w-full flex h-[100vh] bg-gray-100'>
                <div className='w-[240px] h-[100vh] flex-none bg-white overflow-y-auto z-20 border border-gray-300'>
>>>>>>> FE_Tai
                    <Sildebar />

                </div>
<<<<<<< HEAD
                <div className='flex-auto border overflow-y-auto'>
                    <div className='h-full '>
                        <div className='w-full flex justify-end space-x-2 px-[30px] py-[15px]'>
                            <RiAccountCircleLine className='text-2xl text-gray-600' />
                            <span className='text-gray-600 font-medium'>Admin</span>
                        </div>
                        <div className='p-[20px]'> <Outlet /></div>



=======
                <div className='flex-auto border overflow-y-auto '>
                    <div className=''>
                        <div className='fixed top-0 left-0 w-full flex justify-end space-x-2 px-[30px] py-[5px] bg-white border border-gray-300 z-10'>
                            <RiAccountCircleLine className='text-2xl text-blue-800' />
                            <span className='text-blue-800 text-lg font-medium'>{localStorage.getItem('name')} (Admin)</span>
                        </div>
                        <div className='pt-[60px]'>
                            <Outlet />
                        </div>
>>>>>>> FE_Tai
                    </div>

                </div>


            </div>

        </>
    )
}