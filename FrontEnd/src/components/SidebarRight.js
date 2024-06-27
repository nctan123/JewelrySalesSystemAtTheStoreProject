import React, { useState, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { deleteProduct, deleteCustomer, deleteProductAll, deletePromotion } from '../store/slice/cardSilec'
import { MdDeleteOutline } from "react-icons/md";
import { VscGitStashApply } from "react-icons/vsc";
import Popup from 'reactjs-popup';
import { toast } from 'react-toastify';
import axios from 'axios';
import { fetchAllListOrder } from '../apis/jewelryService'

const SidebarRight = () => {

  const [currentTime, setCurrentTime] = useState(new Date().toISOString());
  const [customerPhoneNumber, setcustomerPhoneNumber] = useState('')
  const [staffId, setstaffId] = useState(null)
  const [createDate, setcreateDate] = useState(new Date().toISOString());
  const [description, setdescription] = useState()
  const [productCodesAndQuantity, setproductCodesAndQuantity] = useState(null)
  const [productCodesAndPromotionIds, setproductCodesAndPromotionIds] = useState(null)
  const [specialDiscountRequestId, setspecialDiscountRequestId] = useState(null)
  const [isSpecialDiscountRequested, setisSpecialDiscountRequested] = useState(false)
  const [discountRejectedReason, setdiscountRejectedReason] = useState(null)
  const [specialDiscountRequestStatus, setspecialDiscountRequestStatus] = useState(null)
  const [specialDiscountRate, setspecialDiscountRate] = useState('0')
  const [point, setpoint] = useState("")
  const [submitList, setsubmitList] = useState('')
  //Lưu giá trị sản phẩm
  const [total, setTotal] = useState(0);
  const [discount, setDiscount] = useState(0)
  //Lưu trạng thái gửi yêu cầu giảm giá của khách hàng
  const [checkedItems, setCheckedItems] = useState({});
  const handleCheckboxClick = (id) => {
    setCheckedItems((prev) => ({
      ...prev,
      [id]: true, // Ensure the checkbox can't be unchecked
    }));
    toast.warning('Sent Special Discount');
  };

  //Lấy danh sách được lưu trong redux và sử dùng đường dẫn đi đến store
  const dispatch = useDispatch()
  const CartProduct = useSelector(state => state.cart.CartArr);
  const CusPoint = useSelector(state => state.cart.CusPoint);
  //Set Time cho Order
  useEffect(() => {
    const interval1 = setInterval(() => {
      setcreateDate(new Date().toISOString())
    }, 1000);
    return () => clearInterval(interval1);
  }, []);

  useEffect(() => {
    const calculateTotal = () => {
      let totalValue = 0;
      CartProduct.forEach((product) => {
        totalValue += product.productValue * product.quantity
      });
      setTotal(totalValue);
    };
    calculateTotal();

    const calculateDiscount = () => {
      let totalDiscount = 0;
      CartProduct.forEach((product) => {
        totalDiscount += product.productValue * product.quantity * product.discountRate
      });
      setDiscount(totalDiscount);
    };
    calculateDiscount();

  }, [CartProduct]);
  const totalInvoice = total - discount

  useEffect(() => {
    if (CusPoint && CusPoint[0] && CusPoint[0].phone) {
      setcustomerPhoneNumber(CusPoint[0].phone);
    }
  }, [CusPoint]);

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }


  useEffect(() => {
    const codesAndQuantity = CartProduct.reduce((acc, product) => {
      acc[product.code] = product.quantity;
      return acc;
    }, {});
    setproductCodesAndQuantity(codesAndQuantity);
  }, [CartProduct]);

  const handSubmitOrderDraf = async () => {
    let data = {
      customerPhoneNumber: customerPhoneNumber,
      staffId: 4, // chưa fix
      createDate: createDate,
      description: description,
      discountPoint: 0,  // chưa fix
      productCodesAndQuantity: productCodesAndQuantity, //useEffect
      productCodesAndPromotionIds: productCodesAndPromotionIds,
      isSpecialDiscountRequested: isSpecialDiscountRequested, //thiếu hàm check nếu có giảm giá thì chỉnh thành true
      specialDiscountRate: specialDiscountRate,
      specialDiscountRequestId: specialDiscountRequestId,
      discountRejectedReason: discountRejectedReason,
      specialDiscountRequestStatus: specialDiscountRequestStatus,
    }
    try {
      let res = await axios.post('https://jssatsproject.azurewebsites.net/api/SellOrder/CreateOrder', data);
      console.log('========> res',res)
      console.log('========> data',data)
      setsubmitList(res)
      toast.success('Successful');
      setdescription('');
      setspecialDiscountRate(0);
      setpoint('');
      // setPointRate('');
      console.log(submitList)
    } catch (error) {
      toast.error('Fail');
      console.error('Error invoice :', error);
    }
  }
  useEffect(() => {
    // This will run every time ListInvoice changes
    console.log('===>', submitList);
  }, [submitList]);
  // const [pointRate, setPointRate] = useState(0);
  const [isInvalid, setIsInvalid] = useState(false);
  // const handleRateChange = (event) => {
  //   const value = parseFloat(event.target.value);
  //   if (value < 0 || value > 1) {
  //     setIsInvalid(true);
  //     toast.warning('Wrong Value')
  //   } else {
  //     setIsInvalid(false);
  //     // setPointRate(value);
  //     setspecialDiscountRate(value);
  //     setisSpecialDiscountRequested(true);
  //   }
  // };
  const [intervalId, setIntervalId] = useState(null);
  const [listInvoiceDraft, setlistInvoiceDraft] = useState()
  const getListOrder = async () => {
    let res = await fetchAllListOrder();
    if (res && res.data && res.data.data) {
      setlistInvoiceDraft(res.data.data)
    }
  }
  useEffect(() => {
    getListOrder();
    const id = setInterval(getListOrder, 1000); // Load data every 5 seconds
    setIntervalId(id);
    return () => clearInterval(id);
  }, [])
  useEffect(() => {
    // console.log('listDraft', listInvoiceDraft);
  }, [listInvoiceDraft]);
  useEffect(() => {
    // Check if specialDiscountRate is different from 0
    if (parseFloat(specialDiscountRate) !== 0) {
      setisSpecialDiscountRequested(true);
    } else {
      setisSpecialDiscountRequested(false);
    }
  }, [specialDiscountRate]);

  return (<>


    <div className='flex justify-center '>
      <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[34em] bg-[#f3f1ed] mt-[20px]'>
        <div className='flex justify-end'>
          {/* <select className="ml-[15px] relative text-black bg-transparent outline-none border border-white text-sm font-semibold rounded-md block w-[50%] p-1">
            <option>Sell</option>
            <option>Buy</option>
          </select> */}
          <div className='flex justify-end px-[15px] text-black font-thin'>{currentTime}</div>
        </div>
        <div className='flex justify-start px-[15px] text-black'>
          <p className='font-light'>Address:</p>
          <span className='w-full flex justify-center font-serif'>Jewelry Store</span>
        </div>
        <div className='flex items-center px-[15px] text-[#000]'>
          <p className='w-[260px] font-light '>Customer Phone:</p>
          {CusPoint && CusPoint[0] && CusPoint[0].phone && (
            <>
              <span id="phone" className='w-full flex items-center justify-between' >
                {CusPoint[0].phone}
                <span onClick={() => dispatch(deleteCustomer())} className='cursor-pointer rounded-md bg-[#fff] px-1 py-1'><MdDeleteOutline size='17px' color='#ef4e4e' /></span></span>
            </>
          )}
        </div>
        <div className='grid grid-cols-3 border border-x-0 border-t-0 mx-[10px] border-b-black pb-[2px] mb-2'>
          <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
          <div className='col-start-3 ml-6 flex justify-start'>Price</div>
        </div>
        <div id='screenSeller' className=' h-[45%] overflow-y-auto mb-2'>
          {CartProduct && CartProduct.map((item, index) => {
            return (
              <div className='grid grid-cols-6 '>
                <div className='col-start-1 col-span-4 flex px-[10px] text-sm'>{item.name}</div>
                <div className='col-start-5 flex ml-[65px] justify-end text-[#d48c20] px-[10px]'>{formatPrice(item.productValue * item.quantity)}</div>
                <span onClick={() => dispatch(deleteProduct(item))} className='col-start-6 ml-8 w-[20px] flex items-center cursor-pointer rounded-md'><MdDeleteOutline size='17px' color='#ef4e4e' /></span>
                {!item.startDate && (
                  <div className='col-start-1 col-span-6 flex px-[14px] text-xs text-red-600 mt-[-6px]'>x{item.quantity}</div>
                )}
              </div>
            )
          })}
        </div>
        <div className='border mx-[15px] border-x-0 border-b-0 border-t-black grid grid-cols-2 py-2'>
          <div className='font-bold'>PAYMENT</div>
          <input value={description} onChange={(even) => setdescription(even.target.value)} className="w-42 h-full border-none rounded-md outline-none text-sm bg-[#ffff] text-red font-semibold  pl-2" type="text" placeholder="Note" />
        </div>
        <div className='px-[15px] grid grid-cols-2 grid-rows-2'>
          <div className='row-start-1 font-thin'>Total:</div>
          <div className='col-start-2 flex justify-end'>{formatPrice(total.toFixed())}</div>
          <div className='row-start-2 font-thin'>Discount:</div>
          <div className='col-start-2 flex justify-end'>{formatPrice(discount)}</div>
        </div>
        {CusPoint && CusPoint[0] && (
          <div className='px-[15px] grid grid-cols-2 pb-2' >
            <div className='font-thin'>Point: {formatPrice(CusPoint[0].totalPoint)}</div>
            <input value={point} onChange={(even) => setpoint(even.target.value)} className="w-42 h-full border-none rounded-md outline-none text-sm bg-[#f3f1ed] text-red font-semibold  pl-2" type="number" name="point" min="-9" max={CusPoint[0].totalPoint} id="inputPoint" placeholder="Use Point" />
            <div className='font-thin'>Special Discount:</div>
            <div className='flex items-center justify-center gap-2'>
              <input
                className={`w-42 h-full border-none rounded-md outline-none text-sm bg-[#f3f1ed] text-red font-semibold pl-2 ${isInvalid ? 'border-red-500' : ''
                  }`}
                type="number"
                min="0"
                max="1"
                value={specialDiscountRate}
                onChange={(even) => setspecialDiscountRate(even.target.value)}
                placeholder="Rate"
              />
            </div>
          </div>
        )}
        <div className='bg-[#87A89E] h-[50px] grid grid-cols-3 mt-2 '>

          <div className='mx-[15px] flex items-center font-bold text-lg'>{formatPrice(totalInvoice)}<span>.đ</span></div>
          <div className='col-start-3 flex gap-2 justify-end items-center mr-[15px]'>
            <span
              onClick={() => {
                dispatch(deleteCustomer());
                dispatch(deleteProductAll());
              }} className='col-start-6 ml-8 w-[20px] flex items-center cursor-pointer rounded-md bg-[#fef7f7] py-1 hover:bg-[#ffffff]'><MdDeleteOutline size='20px' color='#ef4e4e' /></span>
            <button type='submit'
              onClick={() => {
                // handSubmitOrder();
                dispatch(deleteCustomer());
                dispatch(deleteProductAll());
              }}
              className=" m-0 border border-[#ffffff] bg-[#3f6d67] text-white px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Invoice</button>
          </div>
        </div>
      </div>
    </div>
    <div className=''>
      <button onClick={() => {
        handSubmitOrderDraf();
        dispatch(deleteCustomer());
        dispatch(deleteProductAll());
      }} className="border border-[#ffffff] bg-[#3f6d67e3] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Temporary</button>
      <Popup trigger={<button className="border border-[#ffffff] bg-[#3f6d67e3] text-white  rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">
        List Temporary
      </button>} position="right center">
        {close => (
          <div className='fixed top-0 bottom-0 left-0 right-0 bg-[#6f85ab61] overflow-y-auto '>
            <div className='bg-[#fff] mx-auto rounded-md w-[50%] shadow-[#b6b0b0] shadow-md h-[95vh] my-auto mt-4'>
              <div className="flex items-center justify-between p-2 md:p-5 border-b rounded-t dark:border-gray-600">
                <h3 className="text-md font-semibold text-gray-900">
                  List Temporary
                </h3>
                <a className='cursor-pointer text-black text-[24px] py-0' onClick={close}>&times;</a>
              </div>
              <form className="p-4 md:p-5">
                <div className=''>
                  <div class="relative overflow-x-auto shadow-md sm:rounded-lg h-[80vh]">
                    <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                      <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                          <th scope="col" class="px-6 py-3">
                            ID Invoice
                          </th>
                          <th scope="col" class="px-6 py-3">
                            Customer Name
                          </th>
                          <th scope="col" class="px-6 py-3">
                            Phone Number
                          </th>
                          <th scope="col" class="px-6 py-3">
                            Total Amount
                          </th>
                          <th scope="col" class="px-6 py-3">
                            Action
                          </th>

                          <th scope="col" class="px-6 py-3">
                            Special Discount
                          </th>

                        </tr>
                      </thead>
                      <tbody className='overflow-y-auto'>
                        {listInvoiceDraft && listInvoiceDraft.map((item) => {
                          return (
                            <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                              <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                {item.id}
                              </th>
                              <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                {item.customer.firstname}
                              </th>
                              <td class="px-6 py-4">
                                {item.customer.phone}
                              </td>
                              <td class="px-6 py-4">
                                {formatPrice(item.totalAmount)}
                              </td>
                              <td class="flex py-4 gap-1 items-center justify-center">
                                <button className='m-0 p-3 bg-green-500'><VscGitStashApply /></button>
                                <button className='m-0 p-3 bg-red-500'><MdDeleteOutline /></button>
                              </td>
                              {item.status === 'waiting for discount response' && (
                              <td class="px-14 py-4 ">
                                <label className="relative inline-flex items-center cursor-pointer">
                                  <input
                                    onChange={() => handleCheckboxClick(item.id)}
                                    type="checkbox"
                                    disabled={checkedItems[item.id]}
                                    checked={checkedItems[item.id] || false}
                                    className="sr-only peer"
                                  />
                                  <div
                                    className={`peer ring-0 bg-rose-400 rounded-full outline-none duration-300 after:duration-500 w-8 h-8 shadow-md ${checkedItems //[item.id]
                                      ? 'bg-emerald-700 after:content-["✔️"]'
                                      : 'after:content-["✖️"]'
                                      } after:rounded-full after:absolute after:outline-none after:h-6 after:w-6 after:bg-gray-50 after:top-1 after:left-1 after:flex after:justify-center after:items-center  peer-hover:after:scale-75`}
                                  />
                                </label>
                              </td>
                              )}
                            </tr>
                          )
                        })}

                      </tbody>
                    </table>
                  </div>
                </div>
              </form>
            </div>
          </div>
        )}
      </Popup>
    </div>
  </>
  )
}

export default SidebarRight