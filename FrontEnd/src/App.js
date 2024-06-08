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
          <Route path={path.ADMIN} element={<Admin />} />
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
        position="top-right"
        autoClose={1999}
        hideProgressBar
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
        transition="Zoom"
      />
    </>
  );
}

export default App;
