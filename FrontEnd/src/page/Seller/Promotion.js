import React, { useEffect, useState } from 'react'
import { fetchAllPromotion } from '../../apis/jewelryService'

const Promotion = () => {

  const [listPromotion, setListPromotion] = useState([])

  useEffect(() => {
    getPromotion();
  }, [])

  const getPromotion = async () => {
    let res = await fetchAllPromotion();
    if (res && res.data && res.data.data) {
      setListPromotion(res.data.data)
    }

  }
  return (

    <div className=''>
      <div className="w-[800px] overflow-hidden">
        <table className="font-inter w-full table-auto border-separate border-spacing-y-1 overflow-y-scroll text-left md:overflow-auto">
          <thead className="w-full rounded-lg bg-[#222E3A]/[6%] text-base font-semibold text-white">
            <tr className="">
              <th className="whitespace-nowrap rounded-l-lg py-3 pl-3 text-sm font-normal text-[#212B36]">Promotion ID</th>
              <th className="whitespace-nowrap py-3 pl-1 text-sm font-normal text-[#212B36]">Promotion</th>
              <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Start Date</th>
              <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Expiration Date</th>
              <th className="whitespace-nowrap py-3 text-sm font-normal text-[#212B36]">Action</th>
            </tr>
          </thead>
          <tbody>
            {listPromotion && listPromotion.length > 0 && listPromotion.map((item, index) => {
              return (
                <tr className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] hover:shadow-2xl">
                  <td className="rounded-l-lg py-4 pl-3 text-sm font-normal text-[#637381]">{item.id}</td>
                  <td className="px-1 py-4 text-sm font-normal text-[#637381]">{item.name}</td>
                  <td className="px-1 py-4 text-sm font-normal text-[#637381]">{item.startDate}</td>
                  <td className="px-1 py-4 text-sm font-normal text-[#637381]">{item.endDate}</td>
                  <td className="px-1 py-4 text-sm font-normal text-[#637381]"><button className="border border-white bg-[#4741b1d7] text-white px-4 py-2 rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none">Apply</button></td>
                </tr>
              )
            })}
            

          </tbody>
        </table>
      </div>
    </div>

  )
}

export default Promotion