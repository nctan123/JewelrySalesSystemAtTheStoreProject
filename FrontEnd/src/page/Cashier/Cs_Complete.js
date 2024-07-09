import React, { useState, useEffect } from 'react'
<<<<<<< HEAD
import styles from '../../style/cardForList.module.css'
import clsx from 'clsx'
import { BsCash } from "react-icons/bs";

const Cs_Complete = () => {
  const [currentTime, setCurrentTime] = useState(new Date());

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentTime(new Date());
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  const [display, setDisplay] = useState('0');

  function updateDisplay(value) {
    let currentValue = document.getElementById('display').value;

    // Check if the last character in the current value is an operator
    const lastCharIsOperator = ['+', '-', '*', '/', '.'].includes(currentValue.slice(-1));

    // Check if the current value is '0' and the entered value is an operator
    if (currentValue === '0') {
        if (['+', '-', '*', '/', '.'].includes(value)) {
            document.getElementById('display').value = '0' + value;
        }else{
            document.getElementById('display').value = value;
        }
    } else if (lastCharIsOperator && ['+', '-', '*', '/', '.'].includes(value)) {
        // If the last character is an operator and the entered value is an operator, do nothing
        return;
    } else {
        document.getElementById('display').value += value;
    }
}

function clearDisplay() {
    // Clear the input display and set it to '0'
    document.getElementById('display').value = '0';
}

function calculate() {
    try {
        // Get the current expression in the display
        let expression = document.getElementById('display').value;

        // Evaluate the expression using JavaScript eval function
        let result = eval(expression);

        // Update the display with the result
        document.getElementById('display').value = result;
    }
    catch(err) {
        document.getElementById('display').value = "Invalid Input";
    }
}

  return (<>
    <div className='flex justify-center '>
      <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[33em] bg-[#e9ddc26d] mt-[20px]'>
        <div className='flex justify-end px-[15px] text-black font-thin'>{currentTime.toLocaleString()}</div>
        <div className='flex justify-start px-[15px] text-black'>
          <p className='font-light'>Name:</p>
          <span className='w-full flex justify-center font-serif'>Truong Giang</span>
        </div>
        <div className='flex  px-[15px] text-black'>
          <p className='w-[260px] font-light '>ID Invoice:</p>
          <span className='w-full flex font-serif'>I_00001</span>
        </div>
        <div className='grid grid-cols-3 border-x-0 border-t-0 border mx-[10px] border-b-black pb-[2px]'>
          <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
          <div className='col-start-3 ml-6 flex justify-start'>Price</div>
        </div>
        <div id='screenSeller' className='grid-cols-3 h-[45%] overflow-y-auto'>

        </div>
        <div className='border border-x-0 border-b-0 mx-[15px] border-t-black py-2'>
          <div className='font-bold'>PAYMENT</div>
        </div>
        <div className='px-[15px] grid grid-cols-2 grid-rows-3 pb-2 '>
          <div className='row-start-1 font-thin'>Subtotal:</div>
          <div className='col-start-2 flex justify-end'>9.000.000</div>
          <div className='row-start-2 font-thin'>Tax:</div>
          <div className='col-start-2 flex justify-end'>1.000.000</div>
          <div className='row-start-3 font-thin'>Point:</div>
          <div className='col-start-2 flex justify-end text-red-500'>1.000.000</div>
        </div>
        <div className='bg-[#87A89E] h-[50px] grid grid-cols-2 '>
          <div className='mx-[15px] flex items-center font-bold text-lg'>9.000.000 <span>.đ</span></div>
          <div className='col-start-2 flex justify-end items-center mr-[15px]'>
            <a href='#popup1' className=" m-0 border border-[#ffffff] bg-[#3f6d67] text-white px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Pay Bill</a>
          </div>
        </div>
      </div>

      <div id="popup1" className={clsx(styles.overlay)}>
        <div className={clsx(styles.popup)} style={{ width: '50%' }}>
          <a className={clsx(styles.close)} href='#'>&times;</a>
          <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
            <h3 className="text-lg font-semibold text-gray-900">
              Payment
            </h3>
          </div>
          {/* <!-- Modal body --> */}
          <form className="p-4 md:p-5">
            <div className="grid gap-4 grid-cols-2">
              <div className='row-start-1 col-start-1 h-[100px]'>
                <h5 className='font-bold '>Customer Info</h5>
                <div id='inforCustomer' className='text-[12px]'>
                  <p className='bg-gray-100 px-2 py-1 rounded-md mb-1'>Name: Nguyen Giang</p>
                  <p className='bg-gray-100 px-2 py-1 rounded-md'>ID Invoice: I_0001</p>
                </div>
              </div>
              <div className='row-start-1 col-start-2 h-[100px]'>
                <h5 className='font-bold'>Select a payment method</h5>
                <select class="mt-[10px] w-[100%] px-2 bg-gray-100 text-gray-800 border-0 rounded-md p-2 mb-4 focus:bg-gray-200 focus:outline-none focus:ring-1 focus:ring-blue-500 transition ease-in-out duration-150">
                  <option><BsCash/> Cash</option>
                  <option>Visa</option>
                </select>
              </div>
            </div>
            <div className="grid gap-4 grid-cols-2">
              <div className='col-start-1 rounded-md bg-gray-100 p-[10px] relative'>
                <h3 className='font-bold border border-x-0 border-t-0 border-black'>Transaction Details</h3>
                <div className='overflow-y-scroll'>
                  {/* Hiển thị thông tin */}
                  Screen
                </div>
                <div className='absolute bottom-5 w-[95%] border border-x-0 border-b-0 border-black grid grid-cols-2 grid-rows-3'>
                  <div className='row-start-1 col-start-1 font-thin'>Item</div>
                  <div className='row-start-1 col-start-2 flex justify-end mr-5'>$</div>
                  <div className='row-start-2 col-start-1 font-thin'>Tax(5%)</div>
                  <div className='row-start-2 col-start-2 flex justify-end mr-5'>$</div>
                  <div className='row-start-3 col-start-1 font-bold'>Total</div>
                  <div className='row-start-3 col-start-2 flex justify-end mr-5'>$</div>
                </div>
              </div>
              {/* <div className='col-start-2 rounded-md bg-gray-100 p-[10px] overflow-y-scroll'>
                

              </div> */}
              {/* <div class="h-screen flex flex-col justify-center items-center col-start-2 overflow-y-scroll "> */}
                <div class="w-[22rem] h-[500px] bg-[#f6f8f9] rounded-3xl p-4 shadow-2xl dark:bg-[#17181a]">
                  <div class="h-[100px]">
                    <textarea type="text" id="display" class="h-full w-full text-3xl font-bold bg-transparent border border-none outline-none resize-none dark:text-white" disabled>0</textarea>
                  </div>

                  <div class="flex space-x-2 mt-3">
                    <input type="button" onClick={() => clearDisplay()} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#2d191e] dark:text-red-500" value="C" />
                    <input type="button" onClick={() => updateDisplay('(')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="(" />
                    <input type="button" onClick={() => updateDisplay(')')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value=")" />
                    <input type="button" onClick={() => updateDisplay('/')} class="w-20 h-14 text-white bg-[#ff9500] hover:bg-[#e68600] shadow-md font-bold py-1 px-2 rounded-2xl" value="/" />
                  </div>

                  <div class="flex space-x-2 mt-3">
                    <input type="button" onClick={() => updateDisplay('7')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="7" />
                    <input type="button" onClick={() => updateDisplay('8')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="8" />
                    <input type="button" onClick={() => updateDisplay('9')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="9" />
                    <input type="button" onClick={() => updateDisplay('+')} class="w-20 h-14 text-white bg-[#ff9500] hover:bg-[#e68600] shadow-md font-bold py-1 px-2 rounded-2xl" value="+" />
                  </div>

                  <div class="flex space-x-2 mt-3">
                    <input type="button" onClick={() => updateDisplay('4')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="4" />
                    <input type="button" onClick={() => updateDisplay('5')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="5" />
                    <input type="button" onClick={() => updateDisplay('6')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="6" />
                    <input type="button" onClick={() => updateDisplay('*')} class="w-20 h-14 text-white bg-[#ff9500] hover:bg-[#e68600] shadow-md font-bold py-1 px-2 rounded-2xl" value="x" />
                  </div>

                  <div class="flex space-x-2 mt-3">
                    <input type="button" onClick={() => updateDisplay('3')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="3" />
                    <input type="button" onClick={() => updateDisplay('2')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="2" />
                    <input type="button" onClick={() => updateDisplay('1')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="1" />
                    <input type="button" onClick={() => updateDisplay('-')} class="w-20 h-14 text-white bg-[#ff9500] hover:bg-[#e68600] shadow-md font-bold py-1 px-2 rounded-2xl" value="-" />
                  </div>

                  <div class="flex space-x-2 mt-3">
                    <input type="button" onClick={() => updateDisplay('0')}class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="" />
                    <input type="button" onClick={() => updateDisplay('0')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="0" />
                    <input type="button" onClick={() => updateDisplay('.')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="." />
                    <input type="button" onClick={() => calculate()} class="w-20 h-14 text-white bg-green-500 hover:bg-green-600 shadow-md font-bold py-1 px-2 rounded-2xl" value="=" />
                  </div>
                </div>
              {/* </div> */}
            </div>
            <div className=''>

            </div>
            <div className=''>

            </div>

            <button type="submit" className="text-white flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-10 py-4 text-center">
              Pay Now
            </button>
          </form>
        </div>
      </div>
    </div>
=======
import { fetchPaymentMethod, fetchStatusInvoice } from '../../apis/jewelryService'
import Popup from 'reactjs-popup';
import QRCode from "react-qr-code";
import SignatureCanvas from 'react-signature-canvas'
import axios from 'axios';
import { toast } from 'react-toastify';
import { current } from '@reduxjs/toolkit';
import { format, parseISO } from 'date-fns';
import ReactPaginate from 'react-paginate';
import { AiFillLeftCircle, AiFillRightCircle } from 'react-icons/ai';
import { IconContext } from 'react-icons';

const FormatDate = ({ isoString }) => {
  // Cs_WaitingPayment
  const parsedDate = parseISO(isoString);
  const formattedDate = format(parsedDate, 'HH:mm yyyy-MM-dd');
  return (
    <div>
      <p>{formattedDate}</p>
    </div>
  );
};
const Cs_Complete = () => {
  const [currentTime, setCurrentTime] = useState(new Date().toISOString());
  const [listInvoice, setlistInvoice] = useState([]); // list full invoice
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [IdOrder, setIdOrder] = useState('')
  const [ChosePayMethodID, setChosePayMethodID] = useState(3);
  const [PaymentID, setPaymentID] = useState();
  const [totalProduct, setTotalProduct] = useState(0);
  const [totalPage, setTotalPage] = useState(0);


  const handlePageClick = event => {
    getInvoice(event.selected + 1);
  };

  useEffect(() => {
    getInvoice(1);
    console.log(listInvoice)
  }, []);

  const getInvoice = async (page) => {
    try {
      let res = await fetchStatusInvoice('completed', page);
      if (res?.data?.data) {
        setlistInvoice(res.data.data);
        setTotalProduct(res.data.totalElements);
        setTotalPage(res.data.totalPages);
      }
    } catch (error) {
      console.error('Error fetching orders:', error);
    }
  };

  useEffect(() => {
    const interval = setInterval(() => {
      setcreateDate(new Date());
    }, 1000);
    return () => clearInterval(interval);
  }, []);

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }

  const [createDate, setcreateDate] = useState(new Date().toISOString())

  const handleComplete = () => {
    
  }



  function calculateTotalPromotionValue(item) {
    return item.sellOrderDetails.reduce((total, orderDetail) => {
      return total + (orderDetail.unitPrice * orderDetail.promotionRate);
    }, 0);
  }
  return (<>
    <div>
      <form className="max-w-md mx-auto">
        <div className="relative">
          <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
            <svg
              className="w-4 h-4 text-gray-500 dark:text-gray-400"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 20 20"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
              />
            </svg>
          </div>
          <input
            type="search"
            id="default-search"
            className="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500"
            placeholder="Search Item, ID in here..."
            required
            // value={searchTerm}
            // onChange={handleSearch}
          />
        </div>
      </form>
      <div className='my-0 mx-auto mt-2'>
        <div className='grid grid-cols-4 w-full px-10 overflow-y-auto h-[78vh]'>
          {listInvoice && listInvoice.length > 0 && listInvoice.map((item, index) => (
            <div key={index} className='shadow-md shadow-gray-600 pt-[10px] rounded-2xl w-[93%] h-[29em] bg-[#ffff] mb-[20px]'>
              <div className='flex flex-col justify-between px-[15px] text-black font-thin'>
                <span className='flex justify-end font-thin italic'><FormatDate isoString={item.createDate} /></span>
              </div>
              <div className='text-[15px]'>
                <div className='flex px-[15px] gap-3 '>
                  <span className='font-serif'>Code:</span>
                  <span className='font-thin'>{item.code}</span>
                  <span className='font-serif'>-</span>
                  <span className='font-thin'>{item.id}</span>
                </div>
                <div className='flex justify-start px-[15px] text-black'>
                  <input hidden className='bg-[#e9ddc200] text-center font-thin' value={item.customerId} readOnly />
                </div>
                <div className='flex justify-start px-[15px] text-black'>
                  <p className='font-serif w-full'>Customer Name: </p>
                  <span className='w-full flex justify-center font-thin'>{item.customerName}</span>
                </div>
                <div className='flex  px-[15px] text-black'>
                  <p className='w-[260px] font-serif '>Staff Name:</p>
                  <span className='w-full flex font-thin'>{item.staffName}</span>
                </div>
              </div>
              <div className='grid grid-cols-3 border-x-0 font-extralight italic border-t-0 border mx-[10px] border-b-black pb-[2px]'>
                <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
                <div className='col-start-3 ml-6 flex justify-start'>Price</div>
              </div>
              <div id='screenSeller' className='grid-cols-3 h-[45%] overflow-y-auto'>
                {item.sellOrderDetails.map((orderDetail, index) => (
                  <div key={index} className='grid grid-cols-3 mx-[10px] border-b-black pb-[2px]'>
                    <div className='col-start-1 col-span-2 flex pl-[5px] items-center text-[12px]'>{orderDetail.productName}</div>
                    <div className='col-start-3 gap-1 flex justify-end text-[12px]'>
                      <span>{formatPrice(orderDetail.unitPrice - orderDetail.unitPrice * orderDetail.promotionRate)}</span>
                      <span className='text-red-500 flex justify-center text-[12px]'>{' x '}{orderDetail.quantity}</span>
                    </div>
                    <span className='text-[12px]'>(-{formatPrice(orderDetail.unitPrice * orderDetail.promotionRate)})</span>
                  </div>
                ))}
              </div>
              <div className='mx-[15px] flex justify-between'>
                <div className='font-bold'>Total</div>
                <span className='font-semibold'>{formatPrice(item.finalAmount)}</span>
              </div>
              <div className='mx-[15px] border-t-black flex justify-between'>
                <div className='font-thin italic'>Discount Promotion</div>
                <span className='font-thin'>{formatPrice(calculateTotalPromotionValue(item))}</span>
              </div>
              <div className='mx-[15px] border-t-black flex justify-between pb-2'>
                <div className='font-thin italic'>Discount Rate</div>
                <span className='font-thin'>{item.specialDiscountRate}</span>
              </div>
              <div className=' flex justify-around'>
                <button type='button' className="m-0 py-2 border border-[#ffffff] bg-[#c0a52c] text-white px-10 rounded-md">Success</button>
              </div>
              <div className='mt-2 bg-white rounded-md shadow-md w-full flex justify-center overflow-x-auto'>
                {item.description}
              </div>
            </div>
          ))}
        </div>
      </div>
      <ReactPaginate
        onPageChange={handlePageClick}
        pageRangeDisplayed={3}
        marginPagesDisplayed={2}
        pageCount={totalPage}
        pageClassName="mx-1"
        pageLinkClassName="px-3 py-2 rounded hover:bg-gray-200 text-black"
        previousClassName="mx-1"
        previousLinkClassName="px-3 py-2 rounded hover:bg-gray-200"
        nextClassName="mx-1"
        nextLinkClassName="px-3 py-2 rounded hover:bg-gray-200"
        breakLabel="..."
        breakClassName="mx-1"
        breakLinkClassName="px-3 py-2 text-black rounded hover:bg-gray-200"
        containerClassName="flex justify-center items-center space-x-4"
        activeClassName="bg-blue-500 text-white rounded-xl"
        renderOnZeroPageCount={null}
        previousLabel={
          <IconContext.Provider value={{ color: "#B8C1CC", size: "36px" }}>
            <AiFillLeftCircle />
          </IconContext.Provider>
        }
        nextLabel={
          <IconContext.Provider value={{ color: "#B8C1CC", size: "36px" }}>
            <AiFillRightCircle />
          </IconContext.Provider>
        }
      />
    </div>
  );

>>>>>>> FE_Giang
  </>)
}

export default Cs_Complete