<<<<<<< HEAD
import React, { useState, useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { deleteProduct, deleteCustomer, deleteProductAll, deletePromotion } from '../store/slice/cardSilec'
import { MdDeleteOutline } from "react-icons/md";
import { VscGitStashApply } from "react-icons/vsc";
import Popup from 'reactjs-popup';
import { toast } from 'react-toastify';
import axios from 'axios';

const SidebarRight = () => {

  const [currentTime, setCurrentTime] = useState(new Date().toISOString());
  const [customerPhoneNumber, setcustomerPhoneNumber] = useState('')
  const [staffId, setstaffId] = useState(null)
  const [createDate,setcreateDate] = useState(new Date().toISOString());
  const [description, setdescription] = useState()
  const [productCodesAndQuantity, setproductCodesAndQuantity] = useState(null)
  const [productCodesAndPromotionIds, setproductCodesAndPromotionIds] = useState(null)
  const [specialDiscountRequestId, setspecialDiscountRequestId] = useState(null)
  const [isSpecialDiscountRequested, setisSpecialDiscountRequested] = useState(false)
  const [discountRejectedReason, setdiscountRejectedReason] = useState(null)
  const [specialDiscountRequestStatus, setspecialDiscountRequestStatus] = useState(null)
  const [specialDiscountRate, setspecialDiscountRate] = useState('0')
  const [point, setpoint] = useState("")
  const [submitList,setsubmitList] = useState('')
  //Lưu giá trị sản phẩm
  const [total, setTotal] = useState(0);
  const [discount, setDiscount] = useState(0)
  //Lưu trạng thái gửi yêu cầu giảm giá của khách hàng
  const [isChecked, setIsChecked] = useState(false);
  const handleCheckboxClick = () => {
    if (!isChecked) {
      setIsChecked(true);
      toast.warning('Sent Special Discount')
    }
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

=======
import React, { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { deleteProduct, deleteCustomer, deleteProductAll, addCustomer, addProduct, addRate } from '../store/slice/cardSilec';
import { MdDeleteOutline } from 'react-icons/md';
import { VscGitStashApply } from 'react-icons/vsc';
import Popup from 'reactjs-popup';
import axios from 'axios';
import { fetchDoubleStatusInvoice, fetchStatusInvoice } from '../apis/jewelryService';
import ReactPaginate from 'react-paginate';
import { AiFillLeftCircle, AiFillRightCircle } from 'react-icons/ai';
import { IconContext } from 'react-icons';
import { toast } from 'react-toastify';
import { FaFileInvoiceDollar } from "react-icons/fa6";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';
import Modal from 'react-modal';

const SidebarRight = () => {
  const [currentTime, setCurrentTime] = useState(new Date().toISOString());
  const [customerPhoneNumber, setCustomerPhoneNumber] = useState('');
  const [description, setDescription] = useState('');
  const [productCodesAndQuantity, setProductCodesAndQuantity] = useState({});
  const [productCodesAndPromotionIds, setProductCodesAndPromotionIds] = useState({})
  const [specialDiscountRate, setSpecialDiscountRate] = useState('0');
  const [checkedItems, setCheckedItems] = useState({});
  const [totalProduct, setTotalProduct] = useState(0);
  const [totalPage, setTotalPage] = useState(0);
  const [totalProductForManager, setTotalProductForManger] = useState(0);
  const [totalPageForManager, setTotalPageForManager] = useState(0);
  const [listInvoiceDraft, setListInvoiceDraft] = useState([]);
  const [listResponseManager, setListResponseManager] = useState([])
  const [total, setTotal] = useState(0);
  const [discount, setDiscount] = useState(0);
  const [point, setpoint] = useState(0);
  const [IdTemPo, setIdTemPo] = useState();
  const [TotalInvoice, setTotalInvoice] = useState(0);

  const dispatch = useDispatch();
  const CartProduct = useSelector(state => state.cart.CartArr);
  const CusPoint = useSelector(state => state.cart.CusPoint);
  const Rate = useSelector(state => state.cart.Rate);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentTime(new Date().toISOString());
    }, 1000);
    return () => clearInterval(interval);
  }, []);
  const getDisCountPoint = async (amount, phone) => {
    let data = {
      customerPhoneNumber: phone,
      totalOrderPrice: amount
    }
    try {
      const res = await axios.post('https://jssatsproject.azurewebsites.net/api/Point/GetPointForOrder', data)
      console.log(res.data)
      setpoint(res.data)
      toast.success('Point Success')
    } catch (error) {
      toast.error('Fail')
    }

  }
>>>>>>> FE_Giang
  useEffect(() => {
    const calculateTotals = () => {
      let totalValue = 0;
<<<<<<< HEAD
      CartProduct.forEach((product) => {
        totalValue += product.productValue * product.quantity
=======
      let totalDiscount = 0;
      CartProduct.forEach(product => {
        totalValue += product.productValue * product.quantity;
        totalDiscount += product.productValue * product.quantity * product.discountRate;
>>>>>>> FE_Giang
      });
      setTotal(totalValue);
      setDiscount(totalDiscount);

      console.log("Total Value:", totalValue);
      console.log("Total Discount:", totalDiscount);
      console.log("Point:", point);
      console.log("Special Discount Rate:", specialDiscountRate);

      const invoiceTotal = (totalValue - totalDiscount - point) * (1 - specialDiscountRate);
      console.log("Calculated Total Invoice:", invoiceTotal);
      setTotalInvoice(invoiceTotal);
    };
<<<<<<< HEAD
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
  },[CusPoint]);

  function formatPrice(price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
=======

    calculateTotals();

    const codesAndQuantity = CartProduct.reduce((acc, product) => {
      acc[product.code] = product.quantity;
      return acc;
    }, {});
    setProductCodesAndQuantity(codesAndQuantity);

    const codesAndPromotion = CartProduct.reduce((acc, product) => {
      acc[product.code] = product.promotionId;
      return acc;
    }, {});
    setProductCodesAndPromotionIds(codesAndPromotion);

    if (CusPoint?.phone) {
      setCustomerPhoneNumber(CusPoint.phone);
    }
  }, [CartProduct, CusPoint, point, specialDiscountRate]);


  useEffect(() => {
    getListOrder(1);
  }, []);
  useEffect(() => {
    getResponseManager(1);
  }, []);

  const handleCheckboxClick = id => {
    setCheckedItems(prev => ({
      ...prev,
      [id]: true,
    }));
    toast.warning('Sent Special Discount');
  };

  const formatNumber = price => {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
  };
  const formatPrice = (value) => {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
      minimumFractionDigits: 0
    }).format(value);
  };

  const handlePageClick = event => {
    getListOrder(event.selected + 1);
  };
  const handlePageClickForManager = event => {
    getResponseManager(event.selected + 1);
  };
  const getListOrder = async page => {
    try {
      const res = await fetchDoubleStatusInvoice('draft', 'waiting for special discount response', page);
      if (res?.data?.data) {
        setListInvoiceDraft(res.data.data);
        setTotalProduct(res.data.totalElements);
        setTotalPage(res.data.totalPages);
      }
    } catch (error) {
      console.error('Error fetching orders:', error);
    }
  };
  const getResponseManager = async page => {
    try {
      const res = await fetchStatusInvoice('waiting for customer confirmation for discount', page);
      if (res?.data?.data) {
        setListResponseManager(res.data.data);
        setTotalProductForManger(res.data.totalElements);
        setTotalPageForManager(res.data.totalPages);
      }
    } catch (error) {
      console.error('Error fetching orders:', error);
    }
  };

  const handleSubmitOrder = async (isDraft = false) => {
    // Create a new object for ProductCodesAndPromotionIds with 0 values replaced by null
    const adjustedProductCodesAndPromotionIds = Object.fromEntries(
      Object.entries(productCodesAndPromotionIds).map(([key, value]) => [key, value === 0 ? null : value])
    );

    // Remove entries with null values
    const filteredProductCodesAndPromotionIds = Object.fromEntries(
      Object.entries(adjustedProductCodesAndPromotionIds).filter(([_, value]) => value !== null)
    );

    const data = {
      id: IdTemPo,
      customerPhoneNumber,
      staffId: 4, // Replace with actual staffId if needed
      createDate: new Date().toISOString(),
      description,
      discountPoint: point,
      productCodesAndQuantity,
      productCodesAndPromotionIds: Object.keys(filteredProductCodesAndPromotionIds).length === 0 ? null : filteredProductCodesAndPromotionIds,
      isSpecialDiscountRequested: parseFloat(specialDiscountRate) !== 0,
      specialDiscountRate,
      specialDiscountRequestId: null,
      discountRejectedReason: '',
      specialDiscountRequestStatus: null,
    };
    console.log(data);

    if (!productCodesAndQuantity || Object.keys(productCodesAndQuantity).length === 0) {
      toast.error('No Product');
      return;
    }

    try {
      const res = await axios.post('https://jssatsproject.azurewebsites.net/api/SellOrder/CreateOrder', data);
      if (!isDraft) {
        await axios.put(`https://jssatsproject.azurewebsites.net/api/SellOrder/UpdateStatus?id=${res.data.id}`, {
          status: 'waiting for customer payment',
          createDate: new Date().toISOString(),
        });
      }
      if (res.status === 201 || res.status === 200) {
        toast.success('Success');
        dispatch(deleteCustomer());
        dispatch(deleteProductAll());
        setDescription('');
        setSpecialDiscountRate('0');
        setTotalInvoice(0);
        setpoint(0)
        // Update the invoice list immediately
        getListOrder(1);
        getResponseManager(1)
      } else {
        toast.error('Add Fail');
        console.error('Unexpected response:', res);
      }
    } catch (error) {
      console.error('Error adding invoice:', error);

      if (error.response) {
        console.error('Error response:', error.response);
        toast.error(`Add Fail: ${error.response.data.message || 'Unknown error'}`);
      } else if (error.request) {
        console.error('Error request:', error.request);
        toast.error('Add Fail: No response received from server');
      } else {
        console.error('Error message:', error.message);
        toast.error(`Add Fail: ${error.message}`);
      }
    }
  };



  const handlegetCodeEx = async (listsellorder) => {
    for (const item of listsellorder) {
      try {
        const res = await axios.get(
          `https://jssatsproject.azurewebsites.net/api/Product/GetByCode?code=${item.productCode}`
        );
        console.log('product export', res.data.data[0]);
        dispatch(addProduct(res.data.data[0]))
      } catch (error) {
        console.error(`Error fetching product with code ${item.productCode}:`, error);
      }
    }
  };

  const handleRequestToScreen = async (event, phone, listsellorder, id, rate) => {
    event.preventDefault();
    try {
      const res = await axios.get(
        `https://jssatsproject.azurewebsites.net/api/Customer/Search?searchTerm=${phone}&pageIndex=1&pageSize=10`
      );
      console.log('customer export', res.data.data[0]);
      console.log('list', listsellorder);
      setIdTemPo(id);
      const item = res.data.data[0];
      dispatch(addCustomer(item));
      setSpecialDiscountRate(rate)
      await handlegetCodeEx(listsellorder);
    } catch (error) {
      console.error('Error fetching customer or products:', error);
    }
  };
  const handleShowListTemPo = () => {
    confirmAlert({
      customUI: ({ onClose }) => {
        return (
          <div className="fixed inset-0 flex items-center justify-center bg-[#0602027d] bg-opacity-20 z-10">
            <div className='bg-[#fff] mx-auto rounded-md w-[55%] shadow-[#b6b0b0] shadow-md h-[95vh] my-auto mt-4'>
              <div className="flex items-center justify-between p-2 md:p-5 border-b rounded-t dark:border-gray-600">
                <h3 className="text-md font-semibold text-gray-900">
                  List Temporary
                </h3>
                <a className='cursor-pointer text-black text-[24px] py-0' onClick={onClose}>&times;</a>
              </div>
              <form className="p-4 md:p-5">
                <div className=''>
                  <div class="relative overflow-x-auto shadow-md sm:rounded-lg h-[75vh]">
                    <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                      <thead class="text-xs text-white uppercase bg-gray-500 dark:bg-gray-700 dark:text-gray-400">
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

                        </tr>
                      </thead>
                      <tbody className='overflow-y-auto'>
                        {listInvoiceDraft && listInvoiceDraft.map((item) => {
                          return (
                            <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 text-center">
                              <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                {item.id}
                              </th>
                              <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                {item.customerName}
                              </th>
                              <td class="px-6 py-4">
                                {item.customerPhoneNumber}
                              </td>
                              <td class="px-6 py-4">
                                {formatPrice(item.finalAmount)}
                              </td>
                              <td class="flex py-4 gap-1 items-center justify-center">
                                <button onClick={(event) => handleRequestToScreen(event, item.customerPhoneNumber, item.sellOrderDetails, item.id)} className='m-0 p-3 bg-green-500'><VscGitStashApply /></button>
                                <Popup trigger={<button type="button" className="m-0 p-3 bg-red-500"><MdDeleteOutline /></button>} position="right center">
                                  {close => (
                                    <div className='fixed flex items-center justify-center top-0 bottom-0 left-0 right-0 bg-[#6f85ab61] overflow-y-auto'>
                                      <div className="bg-[#fff] mx-auto rounded-md w-[23%] shadow-[#b6b0b0] shadow-md p-4">
                                        <h1 className="text-lg font-semibold mb-4">Confirm to delete</h1>
                                        <p className="mb-6 text-center">Are you sure you want to delete this invoice?</p>
                                        <div className="flex justify-end">
                                          <button
                                            onClick={close}
                                            className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded m-0"
                                          >
                                            No
                                          </button>
                                          <button
                                            onClick={async () => {
                                              try {
                                                await axios.put(`https://jssatsproject.azurewebsites.net/api/SellOrder/UpdateStatus?id=${item.id}`, {
                                                  status: 'cancelled',
                                                });
                                                // Update the invoice list immediately
                                                getListOrder(1);
                                                toast.success('Invoice deleted successfully');
                                              } catch (error) {
                                                console.error('Error deleting invoice:', error);
                                                toast.error('Failed to delete invoice');
                                              }
                                              onClose();
                                            }}
                                            className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 m-0 ml-2 rounded"
                                          >
                                            Yes
                                          </button>
                                        </div>
                                      </div>
                                    </div>
                                  )}
                                </Popup>
                              </td>
                            </tr>
                          )
                        })}

                      </tbody>
                    </table>
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
              </form>
            </div>
          </div>
        );
      },
    });
  };
  const handleShowListResponse = () => {
    confirmAlert({
      customUI: ({ onClose }) => {
        return (
          <div className="fixed inset-0 flex items-center justify-center bg-[#0602027d] bg-opacity-20 z-10">
            <div className='bg-[#fff] mx-auto rounded-md w-[55%] shadow-[#b6b0b0] shadow-md h-[95vh] my-auto mt-4'>
              <div className="flex items-center justify-between p-2 md:p-5 border-b rounded-t dark:border-gray-600">
                <h3 className="text-md font-semibold text-gray-900">
                  List Response
                </h3>
                <a className='cursor-pointer text-black text-[24px] py-0' onClick={onClose}>&times;</a>
              </div>
              <form className="p-4 md:p-5">
                <div className=''>
                  <div class="relative overflow-x-auto shadow-md sm:rounded-lg h-[75vh]">
                    <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                      <thead class="text-xs text-white uppercase bg-gray-500 dark:bg-gray-700 dark:text-gray-400">
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
                          <th scope="col" class="px-6 py-3">
                            Status
                          </th>
                        </tr>
                      </thead>
                      <tbody className='overflow-y-auto'>
                        {listResponseManager && listResponseManager.map((item) => {
                          return (
                            <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 text-center">
                              <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                {item.id}
                              </th>
                              <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                {item.customerName}
                              </th>
                              <td class="px-6 py-4">
                                {item.customerPhoneNumber}
                              </td>
                              <td class="px-6 py-4">
                                {formatPrice(item.finalAmount)}
                              </td>
                              <td class="flex py-4 gap-1 items-center justify-center">
                                {/* {item.specialDiscountStatus === 'approved' && ( */}
                                  <button
                                    onClick={(event) => handleRequestToScreen(event, item.customerPhoneNumber, item.sellOrderDetails, item.id, item.specialDiscountRate)}
                                    className='m-0 p-3 bg-green-500'>
                                    <VscGitStashApply />
                                  </button>
                                {/* )} */}

                                <Popup trigger={<button type="button" className="m-0 p-3 bg-red-500"><MdDeleteOutline /></button>} position="right center">
                                  {close => (
                                    <div className='fixed flex items-center justify-center top-0 bottom-0 left-0 right-0 bg-[#6f85ab61] overflow-y-auto'>
                                      <div className="bg-[#fff] mx-auto rounded-md w-[23%] shadow-[#b6b0b0] shadow-md p-4">
                                        <h1 className="text-lg font-semibold mb-4">Confirm to delete</h1>
                                        <p className="mb-6 text-center">Are you sure you want to delete this invoice?</p>
                                        <div className="flex justify-end">
                                          <button
                                            onClick={close}
                                            className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded m-0"
                                          >
                                            No
                                          </button>
                                          <button
                                            onClick={async () => {
                                              try {
                                                await axios.put(`https://jssatsproject.azurewebsites.net/api/SellOrder/UpdateStatus?id=${item.id}`, {
                                                  status: 'cancelled',
                                                });
                                                // Update the invoice list immediately
                                                getListOrder(1);
                                                toast.success('Invoice deleted successfully');
                                              } catch (error) {
                                                console.error('Error deleting invoice:', error);
                                                toast.error('Failed to delete invoice');
                                              }
                                              onClose();
                                            }}
                                            className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 m-0 ml-2 rounded"
                                          >
                                            Yes
                                          </button>
                                        </div>
                                      </div>
                                    </div>
                                  )}
                                </Popup>
                              </td>
                              <td class="px-6 py-4 font-bold text-red-600">
                                {item.specialDiscountRate}
                              </td>
                              <td class="px-6 py-4 font-bold text-red-600">
                                {item.specialDiscountStatus}
                              </td>
                            </tr>
                          )
                        })}

                      </tbody>
                    </table>
                  </div>
                  <ReactPaginate
                    onPageChange={handlePageClickForManager}
                    pageRangeDisplayed={3}
                    marginPagesDisplayed={2}
                    pageCount={totalPageForManager}
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
              </form>
            </div>
          </div>
        );
      },
    });
>>>>>>> FE_Giang
  }


  useEffect(() => {
    const codesAndQuantity = CartProduct.reduce((acc,product) => {
      acc[product.code] = product.quantity;
      return acc;
    }, {});
    setproductCodesAndQuantity(codesAndQuantity);
  }, [CartProduct]);

  const handSubmitOrder = async () => {
    let data = {
      customerPhoneNumber: customerPhoneNumber,
      staffId: 4,
      createDate: createDate,
      description: description,
      discountPoint: 0,
      productCodesAndQuantity: productCodesAndQuantity, //useEffect
      productCodesAndPromotionIds: productCodesAndPromotionIds,
      isSpecialDiscountRequested: isSpecialDiscountRequested,
      specialDiscountRate: specialDiscountRate,
      specialDiscountRequestId: specialDiscountRequestId,
      discountRejectedReason: discountRejectedReason,
      specialDiscountRequestStatus: specialDiscountRequestStatus,
    }
    try {
      let res = await axios.post('https://jssatsproject.azurewebsites.net/api/SellOrder/CreateOrder',data);
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
    console.log(submitList);
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


  return (<>


    <div className='flex justify-center '>
<<<<<<< HEAD
      <div className='shadow-md shadow-gray-600 pt-[10px] rounded-t-2xl w-[90%] h-[34em] bg-[#f3f1ed] mt-[20px]'>
        <div className='flex justify-end'>
          {/* <select className="ml-[15px] relative text-black bg-transparent outline-none border border-white text-sm font-semibold rounded-md block w-[50%] p-1">
            <option>Sell</option>
            <option>Buy</option>
          </select> */}
=======
      <div className='shadow-md shadow-gray-600 pt-[10px] rounded-2xl w-[90%] h-[34em] bg-[#f3f1ed] mt-[20px]'>
        <div className='flex justify-end'>
>>>>>>> FE_Giang
          <div className='flex justify-end px-[15px] text-black font-thin'>{currentTime}</div>
        </div>
        <div className='flex justify-start px-[15px] text-black'>
          <p className='font-light'>Address:</p>
          <span className='w-full flex justify-center font-serif'>Jewelry Store</span>
        </div>
        <div className='flex items-center px-[15px] text-[#000]'>
          <p className='w-[260px] font-light '>Customer Phone:</p>
<<<<<<< HEAD
          {CusPoint && CusPoint[0] && CusPoint[0].phone && (
            <>
              <span id="phone" className='w-full flex items-center justify-between' >
                {CusPoint[0].phone}
                <span onClick={() => dispatch(deleteCustomer())} className='cursor-pointer rounded-md bg-[#fff] px-1 py-1'><MdDeleteOutline size='17px' color='#ef4e4e' /></span></span>
=======
          {CusPoint && (
            <>
              <span id="phone" className='w-full flex items-center justify-between'>
                {CusPoint.phone}
                <span onClick={() => dispatch(deleteCustomer())} className='cursor-pointer rounded-md bg-[#fff] px-1 py-1'>
                  <MdDeleteOutline size='17px' color='#ef4e4e' />
                </span>
              </span>
>>>>>>> FE_Giang
            </>
          )}
        </div>
        <div className='grid grid-cols-3 border border-x-0 border-t-0 mx-[10px] border-b-black pb-[2px] mb-2'>
          <div className='col-start-1 col-span-2 flex pl-[5px]'>Item</div>
          <div className='col-start-3 ml-6 flex justify-start'>Price</div>
        </div>
<<<<<<< HEAD
        <div id='screenSeller' className=' h-[45%] overflow-y-auto mb-2'>
=======
        <div id='screenSeller' className=' h-[40%] overflow-y-auto mb-2'>
>>>>>>> FE_Giang
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
<<<<<<< HEAD
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
            onClick={() => {handSubmitOrder(); 
                           dispatch(deleteCustomer());
                           dispatch(deleteProductAll());
                    }} 
            className=" m-0 border border-[#ffffff] bg-[#3f6d67] text-white px-4 py-1 rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Invoice</button>
=======
          <input value={description} onChange={(even) => setDescription(even.target.value)} className="w-42 h-full border-none rounded-md outline-none text-sm bg-[#ffff] text-red font-semibold  pl-2" type="text" placeholder="Note" />
        </div>
        <div className='px-[15px] grid grid-cols-2 grid-rows-4 mb-2'>
          <div className='row-start-1 font-mono'>Total:</div>
          <div className='col-start-2 flex justify-end text-[#d48c20]'>{formatPrice(total.toFixed())}</div>
          <div className='row-start-2 font-mono'>Discount:</div>
          <div className='col-start-2 flex justify-end text-[#d44420]'>{formatPrice(discount)}</div>
          <div className='row-start-3 flex gap-2'>
            <span className='font-mono'>Point:</span>
            {CusPoint && (
              <span>{formatNumber(CusPoint.point.availablePoint)}</span>
            )}
          </div>
          <div className='col-start-2 flex justify-between'>
            <div>
              <button className='m-0 p-0 px-4 w-fit bg-[#2b4fc6dc]' onClick={() => getDisCountPoint(total - discount, CusPoint.phone)} type='button'>Use</button>
            </div>
            <div>
              <span>{formatPrice(point)}</span>
            </div>
          </div>
          <div className='row-start-4 font-mono'>Special Discount:</div>
          <div className='col-start-2 flex justify-end'>
            <input
              className="w-20 h-full text-center rounded-md outline-none text-sm text-red font-semibold"
              type="number"
              min="0"
              max="1"
              value={specialDiscountRate}
              onChange={(even) => setSpecialDiscountRate(even.target.value)}
              placeholder="Rate"
            />
          </div>
        </div>
        <div className='bg-[#87A89E] h-[50px] grid grid-cols-3 '>

          <div className='mx-[15px] flex items-center font-bold text-lg'>{formatPrice(TotalInvoice)}</div>
          <div className='col-start-3 flex gap-2 justify-end items-center mr-[15px]'>
            <span
              onClick={() => {
                dispatch(deleteCustomer());
                dispatch(deleteProductAll());
                setSpecialDiscountRate(0);
                setpoint(0);
                setTotalInvoice(0)
              }} className='col-start-6 ml-8 w-[20px] flex items-center cursor-pointer rounded-md bg-[#fef7f7] py-1 hover:bg-[#ffffff]'><MdDeleteOutline size='20px' color='#ef4e4e' />
            </span>
            <button onClick={() => {
              handleSubmitOrder(true);
              dispatch(deleteCustomer());
              dispatch(deleteProductAll());
            }} title="Save" class="cursor-pointer flex items-center fill-lime-400 bg-lime-950 hover:bg-lime-900 active:border active:border-lime-400 rounded-md duration-100 p-2 m-0">
              <svg viewBox="0 -0.5 25 25" height="20px" width="20px" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linejoin="round" stroke-linecap="round" stroke-width="1.5" d="M18.507 19.853V6.034C18.5116 5.49905 18.3034 4.98422 17.9283 4.60277C17.5532 4.22131 17.042 4.00449 16.507 4H8.50705C7.9721 4.00449 7.46085 4.22131 7.08577 4.60277C6.7107 4.98422 6.50252 5.49905 6.50705 6.034V19.853C6.45951 20.252 6.65541 20.6407 7.00441 20.8399C7.35342 21.039 7.78773 21.0099 8.10705 20.766L11.907 17.485C12.2496 17.1758 12.7705 17.1758 13.113 17.485L16.9071 20.767C17.2265 21.0111 17.6611 21.0402 18.0102 20.8407C18.3593 20.6413 18.5551 20.2522 18.507 19.853Z" clip-rule="evenodd" fill-rule="evenodd"></path>
              </svg>
              <span class="text-sm text-lime-400 font-bold pr-1">Temporary</span>
            </button>
>>>>>>> FE_Giang
          </div>
        </div>
      </div>
    </div>
<<<<<<< HEAD
    <div className=''>
      <button className="border border-[#ffffff] bg-[#3f6d67e3] text-white rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">Temporary</button>
      <Popup trigger={ <button className="border border-[#ffffff] bg-[#3f6d67e3] text-white  rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">        
          List Temporary
      </button>} position="right center">
      {close => (
        <div className='fixed top-0 bottom-0 left-0 right-0 bg-[#6f85ab61] overflow-y-auto'>
        <div className='bg-[#fff] my-[70px] mx-auto rounded-md w-[50%] shadow-[#b6b0b0] shadow-md'>
          <div className="flex items-center justify-between p-2 md:p-5 border-b rounded-t dark:border-gray-600">
            <h3 className="text-md font-semibold text-gray-900">
            List Temporary
            </h3>
            <a className='cursor-pointer text-black text-[24px] py-0' onClick={close}>&times;</a>
          </div>
          <form className="p-4 md:p-5">
          <div className=''>
            <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
              <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr>
                    <th scope="col" class="px-6 py-3">
                      Customer Name
                    </th>
                    <th scope="col" class="px-6 py-3">
                      Phone Number
                    </th>
                    <th scope="col" class="px-6 py-3">
                      ID Invoice
                    </th>
                    <th scope="col" class="px-6 py-3">
                      Action
                    </th>

                    <th scope="col" class="px-6 py-3">
                      Special Discount
                    </th>

                  </tr>
                </thead>
                <tbody>
                  <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                    <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                      Apple MacBook Pro 17"
                    </th>
                    <td class="px-6 py-4">
                      08191913101
                    </td>
                    <td class="px-6 py-4">
                      I_001
                    </td>
                    <td class="flex py-4 gap-1 items-center justify-center">
                      <button className='m-0 p-3 bg-green-500'><VscGitStashApply /></button>
                      <button className='m-0 p-3 bg-red-500'><MdDeleteOutline /></button>
                    </td>

                    <td class="px-14 py-4 ">
                      <label className="relative inline-flex items-center cursor-pointer">
                        <input
                          onClick={handleCheckboxClick}
                          type="checkbox"
                          checked={isChecked}
                          className="sr-only peer"
                        />
                        <div
                          className={`peer ring-0 bg-rose-400 rounded-full outline-none duration-300 after:duration-500 w-8 h-8 shadow-md ${isChecked
                            ? 'bg-emerald-700 after:content-["✔️"]'
                            : 'after:content-["✖️"]'
                            } after:rounded-full after:absolute after:outline-none after:h-6 after:w-6 after:bg-gray-50 after:top-1 after:left-1 after:flex after:justify-center after:items-center  peer-hover:after:scale-75`}
                        ></div>
                      </label>
                    </td>

                  </tr>
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

    {/* <div id="popupListTemporary" className={clsx(styles.overlay)}>
      <div className={clsx(styles.popup)}>
        <a className={clsx(styles.close)} href='#'>&times;</a>
        <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
          <h3 className="text-lg font-semibold text-gray-900">
            List Temporary
          </h3>
        </div>
      
        <form className="p-4 md:p-5">
          <div className=''>
            <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
              <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr>
                    <th scope="col" class="px-6 py-3">
                      Customer Name
                    </th>
                    <th scope="col" class="px-6 py-3">
                      Phone Number
                    </th>
                    <th scope="col" class="px-6 py-3">
                      ID Invoice
                    </th>
                    <th scope="col" class="px-6 py-3">
                      Action
                    </th>

                    <th scope="col" class="px-6 py-3">
                      Special Discount
                    </th>

                  </tr>
                </thead>
                <tbody>
                  <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                    <th scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                      Apple MacBook Pro 17"
                    </th>
                    <td class="px-6 py-4">
                      08191913101
                    </td>
                    <td class="px-6 py-4">
                      I_001
                    </td>
                    <td class="flex py-4 gap-1 items-center justify-center">
                      <button className='m-0 p-3 bg-green-500'><VscGitStashApply /></button>
                      <button className='m-0 p-3 bg-red-500'><MdDeleteOutline /></button>
                    </td>

                    <td class="px-14 py-4 ">
                      <label className="relative inline-flex items-center cursor-pointer">
                        <input
                          onClick={handleCheckboxClick}
                          type="checkbox"
                          checked={isChecked}
                          className="sr-only peer"
                        />
                        <div
                          className={`peer ring-0 bg-rose-400 rounded-full outline-none duration-300 after:duration-500 w-8 h-8 shadow-md ${isChecked
                            ? 'bg-emerald-600 after:content-["✔️"]'
                            : 'after:content-["✖️"]'
                            } after:rounded-full after:absolute after:outline-none after:h-6 after:w-6 after:bg-gray-50 after:top-1 after:left-1 after:flex after:justify-center after:items-center  peer-hover:after:scale-75`}
                        ></div>
                      </label>
                    </td>

                  </tr>
                </tbody>
              </table>
            </div>
=======
    <div className='mt-2'>
      <div className='flex justify-center'>
        <button type='submit'
          onClick={() => {
            handleSubmitOrder();
            dispatch(deleteCustomer());
            dispatch(deleteProductAll());
          }} class="m-0 relative cursor-pointer opacity-90 hover:opacity-100 transition-opacity p-[2px] bg-black rounded-[16px] bg-gradient-to-t from-[#5d5fef6b] to-[#7b7deb] active:scale-95">
          <span class="w-full h-full flex items-center gap-2 px-8 py-3 text-white rounded-[14px] bg-gradient-to-t from-[#5D5FEF] to-[#5D5FEF]">
            <FaFileInvoiceDollar /> Invoice
          </span>
        </button>
      </div>
      <div className='grid grid-cols-2 justify-center my-4'>
        <div className='flex justify-center'>
          <button onClick={() => handleShowListTemPo()} className="m-0 py-2 px-1 border border-[#ffffff] bg-[#3f6d67e3] text-white  rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">
            Draft/Special Discount
          </button>
        </div>
>>>>>>> FE_Giang

        <div className='flex justify-center'>
          <button onClick={() => handleShowListResponse()} className="m-0 py-2 px-1 border border-[#ffffff] bg-[#3f6d67e3] text-white  rounded-md transition duration-200 ease-in-out hover:bg-[#5fa39a7e] active:bg-[#ffff] focus:outline-none">
            Response Manager
          </button>
        </div>
      </div>
<<<<<<< HEAD
    </div> */}




=======
    </div>
>>>>>>> FE_Giang
  </>
  )
}

export default SidebarRight