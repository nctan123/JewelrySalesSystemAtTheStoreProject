import React, { useState, useEffect } from 'react'
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




  return (<>
    <div>
      <div className='my-0 mx-auto'>
        <div className='grid grid-cols-4 w-full px-10 overflow-y-auto h-[76vh]'>
          {listInvoice && listInvoice.length > 0 && listInvoice.map((item, index) => {
            return (
              <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[28em] bg-[#e9ddc26d] mt-[20px]'>
                <div className='flex flex-col justify-between px-[15px] text-black font-thin'>
                  <span className='flex justify-end font-bold'><FormatDate isoString={currentTime} /></span>
                  <div className='flex'>
                    <span>Code:</span>
                    <input className='bg-[#e9ddc200] text-center' value={item.code} readOnly />
                  </div>
                </div>
                <div className='flex justify-start px-[15px] text-black'>
                  <input hidden className='bg-[#e9ddc200] text-center' value={item.id} readOnly />
                </div>
                <div className='flex justify-start px-[15px] text-black'>
                  <p className='font-light w-full'>Customer Name: </p>
                  <span className='w-full flex justify-center font-serif'>{item.customerName}</span>
                </div>
                <div className='flex  px-[15px] text-black'>
                  <p className='w-[260px] font-light '>Staff Name:</p>
                  <span className='w-full flex font-serif'>{item.staffName}</span>
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
                  <input value={formatPrice(item.totalAmount) + '.đ'} readOnly className='mx-[15px] w-fit bg-[#87A89E] flex items-center font-bold text-lg' />
                  <div className='col-start-2 flex justify-end items-center mr-[15px]'>
                    <button type='button' onClick={handleComplete()} className=" m-0 border border-[#ffffff] bg-[#f3d92c] text-black px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Success</button>
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
        breakClassName="mx-1 "
        breakLinkClassName="px-3 py-2 text-black rounded hover:bg-gray-200"
        containerClassName="flex justify-center items-center space-x-4"
        activeClassName="bg-blue-500 text-white rounded-xl"
        renderOnZeroPageCount={null}
        // className="bg-black flex justify-center items-center"
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

  </>)
}

export default Cs_Complete