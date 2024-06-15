import React from 'react'
import logo from '../../assets/img/home.jpg'
import { useNavigate } from 'react-router-dom';
import './Home.css'

export default function Home() {
    const navigate = useNavigate();

    const handleLoginClick = () => {
        navigate('/login');
    };
    return (
        <>
            <div className="title-orchid">Home</div>
            <img style={{ width: '30%', height: 'auto', display: 'flex', justifyContent: 'center', alignItems: 'center', marginLeft: '536px' }} src={logo} />
            <button onClick={handleLoginClick}>Log in</button>
        </>
    )
}