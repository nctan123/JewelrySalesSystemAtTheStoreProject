import React, { useEffect, useState } from 'react'
import { fetchAllInvoice } from '../../apis/jewelryService'

const SearchInvoice = () => {
  const [listInvoice, setListInvoice] = useState([])

  useEffect(() => {
    getInvoice();
  }, [])

  const getInvoice = async () => {
    let res = await fetchAllInvoice();
    if (res && res.data && res.data.data) {
      setListInvoice(res.data.data)
    }

  }
  return (
    <div className=''>
    <div className="w-[800px] overflow-hidden">
      <table className="font-inter w-full table-auto border-separate border-spacing-y-1 overflow-y-scroll text-left md:overflow-auto">
        <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white">
          <tr className="">
            <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36]">Invoice ID</th>
            <th className="whitespace-nowrap py-3 pl-1 text-sm font-normal text-[#212B36]">Customer ID</th>
            <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Total Amount</th>
            <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Create Date</th>
            <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Action</th>
          </tr>
        </thead>
        <tbody>
          {listInvoice && listInvoice.length > 0 && listInvoice.map((item, index) => {
            return (
              <tr className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                <td className="rounded-l-lg py-4 pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                <td className="px-1 py-4 text-sm font-normal text-[#637381]">{item.first_name}</td>
                <td className="px-1 py-4 text-sm font-normal text-[#637381]">1.000.000</td>
                <td className="px-1 py-4 text-sm font-normal text-[#637381]">20/03/2023,01:10</td>
                <td className="px-1 py-4 text-sm font-normal text-[#637381]"><button className="border border-white bg-[#4741b1d7] text-white px-4 py-2 rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none">View</button></td>
              </tr>
            )
          })}
        </tbody>
      </table>
    </div>
  </div>

  )
}

export default SearchInvoice