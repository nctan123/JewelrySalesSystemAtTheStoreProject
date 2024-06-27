
import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRotateLeft, faEyeSlash, faEye } from '@fortawesome/free-solid-svg-icons';

import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';


import { loginApi } from '../../apis/jewelryService';
import { toast } from 'react-toastify';
import clsx from 'clsx';
import style from './Login.module.css';

export default function LoginToStore() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isShowPassword, setIsShowPassword] = useState(false);
  const [loadingApi, setLoadingApi] = useState(false);

  const navigate = useNavigate();

  const handleBackClick = () => {
    navigate('/');
  };

  const handleLogin = async () => {
    if (!username || !password) {
      toast.error('Username/Password is required!!!');
      return;
    }
    setLoadingApi(true);
    try {
      const response = await fetch('https://jssats.azurewebsites.net/api/account/getall');
      const data = await response.json();
      console.log('>>> check res', data.data)
      const user = data.data.find(user => user.username === username && user.password === password);

<<<<<<< HEAD
      // Determine user role and redirect or show appropriate UI
      switch (user.roleId) {
        case 2:
          navigate('/admin');
          break;
        case 1:
          navigate('/public');
          break;
        case 4:
          navigate('/manager');
          break;
        case 3:
          navigate('/cs_public');
          break;
        // Add more cases for other roles if needed
        default:
          toast.error('Unknown user role');
          break;
=======
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
            navigate('/admin');
            break;
          case 'seller':
            navigate('/public');
            break;
          case 'manager':
            navigate('/manager');
            break;
          case 'cashier':
            navigate('/cs_public');
            break;
          // Add more cases for other roles if needed
          default:
            toast.error('Unknown user role');
            break;
        }
>>>>>>> FE_Tai
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
      <div className='input-2'>
        <input
          type={isShowPassword ? 'text' : 'password'}
          placeholder='Password...'
          value={password}
          onChange={(event) => setPassword(event.target.value)}
        />
        <span>
          <FontAwesomeIcon
            icon={isShowPassword ? faEyeSlash : faEye}
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


<<<<<<< HEAD
      <div className={clsx(style.back)} onClick={handleBackClick}>
        <span>
          <FontAwesomeIcon icon={faRotateLeft} /> Back
        </span>

      </div>
    </div>
  );
}
=======
    </div>
  );
}
>>>>>>> FE_Tai
