// import React, { useEffect } from 'react';
// import SildebarManager from '../../components/Manager/SildebarLeftManager';
// import { RiAccountCircleLine } from "react-icons/ri";
// import { Outlet, useNavigate, useOutlet } from 'react-router-dom';
// import DiamondManager from './Product/DiamondManager';
// export default function Manager() {
//     const navigate = useNavigate();

//     useEffect(() => {
//         const Authorization = localStorage.getItem('token');
//         const role = localStorage.getItem('role');
//         if (!Authorization || role !== 'manager') {
//             navigate('/login');
//         }
//     }, [navigate]);

//     return (
//         <>
//             <div className='w-full flex h-[100vh] bg-gray-100'>
//                 <div className='w-[240px] h-[100vh] flex-none bg-white overflow-y-auto z-20 border border-gray-300'>
//                     <SildebarManager />
//                 </div>
//                 <div className='flex-auto border overflow-y-auto '>
//                     <div className=''>
//                         <div className='fixed top-0 left-0 w-full flex justify-end space-x-2 px-[30px] py-[5px] bg-white border border-gray-300 z-10'>
//                             <RiAccountCircleLine className='text-2xl text-blue-800' />
//                             <span className='text-blue-800 text-lg font-medium'>{localStorage.getItem('name')} (Manager)</span>
//                         </div>
//                         <div className='pt-[60px]'>
//                             <Outlet />
//                         </div>
//                     </div>
//                 </div>
//             </div>
//         </>
//     );
// }
import React, { useEffect } from 'react';
import SildebarManager from '../../components/Manager/SildebarLeftManager';
import { RiAccountCircleLine } from "react-icons/ri";
import { Outlet, useNavigate, useOutlet } from 'react-router-dom';
import DiamondManager from './Product/DiamondManager';
import axios from 'axios';
import { toast } from 'react-toastify';
export default function Manager() {
    const navigate = useNavigate();

    useEffect(() => {
        const Authorization = localStorage.getItem('token');
        const role = localStorage.getItem('role');
        if (!Authorization || role !== 'manager') {
            navigate('/login');
        }
    }, [navigate]);
    // useEffect(() => {
    //     const fetchOptions = async () => {
    //         try {
    //             const token = localStorage.getItem('token');
    //             if (!token) {
    //                 throw new Error('No token found');
    //             }
    //             const headers = { Authorization: `Bearer ${token}` };
    //             const categoryResponse = await axios.get(
    //                 'https://jssatsproject.azurewebsites.net/api/productcategory/getall',
    //                 { headers }
    //             );
    //         } catch (error) {
    //             // console.error('Error fetching options:', error);
    //             navigate('/login');
    //             toast.error('Login session expired')
    //         }
    //     };

    //     // Gọi fetchOptions ngay khi component được mount
    //     fetchOptions();

    //     // Thiết lập interval để gọi fetchOptions mỗi 8 giây
    //     const intervalId = setInterval(fetchOptions, 10000);

    //     // Cleanup interval khi component unmount
    //     return () => clearInterval(intervalId);
    // }, []);

    return (
        <>
            <div className='w-full flex h-[100vh] bg-gray-100'>
                <div className='w-[240px] h-[100vh] flex-none bg-white overflow-y-auto z-20 border border-gray-300'>
                    <SildebarManager />
                </div>
                <div className='flex-auto border overflow-y-auto '>
                    <div className=''>
                        <div className='fixed top-0 left-0 w-full flex justify-end space-x-2 px-[30px] py-[5px] bg-white border border-gray-300 z-10'>
                            <RiAccountCircleLine className='text-2xl text-blue-800' />
                            <span className='text-blue-800 text-lg font-medium'>{localStorage.getItem('name')} (Manager)</span>
                        </div>
                        <div className='pt-[60px]'>
                            <Outlet />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
