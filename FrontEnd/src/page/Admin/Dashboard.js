import React from 'react'
import Revenue from './Dashboard/Revenue'
import TestChart from './Dashboard/CountOrder'
import PieChartCategory from './Dashboard/ChartCategory'
import TooltipChart from './Dashboard/TooltipChart'
export default function Dashboard() {
    return (

        <>
            <div className=" bg-white"><Revenue /></div>

            <div className=" bg-white">
                <TestChart />
            </div>

            <div className=" bg-white"><TooltipChart /></div>
        </>
    )
}
