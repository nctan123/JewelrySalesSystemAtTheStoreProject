import { ToastContainer, Zoom, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useState } from 'react';
import { useSelector,useDispatch } from 'react-redux'; //mang action đến redux, sử dụng useSeletor để lấy giá trị
import { Routes,Route } from 'react-router-dom';
import './App.css';
import { Public,Ring,Diamond,Customer,Jewelry,Necklace,Earring,Bangles, WholesaleGold, RetailGold, SearchInvoice, Promotion, Return_Ex} from './page/Seller';
import Login from './page/Home/Login'
import Admin from './page/Admin/Admin';
import Home from './page/Home/Home';
import path from './ultis/path'


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
          <Route path={path.PUBLIC} element={<Public/>}> 

               <Route path={path.DIAMOND} element={<Diamond/>}/>
               <Route path={path.CUSTOMER} element={<Customer/>}/>
               <Route path={path.JEWELRY} element={<Jewelry/>}>
                   <Route path={path.RING} element={<Ring/>}/>
                   <Route path={path.NECKLACE} element={<Necklace/>}/>
                   <Route path={path.EARRING} element={<Earring/>}/>
                   <Route path={path.BANGLES} element={<Bangles/>}/>
               </Route>
               <Route path={path.WHOLESALEGOLD} element={<WholesaleGold/>}/>
               <Route path={path.RETAILGOLD} element={<RetailGold/>}/>
               <Route path={path.SEARCHINVOICE} element={<SearchInvoice/>}/>
               <Route path={path.PROMOTION} element={<Promotion/>}/>
               <Route path={path.RETURN_EX} element={<Return_Ex/>}/>                             
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
