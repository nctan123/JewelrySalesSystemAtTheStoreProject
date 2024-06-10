import React from 'react'
import icons from '../ultis/icon'
// import { Jewelry } from '../page/public/Jewelry'
const {CiSearch} = icons
const Search = () => {
  return (
    // <div className='w-full flex items-center'>
    //     <span className=' px-2 py-2 rounded-l-2xl bg-[#dcd5d579] text-gray-500'>
    //        <CiSearch size='24'/>
    //     </span>
    //     <input type='text' 
    //     className='outline-none w-full bg-[#dcd5d579] py-3 rounded-r-2xl h-[40px] text-gray-500'
    //     placeholder='Search item ...'
    //     />
    // </div>
    <form class="max-w-md mx-auto">   
    <label for="default-search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
    <div class="relative">
        <div class="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
            <svg class="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20">
                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"/>
            </svg>
        </div>
        <input type="search" id="default-search" class="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500" placeholder="Search Item, ID in here..." required />
        <button type="submit" class="text-white absolute end-2.5 bottom-[-10px] bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2">Search</button>
    </div>
</form>
  )
}

export default Search