import React from 'react'
import { useState } from 'react'
import clsx from 'clsx'

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faRotateLeft, faEyeSlash, faEye } from '@fortawesome/free-solid-svg-icons'
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

import { useNavigate } from 'react-router-dom';
import style from './Login.module.css'
import { loginApi } from '../../apis/jewelryService';
import { toast } from 'react-toastify'

export default function LoginToStore() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isShowPassword, setIsShowPassword] = useState(false);
  const [loadingApi, setLoadingApi] = useState(false);

  const navigate = useNavigate();
  const handleBackClick = () => {
    navigate('/');
    // toast.success('Edit succeed');
  };
  const handleLogin = async () => {
    if (!email || !password) {
      toast.error('Email/Password is required!!!');
      return;
    }
    setLoadingApi(true);
    try {
      const res = await loginApi(email, password);
      if (res && res.token) {
        localStorage.setItem('token', res.token);
        // Xử lý thành công, ví dụ: chuyển hướng tới trang khác
        navigate('/Dashboard');
      } else {
        if (res && res.status === 400) {
          toast.error(res.data.error);
        } else {
          toast.error('Login failed. Please try again.');
        }
      }
    } catch (error) {
      console.error('Login error:', error);
      toast.error('An error occurred during login. Please try again later.');
    }
    setLoadingApi(false);
  }
  const handleAdmin = () => {
    navigate('/admin')
  }
  const handleSeller = () => {
    navigate('/public')
  }
  const handleCashier = () => {
    navigate('/cs_public')
  }
  return (
    <div className={clsx(style.login_container)} >
      <div className='title_login' >Log in</div>
      <div className='text'> Email or username</div>
      <input
        type='text'
        placeholder='Email or username...'
        value={email}
        onChange={(event) => setEmail(event.target.value)}
      />
      <div className={clsx(style.input_2)}>
        <input
          type={isShowPassword === true ? 'text' : 'password'}
          placeholder='Password...'
          value={password}
          onChange={(event) => setPassword(event.target.value)}
        />
        <span> <FontAwesomeIcon icon={isShowPassword === false ? faEyeSlash : faEye} onClick={() => setIsShowPassword(!isShowPassword)} /></span>
      </div>

      <button
        className={email && password ? 'active' : ''}
        disabled={email && password ? false : true}
        onClick={() => handleLogin()}
      >
        {loadingApi &&
          <FontAwesomeIcon
            icon={faSpinner}
            className="fa-spin-pulse fa-spin-reverse fa-1.5x me-2"
          // style={{ fontSize: '1.5rem' }}
          />}Login
      </button>
      {/* phân luồng tạm */}
      <button
        onClick={() => handleAdmin()}
      >
        Admin
      </button>
      <button onClick={() => handleSeller()} >
        seller
      </button>
      <button onClick={() => handleCashier()} >
       Cashier
      </button>


      <div className={clsx(style.back)} onClick={handleBackClick}>
        <span> <FontAwesomeIcon icon={faRotateLeft} />     Back</span>
      </div>

    </div>
  )
}
