import icons from "../icon"

const {GiReceiveMoney,BsCartPlus,BsCartCheck,BsCart3} = icons

export const sidebarMenuCashier = [
    {
        path: 'cs_order/cs_onprocess',
        text: 'Order',
        icons: <BsCart3  size={24} color="white" />,
    },
    {
        path: 'cs_revenue',
        text: 'Revenue',
        icons: <GiReceiveMoney size={24} color="white" />
    },
]