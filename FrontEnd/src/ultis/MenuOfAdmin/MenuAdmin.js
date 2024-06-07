import iconAdmin from "./IconAdmin"

const { FaChartSimple,
    TbReportSearch,
    MdManageAccounts,
    CiGift,
    MdOutlineAssignmentReturned,
    TbDeviceIpadCancel,
    RiAccountPinCircleFill,
    BiLogOut, FaChevronDown, IoMdArrowDropdown, IoMdArrowDropup } = iconAdmin

export const sidebarMenuAdmin = [
    // {
    //     path: 'revenue',
    //     text:'Revenue',
    //     iconAdmin: <LiaMoneyBillWaveSolid size={24} color="white"/>
    // },
    {
        path: 'Dashboard',
        text: 'dashboard',
        iconAdmin: <FaChartSimple size={24} color="white" />
    },
    {
        path: 'Report',
        text: 'report',
        iconAdmin: <TbReportSearch size={24} color="white" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="white" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="white" />,
        subMenu: [
            {
                path: 'Report/Invoice',
                text: 'invoice',
                iconAdmin: <TbReportSearch size={24} color="white" />
            },
            {
                path: 'Report/ProductSold',
                text: 'product sold',
                iconAdmin: <TbReportSearch size={24} color="white" />
            },
            {
                path: 'Report/Employee',
                text: 'employee',
                iconAdmin: <TbReportSearch size={24} color="white" />
            }

        ],

    },
    {
        path: 'Manage',
        text: 'manage',
        iconAdmin: <MdManageAccounts size={24} color="white" />
    },
    {
        path: 'Promotion',
        text: 'promotion',
        iconAdmin: <CiGift size={24} color="white" />
    },
    {
        path: 'ReturnPolicy',
        text: 'return policy',
        iconAdmin: <MdOutlineAssignmentReturned size={24} color="white" />
    },
    {
        path: 'VoidBill',
        text: 'void bill',
        iconAdmin: <TbDeviceIpadCancel size={24} color="white" />
    },
    {
        path: 'SellerFuction',
        text: 'seller fuction',
        iconAdmin: <RiAccountPinCircleFill size={24} color="white" />
    },
    {
        path: '/login',
        text: 'Log out',
        iconAdmin: <BiLogOut size={24} color="white" />
    },
]