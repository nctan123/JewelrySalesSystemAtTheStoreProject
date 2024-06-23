import React, { useState, useEffect } from 'react'
import { toast } from 'react-toastify';
import axios from 'axios'

const Buy = () => {
  const [InvoiceCode, setInvoiceCode] = useState('');
  const [ListInvoice, setListInvoice] = useState('');

  const getInvoiceCode = async () => {
    try {
      const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/SellOrder/getbyid?id=${InvoiceCode}`);
      if (res && res.data && res.data.data && res.data.data) {
        const Invoice = res.data.data;
        if (Invoice) {
          setListInvoice(Invoice);
        } if (Invoice.length === 0) {
          toast.error('Code Invalid');
        }
      }
    } catch (error) {
      toast.error('Error fetching invoice');
    }
  };

  useEffect(() => {
    // This will run every time ListInvoice changes
    console.log(ListInvoice);
  }, [ListInvoice]);
  return (
    <div className='w-full mt-20'>
    <div className='  w-full h-fit flex justify-evenly'>
      <div class="max-w-[400px] w-full rounded-3xl flex flex-col p-6 bg-[#fefffe] bg-clip-padding backdrop-filter backdrop-blur-lg border border-gray-100 text-gray-100 drop-shadow-lg">
        <div class="flex flex-col gap-4">
          <h2 class="text-xl text-black">Check Information Product</h2>
        </div>

        <div class="flex-col flex py-4 gap-2.5">

          <div class="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center" >
            <input
              value={InvoiceCode}
              onChange={(even) => setInvoiceCode(even.target.value)}
              placeholder="Invoice Code / Warranty"
              type="text"
              class="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
            />
            <div class="text-gray-600 cursor-pointer flex items-center w-3/12 px-4 bg-transparent appearance-none border-l-2 border-gray-500 h-5/6 focus:outline-none text-lg ">
              <span className='hover:scale-75 duration-500' onClick={getInvoiceCode}>Check</span>
            </div>
          </div>
          {ListInvoice && ListInvoice.length > 0 && ListInvoice.map((item, index) => {
            return (<>
              <div class="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center" >
                <input
                  value={`${item.customer.firstname} ${item.customer.lastname}`}
                  placeholder="Customer Name"
                  class="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black hover:text"
                />
              </div>

              <div class="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center" >
                <input
                  value={item.customer.phone}
                  placeholder="Phone Number"
                  class="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
                />
              </div>
            </>)
          })}
          <div class="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center" >
            <input
              required=''
              placeholder="Enter Product Code"
              type="text"
              class="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
            />
            <div class="text-gray-600 cursor-pointer flex items-center w-3/12 px-4 bg-transparent appearance-none border-l-2 border-gray-500 h-5/6 focus:outline-none text-lg ">
              <span className='hover:scale-75 duration-500'>Check</span>
            </div>
          </div>

          <div class="m-0 p-0 h-0 flex items-center justify-start mx-2">
            <button onClick={() => toast.success('hello')} className=" absolute text-center w-10 h-10 rounded-full bg-green-600  bg-clip-padding backdrop-filter border-2 border-gray-100 text-gray-100 drop-shadow-lg hover:bg-green-600 hover:scale-105 transition-all duration-300" >
              <svg clip-rule="evenodd" fill-rule="evenodd" xmlns="http://www.w3.org/2000/svg" height="24" width="24" class="fill-gray-100 absolute top-2 left-2" >
                <path d="M21.67 3.955l-2.825-2.202.665-.753 4.478 3.497-4.474 3.503-.665-.753 2.942-2.292h-4.162c-3.547.043-5.202 3.405-6.913 7.023 1.711 3.617 3.366 6.979 6.913 7.022h4.099l-2.883-2.247.665-.753 4.478 3.497-4.474 3.503-.665-.753 2.884-2.247h-4.11c-3.896-.048-5.784-3.369-7.461-6.858-1.687 3.51-3.592 6.842-7.539 6.858h-2.623v-1h2.621c3.6-.014 5.268-3.387 6.988-7.022-1.72-3.636-3.388-7.009-6.988-7.023h-2.621v-1h2.623c3.947.016 5.852 3.348 7.539 6.858 1.677-3.489 3.565-6.81 7.461-6.858h4.047z"></path>
              </svg>
            </button>
          </div>

          <div class="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center">
            <input placeholder="Purchase Price" class="text-black w-9/12 h-14 px-4 bg-transparent focus:outline-none" />
            <div class="text-gray-600 cursor-pointer flex items-center w-3/12 px-4 bg-transparent appearance-none border-l-2 border-gray-500 h-5/6 focus:outline-none text-lg ">
              <span className='hover:scale-75 duration-500 '>Add</span>
            </div>
          </div>
        </div>

        <div class="flex flex-col items-center justify-center">
          <button type="submit" class="h-12 w-full bg-gray-900 rounded-full bg-clip-padding backdrop-filter backdrop-blur-xl bg-opacity-10 border border-gray-100 hover:bg-green-600 transition-all duration-300 hover:scale-95 text-xl shadow-md" >
            Convert
          </button>
        </div>

      </div>

      <div class="max-w-[400px] w-full rounded-3xl flex flex-col gap-5 p-6 bg-[#fefffe] bg-clip-padding backdrop-filter backdrop-blur-lg border border-gray-100 text-gray-100 drop-shadow-lg">
        <div className=' relative h-[50%] bg-[#bdb9b94b] rounded-2xl'>
          <span className='absolute top-[-13px] bg-white px-2 rounded-md shadow-[#b7aaaa] shadow-md left-5 text-black'>Information List</span>
          {ListInvoice && ListInvoice.length > 0 && ListInvoice.map((item, index) => {
            return (
              
               <div className='border-y border-gray-700 h-[90%] mx-auto w-[90%] mt-4 overflow-y-auto'>
                <h1 className='text-black'>
                {item.customerId}
                </h1>
               </div>
            )
          })}
        </div>

        <div className='relative h-[50%] bg-[#bdb9b94b] rounded-2xl'>
          <span className='absolute top-[-13px] bg-white px-2 rounded-md shadow-[#b7aaaa] shadow-md left-5 text-black'>Purchased products</span>
          <div className='border-y border-gray-700 h-[70%] mx-auto w-[90%] mt-4 overflow-y-auto'>
         
          </div>

          <div className='flex justify-center'>
                <button type="submit" class="p-0 px-3 py-1 my-0 mt-2 w-fit bg-gray-900 rounded-full bg-clip-padding backdrop-filter backdrop-blur-xl bg-opacity-10 border border-gray-100 hover:bg-green-600 transition-all duration-300 hover:scale-95 text-xl shadow-md" >
                Payment
                </button>
          </div>
        </div>
      </div>
      
    </div>
    <div class="relative max-w-[96%] mt-4 mx-auto h-16 w-full rounded-2xl p-6 bg-[#fefffe] bg-clip-padding backdrop-filter backdrop-blur-lg border border-gray-100 text-gray-100 drop-shadow-lg">
          <span className='absolute top-[-10px] bg-white border shadow px-2 rounded-md text-black'>Reason</span>
          <div className=' h-fit text-red-500 text-center'>
              Discount 70%
          </div>
    </div>
    </div>
    
  )
}

export default Buy