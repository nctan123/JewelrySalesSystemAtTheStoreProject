import { ToastContainer, Zoom, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux'; //mang action đến redux, sử dụng useSeletor để lấy giá trị
import { Routes, Route } from 'react-router-dom';
import './App.css';
import { Public, Ring, Diamond, Customer, Jewelry, Necklace } from './page/public';
import Home from './page/Home/Home';
import path from './ultis/path'
import Login from './page/Home/Login'
import Admin from './page/Admin/Admin';
import Dashboard from './page/Admin/Dashboard';
import Report from './page/Admin/Report/Report';
import Manage from './page/Admin/Manage/Manage';
import Invoice from '../src/page/Admin/Report/Invoice'
import Employee from '../src/page/Admin/Report/Employee'
import ProductSold from '../src/page/Admin/Report/ProductSold'
import CustomerAdmin from './page/Admin/Manage/CustomerAdmin';
import ProductAdmin from './page/Admin/Manage/ProductAdmin';
import Staff from '../src/page/Admin/Manage/Staff';
import Promotion from './page/Admin/Promotion/Promotion';
import PromotionList from './page/Admin/Promotion/PromotionList';
import PromotionRequest from './page/Admin/Promotion/PromotionRequest';
import ReturnPolicy from './page/Admin/ReturnPolicy';
import VoidBill from './page/Admin/VoidBill'
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
              <Route path={path.CUSTOMERADMIN} element={<CustomerAdmin />} />
              <Route path={path.PRODUCTADMIN} element={<ProductAdmin />} />
              <Route path={path.STAFF} element={<Staff />} />
            </Route>

            <Route path={path.PROMOTION} element={<Promotion />} >
              <Route path={path.PROMOTIONLIST} element={<PromotionList />} />
              <Route path={path.PROMOTIONREQUEST} element={<PromotionRequest />} />
            </Route>

            <Route path={path.RETURNPOLICY} element={<ReturnPolicy />} />

            <Route path={path.VOIDBILL} element={<VoidBill />} />
          </Route >



          {/* seller */}
          <Route path={path.PUBLIC} element={<Public />}>

            <Route path={path.DIAMOND} element={<Diamond />} />
            <Route path={path.CUSTOMER} element={<Customer />} />
            <Route path={path.JEWELRY} element={<Jewelry />}>
              <Route path={path.RING} element={<Ring />} />
              <Route path={path.NECKLACE} element={<Necklace />} />
            </Route>

          </Route>



        </Routes>
      </div>
      <ToastContainer
      />
    </>
  );
}

export default App;
