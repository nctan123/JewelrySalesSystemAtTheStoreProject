import iconAdmin from "./IconAdmin"

const { FaChartSimple,
    TbReportSearch,
    MdManageAccounts,
    CiGift,
    MdOutlineAssignmentReturned,
    TbDeviceIpadCancel,
    RiAccountPinCircleFill,
    BiLogOut, FaChevronDown, IoMdArrowDropdown, IoMdArrowDropup, TbFileInvoice, CgUserList, LiaFileInvoiceDollarSolid, GrUserManager,
<<<<<<< HEAD
    MdOutlineManageAccounts, GiStonePile, LiaGiftsSolid, VscGitPullRequestGoToChanges, GiGoldNuggets } = iconAdmin
=======
    MdOutlineManageAccounts, GiStonePile, LiaGiftsSolid, VscGitPullRequestGoToChanges } = iconAdmin
>>>>>>> develop

export const sidebarMenuAdmin = [
    // {
    //     path: 'revenue',
    //     text:'Revenue',
    //     iconAdmin: <LiaMoneyBillWaveSolid size={24} color="white"/>
    // },
    {
        path: 'Dashboard',
        text: 'Dashboard',
        iconAdmin: <FaChartSimple size={24} color="white" />
    },
    {
        path: 'Report',
        text: 'Report',
        iconAdmin: <TbReportSearch size={24} color="white" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="white" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="white" />,
        subMenu: [
            {
                path: 'Report/Invoice',
                text: 'Invoice',
                iconAdmin: <TbFileInvoice size={24} color="white" />
            },
            {
                path: 'Report/ProductSold',
                text: 'Product sold',
                iconAdmin: <LiaFileInvoiceDollarSolid size={24} color="white" />
            },
            {
                path: 'Report/Employee',
                text: 'Employee',
                iconAdmin: <CgUserList size={24} color="white" />
            }

        ],

    },
    {
        path: 'Manage',
        text: 'Manage',
        iconAdmin: <MdManageAccounts size={24} color="white" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="white" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="white" />,
        subMenu: [
            {
<<<<<<< HEAD
                path: 'Manage/productadmin',
                text: 'Product',
                iconAdmin: <GiStonePile size={24} color="white" />
            },
            {
=======
>>>>>>> develop
                path: 'Manage/customeradmin',
                text: 'Customer',
                iconAdmin: <GrUserManager size={24} color="white" />
            },
            {
<<<<<<< HEAD
                path: 'Manage/point',
                text: 'Point of customer',
                iconAdmin: <GiGoldNuggets size={24} color="white" />
=======
                path: 'Manage/productadmin',
                text: 'Product',
                iconAdmin: <GiStonePile size={24} color="white" />
>>>>>>> develop
            },
            {
                path: 'Manage/staff',
                text: 'Staff',
                iconAdmin: <MdOutlineManageAccounts size={24} color="white" />
            }

        ],
    },
    {
<<<<<<< HEAD
        path: 'Promotionadmin',
=======
        path: 'Promotion',
>>>>>>> develop
        text: 'Promotion',
        iconAdmin: <CiGift size={24} color="white" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="white" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="white" />,
        subMenu: [
            {
<<<<<<< HEAD
                path: 'Promotionadmin/promotionlist',
=======
                path: 'Promotion/promotionlist',
>>>>>>> develop
                text: 'Promotion list',
                iconAdmin: <LiaGiftsSolid size={24} color="white" />
            },
            {
<<<<<<< HEAD
                path: 'Promotionadmin/promotionrequest',
=======
                path: 'Promotion/promotionrequest',
>>>>>>> develop
                text: 'Request',
                iconAdmin: <VscGitPullRequestGoToChanges size={24} color="white" />
            }
        ],
    },
    {
        path: 'ReturnPolicy',
        text: 'Return policy',
        iconAdmin: <MdOutlineAssignmentReturned size={24} color="white" />
    },
    {
        path: 'VoidBill',
        text: 'Void bill',
        iconAdmin: <TbDeviceIpadCancel size={24} color="white" />
    },
    {
        path: '/login',
        text: 'Log out',
        iconAdmin: <BiLogOut size={24} color="white" />
    },
]