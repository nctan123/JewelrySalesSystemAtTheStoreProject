import React, { useState, useEffect } from 'react';
import { toast } from 'react-toastify';
import axios from 'axios';
import { MdDeleteOutline } from "react-icons/md";

const Buy = () => {
  const [InvoiceCode, setInvoiceCode] = useState('');
  const [ListInvoice, setListInvoice] = useState(null); // Initialize as null
  const [selectedProduct, setSelectedProduct] = useState(null); // State to store selected product

  const getInvoiceCode = async () => {
    try {
      console.log(`Fetching invoice for code: ${InvoiceCode}`); // Add logging
      const res = await axios.get(`https://jssatsproject.azurewebsites.net/api/BuyOrder/CheckOrder?orderCode=${InvoiceCode}`);
      console.log('API response:', res); // Add logging

      if (res && res.data) {
        const Invoice = res.data;
        console.log('Invoice data:', Invoice); // Add logging

        if (Invoice && typeof Invoice === 'object') {
          setListInvoice(Invoice);
          console.log('ListInvoice state set:', Invoice); // Add logging
        } else {
          toast.error('Invalid invoice data');
        }
      } else {
        toast.error('Invalid response structure');
      }
    } catch (error) {
      console.error('Error fetching invoice:', error); // Add logging
      toast.error('Error fetching invoice');
    }
  };

  const handleProductSelection = (product) => {
    setSelectedProduct(product);
    // Update Purchase Price with Estimate Buy Price if product is not null
    if (product) {
      document.getElementById('purchasePrice').value = product.estimateBuyPrice;
      // Update Reason with ReasonForEstimateBuyPrice if product is not null
      document.getElementById('reason').innerText = product.reasonForEstimateBuyPrice;
    }
  };

  useEffect(() => {
    console.log('ListInvoice state updated:', ListInvoice); // Add logging
  }, [ListInvoice]);

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  return (
    <div className='w-full mt-20'>
      <div className='w-full h-fit flex justify-evenly'>
        <div className="max-w-[330px] w-full rounded-3xl flex flex-col p-6 bg-[#fefffe] bg-clip-padding backdrop-filter backdrop-blur-lg border border-gray-100 text-gray-100 drop-shadow-lg">
          <div className="flex justify-between items-center gap-4">
            <h2 className="text-xl text-black">Check Information Product</h2>
            <p className='cursor-pointer' onClick={() => { setListInvoice(null); setInvoiceCode(''); }}>
              <MdDeleteOutline size='17px' color='#ef4e4e' />
            </p>
          </div>

          <div className="flex-col flex py-4 gap-2.5">
            <div className="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center">
              <input
                value={InvoiceCode}
                onChange={(event) => setInvoiceCode(event.target.value)}
                placeholder="Invoice Code / Warranty"
                type="text"
                className="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
              />
              <div className="text-gray-600 cursor-pointer flex items-center w-3/12 px-4 bg-transparent appearance-none border-l-2 border-gray-500 h-5/6 focus:outline-none text-lg">
                <span onClick={getInvoiceCode} className='hover:scale-75 duration-500'>Check</span>
              </div>
            </div>
            {ListInvoice && (
              <>
                <div className="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center">
                  <input
                    value={ListInvoice.customerName}
                    placeholder="Customer Name"
                    className="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
                    readOnly
                  />
                </div>
                <div className="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center">
                  <input
                    value={ListInvoice.customerPhoneNumber}
                    placeholder="Phone Number"
                    className="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
                    readOnly
                  />
                </div>
              </>
            )}

            <div className="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center">
              <input
                id="purchasePrice" // Added ID for Purchase Price input
                placeholder="Enter Product Code"
                type="text"
                className="w-9/12 h-14 px-4 bg-transparent focus:outline-none text-black"
              />
              <div className="text-gray-600 cursor-pointer flex items-center w-3/12 px-4 bg-transparent appearance-none border-l-2 border-gray-500 h-5/6 focus:outline-none text-lg">
                <span className='hover:scale-75 duration-500' onClick={() => handleProductSelection(selectedProduct)}>Check</span>
              </div>
            </div>
            <div className="m-0 p-0 h-0 flex items-center justify-start mx-2">
              <button
                onClick={() => toast.success('hello')}
                className="absolute text-center w-10 h-10 rounded-full bg-green-600 bg-clip-padding backdrop-filter border-2 border-gray-100 text-gray-100 drop-shadow-lg hover:bg-green-600 hover:scale-105 transition-all duration-300"
              >
                <svg clip-rule="evenodd" fill-rule="evenodd" xmlns="http://www.w3.org/2000/svg" height="24" width="24" className="fill-gray-100 absolute top-2 left-2">
                  <path d="M21.67 3.955l-2.825-2.202.665-.753 4.478 3.497-4.474 3.503-.665-.753 2.942-2.292h-4.162c-3.547.043-5.202 3.405-6.913 7.023 1.711 3.617 3.366 6.979 6.913 7.022h4.099l-2.883-2.247.665-.753 4.478 3.497-4.474 3.503-.665-.753 2.884-2.247h-4.11c-3.896-.048-5.784-3.369-7.461-6.858-1.687 3.51-3.592 6.842-7.539 6.858h-2.623v-1h2.621c3.6-.014 5.268-3.387 6.988-7.022-1.72-3.636-3.388-7.009-6.988-7.023h-2.621v-1h2.623c3.947.016 5.852 3.348 7.539 6.858 1.677-3.489 3.565-6.81 7.461-6.858h4.047z"></path>
                </svg>
              </button>
            </div>
            <div className="w-12/12 h-14 flex rounded-lg border-2 border-solid border-gray-100 items-center">
              <div id="reason" className="text-black w-9/12 h-14 px-4 bg-transparent focus:outline-none">

              </div> {/* Added div for Reason */}
              <div className="text-gray-600 cursor-pointer flex items-center w-3/12 px-4 bg-transparent appearance-none border-l-2 border-gray-500 h-5/6 focus:outline-none text-lg">
                <span className='hover:scale-75 duration-500'>Add</span>
              </div>
            </div>
          </div>
          <div className="flex flex-col items-center justify-center">
            <button
              type="submit"
              className="h-12 w-full bg-gray-900 rounded-full bg-clip-padding backdrop-filter backdrop-blur-xl bg-opacity-10 border border-gray-100 hover:bg-green-600 transition-all duration-300 hover:scale-95 text-xl shadow-md"
            >
              Convert
            </button>
          </div>
        </div>
        <div className="max-w-[500px] w-full rounded-3xl flex flex-col gap-5 p-6 bg-[#fefffe] bg-clip-padding backdrop-filter backdrop-blur-lg border border-gray-100 text-gray-100 drop-shadow-lg">
          <div className='relative h-[50%] bg-[#bdb9b94b] rounded-2xl'>
            <span className='absolute top-[-13px] bg-white px-2 rounded-md shadow-[#b7aaaa] shadow-md left-5 text-black'>Information List</span>
            <div className='border-y border-gray-700 h-[70%] mx-auto w-[90%] mt-4 overflow-y-auto'>
              {ListInvoice && ListInvoice.products && ListInvoice.products.map((product, index) => (
                <div key={index} className='flex justify-between items-center py-2 border-b border-gray-300 cursor-pointer' onClick={() => handleProductSelection(product)}>
                  <div className='text-black font-semibold'>{product.code}</div>
                  <div className='text-black font-light'>{product.name}</div>
                  <div className='text-black'>{formatPrice(product.priceInOrder)}</div>
                </div>
              ))}
            </div>
          </div>
          <div className='relative h-[50%] bg-[#bdb9b94b] rounded-2xl'>
            <span className='absolute top-[-13px] bg-white px-2 rounded-md shadow-[#b7aaaa] shadow-md left-5 text-black'>Purchased products</span>
            <div className='border-y border-gray-700 h-[70%] mx-auto w-[90%] mt-4 overflow-y-auto'>
            </div>
            <div className='flex justify-center'>
              <button
                type="submit"
                className="p-0 px-3 py-1 my-0 mt-2 w-fit bg-gray-900 rounded-full bg-clip-padding backdrop-filter backdrop-blur-xl bg-opacity-10 border border-gray-100 hover:bg-green-600 transition-all duration-300 hover:scale-95 text-xl shadow-md"
              >
                Payment
              </button>
            </div>
          </div>
        </div>
      </div>
      <div className="relative max-w-[96%] mt-4 mx-auto h-16 w-full rounded-2xl p-6 bg-[#fefffe] bg-clip-padding backdrop-filter backdrop-blur-lg border border-gray-100 text-gray-100 drop-shadow-lg">
        <span className='absolute top-[-10px] bg-white border shadow px-2 rounded-md text-black'>Reason</span>
        <div className='h-fit text-red-500 text-center'>
  
        </div>
      </div>
    </div>
  );
}

export default Buy;
