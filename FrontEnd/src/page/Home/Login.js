import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRotateLeft, faEyeSlash, faEye } from '@fortawesome/free-solid-svg-icons';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import clsx from 'clsx';
import style from './Login.module.css';
import axios from 'axios';

export default function LoginToStore() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isShowPassword, setIsShowPassword] = useState(false);
  const [loadingApi, setLoadingApi] = useState(false);

  const navigate = useNavigate();

  const handleLogin = async () => {
    if (!username || !password) {
      toast.error('Username/Password is required!!!');
      return;
    }
    setLoadingApi(true);
    try {
      const data = await axios.post('https://jssatsproject.azurewebsites.net/api/login', {
        username: username,
        password: password,

      });
      // console.log('>>> check res dta', data)
      const user = data.data;
      // console.log('>>> check user', user)
      if (user && user.token) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('role', user.role);
        localStorage.setItem('staffId', user.staffId);
        localStorage.setItem('name', user.name);
        // console.log('>>> check local', localStorage.getItem('token'))
        // localStorage.setItem('Role', user.role);
        // Determine user role and redirect or show appropriate UI
        switch (user.role) {
          case 'admin':
            navigate('/admin/Dashboard');
            break;
          case 'seller':
            navigate('/public');
            break;
          case 'manager':
            navigate('/manager/Dashboard');
            break;
          case 'cashier':
            navigate('/cs_public/cs_order/cs_waitingPayment');
            break;
          // Add more cases for other roles if needed
          default:
            toast.error('Unknown user role');
            break;
        }
      }

    } catch (error) {
      toast.error('Invalid username or password');
    }
    setLoadingApi(false);
  };

  return (

    <div className={clsx(style.login_container)}>
      {localStorage.clear()}
      <div className={clsx(style.title_login)}>Log in</div>
      <input
        type='text'
        placeholder='Username...'
        value={username}
        onChange={(event) => setUsername(event.target.value)}
      />
      <div className={clsx(style.input_2)}>
        <input
          type={isShowPassword ? 'text' : 'password'}
          placeholder='Password...'
          value={password}
          onChange={(event) => setPassword(event.target.value)}
        />
        <span>
          <FontAwesomeIcon
            icon={isShowPassword ? faEye : faEyeSlash}
            onClick={() => setIsShowPassword(!isShowPassword)}
          />
        </span>
      </div>

      <button
        className={username && password ? clsx(style.active) : ''}
        disabled={!username || !password}
        onClick={() => handleLogin()}
      >
        {loadingApi && (
          <FontAwesomeIcon
            icon={faSpinner}
            className='fa-spin-pulse fa-spin-reverse fa-1.5x me-2'
          />
        )}
        Login
      </button>


    </div>
  );
}
// import React, { useState, useEffect } from 'react';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import { faRotateLeft, faEyeSlash, faEye } from '@fortawesome/free-solid-svg-icons';
// import { faSpinner } from '@fortawesome/free-solid-svg-icons';
// import { useNavigate } from 'react-router-dom';
// import { toast } from 'react-toastify';
// import clsx from 'clsx';
// import style from './Login.module.css';
// import axios from 'axios';

// export default function LoginToStore() {
//   const [username, setUsername] = useState('');
//   const [password, setPassword] = useState('');
//   const [username1, setUsername1] = useState('');
//   const [password1, setPassword1] = useState('');
//   const [isShowPassword, setIsShowPassword] = useState(false);
//   const [loadingApi, setLoadingApi] = useState(false);

//   const navigate = useNavigate();
//   useEffect(() => {
//     localStorage.clear();
//   }, []);

//   const handleLogin = async () => {
//     if (!username || !password) {
//       // toast.error('Username/Password is required!!!');
//       return;
//     }
//     setLoadingApi(true);
//     try {
//       const data = await axios.post('https://jssatsproject.azurewebsites.net/api/login', {
//         username: username,
//         password: password,

//       });
//       // console.log('>>> check res dta', data)
//       const user = data.data;
//       // console.log('>>> check user', user)
//       if (user && user.token) {
//         localStorage.setItem('token', user.token);
//         localStorage.setItem('role', user.role);
//         localStorage.setItem('staffId', user.staffId);
//         localStorage.setItem('name', user.name);
//         switch (user.role) {
//           case 'admin':
//             navigate('/admin/Dashboard');
//             break;
//           case 'seller':
//             navigate('/public');
//             break;
//           case 'manager':
//             navigate('/manager/Dashboard');
//             break;
//           case 'cashier':
//             navigate('/cs_public/cs_order/cs_waitingPayment');
//             break;
//           // Add more cases for other roles if needed
//           default:
//             toast.error('Unknown user role');
//             break;
//         }
//       }

//     } catch (error) {
//       toast.error('Invalid username or password');
//     }
//     setLoadingApi(false);
//   };
//   useEffect(() => {
//     handleLogin();
//   }, [username, password]);
//   const handleAdminLogin = () => {
//     setUsername('admin_user');
//     setPassword('admin_password');
//     // handleLogin();
//   };
//   const handleManageLogin = () => {
//     setUsername('manager_user1');
//     setPassword('manager_password1');
//     // handleLogin();
//   };
//   const handleSellerLogin = () => {
//     setUsername('seller_user1');
//     setPassword('seller_password1');
//     // handleLogin();
//   };

//   const handleCashierLogin = () => {
//     setUsername('cashier_user1');
//     setPassword('cashier_password1');
//     // handleLogin();
//   };
//   const ChangeUserPassword = () => {
//     setUsername(username1);
//     setPassword(password1);
//     // handleLogin();
//   };

//   return (

//     <div className='flex justify-center m-10 space-x-4 mt-96'>
//       <span
//         onClick={handleAdminLogin}
//         className="bg-blue-500 text-white font-bold py-4 px-8 rounded cursor-pointer text-4xl hover:bg-blue-600 transition duration-300 ease-in-out"
//       >
//         Admin
//       </span>
//       <span
//         onClick={handleManageLogin}
//         className="bg-blue-500 text-white font-bold py-4 px-8 rounded cursor-pointer text-4xl hover:bg-blue-600 transition duration-300 ease-in-out"
//       >
//         Manager
//       </span>
//       <span
//         onClick={handleSellerLogin}
//         className="bg-blue-500 text-white font-bold py-4 px-8 rounded cursor-pointer text-4xl hover:bg-blue-600 transition duration-300 ease-in-out"
//       >
//         Seller
//       </span>
//       <span
//         onClick={handleCashierLogin}
//         className="bg-blue-500 text-white font-bold py-4 px-8 rounded cursor-pointer text-4xl hover:bg-blue-600 transition duration-300 ease-in-out"
//       >
//         Cashier
//       </span>
//     </div>


//   );
// }