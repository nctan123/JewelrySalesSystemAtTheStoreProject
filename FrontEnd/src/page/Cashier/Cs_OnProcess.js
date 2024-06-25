import React, { useState, useEffect } from 'react'
import { fetchAllListOrder, fetchPaymentMethod } from '../../apis/jewelryService'
import Popup from 'reactjs-popup';
import QRCode from "react-qr-code";
import SignatureCanvas from 'react-signature-canvas'
import axios from 'axios';
import { toast } from 'react-toastify';
import { current } from '@reduxjs/toolkit';
import { format, parseISO } from 'date-fns';

const FormatDate = ({ isoString }) => {
  const parsedDate = parseISO(isoString);
  const formattedDate = format(parsedDate, 'HH:mm yyyy-MM-dd');
  return (
    <div>
      <p>{formattedDate}</p>
    </div>
  );
};
const Cs_OnProcess = () => {
  const [currentTime, setCurrentTime] = useState(new Date().toISOString());
  const [listInvoice, setlistInvoice] = useState([]); // list full invoice
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [IdOrder, setIdOrder] = useState('')
  const [isPaymentCompleted, setIsPaymentCompleted] = useState(false);
  const [isPaymentCompletedDefault, setIsPaymentCompletedDefault] = useState(true);
  const [paymentMethod, setpaymentMethod] = useState('')
  const [ChosePayMethod, setChosePayMethod] = useState('Cash');

  const handleSelectChange = (event) => {
    setChosePayMethod(event.target.value);
  };
  const closeModal = () => {
    setIsModalOpen(false);
  };
  
  useEffect(() => {
    getInvoice();
  }, []);
  useEffect(() => {
    getPayMentMethod();
  }, []);
  const getInvoice = async () => {
    let res = await fetchAllListOrder();
    if (res && res.data && res.data.data) {
      setlistInvoice(res.data.data)
    }
  };
  const getPayMentMethod = async () => {
    let res = await fetchPaymentMethod();
    if (res && res.data && res.data.data) {
      setpaymentMethod(res.data.data)
    }
  };

  useEffect(() => {
    const interval = setInterval(() => {
      setcreateDate(new Date());
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  function updateDisplay(value) {
    let currentValue = document.getElementById('display').value;

    // Check if the last character in the current value is an operator
    const lastCharIsOperator = ['+', '-', '*', '/', '.'].includes(currentValue.slice(-1));

    // Check if the current value is '0' and the entered value is an operator
    if (currentValue === '0') {
      if (['+', '-', '*', '/', '.'].includes(value)) {
        document.getElementById('display').value = '0' + value;
      } else {
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
    catch (err) {
      document.getElementById('display').value = "Invalid Input";
    }
  }
  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
  const [orderId, setorderId] = useState('')
  const [customerId, setcustomerId] = useState('')
  const [createDate, setcreateDate] = useState(new Date().toISOString())
  const [amount, setamount] = useState('')

  const handleSelectInvoice = (item) => {
    setorderId(item.id);
    setcustomerId(item.customer.id);
    setamount(item.totalAmount);
  };

  const handleSubmitOrder = async (item, event) => {
    event.preventDefault();
    // Ensure state is updated before submission
    handleSelectInvoice(item);
    
    let data = {
      orderId: orderId,
      customerId:customerId,
      createDate: createDate,
      amount:amount
    };

    try {
      let res = await axios.post('https://jssatsproject.azurewebsites.net/api/payment/createpayment', data);
      toast.success('Successful');
      setIsPaymentCompleted(true);
      setIsPaymentCompletedDefault(false)
      console.log(data);
    } catch (error) {
      toast.error('Fail');
      console.error('Error invoice:', error);
    }
  };
  
  return (<>
    <div>
      <div className='my-0 mx-auto'>
        <div className='grid grid-cols-4 w-full px-10 overflow-y-auto h-[80vh]'>
          {listInvoice && listInvoice.length > 0 && listInvoice.filter(item => item.status === 'processing').map((item, index) => {
            return (
              <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[28em] bg-[#e9ddc26d] mt-[20px]'>
                <div className='flex flex-col justify-between px-[15px] text-black font-thin'>
                  <span className='flex justify-end'><FormatDate isoString={currentTime} /></span>
                  <div className='flex justify-center'>
                  <span>ID Invoice:</span>
                  <input className='bg-[#e9ddc200] text-center' value={item.id} readOnly/>
                  </div>
                </div>
                <div className='flex justify-start px-[15px] text-black'>
                <input hidden className='bg-[#e9ddc200] text-center' value={item.customer.id} readOnly/>
                </div>
                <div className='flex justify-start px-[15px] text-black'>
                  <p className='font-light w-full'>Customer Name: </p>
                  <span className='w-full flex justify-center font-serif'>{item.customer.firstname}</span>
                </div>
                <div className='flex  px-[15px] text-black'>
                  <p className='w-[260px] font-light '>Staff Name:</p>
                  <span className='w-full flex font-serif'>{item.staff.firstname} {item.staff.lastname}</span>
                </div>
                <div className='grid grid-cols-3 border-x-0 border-t-0 border mx-[10px] border-b-black pb-[2px]'>
                  <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
                  <div className='col-start-3 ml-6 flex justify-start'>Price</div>
                </div>
                <div id='screenSeller' className='grid-cols-3 h-[45%] overflow-y-auto'>
                  {item.sellOrderDetails.map((orderDetail, index) => (
                    <div className='grid grid-cols-3 mx-[10px] border-b-black pb-[2px]'>
                      <div className='col-start-1 col-span-2 flex pl-[5px] items-center'>{orderDetail.productId}<span className='text-red-500 px-2 text-sm'>x{orderDetail.quantity}</span> </div>
                      <div className='col-start-3  flex justify-start'>{orderDetail.unitPrice}</div>
                    </div>
                  ))}
                </div>

                <div className='border border-x-0 border-b-0 mx-[15px] border-t-black py-2 flex justify-between'>
                  <div className='font-bold'>PAYMENT</div>
                </div>

                <div className='bg-[#87A89E] h-[50px] grid grid-cols-2 '>
                  <input value={item.totalAmount} readOnly className='mx-[15px] bg-[#87A89E] flex items-center font-bold text-lg'/>
                  <div className='col-start-2 flex justify-end items-center mr-[15px]'>
                    <Popup trigger={<button type='button' className=" m-0 border border-[#ffffff] bg-[#3f6d67] text-white px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Pay Bill</button>} position="right center">
                      {close => (
                        <div className='fixed top-0 bottom-0 left-0 right-0 bg-[#6f85ab61] grid grid-cols-2'>
                          <div className='flex justify-center items-center fixed top-0 bottom-0 left-0 p-2'>
                            <div className='bg-[#fff] my-[70px] mx-auto rounded-md w-[100%] shadow-[#b6b0b0] shadow-md'>
                              <form className="p-4 md:p-5 relative">
                                <div className=" flex items-center justify-end md:p-0 rounded-t">
                                  <a className='absolute right-0 cursor-pointer text-black text-[24px] py-0' onClick={close}>&times;</a>
                                </div>
                                <div className="grid gap-4 grid-cols-2">
                                  <div className='row-start-1 col-start-1 h-[100px]'>
                                    <h5 className='font-bold '>Customer Info</h5>
                                    <div id='inforCustomer' className='text-[12px]'>
                                      <p className='bg-gray-100 px-2 py-1 rounded-md mb-1'>Name: {item.customer.firstname} {item.customer.lastname}</p>
                                      <p className='bg-gray-100 px-2 py-1 rounded-md'>ID Invoice: {item.id}</p>
                                    </div>
                                  </div>
                                  <div className='row-start-1 col-start-2 h-[100px]'>
                                    <h5 className='font-bold'>Select a payment method</h5>
                                    <select  onChange={handleSelectChange} class="mt-[10px] w-[100%] px-2 bg-gray-100 text-gray-800 border-0 rounded-md p-2 mb-4 focus:bg-gray-200 focus:outline-none focus:ring-1 focus:ring-blue-500 transition ease-in-out duration-150">
                                    {paymentMethod && paymentMethod.length > 0 && paymentMethod.map((item,index)=> {
                                      return (
                                          <option key={index} value={item.name} className='text-black text-center'>{item.name}</option>
                                      )
                                    })}
                                    </select>
                                  </div>
                                </div>
                                <div className="grid gap-4 grid-cols-2">
                                  <div className='col-start-1 rounded-md bg-gray-100 p-[10px] relative'>
                                    <h3 className='font-bold'>Transaction Details</h3>
                                    <div className='overflow-y-scroll'>
                                      <div className='grid grid-cols-3 border-x-0 border-t-0 border mx-[10px] border-b-black pb-[2px]'>
                                        <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
                                        <div className='col-start-3 ml-6 flex justify-start'>Price</div>
                                      </div>
                                      <div id='screenSeller' className='grid-cols-3 h-[45%] overflow-y-auto'>
                                        {item.sellOrderDetails.map((orderDetail, index) => (
                                          <div className='grid grid-cols-3 mx-[10px] border-b-black pb-[2px]'>
                                            <div className='col-start-1 col-span-2 flex pl-[5px] items-center'>{orderDetail.productId}<span className='text-red-500 px-2 text-sm'>x{orderDetail.quantity}</span> </div>
                                            <div className='col-start-3  flex justify-start'>{orderDetail.unitPrice}</div>
                                          </div>
                                        ))}
                                      </div>
                                    </div>
                                    <div className='absolute bottom-5 w-[95%] border border-x-0 border-b-0 border-black grid grid-cols-2 grid-rows-3'>
                                      <div className='row-start-1 col-start-1 font-bold'>Total</div>
                                      <div className='row-start-1 col-start-2 flex justify-end mr-5'>{formatPrice(item.totalAmount)}<span>.đ</span></div>
                                    </div>
                                  </div>

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
                                      <input type="button" onClick={() => updateDisplay('0')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="" />
                                      <input type="button" onClick={() => updateDisplay('0')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="0" />
                                      <input type="button" onClick={() => updateDisplay('.')} class="w-20 h-14 text-black bg-[#e9f0f4] hover:bg-gray-200 shadow-md font-bold py-1 px-2 rounded-2xl dark:bg-[#222427] dark:text-white" value="." />
                                      <input type="button" onClick={() => calculate()} class="w-20 h-14 text-white bg-green-500 hover:bg-green-600 shadow-md font-bold py-1 px-2 rounded-2xl" value="=" />
                                    </div>
                                  </div>

                                </div>
                                <button type='submit' onClick={(event) => handleSubmitOrder(item, event)}  className="mb-0 text-white flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-10 py-4 text-center">
                                  Pay Now
                                </button>
                              </form>
                            </div>
                          </div>
                          {/* Bill */}
                          {isPaymentCompletedDefault && (
                            <div className='col-start-2 my-auto mx-3 h-[100vh] overflow-y-auto'>
                            <div className="bg-white shadow-lg w-full border border-black rounded-lg">
                              <div className="pt-4 mb-4 grid grid-cols-3 rounded-t">
                                <div className='h-auto mx-2 my-auto max-w-[64px] w-full'>
                                  <QRCode
                                    size={256}
                                    style={{ height: "auto", maxWidth: "100%", width: "100%" }}
                                    value={item.id}
                                    viewBox={`0 0 256 256`}
                                  />
                                </div>
                                <div className='flex flex-col items-center justify-center'>
                                  <h1 onClick={closeModal} className="cursor-pointer text-xl font-semibold text-gray-900">
                                  JEWELRY BILL OF SALE
                                  </h1>
                                  <h2 className='text-center text-gray-600'>
                                  <FormatDate isoString={currentTime} /> 
                                  </h2>
                                </div>
                                <div></div>
                              </div>
                              <div className='border border-black mx-4 my-2 p-4'>
                                {/* Customer Information */}
                                <div className='flex justify-between mb-4'>
                                  <div className='flex items-center'>
                                    <h1 className='font-semibold'>Customer Name:</h1>
                                    <h2 className='text-black ml-2'>............................</h2>
                                  </div>
                                  <div className='flex items-center'>
                                    <h1 className='font-semibold'>Account Number:</h1>
                                    <h2 className='text-black ml-2'>
                                      {[...Array(50)].map((_, index) => (
                                        <span key={index}>.</span>
                                      ))}
                                    </h2>
                                  </div>
                                </div>
                                <div className='flex items-center mb-4'>
                                  <h1 className='font-semibold'>Address:</h1>
                                  <h2 className='text-black ml-2'>
                                    {[...Array(50)].map((_, index) => (
                                      <span key={index}>.</span>
                                    ))}
                                  </h2>
                                </div>
                                <div className='flex items-center mb-4'>
                                  <h1 className='font-semibold'>Payment methods:</h1>
                                  <h2 className='text-black ml-2'>
                                    {[...Array(50)].map((_, index) => (
                                      <span key={index}>.</span>
                                    ))}
                                  </h2>
                                </div>
                                {/* Product Information */}
                                <div className='border border-black mt-5 overflow-hidden'>
                                  <table className="min-w-full text-left text-sm font-light text-gray-900">
                                    <thead className="border-b bg-gray-100 font-medium">
                                      <tr>
                                        <th scope="col" className="px-4 py-4 text-center">N.O</th>
                                        <th scope="col" className="px-6 py-4 text-center">Name Product</th>
                                        <th scope="col" className="px-4 py-4 text-center">Quantity</th>
                                        <th scope="col" className="px-6 py-4 text-center">Cost</th>
                                        <th scope="col" className="px-6 py-4 text-center">Value</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                      <tr className="border-b bg-gray-50">
                                        <td className="whitespace-nowrap px-4 py-4 text-center font-medium">...</td>
                                        <td className="whitespace-nowrap px-6 py-4 text-center">...</td>
                                        <td className="whitespace-nowrap px-4 py-4 text-center">...</td>
                                        <td className="whitespace-nowrap px-6 py-4 text-center">...</td>
                                        <td className="whitespace-nowrap px-6 py-4 text-center">...</td>
                                      </tr>
                                      <tr className="border-b bg-gray-50">
                                        <td className="whitespace-nowrap px-4 py-4 text-center font-medium">...</td>
                                        <td className="whitespace-nowrap px-6 py-4 text-center">...</td>
                                        <td className="whitespace-nowrap px-4 py-4 text-center">...</td>
                                        <td className="whitespace-nowrap px-6 py-4 text-center">...</td>
                                        <td className="whitespace-nowrap px-6 py-4 text-center">...</td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div className='border border-black mt-2 p-4'>
                                  <div className='flex justify-between'>
                                    <h1 className='font-bold'>Total Value</h1>
                                    <h1>............</h1>
                                  </div>
                                </div>
                                <div className='h-40 flex justify-around items-center'>
                                  <div className='text-center '>
                                    <h1 className='font-bold'>Customer</h1>
                                    <h1 className='pb-2'>(Sign, write full name)</h1>
                                    <SignatureCanvas penColor='black' 
                                     canvasProps={{width: 300, height: 100, className: 'sigCanvas', style: { 
                                      // border: '1px solid black', 
                                      backgroundColor: '#f0f0f085' 
                                    }}} />
                                  </div>
                                  <div className='text-center '>
                                    <h1 className='font-bold'>Staff</h1>
                                    <h1 className='pb-2'>(Sign, write full name)</h1>
                                    <SignatureCanvas penColor='black'
                                     canvasProps={{width: 300, height: 100, className: 'sigCanvas', style: { 
                                      // border: '2px solid black', 
                                      backgroundColor: '#f0f0f085' 
                                    }}} />
                                  </div>
                                 </div>
                                
                              </div>
                              <div className='flex justify-between px-4 py-2'>
                                <button className='bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600'>Cancel</button>
                                <button className='bg-green-500 text-white px-4 py-2 rounded-lg hover:bg-green-600'>Complete</button>
                              </div>
                            </div>
                          </div>
                          )}
                          {isPaymentCompleted &&  (
                          <div className='col-start-2 my-auto mx-3 h-[100vh] overflow-y-auto'>
                            <div className="bg-white shadow-lg w-full border border-black rounded-lg">
                              <div className="pt-4 mb-4 grid grid-cols-3 rounded-t">
                                <div className='h-auto mx-2 my-auto max-w-[64px] w-full'>
                                  <QRCode
                                    size={256}
                                    style={{ height: "auto", maxWidth: "100%", width: "100%" }}
                                    value={item.id}
                                    viewBox={`0 0 256 256`}
                                  />
                                </div>
                                <div className='flex flex-col items-center justify-center'>
                                  <h1 onClick={closeModal} className="cursor-pointer text-xl font-semibold text-gray-900">
                                  JEWELRY BILL OF SALE
                                  </h1>
                                  <h2 className='text-center text-gray-600'>
                                    {currentTime}
                                  </h2>
                                </div>
                                <div></div>
                              </div>
                              <div className='border border-black mx-4 my-2 p-4'>
                                {/* Customer Information */}
                                <div className='flex justify-between mb-4'>
                                  <div className='flex items-center'>
                                    <h1 className='font-semibold'>Customer Name:</h1>
                                    <h2 className='text-black ml-2'>{item.customer.firstname} {item.customer.lastname}</h2>
                                  </div>
                                  <div className='flex items-center'>
                                    <h1 className='font-semibold'>Phoner Number:</h1>
                                    <h2 className='text-black ml-2'>
                                      {item.customer.phone}
                                    </h2>
                                  </div>
                                </div>
                                <div className='flex items-center mb-4'>
                                  <h1 className='font-semibold'>Address:</h1>
                                  <h2 className='text-black ml-2'>
                                    {[...Array(50)].map((_, index) => (
                                      <span key={index}>.</span>
                                    ))}
                                  </h2>
                                </div>
                                <div className='flex items-center mb-4'>
                                  <h1 className='font-semibold'>Payment methods:</h1>
                                  <h2 className='text-black ml-2'>
                                    {ChosePayMethod}
                                  </h2>
                                </div>
                                {/* Product Information */}
                                <div className='border border-black mt-5 overflow-hidden'>
                                  <table className="min-w-full text-left text-sm font-light text-gray-900">
                                    <thead className="border-b bg-gray-100 font-medium">
                                      <tr>
                                        <th scope="col" className="px-4 py-4 text-center">N.O</th>
                                        <th scope="col" className="px-6 py-4 text-center">Name Product</th>
                                        <th scope="col" className="px-4 py-4 text-center">Quantity</th>
                                        <th scope="col" className="px-6 py-4 text-center">Cost</th>
                                        <th scope="col" className="px-6 py-4 text-center">Value</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                      {item.sellOrderDetails && item.sellOrderDetails.length > 0 && item.sellOrderDetails.map((p) => {
                                        return(
                                        <tr className="border-b bg-gray-50">
                                          <td className="whitespace-nowrap px-4 py-4 text-center font-medium">1</td>
                                          <td className="whitespace-nowrap px-6 py-4">{p.id}</td>
                                          <td className="whitespace-nowrap px-4 py-4 text-center">{p.quantity}</td>
                                          <td className="whitespace-nowrap px-6 py-4 text-right">{p.unitPrice}</td>
                                          <td className="whitespace-nowrap px-6 py-4 text-right">{p.quantity}*{p.unitPrice}</td>
                                        </tr>)
                                        })}
                                    </tbody>
                                  </table>
                                </div>
                                <div className='border border-black mt-2 p-4'>
                                  <div className='flex justify-between'>
                                    <h1 className='font-bold'>Total Value</h1>
                                    <h1>...</h1>
                                  </div>
                                </div>
                                <div className='h-40 flex justify-around items-center'>
                                  <div className='text-center '>
                                    <h1 className='font-bold'>Customer</h1>
                                    <h1 className='pb-2'>(Sign, write full name)</h1>
                                    <SignatureCanvas penColor='black' 
                                     canvasProps={{width: 300, height: 100, className: 'sigCanvas', style: { 
                                      // border: '1px solid black', 
                                      backgroundColor: '#f0f0f085' 
                                    }}} />
                                  </div>
                                  <div className='text-center '>
                                    <h1 className='font-bold'>Staff</h1>
                                    <h1 className='pb-2'>(Sign, write full name)</h1>
                                    <SignatureCanvas penColor='black'
                                     canvasProps={{width: 300, height: 100, className: 'sigCanvas', style: { 
                                      // border: '2px solid black', 
                                      backgroundColor: '#f0f0f085' 
                                    }}} />
                                  </div>
                                 </div>
                                
                              </div>
                              <div className='flex justify-between px-4 py-2'>
                                <button className='bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600'>Cancel</button>
                                <button className='bg-green-500 text-white px-4 py-2 rounded-lg hover:bg-green-600'>Complete</button>
                              </div>
                            </div>
                          </div>
                          )}
                        </div>
                      )}
                    </Popup>
                  </div>
                </div>
                <div className='mt-2 bg-white rounded-md shadow-md w-full flex justify-center overflow-x-auto'>
                  {item.description}
                </div>
              </div>
            )
          })}
        </div>
      </div>
    </div>

  </>)
}

export default Cs_OnProcess