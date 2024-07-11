import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';

const VNPayCallback = () => {
  const history = useHistory();

  useEffect(() => {
    const handleVNPayCallback = async () => {
      try {
        // Assume the callback URL contains the necessary parameters to identify the transaction
        const params = new URLSearchParams(window.location.search);
        const res = await axios.get('https://jssatsproject.azurewebsites.net/api/VnPay/callback', { params });
        
        if (res.data.success === false) {
          toast.error('Payment failed. Redirecting back to payment page.');
          history.push('/payment-page');
        } else {
          toast.success('Payment successful.');
          // Redirect to success page or order confirmation page
          history.push('/order-confirmation');
        }
      } catch (error) {
        toast.error('Error processing payment. Please try again.');
        console.error('Error handling VNPay callback:', error);
        history.push('/payment-page');
      }
    };

    handleVNPayCallback();
  }, [history]);

  return (
    <div>
      <h2>Processing payment...</h2>
    </div>
  );
};

export default VNPayCallback;
