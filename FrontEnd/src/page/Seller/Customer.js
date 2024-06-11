import React, { useEffect, useState } from 'react'
import { fetchAllCustomer } from '../../apis/jewelryService'
import styles from '../../style/cardForList.module.css'
import clsx from 'clsx'
import { useSelector, useDispatch } from 'react-redux'
import { addCustomer } from '../../store/slice/cardSilec'


const Customer = () => {
  const dispatch = useDispatch()
  const [listCustomer, setListCustomer] = useState([])

  useEffect(() => {
    getCustomer();
  }, [])

  const getCustomer = async () => {
    let res = await fetchAllCustomer();
    if (res && res.data && res.data.data) {
      setListCustomer(res.data.data)
    }
  }
 
  return (
    <>
      <div>
     
        <button type="button" className="m-0 flex justify-center items-center bg-[#00AC7C] text-white gap-1 cursor-pointer tracking-widest rounded-md hover:bg-[#00ac7b85] duration-300 hover:gap-2 hover:translate-x-3">
          <a href='#popup1' id='openPopUp' className='p-0 flex gap-1 items-center'>
            Add
            <svg
              class="w-5 h-5"
              stroke="currentColor"
              stroke-width="1.5"
              viewBox="0 0 24 24"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M6 12 3.269 3.125A59.769 59.769 0 0 1 21.485 12 59.768 59.768 0 0 1 3.27 20.875L5.999 12Zm0 0h7.5"
                stroke-linejoin="round"
                stroke-linecap="round"
              ></path>
            </svg>
          </a>
        </button>


        <div className="w-[800px] overflow-hidden">
          <table className="font-inter w-full table-auto border-separate border-spacing-y-1 overflow-y-scroll text-left md:overflow-auto">
            <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white">
              <tr className="">
                <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36]">Customer ID</th>
                <th className="whitespace-nowrap py-3 pl-1 text-sm font-normal text-[#212B36]">First Name</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Last Name</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">PhoneNumber</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Total Point</th>
                <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Action</th>
              </tr>
            </thead>
            <tbody>
              {listCustomer && listCustomer.length > 0 && listCustomer.map((item, index) => {
                return (
                  <tr className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                    <td className=" text-sm font-normal text-[#637381]">{item.firstname}</td>
                    <td className=" text-sm font-normal text-[#637381]">{item.lastname}</td>
                    <td className=" text-sm font-normal text-[#637381]">{item.phone}</td>
                    <td className=" text-sm font-normal text-[#637381]">{item.point}</td>
                    <td className=" text-sm font-normal text-[#637381]"><button onClick={() => dispatch(addCustomer(item))} className="my-2 border border-white bg-[#4741b1d7] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none">Apply</button></td>
                  </tr>
                )
              })}
            </tbody>
          </table>
        </div>


        <div id="popup1" className={clsx(styles.overlay)}>
          <div className={clsx(styles.popup)}>
            <a className={clsx(styles.close)} href='#'>&times;</a>
            <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
              <h3 className="text-lg font-semibold text-gray-900">
                Create New Customer
              </h3>


            </div>
            {/* <!-- Modal body --> */}
            <form className="p-4 md:p-5">
              <div className="grid gap-4 mb-4 grid-cols-2">
                <div className="col-start-1">
                  <label for="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">First Name</label>
                  <input type="text" name="name" id="name" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5" placeholder="John" required="" />
                </div>
                <div className="col-start-2">
                  <label for="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Last Name</label>
                  <input type="text" name="name" id="name" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5" placeholder="Wick" required="" />
                </div>
                <div className="col-span-2 sm:col-span-1">
                  <label for="price" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Phone Number</label>
                  <input type="text" name="price" id="price" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5" placeholder="0101010101" required="" />
                </div>
                <div className="col-span-2 sm:col-span-1">
                  <label for="category" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email</label>
                  <input type="text" name="price" id="price" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5" placeholder="jewelryStore@gmail.com" required="" />
                </div>
                <div className="col-span-2 sm:col-span-1">
                  <label for="category" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Address</label>
                  <input type="text" name="price" id="price" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5" placeholder="Jewelry Store" required="" />
                </div>
                <div className="col-span-2 sm:col-span-1">
                  <label for="category" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Gender</label>
                  <select className='border border-gray-300 bg-gray-50 rounded-md w-full h-[41.6px] font-normal text-gray-400'>
                    <option>Male</option>
                    <option>Female</option>
                  </select>
                </div>

              </div>
              <button type="submit" className="text-white inline-flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                <svg className="me-1 -ms-1 w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M10 5a1 1 0 011 1v3h3a1 1 0 110 2h-3v3a1 1 0 11-2 0v-3H6a1 1 0 110-2h3V6a1 1 0 011-1z" clip-rule="evenodd"></path></svg>
                Add new customer
              </button>
            </form>
          </div>
        </div>

      </div>

    </>)
}

export default Customer