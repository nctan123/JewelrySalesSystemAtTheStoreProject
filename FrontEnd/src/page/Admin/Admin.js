import React from 'react'
import Sildebar from '../../components/Admin/SildebarLeftMenu'

export default function Admin() {
    return (
        <div className='w-full flex '>
            <div className='w-[240px] h-[100vh] flex-none bg-[#5D5FEF] '>
                <Sildebar />
            </div>
        </div>
    )
}
