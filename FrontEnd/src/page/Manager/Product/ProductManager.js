import React from 'react'
import { Outlet, useOutlet } from 'react-router-dom';
import { Diamond } from '../../Seller';
export default function ProductManager() {
    const outlet = useOutlet();
    return (
        <div>
            {outlet ? outlet : <Diamond />}
        </div>
    )
}
