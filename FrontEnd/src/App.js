import { ToastContainer, Zoom, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux'; //mang action đến redux, sử dụng useSeletor để lấy giá trị
import { Routes, Route } from 'react-router-dom';
import './App.css';
import { Public, Ring, Diamond, Customer, Jewelry, Necklace, Earring, Bangles, WholesaleGold, RetailGold, SearchInvoice, Promotion, Return_Ex } from './page/Seller';
import { Cs_Public, Cs_Complete, Cs_Revenue, Cs_OnProcess } from './page/Cashier';
import Login from './page/Home/Login';
import Admin from './page/Admin/Admin';
import Home from './page/Home/Home';
import path from './ultis/path'
import Cs_Order from './page/Cashier/Cs_Order';
import Dashboard from './page/Admin/Dashboard'
import Report from './page/Admin/Report/Report';
import Manage from './page/Admin/Manage/Manage';
import Invoice from '../src/page/Admin/Report/Invoice'
import Employee from '../src/page/Admin/Report/Employee'
import ProductSold from '../src/page/Admin/Report/ProductSold'
import CustomerAdmin from './page/Admin/Manage/CustomerAdmin';
import ProductAdmin from './page/Admin/Manage/ProductAdmin';
import Staff from '../src/page/Admin/Manage/Staff';
import PromotionAdmin from './page/Admin/Promotion/PromotionAdmin';
import PromotionList from './page/Admin/Promotion/PromotionList';
import PromotionRequest from './page/Admin/Promotion/PromotionRequest';
import ReturnPolicy from './page/Admin/ReturnPolicy';
import VoidBill from './page/Admin/VoidBill'
import Point from './page/Admin/Manage/Point';
import Manager from './page/Manager/Manager';

import ProductManager from './page/Manager/Product/ProductManager';


function App() {
  // test redux có hoạt động không
  // const {test,homeData} = useSelector(state => state.app)
  // console.log(test)

  return (
    <>
      <div className=''>
        <Routes>
          {/* home */}
          <Route path={path.HOME} element={<Home />} />
          <Route path={path.LOGIN} element={<Login />} />
          {/* admin */}
          <Route path={path.ADMIN} element={<Admin />} exact >
            <Route path={path.ADMIN} element={<Dashboard />} />
            <Route path={path.DASHBOARD} element={<Dashboard />} />

            <Route path={path.REPORT} element={<Report />} exact>
              <Route path={path.INVOICE} element={<Invoice />} />
              <Route path={path.PRODUCSOLD} element={<ProductSold />} />
              <Route path={path.EMPLOYEE} element={<Employee />} />
            </Route>

            <Route path={path.MANAGE} element={<Manage />} >
              <Route path={path.PRODUCTADMIN} element={<ProductAdmin />} />
              <Route path={path.CUSTOMERADMIN} element={<CustomerAdmin />} />
              <Route path={path.STAFF} element={<Staff />} />
              <Route path={path.POINT} element={<Point />} />
            </Route>

            <Route path={path.PROMOTIONADMIN} element={<PromotionAdmin />} >
              <Route path={path.PROMOTIONLIST} element={<PromotionList />} />
              <Route path={path.PROMOTIONREQUEST} element={<PromotionRequest />} />
            </Route>

            <Route path={path.RETURNPOLICY} element={<ReturnPolicy />} />

            <Route path={path.VOIDBILL} element={<VoidBill />} />

          </Route >
          {/* manager */}
          <Route path={path.MANAGER} element={<Manager />}>
            <Route path={path.MANAGER} element={<ProductManager />} />
            <Route path={path.PRODUCTMANAGER} element={<ProductManager />} >
              <Route path={path.DIAMOND} element={<Diamond />} />
              <Route path={path.RETAILGOLD} element={<RetailGold />} />
              <Route path={path.WHOLESALEGOLD} element={<WholesaleGold />} />
            </Route>

          </Route>
          {/* Seller */}
          <Route path={path.PUBLIC} element={<Public />}>
            <Route path={path.DIAMOND} element={<Diamond />} />
            <Route path={path.CUSTOMER} element={<Customer />} />
            <Route path={path.JEWELRY} element={<Jewelry />}>
              <Route path={path.RING} element={<Ring />} />
              <Route path={path.NECKLACE} element={<Necklace />} />
              <Route path={path.EARRING} element={<Earring />} />
              <Route path={path.BANGLES} element={<Bangles />} />
            </Route>
            <Route path={path.WHOLESALEGOLD} element={<WholesaleGold />} />
            <Route path={path.RETAILGOLD} element={<RetailGold />} />
            <Route path={path.SEARCHINVOICE} element={<SearchInvoice />} />
            <Route path={path.PROMOTION} element={<Promotion />} />
            <Route path={path.RETURN_EX} element={<Return_Ex />} />
          </Route>
          {/* Cashier */}
          <Route path={path.CS_PUBLIC} element={<Cs_Public />}>
            <Route path={path.CS_ORDER} element={<Cs_Order />}>
              <Route path={path.CS_ONPROCESS} element={<Cs_OnProcess />} />
              <Route path={path.CS_COMPLETE} element={<Cs_Complete />} />
            </Route>
            <Route path={path.CS_REVENUE} element={<Cs_Revenue />} />
          </Route>

        </Routes>



      </div>

      <ToastContainer

      />
    </>
  );
}

export default App;
