import React, { useState } from 'react';

export default function Revenue() {
    const [todayRevenue, setTodayRevenue] = useState(null);
    const [yesterdayRevenue, setYesterdayRevenue] = useState(null);
    const [thisWeekRevenue, setThisWeekRevenue] = useState(null);
    const [lastWeekRevenue, setLastWeekRevenue] = useState(null);

    const [todayCustomer, setTodayCustomer] = useState(null);
    const [yesterdayCustomer, setYesterdayCustomer] = useState(null);
    const [thisWeekCustomer, setThisWeekCustomer] = useState(null);
    const [lastWeekCustomer, setLastWeekCustomer] = useState(null);

    const [todayOrder, setTodayOrder] = useState(null);
    const [yesterdayOrder, setYesterdayOrder] = useState(null);
    const [thisWeekOrder, setThisWeekOrder] = useState(null);
    const [lastWeekOrder, setLastWeekOrder] = useState(null);

    const [todayProduct, setTodayProduct] = useState(null);
    const [yesterdayProduct, setYesterdayProduct] = useState(null);
    const [thisWeekProduct, setThisWeekProduct] = useState(null);
    const [lastWeekProduct, setLastWeekProduct] = useState(null);

    const [view, setView] = useState(''); // state to track which button was clicked
    const [error, setError] = useState(null);

    const getRevenue = async (start, end, setData) => {
        try {
            const response = await fetch(`https://jssatsproject.azurewebsites.net/api/sellorder/SumTotalAmountSellOrderByDateTime?startDate=${start}&endDate=${end}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            setData(result);
        } catch (error) {
            setError(error.message);
        }
    };
    const getNewCustomer = async (start, end, setData) => {
        try {
            const response = await fetch(`https://jssatsproject.azurewebsites.net/api/Customer/CountNewCustomer?startDate=${start}&endDate=${end}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            setData(result);
        } catch (error) {
            setError(error.message);
        }
    };
    const getQuantityOrder = async (start, end, setData) => {
        try {
            const response = await fetch(`https://jssatsproject.azurewebsites.net/api/sellorder/CountSellOrderByDateTime?startDate=${start}&endDate=${end}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            setData(result);
        } catch (error) {
            setError(error.message);
        }
    };
    const getQuantityProduct = async (start, end, setData) => {
        try {
            const response = await fetch(`https://jssatsproject.azurewebsites.net/api/sellorder/CountProductSoldBycategory?startDate=${start}&endDate=${end}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const result = await response.json();
            setData(result);
        } catch (error) {
            setError(error.message);
        }
    };


    const handleDayClick = () => {
        const today = new Date();
        const yesterday = new Date();
        const dayBeforeYesterday = new Date();

        yesterday.setDate(today.getDate() - 1);
        dayBeforeYesterday.setDate(today.getDate() - 2);

        const todayStr = today.toISOString().split('T')[0];
        const yesterdayStr = yesterday.toISOString().split('T')[0];
        const dayBeforeYesterdayStr = dayBeforeYesterday.toISOString().split('T')[0];

        getRevenue(yesterdayStr, todayStr, setTodayRevenue);
        getRevenue(dayBeforeYesterdayStr, yesterdayStr, setYesterdayRevenue);
        getNewCustomer(yesterdayStr, todayStr, setTodayCustomer);
        getNewCustomer(dayBeforeYesterdayStr, yesterdayStr, setYesterdayCustomer);
        getQuantityOrder(yesterdayStr, todayStr, setTodayOrder);
        getQuantityOrder(dayBeforeYesterdayStr, yesterdayStr, setYesterdayOrder);
        getQuantityProduct(yesterdayStr, todayStr, setTodayProduct);
        getQuantityProduct(dayBeforeYesterdayStr, yesterdayStr, setYesterdayProduct);
        setView('day');
    };

    const handleWeekClick = () => {
        const today = new Date();
        const thisMonday = new Date(today.setDate(today.getDate() - today.getDay() + 1)); // Monday this week
        const lastSunday = new Date(today.setDate(thisMonday.getDate() - 1)); // Sunday last week
        const lastMonday = new Date(today.setDate(thisMonday.getDate() - 7)); // Monday last week
        const lastLastSunday = new Date(today.setDate(lastMonday.getDate() - 1)); // Sunday the week before last
        const lastLastMonday = new Date(today.setDate(lastMonday.getDate() - 7)); // Monday the week before last

        const thisMondayStr = thisMonday.toISOString().split('T')[0];
        const lastSundayStr = lastSunday.toISOString().split('T')[0];
        const lastMondayStr = lastMonday.toISOString().split('T')[0];
        const lastLastSundayStr = lastLastSunday.toISOString().split('T')[0];
        const lastLastMondayStr = lastLastMonday.toISOString().split('T')[0];

        getRevenue(lastMondayStr, thisMondayStr, setThisWeekRevenue);
        getRevenue(lastLastMondayStr, lastMondayStr, setLastWeekRevenue);
        getNewCustomer(lastMondayStr, thisMondayStr, setThisWeekCustomer);
        getNewCustomer(lastLastMondayStr, lastMondayStr, setLastWeekCustomer);
        getQuantityOrder(lastMondayStr, thisMondayStr, setThisWeekOrder);
        getQuantityOrder(lastLastMondayStr, lastMondayStr, setLastWeekOrder);
        getQuantityProduct(lastMondayStr, thisMondayStr, setThisWeekProduct);
        getQuantityProduct(lastLastMondayStr, lastMondayStr, setLastWeekProduct);
        setView('week');
    };

    return (
        <div className="container mx-auto p-4">
            <div className="flex justify-end mb-4">
                <button
                    type="button"
                    onClick={handleDayClick}
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mr-2 mx-0"
                    style={{ width: '5rem' }} // Đặt chiều rộng cố định cho nút "Day"
                >
                    Day
                </button>
                <button
                    type="button"
                    onClick={handleWeekClick}
                    className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded mx-0"
                    style={{ width: '5rem' }} // Đặt chiều rộng cố định cho nút "Week"
                >
                    Week
                </button>
            </div>

            {error && <p className="text-red-500">{error}</p>}
            {view === 'day' && (
                <div>
                    <div className="w-1/2 p-4">
                        <h2 className="text-2xl font-semibold mb-2">Today</h2>
                        {todayRevenue ? (
                            <div className="flex">
                                <pre className="bg-gray-100 p-4 rounded"><p>Revenue</p>{JSON.stringify(todayRevenue, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>New Customer</p>{JSON.stringify(todayCustomer, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Order</p>{JSON.stringify(todayOrder, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Number of products sold</p>{JSON.stringify(todayProduct, null, 2)}</pre>
                            </div>
                        ) : (
                            <p>Loading...</p>
                        )}
                    </div>
                    <div className="w-1/2 p-4">
                        <h2 className="text-2xl font-semibold mb-2">Yesterday</h2>
                        {yesterdayRevenue ? (
                            <div className="flex">
                                <pre className="bg-gray-100 p-4 rounded"><p>Revenue</p>{JSON.stringify(yesterdayRevenue, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>New Customer</p>{JSON.stringify(yesterdayCustomer, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Order</p>{JSON.stringify(yesterdayOrder, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Number of products sold</p>{JSON.stringify(yesterdayProduct, null, 2)}</pre>
                            </div>
                        ) : (
                            <p>Loading...</p>
                        )}
                    </div>
                </div>
            )}
            {view === 'week' && (
                <div>
                    <div className="w-1/2 p-4">
                        <h2 className="text-2xl font-semibold mb-2">This Week</h2>
                        {thisWeekRevenue ? (
                            <div className="flex">
                                <pre className="bg-gray-100 p-4 rounded"><p>Revenue</p>{JSON.stringify(thisWeekRevenue, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>New Customer</p>{JSON.stringify(thisWeekCustomer, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Order</p>{JSON.stringify(thisWeekOrder, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Number of products sold</p>{JSON.stringify(thisWeekProduct, null, 2)}</pre>
                            </div>
                        ) : (
                            <p>Loading...</p>
                        )}
                    </div>
                    <div className="w-1/2 p-4">
                        <h2 className="text-2xl font-semibold mb-2">Last Week</h2>
                        {lastWeekRevenue ? (
                            <div className="flex">
                                <pre className="bg-gray-100 p-4 rounded"><p>Revenue</p>{JSON.stringify(lastWeekRevenue, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>New Customer</p>{JSON.stringify(lastWeekCustomer, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Order</p>{JSON.stringify(lastWeekOrder, null, 2)}</pre>
                                <pre className="bg-gray-100 p-4 rounded"><p>Number of products sold</p>{JSON.stringify(lastWeekProduct, null, 2)}</pre>
                            </div>
                        ) : (
                            <p>Loading...</p>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
}

