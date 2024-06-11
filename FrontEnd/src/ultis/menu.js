import icons from "./icon"

const { LiaMoneyBillWaveSolid, SlPeople, GiHeartNecklace, GiStonePile, AiOutlineGold, SlDiamond, LiaFileInvoiceDollarSolid, CiGift, TbArrowsExchange2, TbLogout2, MdDiamond } = icons

export const sidebarMenu = [
    // {
    //     path: 'revenue',
    //     text:'Revenue',
    //     icons: <LiaMoneyBillWaveSolid size={24} color="white"/>
    // },
    {
        path: 'customer',
        text: 'Customer',
        icons: <SlPeople size={24} color="white" />
    },
    {
        path: 'jewelry',
        text: 'Jewelry',
        icons: <GiHeartNecklace size={24} color="white" />,
        subMenu: [
            {
                path: 'jewelry/ring',
                text: 'Ring',
                icons: <GiHeartNecklace size={24} color="white" />,
            },
            {
                path: 'jewelry/necklace',
                text: 'Necklace',
                icons: <GiHeartNecklace size={24} color="white" />,
            }
        ]
    },
    {
        path: 'wholesaleGold',
        text: 'Wholesale Gold',
        icons: <GiStonePile size={24} color="white" />
    },
    {
        path: 'retailGold',
        text: 'Retail Gold',
        icons: <AiOutlineGold size={24} color="white" />
    },
    {
        path: '',
        text: 'Diamond',
        end: true,
        icons: <MdDiamond size={24} color="white" />
    },
    {
        path: 'searchInvoice',
        text: 'Search Invoice',
        icons: <LiaFileInvoiceDollarSolid size={24} color="white" />
    },
    {
        path: 'promotion',
        text: 'Promotion',
        icons: <CiGift size={24} color="white" />
    },
    {
        path: 'returnExchange',
        text: 'Return/Exchange',
        icons: <TbArrowsExchange2 size={24} color="white" />
    },
    {
        path: '/login',
<<<<<<< HEAD
        text:'Log out',
        icons: <TbLogout2 size={24} color="white"/>
=======
        text: 'Log out',
        icons: <TbLogout2 size={24} color="white" />
>>>>>>> develop
    },

]