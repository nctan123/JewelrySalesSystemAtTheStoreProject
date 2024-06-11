import React from 'react'
import Sildebar from '../../components/Admin/SildebarLeftMenu'
import { RiAccountCircleLine } from "react-icons/ri";
import { Outlet } from 'react-router-dom'
import { Header } from '../../components';
export default function Admin() {
    return (
        <>
<<<<<<< HEAD
            <div className='w-full flex h-[100vh] '>
=======
            <div className='w-full flex '>
>>>>>>> develop
                <div className='w-[240px] h-[100vh] flex-none bg-[#5D5FEF] '>
                    <Sildebar />

                </div>
                <div className='flex-auto border overflow-y-auto'>
                    <div className='h-full '>
                        <div className='w-full flex justify-end space-x-2 px-[30px] py-[15px]'>
                            <RiAccountCircleLine className='text-2xl text-gray-600' />
                            <span className='text-gray-600 font-medium'>Admin</span>
                        </div>
                        <div className='p-[20px]'> <Outlet /></div>



                    </div>

                </div>


            </div>

        </>
    )
}